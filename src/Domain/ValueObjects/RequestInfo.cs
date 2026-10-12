using System.Text;

namespace Domain.ValueObjects;

public class RequestInfo(string ipAddress, string agent, DateTime requestTime, int userKey = -1) : ValueObject<RequestInfo>
{
    private const char Separator = ';';

    public RequestInfo()
        : this(string.Empty, string.Empty, DateTime.Now)
    {
    }

    public int Id { get; set; }
    public string IpAddress { get; set; } = ipAddress;
    public string Agent { get; set; } = agent;
    public DateTime RequestTime { get; set; } = requestTime;
    public int UserKey { get; set; } = userKey;

    public static implicit operator string(RequestInfo info)
    {
        var builder = new StringBuilder();
        builder.Append(info.IpAddress);
        builder.Append(Separator);
        builder.Append(info.Agent);
        builder.Append(Separator);
        builder.Append(info.RequestTime);
        builder.Append(Separator);
        builder.Append(info.UserKey);
        return builder.ToString();
    }

    public static implicit operator RequestInfo(string info)
    {
        var data = info.Split(Separator);
        return new RequestInfo(data[0], data[2], DateTime.Parse(data[3]), int.Parse(data[4]));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return IpAddress;
        yield return Agent;
        yield return RequestTime;
    }
}