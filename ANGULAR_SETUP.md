# Angular Frontend - Setup & Development Plan

## Project Overview

**Backend Status**: ✅ Complete (Phases 1-3F)
- Phase 1: Core Compliance & Audit
- Phase 2: Health & Wellness  
- Phase 3A: Reporting & Analytics
- Phase 3B: Notifications & Alerting
- Phase 3C: Document Management
- Phase 3D: Advanced Analytics & Insights
- Phase 3E: Staff Management
- Phase 3F: Compliance & Auditing

**API Endpoints**: 155+ endpoints across 17 controllers

---

## Frontend Architecture

### Technology Stack
- **Angular 18+** - Latest framework
- **TypeScript** - Strong typing
- **RxJS** - Reactive programming
- **Angular Material** - UI components
- **HTTP Client** - API communication
- **JWT** - Authentication
- **NGRX** - State management (optional, but recommended for this scale)

### Project Structure
```
frontend/
├── src/
│   ├── app/
│   │   ├── auth/                      # Authentication module
│   │   │   ├── login/
│   │   │   ├── guards/
│   │   │   └── services/
│   │   ├── core/                      # Core services & interceptors
│   │   │   ├── services/
│   │   │   │   ├── api.service.ts
│   │   │   │   ├── auth.service.ts
│   │   │   │   └── error.interceptor.ts
│   │   │   └── models/
│   │   ├── shared/                    # Shared components & pipes
│   │   │   ├── components/
│   │   │   ├── pipes/
│   │   │   └── directives/
│   │   ├── layout/                    # Main layout
│   │   │   ├── navbar/
│   │   │   ├── sidebar/
│   │   │   └── footer/
│   │   ├── modules/                   # Feature modules
│   │   │   ├── young-persons/
│   │   │   ├── staff/
│   │   │   ├── facilities/
│   │   │   ├── health/
│   │   │   ├── incidents/
│   │   │   ├── documents/
│   │   │   ├── compliance/
│   │   │   ├── analytics/
│   │   │   ├── notifications/
│   │   │   └── reports/
│   │   └── app.component.ts
│   ├── assets/
│   ├── environments/
│   │   ├── environment.ts
│   │   └── environment.prod.ts
│   └── styles/
├── angular.json
├── tsconfig.json
├── package.json
└── .env (local development)
```

---

## Phase 1: Foundation (Next Sprint)

### 1.1 Angular Project Setup
```bash
ng new frontend
cd frontend
ng add @angular/material
npm install rxjs lodash ngx-toastr
```

### 1.2 Core Services
- **ApiService**: HTTP client wrapper for all API calls
- **AuthService**: JWT token management, login/logout
- **ErrorInterceptor**: Global error handling
- **AuthGuard**: Route protection

### 1.3 Authentication Module
- Login page (email/password)
- Password recovery
- Token persistence (localStorage)
- Auto-logout on token expiry

### 1.4 Layout Components
- Navbar with user menu
- Sidebar navigation
- Footer
- Dashboard landing page

---

## Phase 2: Feature Modules (Sprint 2-3)

### Module Priority Order:
1. **Young Persons** - Core entity management (List, Create, Edit, Detail view)
2. **Staff Management** - Staff assignments, certifications
3. **Health Module** - Medical records, medications, health assessments
4. **Incidents** - Incident reporting and tracking
5. **Facilities** - Facility management and rooms
6. **Documents** - Document upload, versioning, access control
7. **Compliance** - Audit tracking, checklists, non-conformities
8. **Analytics** - Dashboards, trends, predictive alerts
9. **Notifications** - Alert preferences, notification center
10. **Reports** - Report generation, scheduling, distribution

### Common Features per Module:
- ✅ List view (paginated, searchable, sortable)
- ✅ Create form with validation
- ✅ Edit form
- ✅ Detail/view page
- ✅ Delete confirmation
- ✅ Export to CSV/PDF (where applicable)
- ✅ Real-time status updates
- ✅ Audit trail visibility

---

## Phase 3: Advanced Features (Sprint 4+)

### State Management (NGRX)
- Centralized store for app state
- Actions for API calls
- Selectors for component consumption
- Effects for side effects

### Performance Optimization
- Lazy loading modules
- OnPush change detection
- Virtual scrolling for large lists
- Image optimization

### Analytics
- User behavior tracking
- Performance monitoring
- Error tracking (Sentry)

### PWA Features
- Offline support
- Service workers
- Installable app

---

## Development Workflow

