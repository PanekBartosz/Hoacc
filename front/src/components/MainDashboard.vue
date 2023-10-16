<script setup lang="ts">
import { ref } from "vue";
import MainNavbar from "./MainNavbar.vue";

const currentDate = new Date()

const formattedDate = new Intl.DateTimeFormat('en-US', {
  day: '2-digit',
  month: 'long',
  year: 'numeric'
}).format(currentDate)

interface Operation {
  date: string
  category: string
  description: string
  amount: number
}

const testOperation: Operation = {
  date: formattedDate,
  category: 'EDUCATION',
  description: 'New book - c# in depth',
  amount: 53.01
}

const operations = ref<Operation[]>([...Array(10).keys()].map(() => testOperation))
</script>
<template>
  <MainNavbar />
  <div class="w-full h-full flex flex-col p-5">
    <!-- Container 1-->
    <div class="w-full h-full bg-white rounded-lg shadow-lg p-4 mb-5">
      <h3 class="text-xl font-medium text-gray-700">Monthly balance</h3>
      <div class="mt-4">
        <div class="flex flex-wrap -mx-6">
          <div class="w-full px-6 sm:w-1/2 xl:w-1/3">
            <div class="flex items-center px-5 py-2 rounded-md bg-green-50 text-green-700 ring-1 ring-inset ring-green-600/20 shadow-sm">
              <div class="mx-auto">
                <h4 class="text-2xl font-semibold">6250 PLN</h4>
                <div>Total income</div>
              </div>
            </div>
          </div>

          <div class="w-full px-6 mt-6 sm:w-1/2 xl:w-1/3 sm:mt-0">
            <div class="flex items-center px-5 py-2 rounded-md text-red-700 ring-1 ring-inset ring-red-600/10 bg-red-50 shadow-sm">
              <div class="mx-auto">
                <h4 class="text-2xl font-semibold ">5300 PLN</h4>
                <div>Total outcome</div>
              </div>
            </div>
          </div>

          <div class="w-full px-6 mt-6 sm:w-1/2 xl:w-1/3 xl:mt-0">
            <div class="flex items-center px-5 py-2 bg-blue-50 text-blue-700 ring-1 ring-inset ring-blue-700/10 rounded-md shadow-sm">
              <div class="mx-auto">
                <h4 class="text-2xl font-semibold ">950 PLN</h4>
                <div class="">Total savings</div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Container 2 -->
    <div class="w-full h-full overflow-hidden bg-white rounded-lg shadow-lg p-4 mb-5">
      <h3 class="text-xl font-medium text-gray-700">Operations</h3>
      <div class="flex flex-col mt-8">
      <div class="py-2 -my-2 overflow-x-auto sm:-mx-6 sm:px-6 lg:-mx-8 lg:px-8">
        <div class="inline-block min-w-full overflow-hidden align-middle border-b border-gray-200 shadow sm:rounded-lg">
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
                  Category
                </th>
                <th
                  class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50"
                >
                  Description
                </th>
                <th
                  class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50"
                >
                  Amount
                </th>
                <th class="px-6 py-3 border-b border-gray-200 bg-gray-50" />
              </tr>
            </thead>

            <tbody class="bg-white">
              <tr v-for="(o, index) in operations" :key="index">
                <td
                  class="px-6 py-4 border-b border-gray-200 whitespace-nowrap"
                >
                  <div class="flex items-center">
                    <div class="ml-4">
                      <div class="text-sm font-medium leading-5 text-gray-900">
                        {{ o.date }}
                      </div>
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
                  >{{ o.category }}</span>
                </td>

                <td
                  class="px-6 py-4 text-sm leading-5 text-gray-900 border-b border-gray-200 whitespace-nowrap"
                >
                  {{ o.amount }}
                </td>

                <td
                  class="px-6 py-4 text-sm font-bold leading-5 text-right border-b border-gray-200 whitespace-nowrap"
                >
                  <a href="#" class="text-indigo-600 hover:text-indigo-900">Edit</a>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
    </div>

    <!-- Container 3 & 4  -->
    <div class="w-full h-full sm:flex">
      <div class="flex-grow bg-white rounded-lg shadow-lg p-4 mb-5 sm:mr-5">
        Container 3
      </div>
      <div class="flex-grow bg-white rounded-lg shadow-lg p-4 mb-5">
        Container 4
      </div>
    </div>

    <!-- Container 5  -->
    <div class="w-full h-full bg-white rounded-lg shadow-lg p-4 mb-5">
      Container 5
    </div>
  </div>
</template>
