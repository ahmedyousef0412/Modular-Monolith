import { AuthUser } from "./auth-user.model";

export function hasRole(user: AuthUser | null, role: string): boolean {
    return !!user?.roles.includes(role);
}

export function hasPermission(user: AuthUser | null, permission: string): boolean {
    return !!user?.permissions.includes(permission);
}