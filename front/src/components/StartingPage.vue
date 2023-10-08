<script setup lang="ts">
import Logo from './Logo.vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const navigateToHome = () => {
  router.push('/')
}

const navigateToAbout = () => {
  router.push('/about')
}

</script>

<template>
  <div class="p-5">
    <Logo @click="navigateToHome"/>
    <div class="text-right xxs:mt-5 flex justify-end">
      <div class="flex items-center md:mr-[50px]">
        <button @click="navigateToHome" class="text-[16px] mt-7 xxs:mt-0 sm:mr-6 focus:text-white mr-3 font-bold sm:text-[32px] sm:focus:text-transparent  sm:focus:font-outline-2">
          HOME
        </button>
        <button @click="navigateToAbout" class="text-[16px] mt-7 xxs:mt-0 sm:mr-6 focus:text-white mr-3 font-bold sm:text-[32px] sm:focus:text-transparent  sm:focus:font-outline-2">
          ABOUT US
        </button>
        <button class="px-2 py-0 mt-7 xxs:mt-0 sm:px-4 sm:py-1 font-semibold rounded-lg text-lg shadow-custom bg-white 
            text-font-black hover:bg-black hover:text-white transition duration-300 ease-in-out">
          LOG IN
        </button>      
      </div>
    </div>
  </div>
</template>

<style scoped>

</style>
