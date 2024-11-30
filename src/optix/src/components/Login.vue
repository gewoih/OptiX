<script setup lang="ts">
import {authService} from "@/services/auth.ts";

const login = async () => {
  await authService.login();
}
</script>

<template>
  <div>
    <h2>Welcome Back!</h2>
    <Button @click="login">Login via Google</Button>
  </div>
</template>
