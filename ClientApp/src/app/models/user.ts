export class User {
  id: string;
  userName: string;
  email: string;
  password: string;
  token?: string;
  isRegistered: boolean;
  isManager: boolean;
  isAdmin: boolean;
  isSuperAdmin: boolean;
}
