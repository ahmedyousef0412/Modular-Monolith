import { isPlatformBrowser } from '@angular/common';
import { inject, Injectable, PLATFORM_ID } from '@angular/core';
import { JwtPayload } from './jwtpayload';

@Injectable({ providedIn: 'root' })
export class TokenStorage {
    private platformId = inject(PLATFORM_ID);
    private accessTokenKey = 'access_token';

    save(token: string): void {
        if (!isPlatformBrowser(this.platformId)) return;
        localStorage.setItem(this.accessTokenKey, token);
    }

    get(): string | null {
        if (!isPlatformBrowser(this.platformId)) return null;
        return localStorage.getItem(this.accessTokenKey);
    }

    clear(): void {
        if (!isPlatformBrowser(this.platformId)) return;
        localStorage.removeItem(this.accessTokenKey);
    }

    decodeToken(token: string): JwtPayload {
        if (!token || !isPlatformBrowser(this.platformId)) throw new Error('Token is null or not in browser');
        try {
            const base64Url = token.split('.')[1];
            const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
            const jsonPayload = decodeURIComponent(
                atob(base64)
                    .split('')
                    .map(function (c) {
                        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
                    })
                    .join(''),
            );
            return JSON.parse(jsonPayload);
        } catch (error) {
            console.error('Error decoding token:', error);
            throw new Error('Error decoding token');
        }
    }

    
}
