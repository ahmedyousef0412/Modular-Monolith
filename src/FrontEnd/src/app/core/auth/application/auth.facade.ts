import { Injectable } from "@angular/core";
import { AuthApi } from "../infrastructure/auth.api";
import { TokenStorage } from "../infrastructure/token.storage";
import { LoginCommand } from "./auth.commands";
import { tap } from "rxjs";
import { authState } from "./auth.state";
import { Router } from "@angular/router";


@Injectable({ providedIn: 'root' })
export class AuthFacade {

    readonly user = authState.user;
    readonly session = authState.session;
    readonly isAuthenticated = authState.isAuthenticated;
    
    constructor(
        private api: AuthApi,
        private tokenStorage: TokenStorage,
        private router: Router
    ) { }


    login(command: LoginCommand) {
        return this.api.login(command).pipe(
            tap(session => {
                this.tokenStorage.save(session.accessToken);
                authState.setSession(session);
            })
        );
    }

    logout() {

        this.api.logout().subscribe({
            next: () => {
                this.handleLocalLogout();
            },
            error: (err) => {
                console.error('Logout Failed:', err);
                this.handleLocalLogout();
            }
        });
    }

    private handleLocalLogout() {
        this.tokenStorage.clear();
        authState.clearSession();
    }
}

