export interface BaseResponse {
  status: Status;
  message?: string;
  data?: any;
  isSuccess?: boolean;
}

export enum Status {
  Success = 'success',
  Error = 'error',
}
