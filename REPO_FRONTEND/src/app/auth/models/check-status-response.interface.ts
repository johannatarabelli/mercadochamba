import { User } from "./user.interface";

export interface CheckStatusResponse {
  status: string;
  message: null;
  data: User;
  isSuccess: boolean;
}