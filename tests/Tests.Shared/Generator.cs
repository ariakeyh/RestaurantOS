using Bogus;

namespace Tests.Shared;

public static class Generator
{
    public static T 
        Generate<T>() where T : class, new()
    {
        var faker = new Faker<T>().CustomInstantiator(_ => new T());
        var properties = typeof(T).GetProperties();
        var rnd = new Random();
        
        foreach (var property in properties)
        {
            if (!property.CanWrite) continue;
            
            if (property.Name.Contains("Id", StringComparison.OrdinalIgnoreCase)) continue;
            if (property.Name.Contains("IsDeleted", StringComparison.OrdinalIgnoreCase)) continue;

            if (property.PropertyType == typeof(string))
            {
                faker.RuleFor(property.Name, f => f.Lorem.Word());
            }
            
            if (property.Name.Contains("Phone", StringComparison.CurrentCultureIgnoreCase))
            {
                faker.RuleFor(property.Name, f => "0912" + rnd.Next(1000000, 9999999));
            }
            
            else if (property.PropertyType == typeof(int))
            {
                faker.RuleFor(property.Name, f => f.Random.Number(2, 999));
            }
            else if (property.PropertyType == typeof(DateTime))
            {
                faker.RuleFor(property.Name, f => f.Date.Future());
            }
            else if (property.PropertyType == typeof(bool))
            {
                faker.RuleFor(property.Name, f => f.Random.Bool());
            }
        }

        return faker.Generate();
    }
    
    public static byte Percent(this Faker faker) => faker.Random.Byte(1, 100);
    public static int Amount(this Faker faker) => faker.Random.Int(10_000, 1_000_000);
}