<script setup lang="ts">
import { ref } from 'vue';
import TypedText from './TypedText.vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const localStorageKey = 'isVisibleState' 

const navigateToSignup = () => {
  localStorage.setItem(localStorageKey, JSON.stringify(false));
  router.push('/signup');
}

</script>
<template>
    <div class="text-center font-bold flex h-[80vh] justify-center">
      <div class="m-auto">
        <p class="text-2xl sm:text-3xl text-transparent font-outline-2">
          HOME ACCOUNTING
        </p>
        <p class="text-font-white text-2xl sm:text-6xl">
          Efficient use of the budget.
        </p>
        <div class="text-font-black text-2xl sm:text-5xl m-2 flex items-center justify-center">
          <TypedText />
          <span class="ml-2">
            accounting
          </span>
        </div>
        <p class="text-font-grey text-xl sm:text-3xl">
          Monitor spending data to increase efficiency in household budget management.
        </p>
        <button @click="navigateToSignup" class="px-5 py-1 sm:px-10 sm:py-5 rounded-lg mt-4 mb-5 text-lg sm:text-2xl shadow-custom bg-white 
          text-font-black hover:bg-black hover:text-white transition duration-300 ease-in-out">
          GET STARTED
        </button>
      </div>
    </div>
</template>