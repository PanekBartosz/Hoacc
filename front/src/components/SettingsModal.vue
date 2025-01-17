<script setup lang="ts">
import { ref, computed, onMounted } from "vue";
import { updateUserPass, getUserEmail } from "../api";
import { useRouter } from "vue-router";

const props = defineProps(["userId"]);

const router = useRouter();

const newPassword = ref("");
const confirmPassword = ref("");
const error = ref("");
const message = ref("");

const isButtonEnabled = computed(() => {
  return newPassword.value !== "" && confirmPassword.value !== "";
});

const open = ref(false);

const updatePassword = async () => {
  if (newPassword.value !== confirmPassword.value) {
    error.value = "The provided passwords are different";
    return;
  }

  try {
    const userId = props.userId;
    const response = await updateUserPass(userId, {
      password: newPassword.value,
    });
    // Handle successful password change
    if (response.status === 204) {
      error.value = "";
      message.value = "Password changed";
      newPassword.value = "";
      confirmPassword.value = "";
      setTimeout(() => {
        message.value = "";
        open.value = false;
      }, 3000); // Change password success message disappears after 3 seconds
    }
  } catch (error) {
    error.value = "Failed to change password";
  }
};

const email = ref("");

onMounted(async () => {
  try {
    const response = await getUserEmail(
      Number(router.currentRoute.value.params.id)
    );
    email.value = response.data;
  } catch (error) {
    error.value = "Failed to fetch email";
  }
});
</script>

<template>
  <div>
    <button
      class="text-sm font-semibold leading-6 text-gray-900 hover:text-gray-500"
      @click="open = true"
    >
      Settings
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
          <div class="flex items-center justify-between pb-3">
            <p class="text-2xl font-bold">Settings</p>
            <div class="z-50 cursor-pointer modal-close" @click="open = false">
              <img width="12" src="../icons/close-icon.png" />
            </div>
          </div>

          <p class="text-center">Account details</p>
          <hr />
          <div class="mt-4 mb-4 text-center">
            Your email: <b>{{ email }}</b>
          </div>
          <p class="text-center mt-10">Change password</p>
          <hr />
          <div class="flex">
            <div class="mt-4 w-1/2 mr-2">
              <input
                id="NewPass"
                placeholder="New Password"
                name="Newpassword"
                type="password"
                class="block w-full text-center rounded-md border-0 py-1.5 px-1.5 text-gray-900 shadow-sm ring-1 ring-gray-300 ring-inset placeholder:text-gray-400 focus:ring-1 focus:ring-inset sm:text-sm sm:leading-6"
                v-model="newPassword"
              />
            </div>
            <div class="mt-4 w-1/2">
              <input
                id="ConPass"
                placeholder="Confirm Password"
                name="Conpassword"
                type="password"
                class="block w-full text-center rounded-md border-0 py-1.5 px-1.5 text-gray-900 shadow-sm ring-1 ring-gray-300 ring-inset placeholder:text-gray-400 focus:ring-1 focus:ring-inset sm:text-sm sm:leading-6"
                v-model="confirmPassword"
              />
            </div>
          </div>

          <div class="flex justify-center mt-5">
            <p v-if="error !== ''" class="text-red-500 text-sm">{{ error }}</p>
            <p v-if="message !== ''" class="text-green-500 text-sm">
              {{ message }}
            </p>
          </div>

          <div class="flex justify-center mt-5">
            <button
              type="submit"
              class="rounded-lg w-full bg-slate-900 py-2 font-sans text-xs font-bold uppercase text-white shadow-md shadow-slate-500/20 transition-all hover:shadow-lg hover:shadow-slate-500/40 focus:opacity-[0.85] focus:shadow-none active:opacity-[0.85] active:shadow-none disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
              data-ripple-light="true"
              @click="updatePassword"
              :disabled="!isButtonEnabled"
            >
              Change password
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
