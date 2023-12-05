<template>
  <div
    class="w-full bg-white border-2 border-slate-50 rounded-lg shadow-lg p-4 md:p-6"
  >
    <div class="flex justify-center border-gray-200 border-b pb-3">
      <dl>
        <dt class="leading-none text-xl font-medium text-gray-700 pb-1">
          Operations over time
        </dt>
      </dl>
    </div>

    <div class="grid grid-cols-2 py-3 text-center">
      <dl>
        <dt class="text-base font-normal text-gray-500">Income</dt>
        <dd class="leading-none text-xl font-bold text-green-500">
          {{ totalIncome }} PLN
        </dd>
      </dl>
      <dl>
        <dt class="text-base font-normal text-gray-500">Expense</dt>
        <dd class="leading-none text-xl font-bold text-red-600">
          {{ totalExpense }} PLN
        </dd>
      </dl>
    </div>

    <div id="bar-chart"></div>
    <p v-if="isDataLacking" class="text-center text-xl mt-10 mb-10">
      Lack of data
    </p>

    <div class="grid grid-cols-1 items-center border-gray-200 mt-5 border-t">
      <div class="flex items-center justify-center pt-5 relative">
        <!-- Button -->
        <button
          ref="dropdownButton"
          @click="toggleDropdown"
          class="text-sm font-medium text-gray-500 hover:text-gray-900 text-center inline-flex items-center"
          type="button"
        >
          {{ selectedOption }}
          <svg
            class="ml-2"
            xmlns="http://www.w3.org/2000/svg"
            height="1em"
            viewBox="0 0 320 512"
            fill="#6b7280"
          >
            <path
              v-if="!isDropdownOpen"
              d="M137.4 374.6c12.5 12.5 32.8 12.5 45.3 0l128-128c9.2-9.2 11.9-22.9 6.9-34.9s-16.6-19.8-29.6-19.8L32 192c-12.9 0-24.6 7.8-29.6 19.8s-2.2 25.7 6.9 34.9l128 128z"
            />
            <path
              v-else="!isDropdownOpen"
              d="M182.6 137.4c-12.5-12.5-32.8-12.5-45.3 0l-128 128c-9.2 9.2-11.9 22.9-6.9 34.9s16.6 19.8 29.6 19.8H288c12.9 0 24.6-7.8 29.6-19.8s2.2-25.7-6.9-34.9l-128-128z"
            />
          </svg>
        </button>
        <div
          ref="dropdownMenu"
          :class="{
            'absolute top-0 -mt-[16rem]': isDropdownOpen,
            hidden: !isDropdownOpen,
          }"
          class="z-10 bg-white divide-y divide-gray-100 rounded-lg shadow w-44"
        >
          <ul class="py-2 text-sm text-gray-700">
            <li v-for="(option, index) in optionsBtn" :key="index">
              <a
                class="block px-4 py-2 hover:bg-gray-100"
                @click="selectOption(index)"
              >
                {{ option }}
              </a>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import ApexCharts from "apexcharts";
import { onMounted, ref, watch } from "vue";
import { getProfitDataByUser } from "../api";
import { useRouter } from "vue-router";
import dayjs from "dayjs";

const router = useRouter();

const totalIncome = ref(0);
const totalExpense = ref(0);
const userId = Number(router.currentRoute.value.params.id);

let chart = null;
const isDataLacking = ref(false);
const chartHeight = 250;

const operations = defineProps({
  operations: {
    type: Array,
    required: true,
  },
});

// Watch for changes in operations and update the chart
watch(operations, () => {
  fetchProfitData(selectedOption.value);
});

const formatDate = (date) => {
  const newDate = dayjs(date).format("YYYY-MM-DD");
  return newDate;
};

const fetchProfitData = async (selected) => {
  try {
    const { startDate, endDate } = calculateStartDate(selected);
    const response = await getProfitDataByUser(userId, startDate, endDate);
    const data = response.data;
    totalIncome.value = data.totalIncome;
    totalExpense.value = data.totalExpense;
    initOrUpdateChart(); // Update the chart after data fetch
  } catch (error) {
    console.error("Error fetching profit data:", error);
  }
};

// Function to initialize or update the chart
const initOrUpdateChart = () => {
  const barChart = document.getElementById("bar-chart");
  if (barChart && typeof ApexCharts !== "undefined") {
    if (isDataLacking.value) {
      if (chart) {
        chart.destroy(); // Destroy the existing chart instance if present
        chart = null; // Reset the chart instance
      }
    } else {
      chart = new ApexCharts(barChart, options);
      chart.render();
      const seriesData = [
        { data: [totalIncome.value] },
        { data: [totalExpense.value] },
      ];

      if (chart) {
        chart.updateSeries(seriesData);
      } else {
        chart = new ApexCharts(barChart, options);
        chart.render();
      }
    }
  }
};

const checkDataAvailability = () => {
  isDataLacking.value = totalIncome.value === 0 && totalExpense.value === 0;
};

watch([totalIncome, totalExpense], () => {
  checkDataAvailability();
  initOrUpdateChart();
});

