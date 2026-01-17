# Domum Care Frontend - Angular Application

A comprehensive Angular frontend for the Domum Care care facility management system with role-based access control for 7 distinct user roles.

## Project Setup

This is a standalone Angular 18 application that communicates with the DomumBackend API.

### Prerequisites

- Node.js 18+
- npm or yarn
- Angular CLI 18+

### Installation

```bash
cd frontend
npm install
```

### Development Server

```bash
ng serve
# or
npm start
```

Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

### Build

```bash
npm run build
# or
ng build
```

The build artifacts will be stored in the `dist/` directory.

## Project Structure

```
src/app/
├── auth/                          # Authentication & authorization
│   ├── login/
│   ├── guards/
│   │   ├── auth.guard.ts         # Route protection guard
│   │   └── role.guard.ts         # Role-based access guard
│   └── password-recovery/
│
├── core/                          # Core services & models
│   ├── models/
│   │   └── index.ts              # User, UserRole, ApiResponse types
│   ├── services/
│   │   ├── api.service.ts        # HTTP client wrapper
│   │   ├── auth.service.ts       # Authentication service
│   │   ├── storage.service.ts    # localStorage management
│   │   └── notification.service.ts
│   └── interceptors/
│       └── auth.interceptor.ts   # JWT token injection
│
├── shared/                        # Shared components & utilities
│   ├── components/
│   ├── pipes/
│   └── directives/
│
├── layout/                        # Layout components
│   ├── navbar/
│   └── sidebar/
│
├── modules/                       # Feature modules (lazy-loaded)
│   ├── dashboard/                # Role-specific dashboards
│   ├── young-persons/            # Resident management
│   ├── staff/
│   ├── facilities/
│   ├── health/
│   ├── incidents/
│   ├── documents/
│   ├── compliance/
│   ├── analytics/
│   ├── notifications/
│   └── reports/
│
├── app.config.ts                 # Application configuration
├── app.routes.ts                 # Route definitions
└── app.component.ts
```

## User Roles Supported

1. **Admin** - System administration, user management
2. **Director** - Executive oversight, organizational reports
3. **Regional Project Manager** - Regional facility management
4. **Senior Manager** - Facility operations management
5. **Operation Manager** - Daily operations, activity logging
6. **Key Worker** - Direct care staff, care activities
7. **Social Worker** - Case management (read-only access)

## Environment Configuration

### Development (`src/environments/environment.ts`)

```typescript
export const environment = {
  production: false,
  apiUrl: 'https://localhost:5152/api',
  apiTimeout: 30000,
  logLevel: 'debug',
  tokenKey: 'auth_token',
  refreshTokenKey: 'refresh_token',
  userKey: 'current_user'
};
```

### Production (`src/environments/environment.prod.ts`)

```typescript
export const environment = {
  production: true,
  apiUrl: 'https://api.domumcare.co.uk/api',
  apiTimeout: 30000,
  logLevel: 'error',
  tokenKey: 'auth_token',
  refreshTokenKey: 'refresh_token',
  userKey: 'current_user'
};
```

## Authentication Flow

1. User logs in with email/password on `/auth/login`
2. `AuthService.login()` sends credentials to backend
3. Backend returns JWT token
4. Token stored in localStorage
5. HTTP Interceptor adds token to all subsequent requests
6. On 401 response, user is redirected to login page
7. User navigates to dashboard based on their role

## API Communication

All API calls go through `ApiService` which:

- Wraps HttpClient
- Handles timeouts (30s default)
- Retries failed requests (once)
- Provides error handling
- Adds JWT token via HTTP Interceptor

Example usage:

```typescript
this.apiService.get<ResidentList>('residents/list').subscribe(
  (response) => {
    this.residents = response.data;
  }
);
```

## Role-Based Route Guards

Routes are protected by `AuthGuard` and optionally `RoleGuard`:

```typescript
{
  path: 'admin',
  canActivate: [AuthGuard, RoleGuard],
  data: { roles: [UserRole.ADMIN] },
  children: [ /* ... */ ]
}
```

## Dependencies

- **@angular/core** - Angular framework
- **@angular/material** - Material Design components
- **@angular/cdk** - Angular Component Dev Kit
- **rxjs** - Reactive programming
- **ngx-toastr** - Toast notifications
- **lodash** - Utility library

## Development Workflow

1. **Feature Development**: Create feature components in appropriate module
2. **Services**: Use dependency injection via `inject()`
3. **Routing**: Define routes with role-based guards
4. **State**: Use RxJS Observables and BehaviorSubjects
5. **HTTP**: Use ApiService for all backend calls
6. **Styling**: SCSS with Angular Material theming

## Build & Deployment

### Development Build

```bash
ng build --configuration development
```

### Production Build

```bash
ng build --configuration production
```

### Serve Production Build Locally

```bash
npm install -g http-server
http-server dist/frontend
```

## Docker Deployment

See Dockerfile for containerized deployment.

## Testing

```bash
ng test
```

## Additional Resources

- [Angular Documentation](https://angular.dev)
- [Angular Material](https://material.angular.io)
- [RxJS Documentation](https://rxjs.dev)
- [TypeScript Handbook](https://www.typescriptlang.org/docs/)

## License

Proprietary - Domum Care Ltd

## Support

For issues and questions, contact the development team.
