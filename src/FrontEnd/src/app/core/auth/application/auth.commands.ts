export interface LoginCommand {
  email: string;
  password: string;
  rememberMe: boolean;
}

export interface RegisterCommand {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
}

export interface RefreshTokenCommand {
  refreshToken: string;
}

export interface RevokeTokenCommand {
  refreshToken: string;
}

export interface ForgotPasswordCommand {
  email: string;
}

export interface ResetPasswordCommand {
  token: string;
  email: string;
  newPassword: string;
  confirmNewPassword: string;
}
