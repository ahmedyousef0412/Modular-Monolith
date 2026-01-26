
import { TokenStorage } from "./infrastructure/token.storage";
import { authState } from "./application/auth.state";
import { mapJwtToUser } from "./application/auth.mapper";


export const authInitializer = (tokenStorage: TokenStorage) => {

    return () => {
        const token = tokenStorage.get();
        if (!token) return;

        const decodedRaw = tokenStorage.decodeToken(token);

        const expirationDate = decodedRaw.exp
            ? new Date(decodedRaw.exp * 1000) // convert seconds to milliseconds
            : new Date(Date.now() + 3600 * 1000); // 1 hour from now

        if (expirationDate <= new Date()) {
            tokenStorage.clear();
            return;
        }

        if (decodedRaw) {
            const user = mapJwtToUser(decodedRaw);
            authState.session.set({
                accessToken: token,
                user: user,
                expiresAt: expirationDate
            })
        }
    }


}