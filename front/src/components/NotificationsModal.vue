<script setup lang="ts">
import { ref, computed } from 'vue';
import Datepicker from 'vuejs3-datepicker';

const title = ref('');
const amount = ref('');
const selectedDate = ref(new Date()); // Initialize with the current date

const isButtonEnabled = computed(() => {
  return title.value !== '' && amount.value !== '';
});

const open = ref(false);

const formatDate = (date) => {
  const day = date.getDate().toString().padStart(2, '0')
  const month = (date.getMonth() + 1).toString().padStart(2, '0')
  const year = date.getFullYear()
  return `${month}.${day}.${year}`
}

const color = 'red';
</script>

<template>
  <div>
    <button
      class="rounded-lg bg-slate-900 py-2 px-6 font-sans text-xs font-bold uppercase text-white shadow-md shadow-slate-500/20 transition-all hover:shadow-lg hover:shadow-slate-500/40 focus:opacity-[0.85] focus:shadow-none active:opacity-[0.85] active:shadow-none disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
      data-ripple-light="true"
      @click="open = true"
    >
      Add new
    </button>
    <div
      :class="`modal ${
        !open && 'opacity-0 pointer-events-none'
      } z-50 fixed w-full h-full top-0 left-0 flex items-center justify-center`"
    >
      <div
        class="absolute w-full h-full bg-gray-900 opacity-60 modal-overlay"
        @click="open = false"
      />

      <div
        class="z-50 w-11/12 mx-auto overflow-y-auto bg-white rounded shadow-lg modal-container md:max-w-md"
      >
        <div class="px-6 py-4 text-left modal-content">
          <!-- Title -->
          <div class="flex items-center justify-between pb-3">
            <p class="text-2xl font-bold">Add notification</p>
            <div class="z-50 cursor-pointer modal-close" @click="open = false">
              <img width="12" src="../icons/close-icon.png" />
            </div>
          </div>

          <!-- Body -->
            <div class="items-center justify-center text-center">
                <div class="mt-4 w-3/4 mx-auto">
                    <input
                    id="title"
                    placeholder="Enter title"
                    name="title"
                    type="text"
                    class="block w-full text-center rounded-md border-0 py-1.5 px-1.5 text-gray-900 shadow-sm ring-1 ring-gray-300 ring-inset placeholder:text-gray-400 focus:ring-1 focus:ring-inset sm:text-sm sm:leading-6"
                    v-model="title"
                    />
                </div>
                <div class="mt-4 w-3/4 mx-auto">
                    <input
                    id="amount"
                    placeholder="Enter amount"
                    name="amount"
                    type="text"
                    class="block w-full text-center rounded-md border-0 py-1.5 px-1.5 text-gray-900 shadow-sm ring-1 ring-gray-300 ring-inset placeholder:text-gray-400 focus:ring-1 focus:ring-inset sm:text-sm sm:leading-6"
                    v-model="amount"
                    />
                </div>
                <p class="mt-5">Pick date</p>
                <hr class="w-3/4 mx-auto">
                <Datepicker
                    v-model="selectedDate"
                    inline
                    monday-first
                    placeholder="DD-MM-YYYY"
                    :typeable="false"
                    :hideInput="true"
                />
            </div>

            <div class="flex justify-center mt-5">
                <button
                    type="submit"
                    class="rounded-lg w-3/4 bg-slate-900 py-2 font-sans text-xs font-bold uppercase text-white shadow-md shadow-slate-500/20 transition-all hover:shadow-lg hover:shadow-slate-500/40 focus:opacity-[0.85] focus:shadow-none active:opacity-[0.85] active:shadow-none disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
                    data-ripple-light="true"
                    @click="open = false"
                    :disabled="!isButtonEnabled"
                >
                    Add notification
                </button>
            </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style>

.modal {
  transition: opacity 0.25s ease;
}

.vuejs3-datepicker__calendar-topbar {
    background-color: rgba(30, 170, 241, 0.5);
}

.vuejs3-datepicker__calendar .cell.selected {
    background-color: rgba(30, 170, 241, 0.5);
    border-radius: 25%;
}

.vuejs3-datepicker__calendar .cell.selected:hover {
    background-color: rgba(30, 170, 241, 0.5);
    border-radius: 25%;
    border-color: rgba(30, 170, 241, 0.5);
}

</style>
