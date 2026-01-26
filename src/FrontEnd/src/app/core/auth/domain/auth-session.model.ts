import { AuthUser } from "./auth-user.model";

export interface AuthSession {
    accessToken: string;
    // refreshToken: string;
    expiresAt: Date;
    user: AuthUser;
}