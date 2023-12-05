<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from "vue-router";
import { authenticateUser, setAuthToken } from "../api";

const email = ref("");
const password = ref("");
const router = useRouter();
const errorMessage = ref("");

const loginUser = async () => {
  try {
    const response = await authenticateUser(email.value, password.value);

    const userId = response.data.userId;
    const token = response.data.token;

    // The token is now set in the Axios headers using setAuthToken function
    setAuthToken(token);
    router.push({ name: "Dashboard", params: { id: userId } });
  } catch (error) {
    // Handle authentication failure
    errorMessage.value = "Invalid email or password.";
  }
};
</script>
<template>
  <div class="flex min-h-[90vh] justify-center items-center">
    <div
      class="w-90 xs:w-80 sm:w-50 lg:w-30 bg-white rounded-2xl shadow-lg flex flex-col justify-center p-6 lg:p-8"
    >
      <div class="sm:w-full sm:max-w-sm">
        <h2
          class="mt-2 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900"
        >
          Login
        </h2>
      </div>

      <div class="mt-10 mx-auto w-full max-w-sm">
        <form
          @submit.prevent="loginUser"
          class="space-y-6"
          action="#"
          method="POST"
        >
          <div>
            <label
              for="email"
              class="block text-sm font-medium leading-6 text-gray-900"
            >
              Email address
            </label>
            <div class="mt-2">
              <input
                v-model="email"
                id="email"
                name="email"
                type="email"
                autocomplete="email"
                required="true"
                maxlength="200"
                class="block w-full rounded-md border-0 py-1.5 px-1.5 text-gray-900 shadow-sm ring-1 ring-gray-300 ring-inset placeholder:text-gray-400 focus:ring-1 focus:ring-inset sm:text-sm sm:leading-6"
              />
            </div>
          </div>

          <div>
            <div class="flex items-center justify-between">
              <label
                for="password"
                class="block text-sm font-medium leading-6 text-gray-900"
              >
                Password
              </label>
            </div>
            <div class="mt-2">
              <input
                v-model="password"
                id="password"
                name="password"
                type="password"
                autocomplete="current-password"
                required="true"
                maxlength="200"
                class="block w-full rounded-md border-0 py-1.5 px-1.5 text-gray-900 shadow-sm ring-1 ring-gray-300 ring-inset placeholder:text-gray-400 focus:ring-1 focus:ring-inset sm:text-sm sm:leading-6"
              />
            </div>
          </div>

          <!-- <div class="text-center">
            <a
              href="#"
              class="font-semibold leading-6 text-button-blue hover:text-button-blue-hover"
            >
              Forgot password?
            </a>
          </div> -->

          <div>
            <p v-if="errorMessage" class="text-red-500 text-center">
              {{ errorMessage }}
            </p>
            <button
              type="submit"
              class="flex w-full justify-center rounded-md bg-button-blue px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-button-blue-hover focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-button-blue-hover"
            >
              Login
            </button>
          </div>
        </form>

        <p class="mt-10 text-center text-sm text-gray-500">
          Not a member?
          {{ " " }}
          <a
            href="/signup"
            class="font-semibold leading-6 text-button-blue hover:text-button-blue-hover"
          >
            Sign up
          </a>
        </p>
      </div>
    </div>
  </div>
</template>
