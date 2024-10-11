import { JwtPayload } from "jwt-decode";

export interface CustomJwtPayload extends JwtPayload {
  preferred_username: string;
}