onMounted(async () => {
  await fetchProfitData(selectedOption.value);
  checkDataAvailability();
  initOrUpdateChart();
});

const calculateStartDate = (selected) => {
  const currentDate = new Date();

  switch (selectedOption.value) {
    case "Yesterday": {
      const yesterday = new Date(currentDate);
      yesterday.setDate(currentDate.getDate() - 1);
      return {
        startDate: formatDate(yesterday),
        endDate: formatDate(yesterday),
      };
    }
    case "Today": {
      return {
        startDate: formatDate(currentDate),
        endDate: formatDate(currentDate),
      };
    }
    case "Last 7 days": {
      const sevenDaysAgo = new Date(currentDate);
      sevenDaysAgo.setDate(currentDate.getDate() - 7);
      return {
        startDate: formatDate(sevenDaysAgo),
        endDate: formatDate(currentDate),
      };
    }
    case "Last 30 days": {
      const thirtyDaysAgo = new Date(currentDate);
      thirtyDaysAgo.setDate(currentDate.getDate() - 30);
      return {
        startDate: formatDate(thirtyDaysAgo),
        endDate: formatDate(currentDate),
      };
    }
    case "Last 90 days": {
      const ninetyDaysAgo = new Date(currentDate);
      ninetyDaysAgo.setDate(currentDate.getDate() - 90);
      return {
        startDate: formatDate(ninetyDaysAgo),
        endDate: formatDate(currentDate),
      };
    }
    case "Last 6 months": {
      const sixMonthsAgo = new Date(currentDate);
      sixMonthsAgo.setMonth(currentDate.getMonth() - 6);
      return {
        startDate: formatDate(sixMonthsAgo),
        endDate: formatDate(currentDate),
      };
    }
    case "Last year": {
      const lastYear = new Date(currentDate);
      lastYear.setFullYear(currentDate.getFullYear() - 1);
      return {
        startDate: formatDate(lastYear),
        endDate: formatDate(currentDate),
      };
    }
    default: {
      return {
        startDate: formatDate(currentDate),
        endDate: formatDate(currentDate),
      };
    }
  }
};

const dropdownButton = ref(null);
const dropdownMenu = ref(null);
const optionsBtn = [
  "Yesterday",
  "Today",
  "Last 7 days",
  "Last 30 days",
  "Last 90 days",
  "Last 6 months",
  "Last year",
];

const isDropdownOpen = ref(false);

const toggleDropdown = () => {
  isDropdownOpen.value = !isDropdownOpen.value;

  if (dropdownMenu.value) {
    dropdownMenu.value.style.display = isDropdownOpen.value ? "block" : "none";
  }
};

const selectOption = (index) => {
  selectedOption.value = optionsBtn[index];
  isDropdownOpen.value = false;
  if (dropdownMenu.value) {
    dropdownMenu.value.style.display = "none";
  }
  fetchProfitData(selectedOption.value);
};

onMounted(() => {
  document.addEventListener("click", closeDropdownOnClick);
});

const closeDropdownOnClick = (event) => {
  const dropdownButtonElem = dropdownButton.value;
  const dropdownMenuElem = dropdownMenu.value;

  if (dropdownButtonElem && dropdownMenuElem) {
    if (
      !dropdownButtonElem.contains(event.target) &&
      !dropdownMenuElem.contains(event.target)
    ) {
      isDropdownOpen.value = false;
      dropdownMenuElem.style.display = "none";
    }
  }
};

const selectedOption = ref(optionsBtn[3]); //[0]
// ApexCharts options and config
const options = {
  series: [
    {
      name: "Income",
      color: "#31C48D",
      data: [totalIncome.value],
    },
    {
      name: "Expense",
      data: [totalExpense.value],
      color: "#F05252",
    },
  ],
  chart: {
    sparkline: {
      enabled: false,
    },
    type: "bar",
    width: "100%",
    height: [chartHeight],
    toolbar: {
      show: false,
    },
  },
  fill: {
    opacity: 1,
  },
  plotOptions: {
    bar: {
      horizontal: true,
      columnWidth: "100%",
      borderRadiusApplication: "end",
      borderRadius: 6,
      dataLabels: {
        position: "top",
      },
    },
  },
  legend: {
    show: true,
    position: "bottom",
  },
  dataLabels: {
    enabled: false,
  },
  xaxis: {
    labels: {
      show: true,
      style: {
        fontFamily: "Inter, sans-serif",
        cssClass: "text-[11px] font-normal fill-gray-500",
      },
      formatter: function (value) {
        return value + " PLN";
      },
    },
    categories: [""],
    axisTicks: {
      show: false,
    },
    axisBorder: {
      show: false,
    },
  },
  yaxis: {
    labels: {
      show: true,
      style: {
        fontFamily: "Inter, sans-serif",
        cssClass: "text-xs font-normal fill-gray-500",
      },
    },
  },
  tooltip: {
    y: {
      formatter: function (value) {
        return value + " PLN"; // Append "PLN" to the tooltip values for Income and Expense
      },
    },
  },
  grid: {
    show: true,
    strokeDashArray: 4,
    padding: {
      left: 2,
      right: 2,
      top: -20,
    },
  },
};
</script>
