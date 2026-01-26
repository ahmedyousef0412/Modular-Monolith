
export interface AuthUser {
    id: string;
    email: string;
    displayName: string;
    roles: string[];
    permissions: string[];
    exp?: number;
    iat?: number;
}