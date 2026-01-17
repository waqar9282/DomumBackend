# 🚀 FULL STACK SETUP GUIDE - BACKEND + FRONTEND

**Date**: January 17, 2026 | **Fix Applied**: API Connection Issue Resolved

---

## 🔴 ISSUE ENCOUNTERED

**Error**: `POST https://localhost:5152/api/auth/login net::ERR_CONNECTION_REFUSED`

**Root Cause**: Frontend was configured to connect to wrong backend port:
- Frontend tried: `https://localhost:5152` ❌
- Backend actually runs on: `https://localhost:7219` ✅

**Solution**: Updated `environment.ts` to use correct port.

---

## 📋 BACKEND & FRONTEND CONFIGURATION

### Backend Port Configuration
From `launchSettings.json`:
```json
"https": {
  "applicationUrl": "https://localhost:7219;http://localhost:5152"
}
```

- **HTTPS**: `https://localhost:7219` ← Use this
- **HTTP**: `http://localhost:5152` ← Also available

### Frontend Configuration
Updated in `src/environments/environment.ts`:
```typescript
apiUrl: 'https://localhost:7219/api'
```

---

## 🎯 STEP-BY-STEP TO RUN FULL STACK

### Step 1: Start the Backend API

**Terminal 1** - Start .NET Backend:
```bash
cd C:\Projects\DomumBackend
dotnet run
```

**OR** using Visual Studio:
1. Open `DomumBackend.sln` in Visual Studio
2. Press `F5` (Debug) or `Ctrl+F5` (Run without debug)
3. Wait for "Now listening on: https://localhost:7219"

**Expected output:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:7219
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to stop.
```

✅ Backend is ready when you see Swagger at: `https://localhost:7219/swagger`

---

### Step 2: Start the Angular Frontend

**Terminal 2** - Start Angular App:
```bash
cd C:\Projects\DomumBackend\frontend
npm start
```

**Expected output:**
```
✔ Compiled successfully.

➜  Local:   http://localhost:4200/
```

✅ Frontend opens at: `http://localhost:4200`

---

## 🔒 SSL/HTTPS Certificate ISSUE (if you get one)

If you see: `ERR_SSL_PROTOCOL_ERROR` or untrusted certificate warning

### Solution: Accept the Certificate

The backend uses a self-signed certificate for HTTPS development. 

**Option A - Accept Certificate in Browser:**
1. Navigate to `https://localhost:7219/swagger`
2. Click "Advanced" 
3. Click "Proceed to localhost" (ignore warning)
4. Browser accepts certificate for session

**Option B - Bypass SSL Verification (Development Only):**

Edit `src/core/services/api.service.ts`:
```typescript
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

// Only for development - NEVER use in production!
import { HttpClientModule } from '@angular/common/http';
```

Or temporarily use HTTP instead:
```typescript
// In environment.ts (development only)
apiUrl: 'http://localhost:5152/api'
```

---

## 🧪 TESTING THE CONNECTION

### Test 1: Backend Health Check
1. Open browser to: `https://localhost:7219/swagger`
2. Look for Swagger UI
3. ✅ Backend is running

### Test 2: Frontend Dashboard
1. Open browser to: `http://localhost:4200`
2. Should see login page (or dashboard if already logged in)
3. ✅ Frontend is running

### Test 3: Login Flow
1. Go to login page: `http://localhost:4200/auth/login`
2. Enter credentials (test user from backend)
3. Should connect successfully now! ✅

---

## 📝 TEST CREDENTIALS

You need test users in your database. Contact your backend admin for:
- **Username/Email**: admin@example.com
- **Password**: (from backend setup)

Or create test data using:
```bash
cd C:\Projects\DomumBackend
.\create-demo-data.ps1
```

---

## 🛠️ TROUBLESHOOTING

### Issue: "Connection Refused"
**Check:**
1. Is backend running? `https://localhost:7219/swagger`
2. Are you using correct port? (7219 for HTTPS, 5152 for HTTP)
3. Check firewall isn't blocking port 7219

**Fix:**
```bash
# Kill any processes on port 7219
netstat -ano | findstr :7219
taskkill /PID [PID] /F
```

### Issue: SSL Certificate Error
**Check:**
1. Is backend running on HTTPS?
2. Browser cache - hard refresh (Ctrl+Shift+R)

**Fix:**
- Accept the certificate warning
- Or switch to HTTP (port 5152) in environment.ts

### Issue: CORS Error
**If you see CORS error:**
1. Check backend CORS configuration
2. Verify frontend URL is whitelisted
3. Restart backend to reload config

---

## 🔄 QUICK REFERENCE PORTS

| Service | Port | Protocol | URL |
|---------|------|----------|-----|
| **Backend API** | 7219 | HTTPS | https://localhost:7219 |
| **Backend API** | 5152 | HTTP | http://localhost:5152 |
| **Swagger Docs** | 7219 | HTTPS | https://localhost:7219/swagger |
| **Frontend App** | 4200 | HTTP | http://localhost:4200 |
| **Database** | 1433 | - | SQL Server (if local) |

---

## 📦 UPDATED FILES

✅ `src/environments/environment.ts` - Changed from `5152` to `7219`

**Before:**
```typescript
apiUrl: 'https://localhost:5152/api'  // ❌ Wrong port
```

**After:**
```typescript
apiUrl: 'https://localhost:7219/api'  // ✅ Correct port
```

---

## 🚀 QUICK START COMMANDS

**Run Everything (from project root):**

```bash
# Terminal 1 - Backend
cd C:\Projects\DomumBackend
dotnet run

# Terminal 2 - Frontend
cd C:\Projects\DomumBackend\frontend
npm start
```

Wait for both to finish starting, then:
- Backend: https://localhost:7219/swagger
- Frontend: http://localhost:4200

---

## ✅ VERIFICATION CHECKLIST

- [ ] Backend running on `https://localhost:7219`
- [ ] Backend Swagger accessible
- [ ] Frontend running on `http://localhost:4200`
- [ ] Login page displays
- [ ] Can attempt login
- [ ] API requests go to correct port (7219)
- [ ] No SSL certificate errors (or accepted)
- [ ] No CORS errors

---

## 🎉 SUCCESS!

Once both are running:

1. ✅ Go to `http://localhost:4200`
2. ✅ You should see the **branded Domum Care login page**
3. ✅ Enter credentials
4. ✅ API calls to `https://localhost:7219/api/auth/login`
5. ✅ Should work! 🎊

---

## 📚 DOCUMENTATION REFERENCES

- Backend API Documentation: `https://localhost:7219/swagger`
- Frontend Documentation: `C:\Projects\DomumBackend\frontend\FRONTEND_README.md`
- Backend Setup: `C:\Projects\DomumBackend\README.md`

---

**Status**: ✅ FIXED AND READY  
**Next**: Start backend, start frontend, and test login!
