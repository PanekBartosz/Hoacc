<script setup lang="ts">
import { computed, ref, onMounted, Ref, nextTick  } from "vue";
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
import { getOperations,getNotifications,deleteNotification,getGoals } from '../api';
import { format } from 'date-fns';
import { enUS } from 'date-fns/locale';
import Chart, { ChartOptions } from 'chart.js/auto';
import { useRouter } from "vue-router";

interface Operation {
  operationId: number;
  type: string;
  date: Date;
  description: string;
  category: number;
  amount: number;
  userId: number;
}

interface Category {
  value: number;
  name: string;
  color: string;
}

interface Notification {
  notificationId: number;
  name: string;
  date: Date;
  amount: number;
}

interface Goal {
  goalsId: number;
  name: string;
  goalAmount: number;
  currentAmount: number;
}

const categoryMappings: Category[] = [
  { value: 0, name: 'Bills', color: 'red' },
  { value: 1, name: 'Food', color: 'green' },
  { value: 2, name: 'Education', color: 'blue' },
  { value: 3, name: 'Entertainment', color: 'purple' },
]

const currentPage = ref(1);
const operations = ref<Operation[]>([]);
const userId = ref<number | undefined>(undefined);
const notifications = ref<Notification[]>([]);
const goals = ref<Goal[]>([]);

const router = useRouter();

const totalIncome = computed(() => {
  const currentDate = new Date();
  const currentMonth = currentDate.getMonth(); // Get the current month (0-indexed)
  
  return operations.value
    .filter(o => {
      const operationDate = new Date(o.date);
      return operationDate.getMonth() === currentMonth && o.type.toLowerCase() === 'income';
    })
    .reduce((total, operation) => total + operation.amount, 0);
});

const totalOutcome = computed(() => {
  const currentDate = new Date();
  const currentMonth = currentDate.getMonth(); // Get the current month (0-indexed)
  
  return operations.value
    .filter(o => {
      const operationDate = new Date(o.date);
      return operationDate.getMonth() === currentMonth && o.type.toLowerCase() === 'outcome';
    })
    .reduce((total, operation) => total + operation.amount, 0);
});

const totalSavings = computed(() => {
  return totalIncome.value - totalOutcome.value;
});

const formatDate = (dateString: Date): string => {
  const date = new Date(dateString);
  return format(date, 'dd MMMM yyyy', { locale: enUS }).toUpperCase();
};

const formatDateNotification = (dateString: Date): string => {
  const date = new Date(dateString);
  return format(date, 'dd.MM.yyyy').toUpperCase();
};

const fetchOperations = async () => {
  try {
    if (userId.value !== undefined) {
      const response = await getOperations(userId.value);
      operations.value = response.data;
    } else {
      console.error('User ID is undefined.');
    }
  } catch (error) {
    console.error('Error fetching operations:', error.response?.data);
  }
};

const fetchGoals = async () => {
  try {
    if (userId.value !== undefined) {
      const response = await getGoals(userId.value);
      goals.value = response.data;
    } else {
      console.error('User ID is undefined.');
    }
    updateCharts()
  } catch (error) {
    console.error('Error fetching goals:', error.response?.data);
  }
};

// Function to generate a random color excluding white and black
const generateRandomColor = () => {
      let randomColor;
      do {
        randomColor = '#' + Math.floor(Math.random() * 16777215).toString(16);
      } while (randomColor === '#ffffff' || randomColor === '#000000');
      return randomColor;
    };

// Function to calculate percentage completed
const calculatePercentageCompleted = (currentAmount, goalAmount) => {
  return ((currentAmount / goalAmount) * 100).toFixed(2) + '%';
};

const chartOptions: ChartOptions<'doughnut'> = {
  elements: {
    arc: {
      spacing: 3,
      borderRadius: 5,
    },
  },
  plugins: {
    tooltip: {
      callbacks: {
        label: (context) => {
          const labelIndex = context.dataIndex;
          const value = context.dataset.data[labelIndex];

          if (labelIndex === 0) {
            return `Remaining: ${value}`;
          } else {
            return `Completed: ${value}`;
          }
        },
      },
    },
  },
};

const updateCharts = async () => {
  try {
    const response = await getGoals(Number(router.currentRoute.value.params.id));
    goals.value = response.data;

    await nextTick();

    goals.value.forEach(async (goal, index) => {
      const canvasRef = document.querySelector(`#goalChart_${index}`) as HTMLCanvasElement;
      if (canvasRef) {
        const ctx = canvasRef.getContext('2d');
        if (ctx) {
          console.log(`Creating or updating chart for goal ${goal.name}`);

          const percentageCompleted = calculatePercentageCompleted(goal.currentAmount, goal.goalAmount);

          // Check if chart already exists for this canvas
          const existingChart = Chart.getChart(ctx);
          if (existingChart) {
            existingChart.data.datasets = [
              {
                data: [goal.goalAmount - goal.currentAmount, goal.currentAmount],
                backgroundColor: ['#CCCCCC', generateRandomColor()],
              },
            ];
            existingChart.update();
          } else {
            new Chart(ctx, {
              type: 'doughnut',
              data: {
                datasets: [
                  {
                    data: [goal.goalAmount - goal.currentAmount, goal.currentAmount],
                    backgroundColor: ['#CCCCCC', generateRandomColor()],
                  },
                ],
              },
              options: chartOptions,
            });
          }
        }
      }
    });
  } catch (error) {
    console.error('Error fetching goals:', error.response?.data);
  }
};

