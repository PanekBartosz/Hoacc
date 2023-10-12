<script setup lang="ts">
import { computed, ref, watch } from 'vue'
import Logo from './Logo.vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const localStorageKey = 'isVisibleState' // Key for storing in localStorage
let initialIsVisible = localStorage.getItem(localStorageKey)
const isVisible = ref(true)

const navigateToHome = () => {
  isVisible.value = true
  localStorage.setItem(localStorageKey, JSON.stringify(true))
  router.push({ name: 'Home' })
  router.push('/')
}

const navigateToAbout = () => {
  router.push({ name: 'About' })
  router.push('/about')
}
const navigateToLogin = () => {
  isVisible.value = false
  localStorage.setItem(localStorageKey, JSON.stringify(false))
  router.push('/login')
}

watch(() => router.currentRoute.value, () => {
  initialIsVisible = localStorage.getItem(localStorageKey)
  isVisible.value = initialIsVisible !== null ? JSON.parse(initialIsVisible) : true
})

const isActiveRoute = (routeName) => {
  return router.currentRoute.value.name === routeName;
}

const isDashboardRoute = computed(() => {
  return router.currentRoute.value.path === '/dashboard';
})

</script>

<template>
  <div class="p-5">
    <Logo v-if="!isDashboardRoute" @click="navigateToHome"/>
    <div v-if="isVisible && !isDashboardRoute">
      <div class="text-right xxs:mt-5 flex justify-end">
        <div class="flex items-center md:mr-[50px]">
          <button @click="navigateToHome" :class="{ 'text-white': isActiveRoute('Home') }" class="text-[16px] mt-7 xxs:mt-0 sm:mr-6 mr-3 font-bold sm:text-[32px]">
            HOME
          </button>
          <button @click="navigateToAbout" :class="{ 'text-white': isActiveRoute('About') }" class="text-[16px] mt-7 xxs:mt-0 sm:mr-6 mr-3 font-bold sm:text-[32px]">
            ABOUT US
          </button>
          <button @click="navigateToLogin"  class="px-2 py-0 mt-7 xxs:mt-0 sm:px-4 sm:py-1 font-semibold rounded-lg text-lg shadow-custom bg-white 
              text-font-black hover:bg-black hover:text-white transition duration-300 ease-in-out">
            LOG IN
          </button>      
        </div>
      </div>
    </div>
  </div>
</template>