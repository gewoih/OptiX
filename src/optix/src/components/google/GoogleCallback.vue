<script setup lang="ts">
import {onMounted} from "vue";
import router from "../../router";
import {useUserStore} from "@/stores/user.ts";

onMounted(() => {
  handleCallback();
})

const handleCallback = () => {
  const userStore = useUserStore();
  const urlParams = new URLSearchParams(window.location.search);
  const token = urlParams.get('token');

  if (token) {
    userStore.setAuthToken(token);
    router.push('/');
  } else {
    console.error('Token not found in URL');
    router.push('/login');
  }
}
</script>

<template>
  <div>
    Redirecting...
  </div>
</template>
