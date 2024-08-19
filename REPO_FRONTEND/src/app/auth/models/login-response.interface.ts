import { BaseResponse, Status } from "../../shared/models/response.interface";

export interface LoginResponse extends BaseResponse {
    status: Status;
    message: string;
    data: null;
    isSuccess: boolean;
}