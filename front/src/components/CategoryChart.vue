<template>
    <div class="w-full bg-white border-2 border-slate-50 rounded-lg shadow-lg p-4 md:p-6 ">
      <div class="flex justify-center border-gray-200 border-b pb-3">
        <dl>
          <dt class="leading-none text-xl font-medium text-gray-700  pb-1">Expense by Categories</dt>
        </dl>
      </div>
      
      <div class="mt-[50px]"/>

      <div class="flex items-center justify-center">
        <div>
          <canvas ref="pieChart"></canvas>
        </div>
      </div>

      <div class="grid grid-cols-1 items-center border-gray-200 mt-5 border-t dark:border-gray-700">
        <div class="flex items-center justify-center pt-5">
          <!-- Button -->
          <button
            id="dropdownDefaultButton"
            data-dropdown-toggle="lastDaysdropdown"
            data-dropdown-placement="bottom"
            class="text-sm font-medium text-gray-500 dark:text-gray-400 hover:text-gray-900 text-center inline-flex items-center dark:hover:text-white"
            type="button"
          >
            Last month
            <svg class="ml-2" xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 320 512" fill="#6b7280">
              <path d="M137.4 374.6c12.5 12.5 32.8 12.5 45.3 0l128-128c9.2-9.2 11.9-22.9 6.9-34.9s-16.6-19.8-29.6-19.8L32 192c-12.9 0-24.6 7.8-29.6 19.8s-2.2 25.7 6.9 34.9l128 128z"/>
            </svg>
          </button>
          <!-- Dropdown menu -->
          <div id="lastDaysdropdown" class="z-10 hidden bg-white divide-y divide-gray-100 rounded-lg shadow w-44 dark:bg-gray-700">
            <ul class="py-2 text-sm text-gray-700 dark:text-gray-200" aria-labelledby="dropdownDefaultButton">
              <li>
                <a href="#" class="block px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white">Yesterday</a>
              </li>
              <li>
                <a href="#" class="block px-4 py-2 hover-bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white">Today</a>
              </li>
              <li>
                <a href="#" class="block px-4 py-2 hover-bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white">Last 7 days</a>
              </li>
              <li>
                <a href="#" class="block px-4 py-2 hover-bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white">Last 30 days</a>
              </li>
              <li>
                <a href="#" class="block px-4 py-2 hover-bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white">Last 90 days</a>
              </li>
              <li>
                <a href="#" class="block px-4 py-2 hover-bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white">Last 6 months</a>
              </li>
              <li>
                <a href="#" class="block px-4 py-2 hover-bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white">Last year</a>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  </template>

<script setup>
import { onMounted, ref } from 'vue';
import Chart from 'chart.js/auto';

const pieChart = ref(null);

onMounted(() => {
  const ctx = pieChart.value.getContext('2d');
  new Chart(ctx, {
    type: 'pie',
    data: {
      labels: ['Education', 'Bills', 'Holidays'],
      datasets: [
        {
          data: [20, 60 ,20],
          backgroundColor: ['#93C5FD', '#34D399','#534323'],
        },
      ],
    },
  });
});
</script>
