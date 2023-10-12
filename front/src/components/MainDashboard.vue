<script setup lang="ts">
import MainNavbar from './MainNavbar.vue';
</script>
<template>
  <MainNavbar />
  <div class="w-full h-full flex flex-col p-5">
    <!-- Container 1-->
    <div class="w-full h-full bg-white rounded-lg shadow-lg p-4 mb-5">
      Container 1
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
