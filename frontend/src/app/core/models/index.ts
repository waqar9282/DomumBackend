export enum UserRole {
  ADMIN = 'Admin',
  DIRECTOR = 'Director',
  REGIONAL_PROJECT_MANAGER = 'RegionalProjectManager',
  SENIOR_MANAGER = 'SeniorManager',
  OPERATION_MANAGER = 'OperationManager',
  KEY_WORKER = 'KeyWorker',
  SOCIAL_WORKER = 'SocialWorker'
}

export interface User {
  id: string;
  fullName: string;
  userName: string;
  email: string;
  roles: string[];
}

export interface AuthResponse {
  userId: string;
  name: string;
  token: string;
}

export interface LoginRequest {
  userName: string;
  password: string;
}

export interface ApiResponse<T> {
  success: boolean;
  data?: T;
  message?: string;
  errors?: string[];
}
