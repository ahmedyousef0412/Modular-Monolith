export interface JwtPayload {
  sub: string;
  email?: string;
  given_name?: string;
  family_name?: string;
  roles?: string[];
  permissions?: string[];
  exp: number;
  iat: number;
  iss?: string;
  aud?: string;
}
