<script setup lang="ts">
import { ref,computed, onMounted } from "vue";
import Datepicker from 'vuejs3-datepicker';
import { postOperation } from '../api';
import dayjs from 'dayjs'

const props = defineProps(['fetchOperations','userId']);

const open = ref(false);

const selectedType = ref('income')
const selectedDate = ref(new Date());
const description = ref('');
const category = ref('');
const amount = ref('');

const isButtonEnabled = computed(() => {
  return description.value !== '' && amount.value !== '';
});

const validateInput = (event) => {
  const pattern = /^(\d+(\.\d{0,2})?)?$/;
  const inputValue = event.target.value;

  if (!pattern.test(inputValue)) {
    amount.value = '';
  }
}

const addNewOperation = async () => {
  try {
    const newOperation = {
      type: selectedType.value,
      date: dayjs(selectedDate.value).format('YYYY-MM-DD'),
      description: description.value,
      category: category.value,
      amount: amount.value,
      userId: props.userId,
    };
    // Call the API to add a new operation
    await postOperation(newOperation);
    await props.fetchOperations();

    // Close the modal
    open.value = false;
  } catch (error) {
    console.error('Error adding new operation:', error.response?.data);
    alert('Error adding new operation')
  }
};

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
        class="z-50 w-11/12 bg-white rounded shadow-lg modal-container md:max-w-md"
        >
        <div class="max-h-[90vh] overflow-auto px-6 py-4 text-left modal-content">
          <!-- Title -->
          <div class="flex items-center justify-between pb-3">
            <p class="text-2xl font-bold">Add operation</p>
            <div class="z-50 cursor-pointer modal-close" @click="open = false">
              <img width="12" src="../icons/close-icon.png" />
            </div>
          </div>

          <!-- Body -->
          <label class="block mt-5 mb-2 text-center text-sm font-medium text-gray-900">Select Type</label>
          <div class="text-center">
            <label>
              <input
                id="income"
                type="radio"
                class="w-5 h-5 text-blue-600 focus:ring-blue-500"
                name="radio"
                checked
                @change="selectedType = 'income'"
              ><span class="ml-2 mr-5 text-gray-700">Income</span>
              <input
                id="outcome"
                type="radio"
                class="w-5 h-5 text-blue-600 focus:ring-blue-500"
                name="radio"
                @change="selectedType = 'outcome'"
              ><span class="ml-2 text-gray-700">Outcome</span>
            </label>
          </div>

          <label class="block mt-5 mb-2 text-center text-sm font-medium text-gray-900">Select an Category</label>
          <div class="w-3/4 mx-auto">
            <select v-model="category" class="bg-gray-50 border text-center border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 ">
              <option selected value="0">Other</option>
              <option value="1">Bills</option>
              <option value="2">Food</option>
              <option value="3">Education</option>
              <option value="4">Entertaiment</option>
            </select>
          </div>

          <label class="block mt-5 mb-2 text-center text-sm font-medium text-gray-900">Select Date</label>
          <div class="text-center">
            <Datepicker
                      v-model="selectedDate"
                      inline
                      monday-first
                      placeholder="DD-MM-YYYY"
                      :typeable="false"
                      :hideInput="true"
              />
          </div>

          <div class="items-center justify-center text-center">
                <div class="mt-4 w-3/4 mx-auto">
                    <input
                    placeholder="Enter Description"
                    name="title"
                    type="text"
                    maxlength="50"
                    class="block w-full text-center rounded-md border-0 py-1.5 px-1.5 text-gray-900 shadow-sm ring-1 ring-gray-300 ring-inset placeholder:text-gray-400 focus:ring-1 focus:ring-inset sm:text-sm sm:leading-6"
                    v-model="description"
                    />
                </div>
                <div class="mt-4 w-3/4 mx-auto">
                    <input
                    placeholder="Enter amount"
                    name="amount"
                    type="text"
                    maxlength="10"
                    class="block w-full text-center rounded-md border-0 py-1.5 px-1.5 text-gray-900 shadow-sm ring-1 ring-gray-300 ring-inset placeholder:text-gray-400 focus:ring-1 focus:ring-inset sm:text-sm sm:leading-6"
                    v-model="amount"
                    @input="validateInput"
                    />
                </div>
            </div>
          <!-- Footer -->
          <div class="flex justify-center pt-2">
            <button
              class="rounded-lg w-3/4 mt-5 bg-slate-900 py-2 px-6 font-sans text-xs font-bold uppercase text-white shadow-md shadow-slate-500/20 transition-all hover:shadow-lg hover:shadow-slate-500/40 focus:opacity-[0.85] focus:shadow-none active:opacity-[0.85] active:shadow-none disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
              data-ripple-light="true"
              @click="addNewOperation"
              :disabled="!isButtonEnabled"
            >
              Add operation
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
</style>
