import { Injectable, inject } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { ApiService } from './api.service';
import { StorageService } from './storage.service';
import { AuthResponse, LoginRequest, User } from '../models';
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiService = inject(ApiService);
  private storageService = inject(StorageService);

  private currentUserSubject = new BehaviorSubject<User | null>(null);
  public currentUser$ = this.currentUserSubject.asObservable();

  private isAuthenticatedSubject = new BehaviorSubject<boolean>(this.hasValidToken());
  public isAuthenticated$ = this.isAuthenticatedSubject.asObservable();

  constructor() {
    // Initialize from storage
    const user = this.storageService.getItem<User>(environment.userKey);
    if (user) {
      this.currentUserSubject.next(user);
    }
  }

  login(credentials: LoginRequest): Observable<AuthResponse> {
    return this.apiService.post<AuthResponse>('auth/login', credentials).pipe(
      tap(response => {
        // Handle both wrapped (response.data) and direct response formats
        const authData = (response as any).data || response;
        if (authData?.token) {
          this.storageService.setItem(environment.tokenKey, authData.token);
          this.isAuthenticatedSubject.next(true);
          
          // Create and store user object
          const user: User = {
            id: authData.userId || authData.id || '',
            fullName: authData.name || authData.fullName || '',
            userName: authData.userName || '',
            email: authData.email || '',
            roles: authData.roles || []
          };
          this.setCurrentUser(user);
        }
      }),
      map(response => {
        const authData = (response as any).data || response;
        return authData as AuthResponse;
      })
    );
  }

  logout(): void {
    this.storageService.removeItem(environment.tokenKey);
    this.storageService.removeItem(environment.userKey);
    this.currentUserSubject.next(null);
    this.isAuthenticatedSubject.next(false);
  }

  setCurrentUser(user: User): void {
    this.storageService.setItem(environment.userKey, user);
    this.currentUserSubject.next(user);
  }

  getCurrentUser(): User | null {
    return this.currentUserSubject.value;
  }

  getToken(): string | null {
    return this.storageService.getItem<string>(environment.tokenKey);
  }

  isAuthenticated(): boolean {
    return this.hasValidToken();
  }

  hasRole(role: string | string[]): boolean {
    const user = this.getCurrentUser();
    if (!user) return false;

    if (typeof role === 'string') {
      return user.roles.includes(role);
    }

    return role.some(r => user.roles.includes(r));
  }

  private hasValidToken(): boolean {
    const token = this.storageService.getItem<string>(environment.tokenKey);
    return !!token;
  }
}
