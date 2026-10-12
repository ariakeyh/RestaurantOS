using Domain.Entities.Orders;

namespace Domain.Entities.Shops;

public class POSDevice : ShopUser
{
    public string IMEI { get; set; }
    public string SerialNumber { get; set; }
    public ICollection<PosOrder> Orders { get; set; }
}