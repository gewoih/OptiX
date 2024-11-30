import { defineStore } from 'pinia';

export const useUserStore = defineStore('user', {
  state: () => ({
    authToken: localStorage.getItem('access_token') ?? '',
  }),
  actions: {
    setAuthToken(token: string) {
      this.authToken = token;
      localStorage.setItem('access_token', token);
    },
    clearAuthToken() {
      this.authToken = '';
      localStorage.removeItem('access_token');
    },
  },
  getters: {
    isAuthenticated: (state) => !!state.authToken,
  },
});
