<script setup lang="ts">
import {ref} from "vue";

const items = ref([
  {
    label: 'OptiX',
    icon: 'pi pi-wave-pulse',
    route: '/'
  },
  {
    label: '',
    icon: 'pi pi-user',
    items: [
      {
        label: 'Войти',
        route: '/login'
      },
    ]
  },
]);
</script>

<template>
  <Menubar :model="items" class="flex justify-content-center flex-wrap">
    <template #item="{ item, props, hasSubmenu }" class="flex align-items-center justify-content-center">
      <router-link v-if="item.route" v-slot="{ href, navigate }" :to="item.route" custom>
        <a :href="href" v-bind="props.action" @click="navigate">
          <span :class="item.icon"/>
          <span>{{ item.label }}</span>
        </a>
      </router-link>
      <a v-else :href="item.url" :target="item.target" v-bind="props.action">
        <span :class="item.icon"/>
        <span>{{ item.label }}</span>
        <span v-if="hasSubmenu" class="pi pi-fw pi-angle-down"/>
      </a>
    </template>
  </Menubar>
</template>
