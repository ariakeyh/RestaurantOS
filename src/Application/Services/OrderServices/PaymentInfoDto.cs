using System.Text.Json.Serialization;
using Domain.Entities.Orders;
using Domain.Enums;

namespace Application.Services.OrderServices;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(CashPaymentInfoDto), nameof(PaymentInfoType.Cash))]
[JsonDerivedType(typeof(PGPaymentInfoDto), nameof(PaymentInfoType.PG))]
[JsonDerivedType(typeof(PosPaymentInfoDto), nameof(PaymentInfoType.POS))]
[JsonDerivedType(typeof(TransferPaymentInfoDto), nameof(PaymentInfoType.Transfer))]
public abstract class PaymentInfoDto
{
    public decimal Amount { get; set; }
    public DateTime PaidAt { get; set; }

    public abstract PaymentInfo MapToModel();
  
    public static PaymentInfoDto FromModel(PaymentInfo model)
    {
        return model.Type switch
        {
            PaymentInfoType.Cash => new CashPaymentInfoDto
            {
                Amount = model.Amount,
                PaidAt = model.PaidAt
            },
            PaymentInfoType.PG => new PGPaymentInfoDto
            {
                Amount = model.Amount,
                PaidAt = model.PaidAt,
                ReferenceNo = ((PGPaymentInfo)model).ReferenceNo,
                MaskedCardNo = ((PGPaymentInfo)model).MaskedCardNo,
                AdditionalData = ((PGPaymentInfo)model).AdditionalData
            },
            PaymentInfoType.POS => new PosPaymentInfoDto
            {
                Amount = model.Amount,
                PaidAt = model.PaidAt,
                TerminalNo = ((PosPaymentInfo)model).TerminalNo,
                AcceptorCode = ((PosPaymentInfo)model).AcceptorCode,
                TraceNo = ((PosPaymentInfo)model).TraceNo,
                MaskedCardNo = ((PosPaymentInfo)model).MaskedCardNo
            },
            PaymentInfoType.Transfer => new TransferPaymentInfoDto
            {
                Amount = model.Amount,
                PaidAt = model.PaidAt,
                DestinationCardNo = ((TransferPaymentInfo)model).DestinationCardNo,
                ReferenceNo = ((TransferPaymentInfo)model).ReferenceNo
            },
            _ => throw new ArgumentOutOfRangeException(nameof(model.Type))
        };
    }



}