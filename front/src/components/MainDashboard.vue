<script setup lang="ts">
import MainNavbar from "./MainNavbar.vue";
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
    <div class="w-full h-full bg-white rounded-lg shadow-lg p-4 mb-5">
      Container 2
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
