namespace Application.Services.OrderServices;

public class PosSourceInfoDto : SourceInfoDto
{
    public Guid PosId { get; set; }
    public string PosName { get; set; }
}