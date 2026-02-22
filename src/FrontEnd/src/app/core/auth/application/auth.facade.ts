import { Injectable } from "@angular/core";
import { AuthApi } from "../infrastructure/auth.api";
import { TokenStorage } from "../infrastructure/token.storage";
import { ForgotPasswordCommand, LoginCommand, RefreshTokenCommand, RegisterCommand, ResetPasswordCommand, RevokeTokenCommand } from "./auth.commands";
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


    register(command: RegisterCommand) {
        return this.api.register(command);
    }
    
    forgotPassword(command: ForgotPasswordCommand) {
        return this.api.forgotPassword(command);
    }

    resetPassword(command: ResetPasswordCommand) {
        return this.api.resetPassword(command);
    }

    revokeToken(command: RevokeTokenCommand) {
        return this.api.revokeToken(command);
    }

    refreshToken(command: RefreshTokenCommand) {
        return this.api.refreshToken(command);
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
        this.router.navigate(['/login']);
    }
}

