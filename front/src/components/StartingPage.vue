<script setup lang="ts">
import TypedText from './TypedText.vue'
import Logo from './Logo.vue'
</script>

<template>
  <Logo />
  <button class="text-[32px] mr-6 font-bold focus:text-transparent  focus:font-outline-2">
    HOME
  </button>
  <button class="text-[32px] mr-6 font-bold focus:text-transparent  focus:font-outline-2">
    ABOUT US
  </button>
  <button>
    LOG IN
  </button>
  <div class="text-center font-bold overflow-auto">
    <p class="text-3xl text-transparent font-outline-2">
      HOME ACCOUNTING
    </p>
    <p class="text-font-white text-6xl">
      Efficient use of the budget.
    </p>
    <div class="text-font-black text-5xl flex items-center justify-center">
      <TypedText />
      <span class="ml-2">
        accounting
      </span>
    </div>
    <p class="text-font-grey text-3xl">
      Monitor spending data to increase efficiency in household budget management.
    </p>
    <button class="px-5 py-1 rounded-lg mt-4 mb-5 text-lg shadow-custom bg-white 
      text-font-black hover:bg-black hover:text-white transition duration-300 ease-in-out">
      GET STARTED
    </button>
  </div>
</template>

<style scoped>

</style>
