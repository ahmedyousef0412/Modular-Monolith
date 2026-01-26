import { environment } from "../../../../environments/environment";

export class AuthEndpoints {
    
    private static readonly baseUrl = environment.apiUrl;

    public static readonly login = `${this.baseUrl}/api/identity/auth/login`;
    public static readonly register = `${this.baseUrl}/api/identity/auth/register`;
    public static readonly logout = `${this.baseUrl}/api/identity/auth/revoke`;
}