<script setup lang="ts">
import { ref, computed } from 'vue';
import { updateGoal, deleteGoal } from '../api';


const props = defineProps(['fetchGoals', 'userId','goal']);

const name = ref('');
const currentAmount = ref('');
const goalAmount = ref('');

const isButtonEnabled = computed(() => {
  return name.value !== '' && currentAmount.value !== '' && goalAmount.value !== '';
});

const open = ref(false);

const validateInput = (event) => {
  const pattern = /^(\d+(\.\d{0,2})?)?$/;
  const inputValue = event.target.value;

  if (!pattern.test(inputValue)) {
    currentAmount.value = ''
    goalAmount.value = ''
  }
}

const fetchGoalData = async () => {
    name.value = props.goal.name;
    currentAmount.value = props.goal.currentAmount;
    goalAmount.value = props.goal.goalAmount;

    open.value = true;
};

const updateGoalLocal = async () => {
  try {
    const newGoal = {
      name: name.value,
      currentAmount: currentAmount.value,
      goalAmount: goalAmount.value,
      userId: props.userId,
    };
    // Call the API to add a new goal
    await updateGoal(props.goal.goalsId, newGoal);
    await props.fetchGoals();

    // Close the modal
    open.value = false;
  } catch (error) {
    alert('Error adding new goal')
  }
};

const deleteGoalLocal = async () => {
  try {
    await deleteGoal(props.goal.goalsId);
    await props.fetchGoals();
  } catch (error) {
    alert('Error cannot delete goal')
  }
  open.value = false
};
</script>

<template>
  <div>
    <button
      class="rounded-lg bg-slate-900 py-2 px-6 font-sans text-xs font-bold uppercase text-white shadow-md shadow-slate-500/20 transition-all hover:shadow-lg hover:shadow-slate-500/40 focus:opacity-[0.85] focus:shadow-none active:opacity-[0.85] active:shadow-none disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
      data-ripple-light="true"
      @click="fetchGoalData"
    >
      Update
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
            <p class="text-2xl font-bold">Edit goal</p>
            <div class="z-50 cursor-pointer modal-close" @click="open = false">
              <img width="12" src="../icons/close-icon.png" />
            </div>
          </div>

          <!-- Body -->
            <div class="items-center justify-center text-center">
                <label class="block mt-4 mb-0 text-sm font-medium text-gray-500">Enter name</label>
                <div class="mt-0 w-3/4 mx-auto">
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
                <label class="block mt-4 mb-0 text-sm font-medium text-gray-500">Enter current amount</label>
                <div class="mt-0 w-3/4 mx-auto">
                    <input
                    placeholder="Enter current amount"
                    name="amount"
                    type="text"
                    maxlength="10"
                    class="block w-full text-center rounded-md border-0 py-1.5 px-1.5 text-gray-900 shadow-sm ring-1 ring-gray-300 ring-inset placeholder:text-gray-400 focus:ring-1 focus:ring-inset sm:text-sm sm:leading-6"
                    v-model="currentAmount"
                    @input="validateInput"
                    />
                </div>
                <label class="block mt-4 mb-0 text-sm font-medium text-gray-500">Enter goal amount</label>
                <div class="mt-0 w-3/4 mx-auto">
                    <input
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

            <div class="justify-center mt-5 text-center">
                <button
                    type="submit"
                    class="rounded-lg mb-3 w-3/4 bg-red-700 py-2 font-sans text-xs font-bold uppercase text-white shadow-md shadow-slate-500/20 transition-all hover:shadow-lg hover:shadow-slate-500/40 focus:opacity-[0.85] focus:shadow-none active:opacity-[0.85] active:shadow-none disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
                    data-ripple-light="true"
                    @click="deleteGoalLocal"
                >
                    Delete
                </button>
                <button
                    type="submit"
                    class="rounded-lg w-3/4 bg-slate-900 py-2 font-sans text-xs font-bold uppercase text-white shadow-md shadow-slate-500/20 transition-all hover:shadow-lg hover:shadow-slate-500/40 focus:opacity-[0.85] focus:shadow-none active:opacity-[0.85] active:shadow-none disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
                    data-ripple-light="true"
                    @click="updateGoalLocal"
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
