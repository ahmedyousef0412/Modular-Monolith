import { HttpInterceptorFn } from "@angular/common/http";
import { inject } from "@angular/core";
import { TokenStorage } from "./token.storage";

export const authInterceptor: HttpInterceptorFn = (req, next) => {
    const token = inject(TokenStorage).get();

    if (token) {
        req = req.clone({
            setHeaders: {
                Authorization: `Bearer ${token}`
            }, withCredentials: true
        })
    }

    //Will handle Refresh token later
    return next(req);
};




