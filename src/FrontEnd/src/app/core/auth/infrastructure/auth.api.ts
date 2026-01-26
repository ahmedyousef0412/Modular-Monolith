import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { LoginCommand } from '../application/auth.commands';
import { map, Observable } from 'rxjs';
import { AuthSession } from '../domain/auth-session.model';
import { mapJwtToUser } from '../application/auth.mapper';
import { TokenStorage } from './token.storage';
import { AuthEndpoints } from './auth.endpoints';

@Injectable({ providedIn: 'root' })
export class AuthApi {
    private http = inject(HttpClient);
    private tokenStorage = inject(TokenStorage);

    login(command: LoginCommand): Observable<AuthSession> {
        return this.http
            .post<{ accessToken: string; }>(AuthEndpoints.login, command, { withCredentials: true })
            .pipe(
                map((response) => {
                    const rawPayload = this.tokenStorage.decodeToken(response.accessToken);
                    return {
                        accessToken: response.accessToken,
                        user: mapJwtToUser(rawPayload),
                        expiresAt: rawPayload?.exp
                            ? new Date(rawPayload.exp * 1000)
                            : new Date(Date.now() + 3600 * 1000),
                    };
                }),
            );
    }

    logout(): Observable<void> {
        return this.http.post<void>(AuthEndpoints.logout, null, { withCredentials: true });
    }
}
