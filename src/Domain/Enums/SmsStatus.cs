namespace Domain.Enums;

public enum SmsRequestStatus : byte
{
    PendingApproval = 1,
    Approved = 10,
    Rejected = 11,
    Cancelled = 12,
    RejectedForInSufficientBalance = 13
}