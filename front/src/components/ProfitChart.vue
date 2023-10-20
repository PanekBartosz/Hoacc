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
        <dt class="text-base font-normal text-gray-500 dark:text-gray-400 pb-1">
          Income
        </dt>
        <dd
          class="leading-none text-xl font-bold text-green-500 dark:text-green-400"
        >
          $23,635
        </dd>
      </dl>
      <dl>
        <dt class="text-base font-normal text-gray-500 dark:text-gray-400 pb-1">
          Expense
        </dt>
        <dd
          class="leading-none text-xl font-bold text-red-600 dark:text-red-500"
        >
          -$18,230
        </dd>
      </dl>
    </div>

    <div id="bar-chart"></div>

    <div
      class="grid grid-cols-1 items-center border-gray-200 mt-5 border-t dark:border-gray-700"
    >
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
        <!-- Dropdown menu -->
        <div
          ref="dropdownMenu"
          :class="{
            'absolute top-0 -mt-[16rem]': isDropdownOpen,
            hidden: !isDropdownOpen,
          }"
          class="z-10 bg-white divide-y divide-gray-100 rounded-lg shadow w-44 dark:bg-gray-700"
        >
          <ul class="py-2 text-sm text-gray-700 dark:text-gray-200">
            <li v-for="(option, index) in optionsBtn" :key="index">
              <a
                class="block px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white"
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
import { onMounted, ref, onUnmounted } from "vue";

// ApexCharts options and config
const options = {
  series: [
    {
      name: "Income",
      color: "#31C48D",
      data: ["1420", "1620", "1820", "1420", "1650", "2120"],
    },
    {
      name: "Expense",
      data: ["788", "810", "866", "788", "1100", "1200"],
      color: "#F05252",
    },
  ],
  chart: {
    sparkline: {
      enabled: false,
    },
    type: "bar",
    width: "100%",
    height: 400,
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
  tooltip: {
    shared: true,
    intersect: false,
    formatter: function (value) {
      return "$" + value;
    },
  },
  xaxis: {
    labels: {
      show: true,
      style: {
        fontFamily: "Inter, sans-serif",
        cssClass: "text-xs font-normal fill-gray-500",
      },
      formatter: function (value) {
        return "$" + value;
      },
    },
    categories: ["Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
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
const selectedOption = ref(optionsBtn[0]);
const isDropdownOpen = ref(false);

const toggleDropdown = () => {
  isDropdownOpen.value = !isDropdownOpen.value;
  if (isDropdownOpen.value) {
    dropdownMenu.value.style.display = "block";
  } else {
    dropdownMenu.value.style.display = "none";
  }
};

const selectOption = (index) => {
  selectedOption.value = optionsBtn[index];
  isDropdownOpen.value = false;
  dropdownMenu.value.style.display = "none";
};

onUnmounted(() => {
  document.removeEventListener("click", closeDropdownOnClick);
});

const closeDropdownOnClick = (event) => {
  if (
    !dropdownButton.value.contains(event.target) &&
    !dropdownMenu.value.contains(event.target)
  ) {
    isDropdownOpen.value = false;
    dropdownMenu.value.style.display = "none";
  }
};

onMounted(() => {
  document.addEventListener("click", closeDropdownOnClick);
  if (
    document.getElementById("bar-chart") &&
    typeof ApexCharts !== "undefined"
  ) {
    const chart = new ApexCharts(document.getElementById("bar-chart"), options);
    chart.render();
  }
});
</script>
