class AuthService {
  async login() {
    window.location.href = "https://localhost:5001/api/auth/google?returnUrl=http://localhost:8080/callback";
  }
}

export const authService = new AuthService();
