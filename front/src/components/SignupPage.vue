<script setup lang="ts">

</script>
<template>
    <div class="flex min-h-[90vh] justify-center items-center">
      <div class="w-90 xs:w-80 sm:w-50 lg:w-30 bg-white rounded-2xl shadow-lg flex flex-col justify-center p-6 lg:p-8">
        <div class="sm:w-full sm:max-w-sm">
          <h2 class="mt-2 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900">
            Sign up
          </h2>
        </div>
    
        <div class="mt-10 mx-auto w-full max-w-sm">
          <form class="space-y-6" action="#" method="POST">
            <div>
              <label for="email" class="block text-sm font-medium leading-6 text-gray-900">
                Email address
              </label>
              <div class="mt-2">
                <input id="email" name="email" type="email" autocomplete="email" required="true" 
                class="block w-full rounded-md border-0 py-1.5 px-1.5 text-gray-900 shadow-sm ring-1
                ring-gray-300 ring-inset placeholder:text-gray-400 focus:ring-1 focus:ring-inset  sm:text-sm sm:leading-6" />
              </div>
            </div>
    
            <div>
              <div class="flex items-center justify-between">
                <label for="password" class="block text-sm font-medium leading-6 text-gray-900">
                  Password
                </label>
              </div>
              <div class="mt-2">
                <input id="password" name="password" type="password" autocomplete="current-password" required="true" 
                class="block w-full rounded-md border-0 py-1.5 px-1.5 text-gray-900 shadow-sm ring-1 ring-gray-300
                ring-inset placeholder:text-gray-400 focus:ring-1 focus:ring-inset  sm:text-sm sm:leading-6" />
              </div>
            </div>

            <div>
              <div class="flex items-center justify-between">
                <label for="password" class="block text-sm font-medium leading-6 text-gray-900">
                  Confirm password
                </label>
              </div>
              <div class="mt-2">
                <input id="c_password" name="c_password" type="password" autocomplete="current-password" required="true" 
                class="block w-full rounded-md border-0 py-1.5 px-1.5 text-gray-900 shadow-sm ring-1 ring-gray-300 
                ring-inset placeholder:text-gray-400 focus:ring-1 focus:ring-inset  sm:text-sm sm:leading-6" />
              </div>
            </div>
    
            <div>
              <button type="submit" class="flex w-full justify-center rounded-md bg-button-blue px-3 py-1.5 
              text-sm font-semibold leading-6 text-white shadow-sm hover:bg-button-blue-hover focus-visible:outline
              focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-button-blue-hover">
              Sign up
              </button>
            </div>
          </form>
    
          <p class="mt-10 text-center text-sm text-gray-500">
            Already have account?
            {{ ' ' }}
            <a href="/login" class="font-semibold leading-6 text-button-blue hover:text-button-blue-hover">
              Login
            </a>
          </p>
        </div>
      </div>
    </div>
  </template>