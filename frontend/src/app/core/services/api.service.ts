import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError, timeout } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { environment } from '@env/environment';
import { ApiResponse } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = environment.apiUrl;
  private timeoutMs = environment.apiTimeout;

  constructor(private http: HttpClient) {}

  get<T>(endpoint: string): Observable<ApiResponse<T>> {
    return this.http.get<ApiResponse<T>>(`${this.apiUrl}/${endpoint}`).pipe(
      timeout(this.timeoutMs),
      retry(1),
      catchError(this.handleError)
    );
  }

  post<T>(endpoint: string, data: any): Observable<ApiResponse<T>> {
    return this.http.post<ApiResponse<T>>(`${this.apiUrl}/${endpoint}`, data).pipe(
      timeout(this.timeoutMs),
      catchError(this.handleError)
    );
  }

  put<T>(endpoint: string, data: any): Observable<ApiResponse<T>> {
    return this.http.put<ApiResponse<T>>(`${this.apiUrl}/${endpoint}`, data).pipe(
      timeout(this.timeoutMs),
      catchError(this.handleError)
    );
  }

  delete<T>(endpoint: string): Observable<ApiResponse<T>> {
    return this.http.delete<ApiResponse<T>>(`${this.apiUrl}/${endpoint}`).pipe(
      timeout(this.timeoutMs),
      catchError(this.handleError)
    );
  }

  patch<T>(endpoint: string, data: any): Observable<ApiResponse<T>> {
    return this.http.patch<ApiResponse<T>>(`${this.apiUrl}/${endpoint}`, data).pipe(
      timeout(this.timeoutMs),
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'An error occurred';

    if (error.error instanceof ErrorEvent) {
      // Client-side error
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
      if (error.error?.message) {
        errorMessage = error.error.message;
      }
    }

    console.error(errorMessage);
    return throwError(() => new Error(errorMessage));
  }
}
