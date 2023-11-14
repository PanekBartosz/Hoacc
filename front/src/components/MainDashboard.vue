<script setup lang="ts">
import { computed, ref } from "vue";
import MainNavbar from "./MainNavbar.vue";
import { Pagination } from "flowbite-vue";
import ProfitChart from "./ProfitChart.vue"
import SavingsChart from "./SavingsChart.vue";
import CategoryChart from "./CategoryChart.vue";
import OperationsModal from "./OperationsModal.vue";
import NotificationsModal from "./NotificationsModal.vue";
import EditOperationsModal from "./EditOperationsModal.vue";
import GoalsModal from "./GoalsModal.vue";
import EditGoalsModal from "./EditGoalsModal.vue"

const currentDate = new Date();
const currentPage = ref(1);
const totalItems = 20

const formattedDate = new Intl.DateTimeFormat("en-US", {
  day: "2-digit",
  month: "long",
  year: "numeric",
}).format(currentDate);

const genNumber = () => {
  return Math.floor(Math.random() * 100) + 1;
}

interface Operation {
  date: string;
  category: string;
  description: string;
  amount: number;
}

const operations = ref<Operation[]>(
  [...Array(totalItems).keys()].map(() => ({
    date: formattedDate,
    category: "EDUCATION",
    description: "New book - c# in depth",
    amount: genNumber(),
  }))
);
const perPage = 5;

const paginatedOperations = computed(() => {
  const startIndex = (currentPage.value - 1) * perPage;
  const endIndex = startIndex + perPage;
  return operations.value.slice(startIndex, endIndex);
})

const notifications = ref([
  {
    title: 'Netflix',
    date: '17.10.2023',
    price: genNumber() + ' PLN',
  },
  {
    title: 'Netflix',
    date: '17.10.2023',
    price: genNumber() + ' PLN',
  },
  {
    title: 'Netflix',
    date: '17.10.2023',
    price: genNumber() + ' PLN',
  },
  {
    title: 'Netflix',
    date: '17.10.2023',
    price: genNumber() + ' PLN',
  },
  {
    title: 'Netflix',
    date: '17.10.2023',
    price: genNumber() + ' PLN',
  },
  {
    title: 'Netflix',
    date: '17.10.2023',
    price: genNumber() + ' PLN',
  },
  {
    title: 'Netflix',
    date: '17.10.2023',
    price: genNumber() + ' PLN',
  },
]);

const deleteNotification = (index) => {
  notifications.value.splice(index, 1);
};

import {watch } from 'vue';
import Chart from 'chart.js/auto';

const donutChart = ref<HTMLCanvasElement | null>(null);

watch(() => donutChart.value, (newValue) => {
  if (newValue) {
    const ctx = (newValue as HTMLCanvasElement).getContext('2d'); // Type assertion
    if (ctx) { // Check if getContext returns a non-null value
      new Chart(ctx, {
        type: 'doughnut',
        data: {
          datasets: [
            {
              data: [33, 67],
              backgroundColor: ['grey', '#34D399'],
            },
          ],
        },
      });
    }
  }
});