### Daily Workflow:
```bash
# Terminal 1: Backend
cd DomumBackend
dotnet run --project DomumBackend.Api

# Terminal 2: Frontend  
cd frontend
ng serve

# Terminal 3: IDE
code .
```

### API Communication Pattern:
```typescript
// All API calls go through ApiService
constructor(private api: ApiService) {}

this.api.get<YoungPerson[]>('/api/youngperson')
  .subscribe(data => this.persons = data);
```

### Environment Configuration:
```typescript
// environment.ts (development)
export const environment = {
  production: false,
  apiUrl: 'https://localhost:5152/api'
};

// environment.prod.ts (production)
export const environment = {
  production: true,
  apiUrl: 'https://your-api-domain.com/api'
};
```

---

## Key Decisions & Best Practices

### 1. **HTTP Interceptor Pattern**
- All HTTP calls go through HttpClient
- Automatic JWT token injection
- Global error handling
- Loading spinner management

### 2. **Route Structure**
```
/auth/login
/dashboard
/young-persons
/young-persons/:id
/staff
/health
/incidents
/documents
/compliance
/analytics
/reports
```

### 3. **Component Hierarchy**
- Smart Components (containers) - Handle data & logic
- Dumb Components (presentational) - Receive @Input, emit @Output
- Shared Components - Reusable across modules

### 4. **Type Safety**
- Strict TypeScript mode enabled
- Strong typing for all API responses
- Shared interface definitions

### 5. **Error Handling**
- Global error interceptor
- Toast notifications for user feedback
- Detailed logging (console in dev, external in prod)

---

## Security Considerations

### ✅ Implemented
- JWT authentication
- HTTP-only cookies for token storage (when applicable)
- CORS properly configured
- Input validation
- Output sanitization
- Role-based access control (RBAC)

### 🔐 Headers to Add
- X-CSRF-Token
- Content-Security-Policy
- X-Content-Type-Options: nosniff
- X-Frame-Options: DENY

---

## Deployment Strategy

### Development → Production
1. **Dev**: localhost:4200 ↔ localhost:5152
2. **Staging**: staging.domain.com ↔ api-staging.domain.com
3. **Production**: app.domain.com ↔ api.domain.com

### Build & Deploy
```bash
# Build for production
ng build --configuration production

# Deploy dist/ folder to CDN/server
```

---

## Testing Strategy

### Unit Tests
```bash
ng test
```
- Components (fixtures, mocking)
- Services (HTTP mocking)
- Pipes & Directives

### E2E Tests
```bash
ng e2e
```
- Critical user flows
- Form submissions
- Navigation

### Manual Testing Checklist
- [ ] Login/logout
- [ ] CRUD operations per module
- [ ] Search/filter functionality
- [ ] Error scenarios
- [ ] Permission-based access

---

## Dependencies

### Core
- `@angular/core`
- `@angular/common`
- `@angular/router`
- `@angular/forms`
- `@angular/cdk`

### Material & UI
- `@angular/material`
- `ngx-toastr`
- `ngx-spinner`

### Utilities
- `rxjs`
- `lodash`
- `moment` or `date-fns`
- `uuid`

### Optional (for scalability)
- `@ngrx/store` (state management)
- `@ngrx/effects`
- `@ngrx/entity`
- `@angular/cdk/virtual-scroll`

---

## Next Steps

### Immediate (Today):
1. ✅ Create Angular project structure
2. ✅ Set up core services (ApiService, AuthService)
3. ✅ Create authentication module with login page
4. ✅ Set up layout components
5. ✅ Test connectivity to backend

### Short Term (This Week):
1. Build Young Persons module
2. Build Staff module
3. Set up Material theme
4. Add routing & guards

### Medium Term (Next 2 Weeks):
1. Complete remaining feature modules
2. Add comprehensive error handling
3. Implement state management
4. Add analytics integration

---

## Questions Before Starting?

1. Should we use NGRX for state management from the start, or add it later?
2. Do you want Material Design or custom design system?
3. Any specific charts library for analytics (Chart.js, ECharts, Plotly)?
4. Multi-language support needed?
5. Mobile responsiveness requirements?

---

## Success Criteria

✅ **MVP (Minimum Viable Product)**
- User login/authentication
- Young Persons CRUD
- Staff management
- Basic dashboard

✅ **Full Feature**
- All 10 modules functional
- Real-time data updates
- Export capabilities
- Analytics & reporting

✅ **Production Ready**
- 80%+ test coverage
- Performance optimized
- Security hardened
- Deployment automated
