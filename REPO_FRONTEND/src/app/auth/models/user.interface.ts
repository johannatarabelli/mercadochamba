export interface User {
  userName: string;
  role: string;
}

export interface UserRegister {
  userName?: string | null;
  firstName?: string | null;
  lastName?: string | null;
  email?: string | null;
  dni?: string | null;
  phoneNumber?: string | null;
  password?: string | null;
  address?: string | null;
}

export interface UserLogin {
  userName?: string | null;
  password?: string | null;
}

export interface UserAdmin {
  id?: number,
  userName: string,
  firstName: string,
  lastName: string,
  email: string,
  password?: string
}

export interface UserAdminResponse {
  id:          number;
  userName:    string;
  firstName:   string;
  lastName:    string;
  email:       string;
  dni:         null;
  phoneNumber: null;
  address:     null;
  password:    string;
  profile:     null;
  job:         null;
  userRoles:   UserRole[];
}

export interface UserRole {
  userId: number;
  user:   null;
  roleId: number;
  role:   null;
}