</script>
<template>
  <MainNavbar class="" />
  <div class="w-full h-full flex flex-col p-5">
    <!-- Container 1-->
    <div id="home" class="w-full h-full mt-10 bg-white rounded-lg shadow-lg p-4 mb-5">
      <h3 class="text-xl font-medium text-gray-700">Monthly balance</h3>
      <div class="mt-3">
        <div class="flex flex-wrap -mx-6">
          <div class="w-full px-6 sm:w-1/2 xl:w-1/3">
            <div
              class="flex items-center px-5 py-2 rounded-md bg-green-50 text-green-700 ring-1 ring-inset ring-green-600/20 shadow-sm"
            >
              <div class="mx-auto">
                <div class="text-center">Total income</div>
                <h4 class="text-2xl font-semibold">6250 PLN</h4>
              </div>
            </div>
          </div>

          <div class="w-full px-6 mt-6 sm:w-1/2 xl:w-1/3 sm:mt-0">
            <div
              class="flex items-center px-5 py-2 rounded-md text-red-700 ring-1 ring-inset ring-red-600/10 bg-red-50 shadow-sm"
            >
              <div class="mx-auto">
                <div class="text-center">Total outcome</div>
                <h4 class="text-2xl font-semibold">5300 PLN</h4>
              </div>
            </div>
          </div>

          <div class="w-full px-6 mt-6 sm:w-1/2 xl:w-1/3 xl:mt-0">
            <div
              class="flex items-center px-5 py-2 bg-blue-50 text-blue-700 ring-1 ring-inset ring-blue-700/10 rounded-md shadow-sm"
            >
              <div class="mx-auto">
                <div class="text-center">Total savings</div>
                <h4 class="text-2xl font-semibold">950 PLN</h4>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Container 2 -->
    <div
      class="w-full h-full overflow-hidden bg-white rounded-lg shadow-lg p-4 mb-5"
    >
      <div id="operations" class="flex flex-row justify-between">
        <h3 class="text-xl font-medium text-gray-700">Operations</h3>
        <OperationsModal />
      </div>
      <div class="flex flex-col mt-3">
        <div
          class="py-2 -my-2 overflow-x-auto sm:-mx-6 sm:px-6 lg:-mx-8 lg:px-8"
        >
          <div
            class="inline-block min-w-full overflow-hidden align-middle border-b border-gray-200 shadow sm:rounded-lg"
          >
            <table class="min-w-full">
              <thead>
                <tr>
                  <th
                    class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50"
                  >
                    Date
                  </th>
                  <th
                    class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50"
                  >
                    Description
                  </th>
                  <th
                    class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50"
                  >
                    Category
                  </th>
                  <th
                    class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50"
                  >
                    Amount
                  </th>
                  <th
                    class="px-6 py-3 border-b border-gray-200 bg-gray-50"
                  ></th>
                </tr>
              </thead>

              <tbody class="bg-white">
                <tr v-for="(o, index) in paginatedOperations" :key="index">
                  <td
                    class="px-6 py-4 border-b border-gray-200 whitespace-nowrap"
                  >
                    <div class="flex items-center">
                      <div class="text-sm font-medium leading-5 text-gray-900">
                        {{ o.date }}
                      </div>
                    </div>
                  </td>

                  <td
                    class="px-6 py-4 border-b border-gray-200 whitespace-nowrap"
                  >
                    <div class="text-sm leading-5 text-gray-900">
                      {{ o.description }}
                    </div>
                  </td>

                  <td
                    class="px-6 py-4 border-b border-gray-200 whitespace-nowrap"
                  >
                    <span
                      class="inline-flex px-2 text-xs font-semibold leading-5 text-blue-800 bg-blue-200 rounded-full"
                      >{{ o.category }}</span
                    >
                  </td>

                  <td
                    class="px-6 py-4 text-sm leading-5 text-gray-900 border-b border-gray-200 whitespace-nowrap"
                  >
                    {{ o.amount + " PLN" }}
                  </td>

                  <td
                    class="px-6 py-4 leading-5 text-right border-b border-gray-200"
                  >
                  <EditOperationsModal />
                  </td>
                </tr>
              </tbody>
            </table>
            <div class="flex items-center justify-center text-cente">
              <Pagination
                v-model="currentPage"
                :layout="'table'"
                :per-page="5"
                :total-items="totalItems"
                class="mb-2 text-center"
              />
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Container 3 & 4  -->
    <div class="w-full h-full lg:flex">
    <div class="flex-grow bg-white rounded-lg shadow-lg p-4 mb-5 lg:w-1/2 lg:mr-5">
      <div id="notifications" class="flex flex-row justify-between">
        <h3 class="text-xl font-medium text-gray-700">Notifications</h3>
        <NotificationsModal/>
      </div>
      <div class="flex flex-row my-3 overflow-auto">
          <div v-for="(notification, index) in notifications" :key="index" class="bg-neutral-200 rounded-lg text-center shadow-md p-4 mx-3 my-3">
            <div class="font-bold text-gray-700">{{ notification.title }}</div>
            <div class="text-gray-500">{{ notification.date }}</div>
            <div class="text-gray-900">{{ notification.price }}</div>
            <button @click="deleteNotification(index)" class="text-red-600 hover:text-red-900 mt-2">Delete</button>
          </div>
      </div>
    </div>
    <div class="flex-grow bg-white rounded-lg shadow-lg p-4 mb-5 lg:w-1/2">
      <div id="goals" class="flex flex-row justify-between mb-6">
        <h3 class="text-xl font-medium text-gray-700">Goals</h3>
        <GoalsModal />
      </div>

        <div class="rounded-lg w-min flex items-center shadow-lg p-1 border-2 border-slate-50">
          <div class="w-32">
            <canvas ref="donutChart"></canvas>
          </div>
          <div class="text-center">
            <h3 class="text-3xl mx-5 font-medium mb-2">Holidays</h3>
            <EditGoalsModal />
          </div>
        </div>

    </div>
  </div >

    <!-- Container 5  -->
    <div id="statics" class="w-full h-full bg-white rounded-lg shadow-lg p-4">
      <div  class="flex flex-row mb-3">
        <h3 class="text-xl font-medium text-gray-700">Statics</h3>
      </div>
      <div class="w-full flex flex-wrap">
        <div class="w-full">
          <ProfitChart class="mb-5"/>
        </div>
        <div class="w-full md:flex md:flex-wrap md:space-x-0">
          <div class="w-full md:w-1/2 mb-2">
            <SavingsChart class="w-full" />
          </div>
          <div class="w-full md:w-1/2 mb-2">
            <CategoryChart class="w-full" />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
