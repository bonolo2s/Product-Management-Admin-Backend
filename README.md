# Product Management Admin Backend

## Description
This is a modular **Admin Backend** system designed to manage products (T-shirts), process payments, and retain customer information for email notifications. Only administrators can log in to manage the platform, with a **SuperAdmin** role controlling all admin accounts.

The system is **secure, fast, and cloud-ready**, designed with modular principles to allow future scalability.

---

## Features

### Admin Module
- SuperAdmin can create, update, and delete Admin accounts.
- Role-based access control (SuperAdmin vs Admin).
- Secure login with hashed passwords.
- Audit logging for all admin actions.

### Product Module
- Add, update, delete T-shirt products.
- Track stock quantity and prices.

### Order & Payment Module
- Capture customer orders (no account required).
- Integrate with payment gateway to process payments.
- Track order status: **Created → Processed → Out for Delivery**.

### Customer Communication
- Store customer emails and order history.
- Send transactional emails for order updates.
- Support opt-in/opt-out for promotional emails.

---

## Security
- Passwords securely hashed and salted.
- Role-based access and authorization enforced.
- Audit logs for accountability.
- Payment information handled via secure gateway; no card data stored.
- Webhook signature verification for payment callbacks.

---

## Technology Stack
- **Backend:** ASP.NET Core
- **Database:** PostgreSQL (AWS RDS)
- **Authentication:** JWT tokens
- **Email:** SMTP / SendGrid (transactional & promotional)
- **Cloud-Ready:** Docker support; prepared for AWS deployment
