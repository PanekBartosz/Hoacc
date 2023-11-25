<template>
  <div class="w-full bg-white border-2 border-slate-50 rounded-lg shadow-lg p-4 md:p-6">
    <div class="flex justify-center border-gray-200 border-b pb-3">
      <dl>
        <dt class="leading-none text-xl font-medium text-gray-700 pb-1">
          Expense by Categories
        </dt>
      </dl>
    </div>

    <div class="mt-8">
      <div class="flex items-center justify-center">
        <canvas ref="pieChart" style="max-width: 320px; max-height: 319px;"></canvas>
      </div>

      <div class="grid grid-cols-1 items-center border-gray-200 mt-5 border-t dark:border-gray-700">
        <div class="flex items-center justify-center pt-5 relative">
          <button
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
            class="z-10 bg-white divide-y divide-gray-100 rounded-lg shadow w-44 dark:bg-gray-700"
          >
            <ul class="py-2 text-sm text-gray-700 dark:text-gray-200">
              <li v-for="(option, index) in options" :key="index">
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
  </div>
</template>

<script setup>
import { onMounted, ref, watch } from "vue";
import Chart from "chart.js/auto";
import { getOperationsFiltered } from "../api";
import { useRouter } from "vue-router";

const router = useRouter();
const userId = Number(router.currentRoute.value.params.id)

const categoryMappings = [
  { value: 0, name: 'Bills', color: 'red' },
  { value: 1, name: 'Food', color: 'green' },
  { value: 2, name: 'Education', color: 'blue' },
  { value: 3, name: 'Entertainment', color: 'purple' },
];

const pieChart = ref(null);
const options = [
  "Yesterday",
  "Today",
  "Last 7 days",
  "Last 30 days",
  "Last 90 days",
  "Last 6 months",
  "Last year",
];
const selectedOption = ref(options[0]);
const isDropdownOpen = ref(false);

onMounted(async () => {
  await fetchData(selectedOption.value);
});


const fetchData = async (selected) => {
  try {
    const { startDate, endDate } = calculateStartDate(selected);
    const response = await getOperationsFiltered(userId, startDate, endDate, 'outcome');
    const { data } = response;

    if (data.length > 0) {
      const { labels, values, colors } = transformDataForChart(data);

      if (pieChart.value.chart) {
        pieChart.value.chart.destroy();
      }

      updateChart(labels, values, colors);
    } else {
      if (pieChart.value.chart) {
        pieChart.value.chart.destroy();
      }
      showNoDataMessage();
    }
  } catch (error) {
    if (pieChart.value.chart) {
        pieChart.value.chart.destroy();
      }
    showNoDataMessage();
  }
};

const showNoDataMessage = () => {
  const ctx = pieChart.value.getContext('2d');
  ctx.clearRect(0, 0, pieChart.value.width, pieChart.value.height);
  ctx.font = '20px Arial';
  ctx.fillStyle = 'black';
  ctx.textAlign = 'center';
  ctx.fillText('Lack of data', pieChart.value.width / 2, pieChart.value.height / 2);
};

const operations = defineProps({
  operations: {
    type: Array,
    required: true,
  },
});

// Watch for changes in operations and update the chart
watch(operations, () => {
  fetchData(selectedOption.value);
});

const formatDate = (date) => {
  console.log(date.toISOString().split('T')[0])
  return date.toISOString().split('T')[0];
};

const calculateStartDate = (selected) => {
  const currentDate = new Date();

  switch (selected) {
    case "Yesterday": {
      const yesterday = new Date(currentDate);
      const yesterdayEnd = new Date(currentDate);
      yesterday.setDate(currentDate.getDate() - 2);
      yesterdayEnd.setDate(currentDate.getDate() - 1);
      return {
        startDate: formatDate(yesterday),
        endDate: formatDate(yesterdayEnd),
      };
    }
    case "Today": {
      const today = new Date(currentDate);
      today.setDate(currentDate.getDate() - 1);
      return {
        startDate: formatDate(today),
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
        endDate: formatDate(currentDate)
      };
    }
  }
};

const transformDataForChart = (data) => {
  const categoryAmounts = {};

  data.forEach((operation) => {
      const category = operation.category;

      if (!categoryAmounts[category]) {
        categoryAmounts[category] = 0;
      }

      categoryAmounts[category] += operation.amount;
  });

  const labels = [];
  const values = [];
  const colors = [];

  Object.entries(categoryAmounts).forEach(([categoryId, totalAmount]) => {
    const categoryMapping = categoryMappings.find((cat) => cat.value === parseInt(categoryId));

    if (categoryMapping) {
      labels.push(categoryMapping.name);
      values.push(totalAmount);
      colors.push(categoryMapping.color);
    }
  });

  return { labels, values, colors };
};

const updateChart = (labels, values, colors) => {
  const ctx = pieChart.value.getContext('2d');
  const data = {
    labels: labels,
    datasets: [
      {
        data: values,
        backgroundColor: colors,
      },
    ],
  };

  if (pieChart.value.chart) {
    pieChart.value.chart.destroy();
  }

  pieChart.value.chart = new Chart(ctx, {
    type: 'pie',
    data: data,
    options: {
      elements: {
        arc: {
          spacing: 0,
          borderRadius: 15,
          offset: 20,
          },
      },
      plugins: {
        tooltip: {
          callbacks: {
            label: (context) => {
              const label = context.label || '';
              const value = context.parsed || 0;
              return `${label}: ${value.toFixed(2)} PLN`;
            },
          },
        },
      },
    },
  });
};

const toggleDropdown = () => {
  isDropdownOpen.value = !isDropdownOpen.value;
};

const selectOption = (index) => {
  selectedOption.value = options[index];
  isDropdownOpen.value = false;
  fetchData(options[index]);
};
</script>