onMounted(async () => {
  try {
    userId.value = Number(router.currentRoute.value.params.id);
    await fetchNotifications();
    await fetchOperations();
    await fetchGoals();
  } catch (error) {
    console.error('Error fetching goals:', error.response?.data);
  }
});

const perPage = 5;

const paginatedOperations = computed(() => {
  const startIndex = (currentPage.value - 1) * perPage;
  const endIndex = startIndex + perPage;

  return operations.value.slice(startIndex, endIndex).map((o: Operation) => {
    const categoryInfo = categoryMappings.find(c => c.value === o.category);
    return { ...o, categoryInfo };
  });
});

const fetchNotifications = async () => {
  try {
    if (userId.value !== undefined) {
      const response = await getNotifications(userId.value);
      notifications.value = response.data;
    } else {
      console.error('User ID is undefined.');
    }
  } catch (error) {
    console.error('Error fetching notifications:', error.response?.data);
  }
};

const deleteNotificationLocal = async (index) => {
  try {
    const notificationId = notifications.value[index].notificationId;
    await deleteNotification(notificationId);
    notifications.value.splice(index, 1);
  } catch (error) {
    alert('Error deleting notification')
  }
};

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
                <h4 class="text-2xl font-semibold">{{ totalIncome }} PLN</h4>
              </div>
            </div>
          </div>

          <div class="w-full px-6 mt-6 sm:w-1/2 xl:w-1/3 sm:mt-0">
            <div
              class="flex items-center px-5 py-2 rounded-md text-red-700 ring-1 ring-inset ring-red-600/10 bg-red-50 shadow-sm"
            >
              <div class="mx-auto">
                <div class="text-center">Total outcome</div>
                <h4 class="text-2xl font-semibold">{{ totalOutcome }} PLN</h4>
              </div>
            </div>
          </div>

          <div class="w-full px-6 mt-6 sm:w-1/2 xl:w-1/3 xl:mt-0">
            <div
              class="flex items-center px-5 py-2 bg-blue-50 text-blue-700 ring-1 ring-inset ring-blue-700/10 rounded-md shadow-sm"
            >
              <div class="mx-auto">
                <div class="text-center">Total savings</div>
                <h4 class="text-2xl font-semibold">{{ totalSavings }} PLN</h4>
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
        <OperationsModal 
        :fetchOperations="fetchOperations"
        :userId="userId"
        />
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
                        {{ formatDate(o.date) }}
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
                    :class="`inline-flex px-2 text-xs font-semibold leading-5 text-${o.categoryInfo?.color}-800 bg-${o.categoryInfo?.color}-200 rounded-full`"
                    >{{ o.categoryInfo?.name }}</span>
                  </td>

                  <td
                    class="px-6 py-4 text-sm leading-5 text-gray-900 border-b border-gray-200 whitespace-nowrap"
                    :style="{ color: (o.type.toLowerCase() === 'income' ? 'green' : 'red') as any }"
                  >
                    {{ o.amount + " PLN" }}
                  </td>

                  <td
                    class="px-6 py-4 leading-5 text-right border-b border-gray-200"
                  >
                  <EditOperationsModal 
                    :operation="o"
                    :fetchOperations="fetchOperations"
                  />
                  </td>
                </tr>
              </tbody>
            </table>
            <div class="flex items-center justify-center text-cente">
              <Pagination
                v-model="currentPage"
                :layout="'table'"
                :per-page="perPage"
                :total-items="operations.length"
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
          <NotificationsModal 
            :userId="userId" 
            :fetchNotifications="fetchNotifications"
          />
        </div>
        <div class="flex flex-row my-3 overflow-auto">
            <div v-for="(notification, index) in notifications" :key="index" class="bg-white border border-t-4 border-t-blue-500 shadow-sm rounded-xl text-center shadow-md p-4 mx-3 my-3">
              <div class="font-bold text-gray-700">{{ notification.name }}</div>
              <div class="text-gray-500">{{ formatDateNotification(notification.date) }}</div>
              <div class="text-gray-900">{{ notification.amount + " PLN"}}</div>
              <button @click="deleteNotificationLocal(index)" class="text-red-600 hover:text-red-900 mt-2">Delete</button>
            </div>
        </div>
      </div>
      

      <div class="flex-grow bg-white rounded-lg shadow-lg p-4 mb-5 lg:w-1/2 overflow-x-auto">
    <div id="goals" class="flex flex-row justify-between mb-6">
      <h3 class="text-xl font-medium text-gray-700">Goals</h3>
      <GoalsModal 
        :userId="userId" 
        :fetchGoals="fetchGoals"
      />
    </div>

    <div class="flex flex-no-wrap space-x-4 overflow-x-auto">
      <!-- Loop through goals and create a chart for each one -->
      <div v-for="(goal, index) in goals" :key="index" class="rounded-lg w-min flex items-center shadow-lg p-1 border-2 border-slate-50 mb-4">
        <div class="w-32">
          <!-- Use ref to create a reactive reference for the chart -->
          <canvas :id="`goalChart_${index}`" class="h-[120px] w-[120px]"></canvas>
        </div>
        <div class="text-center mr-2">
          <h3 class="text-3xl mx-5 font-medium mb-2">{{ goal.name }}</h3>
          <EditGoalsModal 
            :userId="userId"
            :goal="goal"
            :fetchGoals="fetchGoals"
          />
        </div>
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
