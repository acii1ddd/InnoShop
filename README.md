# InnoShop
InnoShop is a simple shop application built as two separate ASP.NET Core services:

- **UserService** – user registration, authentication, roles, email confirmation and password reset.
- **ProductService** – CRUD for products, availability management and basic product queries.

Each service runs independently and exposes its own HTTP API and API documentation UI.

## Services
### UserService
- **Port:** `7878`
- **Base URL:** `http://localhost:7878/api`
- **Docs UI (Scalar):** `http://localhost:7878/scalar`
- **Responsibilities:**
  - User registration and login
  - Email confirmation
  - Password reset ("forgot password")
  - User activation/deactivation, role management

### ProductService
- **Port:** `7979`
- **Base URL:** `http://localhost:7979/api`
- **Docs UI (Scalar):** `http://localhost:7979/scalar`
- **Responsibilities:**
  - Create/update/delete products
  - List and filter products
  - Mark products as available/unavailable
  - Integrate with UserService for user info

## How to run the project
The easiest way to start the whole system is with Docker.

### 1. Start services with Docker Compose
From the project root:

```bash
cd docker
docker compose up -d
```

This will start:
- `users-db` (PostgreSQL)
- `products-db` (PostgreSQL)
- `user-service` (HTTP on port 7878)
- `product-service` (HTTP on port 7979)

### 2. Open the APIs in your browser
- **UserService API UI:** `http://localhost:7878/scalar`
- **ProductService API UI:** `http://localhost:7979/scalar`

All HTTP endpoints are grouped under `/api` inside each service. You can explore and call them directly from the Scalar UI.

## Default users and credentials
The system has two predefined users:

- **Admin:**
  - Email: `alice@com`
  - Password: `123`
- **Default user:**
  - Email: `bob@com`
  - Password: `1234`

Use the UserService authentication endpoints (via `http://localhost:7878/scalar`) to log in with these accounts and obtain a JWT.

## Registration and email confirmation
When you register a new user through the UserService registration endpoint, the application sends an email to the address you provided.

- The email contains an **email confirmation link**.
- Follow this link to confirm your email before using protected endpoints.

## Forgot password feature
UserService also provides a **"forgot password"** flow (users/forgot-password):

- You call the corresponding endpoint with your email.
- The application sends a message to your email address with a link or token to reset your password.

You can discover and test both the registration and forgot-password endpoints through the UserService API UI at `http://localhost:7878/scalar`.
