import { computed, signal } from "@angular/core";
import { AuthSession } from "../domain/auth-session.model";


const _session = signal<AuthSession | null>(null);

export const authState = {
    session: _session,
    user: computed(() => _session()?.user ?? null),
    isAuthenticated: computed(() => !!_session()),


    setSession(session: AuthSession) {
        _session.set(session);
    },

    clearSession() {
        _session.set(null);
    },

    userName: computed(() => _session()?.user.displayName ?? 'Guest'),

    isAdmin: computed(() => _session()?.user.roles.includes('Admin') ?? false),
}
