import { AuthUser } from "../domain/auth-user.model";

export const mapJwtToUser = (decoded: any): AuthUser => {

    const ensureArray = (value: any): string[] => {
        return Array.isArray(value) ? value : value ? [value] : [];
    }
    return {
        id: decoded.sub,
        email: decoded.email,
        displayName: `${decoded.given_name} ${decoded.family_name}`,
        roles: ensureArray(decoded["role"]),
        permissions: ensureArray(decoded["permission"])
    };
};