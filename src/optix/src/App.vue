<script setup lang="ts">
import { RouterView } from 'vue-router'
import Header from '@/components/layout/Header.vue'
</script>

<template>
  <div id="app" class="flex flex-column min-h-screen">
    <header>
      <Header />
    </header>

    <main class="flex align-items-center justify-content-center flex-grow-1">
      <RouterView />
    </main>
  </div>
</template>
