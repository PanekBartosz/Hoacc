<script setup lang="ts">
import { ref, computed } from "vue";
import { postGoal } from "../api";

const props = defineProps(["fetchGoals", "userId"]);

const name = ref("");
const currentAmount = ref("");
const goalAmount = ref("");

const isButtonEnabled = computed(() => {
  return (
    name.value !== "" && currentAmount.value !== "" && goalAmount.value !== ""
  );
});

const open = ref(false);

const validateInput = (event) => {
  const pattern = /^(\d+(\.\d{0,2})?)?$/;
  const inputValue = event.target.value;

  if (!pattern.test(inputValue)) {
    currentAmount.value = "";
    goalAmount.value = "";
  }
};

const addNewGoal = async () => {
  try {
    const newGoal = {
      name: name.value,
      currentAmount: currentAmount.value,
      goalAmount: goalAmount.value,
      userId: props.userId,
    };
    // Call the API to add a new goal
    await postGoal(newGoal);
    await props.fetchGoals();

    // Clear input fields
    name.value = "";
    currentAmount.value = "";
    goalAmount.value = "";

    // Close the modal
    open.value = false;
  } catch (error) {
    console.error("Error adding new goal:", error.response?.data);
    alert("Error adding new goal");
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
        class="z-50 w-11/12 mx-auto overflow-y-auto bg-white rounded shadow-lg modal-container md:max-w-md"
      >
        <div class="px-6 py-4 text-left modal-content">
          <!-- Title -->
          <div class="flex items-center justify-between pb-3">
            <p class="text-2xl font-bold">Add goal</p>
            <div class="z-50 cursor-pointer modal-close" @click="open = false">
              <img width="12" src="../icons/close-icon.png" />
            </div>
          </div>

          <!-- Body -->
          <div class="items-center justify-center text-center">
            <div class="mt-4 w-3/4 mx-auto">
              <input
                placeholder="Enter name"
                name="name"
                type="text"
                maxlength="20"
                autocomplete="name"
                class="block w-full text-center rounded-md border-0 py-1.5 px-1.5 text-gray-900 shadow-sm ring-1 ring-gray-300 ring-inset placeholder:text-gray-400 focus:ring-1 focus:ring-inset sm:text-sm sm:leading-6"
                v-model="name"
              />
            </div>
            <div class="mt-4 w-3/4 mx-auto">
              <input
                id="CurrentAmount"
                placeholder="Enter current amount"
                name="amount"
                type="text"
                maxlength="10"
                class="block w-full text-center rounded-md border-0 py-1.5 px-1.5 text-gray-900 shadow-sm ring-1 ring-gray-300 ring-inset placeholder:text-gray-400 focus:ring-1 focus:ring-inset sm:text-sm sm:leading-6"
                v-model="currentAmount"
                @input="validateInput"
              />
            </div>
            <div class="mt-4 w-3/4 mx-auto">
              <input
                id="GoalAmount"
                placeholder="Enter goal amount"
                name="amount"
                type="text"
                maxlength="10"
                class="block w-full text-center rounded-md border-0 py-1.5 px-1.5 text-gray-900 shadow-sm ring-1 ring-gray-300 ring-inset placeholder:text-gray-400 focus:ring-1 focus:ring-inset sm:text-sm sm:leading-6"
                v-model="goalAmount"
                @input="validateInput"
              />
            </div>
          </div>

          <div class="flex justify-center mt-5">
            <button
              type="submit"
              class="rounded-lg w-3/4 bg-slate-900 py-2 font-sans text-xs font-bold uppercase text-white shadow-md shadow-slate-500/20 transition-all hover:shadow-lg hover:shadow-slate-500/40 focus:opacity-[0.85] focus:shadow-none active:opacity-[0.85] active:shadow-none disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
              data-ripple-light="true"
              @click="addNewGoal"
              :disabled="!isButtonEnabled"
            >
              Add goal
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
