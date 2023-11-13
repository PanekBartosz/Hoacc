<script setup lang="ts">
import { ref } from "vue"
import { Dialog, DialogPanel } from "@headlessui/vue"
import { Bars3Icon, XMarkIcon } from "@heroicons/vue/24/outline"
import SettingsModal from "./SettingsModal.vue"
import SettingsMobileModal from "./SettingsMobileModal.vue"

const navigation = [
  { name: "Home", href: "#" },
  { name: "Operations", href: "#" },
  { name: "Notifications", href: "#" },
  { name: "Goals", href: "#" },
  { name: "Statics", href: "#" }
]

const mobileMenuOpen = ref(false)

const scrollToSection = (sectionId) => {
  if (sectionId === "home") {
    window.scrollTo({
      top: 0, // Scroll to the top of the page
      behavior: "smooth",
    });
  } else {
    const element = document.getElementById(sectionId);

    if (element) {
      const offset = 25
      window.scrollTo({
        top: element.offsetTop - offset,
        behavior: "smooth",
      });
    }
  }
};

</script>
<template>
  <div class="bg-white mb-5 absolute top-0 w-full">
    <header class="absolute inset-x-0 top-0 z-1000">
      <nav
        class="flex items-center justify-between bg-white shadow-lg rounded-b-2xl p-2 px-8"
        aria-label="Global"
      >
        <div class="flex lg:flex-1">
          <a href="#" class="-m-1.5 p-1.5">
            <span class="sr-only">Hoacc</span>
            <img class="h-7 w-auto" src="../icons/logo.png" alt="HOACC" />
          </a>
        </div>
        <div class="flex lg:hidden">
          <button
            type="button"
            class="-m-2.5 inline-flex items-center justify-center rounded-md p-2.5 text-gray-700"
            @click="mobileMenuOpen = true"
          >
            <span class="sr-only">Open main menu</span>
            <Bars3Icon class="h-6 w-6" aria-hidden="true" />
          </button>
        </div>
        <div class="hidden lg:flex lg:gap-x-12">
          <a
            v-for="item in navigation"
            :key="item.name"
            @click.prevent="scrollToSection(item.name.toLowerCase())"
            class="text-sm font-semibold leading-6 text-gray-900 hover:text-gray-500"
            >{{ item.name }}</a
          >
          <SettingsModal />
        </div>
        <div class="hidden lg:flex lg:flex-1 lg:justify-end">
          <a href="/" class="text-sm font-semibold leading-6 text-gray-900 hover:text-gray-500"
            >Log out <span aria-hidden="true">&rarr;</span></a
          >
        </div>
      </nav>
      <Dialog
        as="div"
        class="lg:hidden"
        @close="mobileMenuOpen = false"
        :open="mobileMenuOpen"
      >
        <div class="fixed inset-0 z-100"></div>
        <DialogPanel
          class="fixed inset-y-0 right-0 z-1000 w-full overflow-y-auto bg-white px-6 py-6 sm:max-w-sm sm:ring-1 sm:ring-gray-900/10"
        >
          <div class="flex items-center justify-between">
            <a href="#" class="-m-1.5 p-1.5">
              <span class="sr-only">HOACC</span>
              <img class="h-5 w-auto" src="../icons/logo.png" alt="HOACC" />
            </a>
            <button
              type="button"
              class="-m-2.5 rounded-md p-2.5 text-gray-700"
              @click="mobileMenuOpen = false"
            >
              <span class="sr-only">Close menu</span>
              <XMarkIcon class="h-6 w-6" aria-hidden="true" />
            </button>
          </div>
          <div class="mt-6 flow-root">
            <div class="-my-6 divide-y divide-gray-500/10">
              <div class="space-y-2 py-6">
                <a
                  v-for="item in navigation"
                  :key="item.name"
                  @click.prevent="scrollToSection(item.name.toLowerCase())"
                  class="-mx-3 block rounded-lg px-3 py-2 text-base font-semibold leading-7 text-gray-900 hover:bg-gray-50"
                  >{{ item.name }}</a
                >
                <SettingsMobileModal :mobileMenuOpen="mobileMenuOpen"/>
              </div>
              <div class="py-6">
                <a
                  href="/"
                  class="-mx-3 block rounded-lg px-3 py-2.5 text-base font-semibold leading-7 text-gray-900 hover:bg-gray-50"
                  >Log out</a
                >
              </div>
            </div>
          </div>
        </DialogPanel>
      </Dialog>
    </header>
  </div>
</template>