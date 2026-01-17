# Frontend Quick Start Guide

## 🚀 Getting Started

### 1. Install Dependencies

```bash
cd C:\Projects\DomumBackend\frontend
npm install
```

### 2. Start Development Server

```bash
npm start
# or
ng serve
```

The application will open at `http://localhost:4200`

### 3. Login

- **URL**: http://localhost:4200/auth/login
- **Credentials**: (Use credentials from your backend test data)

## 📋 Project Structure Overview

```
frontend/
├── src/app/
│   ├── auth/              # Login & authentication
│   ├── core/              # Services & models
│   ├── shared/            # Reusable components
│   ├── layout/            # Navbar, sidebar
│   ├── modules/           # Feature modules
│   │   └── dashboard/     # Role-specific dashboards
│   ├── app.config.ts      # App configuration & providers
│   ├── app.routes.ts      # Route definitions
│   └── app.component.ts   # Root component
│
├── src/environments/      # Environment configs
├── angular.json           # Angular config
├── tsconfig.json          # TypeScript config
├── package.json           # Dependencies
└── dist/                  # Build output
```

## 🔑 Key Features Implemented

### ✅ Authentication
- Login component with form validation
- JWT token management
- HTTP Interceptor for token injection
- Auto-logout on token expiry

### ✅ Role-Based Access Control
- AuthGuard: Protects authenticated-only routes
- RoleGuard: Checks user roles for specific routes
- 7 user roles fully supported:
  - Admin
  - Director
  - Regional Project Manager
  - Senior Manager
  - Operation Manager
  - Key Worker
  - Social Worker

### ✅ Core Services
- **ApiService**: HTTP wrapper with timeout/retry
- **AuthService**: Authentication & user state
- **StorageService**: localStorage wrapper
- **NotificationService**: Toast notifications (ngx-toastr)

### ✅ Dashboard
- Role-aware dashboard component
- Shows current user info
- Displays system status

## 🛣️ Available Routes

| Route | Requires Auth | Requires Role | Purpose |
|-------|---------------|---------------|---------|
| `/auth/login` | No | - | Login page |
| `/dashboard` | Yes | Any | Main dashboard |
| `/admin` | Yes | Admin | Admin panel (to be built) |

## 🔌 API Integration

### Connecting to Backend

Update `src/environments/environment.ts`:

```typescript
export const environment = {
  apiUrl: 'https://localhost:5152/api',  // Backend API URL
  // ... other config
};
```

### Example API Call

```typescript
import { inject } from '@angular/core';
import { ApiService } from '@core/services/api.service';

export class ResidentsService {
  private apiService = inject(ApiService);

  getResidents() {
    return this.apiService.get('young-persons/list');
  }
}
```

## 📱 Path Aliases

Use these aliases in imports instead of relative paths:

```typescript
// Instead of: '../../../core/services/auth.service'
// Use:
import { AuthService } from '@core/services/auth.service';

// Available aliases:
@core/*        → src/app/core/*
@shared/*      → src/app/shared/*
@modules/*     → src/app/modules/*
@auth/*        → src/app/auth/*
@layout/*      → src/app/layout/*
@env/*         → src/environments/*
```

## 🎨 Styling

- **Framework**: SCSS with Angular Material
- **Components**: Material Design components (Card, Form, Button, etc.)
- **Responsive**: Mobile-first responsive design
- **Theme**: Customizable Material theme

## 🧪 Development Tasks

### Build

```bash
npm run build
```

### Format Code

```bash
ng format
```

### Run Tests (when created)

```bash
npm test
```

## 📦 Available Commands

```bash
npm start              # Start dev server
npm run build          # Build for production
npm run build:prod     # Production build
npm run serve:ssr      # Serve with SSR
npm test               # Run unit tests
npm run lint           # Lint code
npm run format         # Format code
```

## 🐛 Troubleshooting

### Port 4200 already in use

```bash
ng serve --port 4201
```

### Clear node_modules and reinstall

```bash
rm -r node_modules package-lock.json
npm install
```

### Build errors

```bash
ng build --verbose
```

## 🚀 Next Steps

1. **Create Components**: Add components for each module (Young Persons, Staff, Health, etc.)
2. **Build Services**: Create domain services for each feature module
3. **Add Forms**: Implement reactive forms for data entry
4. **Connect API**: Integrate all API endpoints
5. **Add Navigation**: Build navbar and sidebar with role-based menus
6. **Implement Features**: Build dashboards and workflows for each role

## 📚 Resources

- Frontend Design Doc: `../ANGULAR_FRONTEND_DESIGN.md`
- Implementation Guide: `../ANGULAR_IMPLEMENTATION_GUIDE.md`
- User Flows: `../ANGULAR_USER_FLOWS.md`
- System Narrative: `../DOMUM_CARE_NARRATIVE.md`

## ⚙️ Environment Setup Complete

✅ Angular 18 project created  
✅ Dependencies installed  
✅ Core services implemented  
✅ Authentication guards set up  
✅ HTTP interceptor configured  
✅ Route guards configured  
✅ Login component created  
✅ Dashboard component created  
✅ Project builds successfully  

Ready to start building features! 🎉
