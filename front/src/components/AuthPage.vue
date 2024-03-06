<script setup lang="ts">
import { useRouter } from "vue-router";

const router = useRouter();
const localStorageKey = "isVisibleState";

const navigateToHome = () => {
  localStorage.setItem(localStorageKey, JSON.stringify(true));
  localStorage.removeItem("token");
  router.push("/");
};
</script>
<template>
  <div class="bg-gray-100">
      <div class="h-screen flex flex-col justify-center items-center">
          <h1 class="text-8xl font-bold text-gray-800">403</h1>
          <p class="text-4xl font-medium text-gray-800">Forbidden acces</p>
          <button
            @click="navigateToHome"
            class="px-5 py-1 sm:px-10 sm:py-5 rounded-lg mt-4 mb-5 text-lg sm:text-2xl shadow-custom bg-white text-font-black hover:bg-black hover:text-white transition duration-300 ease-in-out"
          >
            GET BACK
          </button>
      </div>
  </div>
</template>