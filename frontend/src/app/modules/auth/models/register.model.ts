export interface RegisterFormModel {
  userName: string;
  email: string;
  password: string;
  confirmPassword: string;
};

export interface RegisterRequest {
  userName: string;
  email: string;
  password: string;
}