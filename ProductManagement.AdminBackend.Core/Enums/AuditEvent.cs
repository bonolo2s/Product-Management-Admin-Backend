namespace ProductManagement.AdminBackend.Core.Enums
{
    public enum AuditEvent
    {
        AdminCreated,
        AdminUpdated,
        AdminDeleted,
        ProductCreated,
        ProductUpdated,
        ProductDeleted,
        OrderStatusUpdated,
        OrderCreated,
        LoginSuccess,
        LoginFailed,
        PaymentUpdated,
        OrderStatusChanged
    }
}

