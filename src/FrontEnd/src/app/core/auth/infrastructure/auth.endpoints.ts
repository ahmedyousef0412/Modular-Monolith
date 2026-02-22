import { environment } from "../../../../environments/environment";

export class AuthEndpoints {

    private static readonly baseUrl = environment.apiUrl;

    public static readonly login = `${this.baseUrl}/api/identity/auth/login`;
    public static readonly register = `${this.baseUrl}/api/identity/auth/register`;
    public static readonly logout = `${this.baseUrl}/api/identity/auth/revoke`;
    public static readonly refreshToken = `${this.baseUrl}/api/identity/auth/refresh-token`;
    public static readonly revokeToken = `${this.baseUrl}/api/identity/auth/revoke`;
    public static readonly forgotPassword = `${this.baseUrl}/api/identity/auth/forgot-password`;
    public static readonly resetPassword = `${this.baseUrl}/api/identity/auth/reset-password`;
}