import { createRouter, createWebHistory } from 'vue-router'
import HomeView from "@/views/HomeView.vue";
import GoogleCallback from "@/components/google/GoogleCallback.vue";
import Login from "@/components/Login.vue";
import { useUserStore } from '@/stores/user.ts'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    { path: '/', name: 'home', component: HomeView },
    { path: '/callback', component: GoogleCallback },
    { path: '/login', name: 'login', component: Login }
  ],
});

router.beforeEach(to => {
  const authStore = useUserStore();
  if (!authStore.isAuthenticated) {
    to.name = '/login';
  }
});

export default router
