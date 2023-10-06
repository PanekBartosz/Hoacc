<template>
    <div id="typed-output"></div>
  </template>
  
<script setup lang="ts">
import Typed from 'typed.js';
import { onMounted } from 'vue';
  
onMounted(() => {
    const typed = new Typed('#typed-output', {
        strings: ['Fast', 'Flexible', 'Transparent'],
        typeSpeed: 100,
        backSpeed: 120,
        loop: true,
    })
})
</script>
  