import { EnvironmentProviders, inject, makeEnvironmentProviders, provideAppInitializer } from "@angular/core";
import { authInitializer } from "../auth/auth.initializer";
import { TokenStorage } from "../auth/infrastructure/token.storage";
import { AuthApi } from "../auth/infrastructure/auth.api";
import { AuthFacade } from "../auth/application/auth.facade";


export const provideAuth = (): EnvironmentProviders => {
  return makeEnvironmentProviders([
    TokenStorage,
    AuthApi,
    AuthFacade,

    provideAppInitializer(() => {
      const tokenStorage = inject(TokenStorage);
      authInitializer(tokenStorage)();
    })
  ]);
};