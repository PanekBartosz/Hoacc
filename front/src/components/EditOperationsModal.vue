<script setup lang="ts">
import { ref, computed } from "vue";
import Datepicker from "vuejs3-datepicker";
import { updateOperation, deleteOperation } from "../api";
import dayjs from "dayjs";

const props = defineProps(["fetchOperations", "operation"]);

const open = ref(false);

const selectedType = ref("");
const selectedDate = ref(new Date());
const category = ref("");
const description = ref("");
const amount = ref("");

const isButtonEnabled = computed(() => {
  return description.value !== "" && amount.value !== "";
});

const validateInput = (event) => {
  const pattern = /^(\d+(\.\d{0,2})?)?$/;
  const inputValue = event.target.value;

  if (!pattern.test(inputValue)) {
    amount.value = "";
  }
};

const fetchOperationsData = async () => {
  selectedType.value = props.operation.type;
  selectedDate.value = props.operation.date;
  description.value = props.operation.description;
  category.value = props.operation.category;
  amount.value = props.operation.amount;

  open.value = true;
};

const updateOperationLocal = async () => {
  try {
    const newOperation = {
      type: selectedType.value,
      date: dayjs(selectedDate.value),
      description: description.value,
      category: category.value,
      amount: amount.value,
      userId: props.operation.userId,
    };
    // Call the API to add a new goal
    await updateOperation(props.operation.operationId, newOperation);
    await props.fetchOperations();

    // Close the modal
    open.value = false;
  } catch (error) {
    console.error("Error adding new operation:", error.response?.data);
    alert("Error adding new operation");
  }
};

const deleteOperationLocal = async () => {
  try {
    await deleteOperation(props.operation.operationId);
    await props.fetchOperations();
  } catch (error) {
    alert("Error cannot delete operation");
  }
  open.value = false;
};
</script>

<template>
  <div>
    <button
      class="rounded-lg bg-slate-900 py-2 px-6 font-sans text-xs font-bold uppercase text-white shadow-md shadow-slate-500/20 transition-all hover:shadow-lg hover:shadow-slate-500/40 focus:opacity-[0.85] focus:shadow-none active:opacity-[0.85] active:shadow-none disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
      data-ripple-light="true"
      @click="fetchOperationsData"
    >
      Edit
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
        <div
          class="max-h-[90vh] overflow-auto px-6 py-4 text-left modal-content"
        >
          <!-- Title -->
          <div class="flex items-center justify-between pb-3">
            <p class="text-2xl font-bold">Edit operation</p>
            <div class="z-50 cursor-pointer modal-close" @click="open = false">
              <img width="12" src="../icons/close-icon.png" />
            </div>
          </div>

          <!-- Body -->
          <label
            class="block mt-5 mb-2 text-center text-sm font-medium text-gray-900"
            >Select Type</label
          >
          <div class="text-center">
            <label>
              <input
                type="radio"
                class="w-5 h-5 text-blue-600 focus:ring-blue-500"
                name="radio"
                value="income"
                v-model="selectedType"
              /><span class="ml-2 mr-5 text-gray-700">Income</span>
              <input
                type="radio"
                class="w-5 h-5 text-blue-600 focus:ring-blue-500"
                name="radio"
                value="outcome"
                v-model="selectedType"
              /><span class="ml-2 text-gray-700">Outcome</span>
            </label>
          </div>

          <label
            class="block mt-5 mb-0 text-center text-sm font-medium text-gray-900"
            >Select an Category</label
          >
          <div class="w-3/4 mx-auto">
            <select
              id="EOcategory"
              v-model="category"
              class="bg-gray-50 border text-center border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5"
            >
              <option value="0">Other</option>
              <option value="1">Bills</option>
              <option value="2">Food</option>
              <option value="3">Education</option>
              <option value="4">Entertaiment</option>
            </select>
          </div>

          <label
            class="block mt-3 mb-0 text-center text-sm font-medium text-gray-900"
            >Select Date</label
          >
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
            <label
              class="block mt-5 text-center text-sm font-medium text-gray-900"
              >Enter description</label
            >
            <div class="mt-1 w-3/4 mx-auto">
              <input
                placeholder="Enter Description"
                name="title"
                type="text"
                maxlength="50"
                class="block w-full text-center rounded-md border-0 py-1.5 px-1.5 text-gray-900 shadow-sm ring-1 ring-gray-300 ring-inset placeholder:text-gray-400 focus:ring-1 focus:ring-inset sm:text-sm sm:leading-6"
                v-model="description"
              />
            </div>
            <label
              class="block mt-5 text-center text-sm font-medium text-gray-900"
              >Enter amount</label
            >
            <div class="mt-1 w-3/4 mx-auto">
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
          <div class="text-center justify-center pt-2">
            <button
              class="rounded-lg w-3/4 mt-1 bg-red-700 py-2 px-6 font-sans text-xs font-bold uppercase text-white shadow-md shadow-slate-500/20 transition-all hover:shadow-lg hover:shadow-slate-500/40 focus:opacity-[0.85] focus:shadow-none active:opacity-[0.85] active:shadow-none disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
              data-ripple-light="true"
              @click="deleteOperationLocal"
            >
              Delete
            </button>
            <button
              class="rounded-lg w-3/4 mt-1 bg-slate-900 py-2 px-6 font-sans text-xs font-bold uppercase text-white shadow-md shadow-slate-500/20 transition-all hover:shadow-lg hover:shadow-slate-500/40 focus:opacity-[0.85] focus:shadow-none active:opacity-[0.85] active:shadow-none disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
              data-ripple-light="true"
              @click="updateOperationLocal"
              :disabled="!isButtonEnabled"
            >
              Save
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
