using Application.Interface;
using Domain.Entities.Agg;
using Domain.Entities.Auth;
using Domain.Entities.Customers;
using Domain.Entities.Menu;
using Domain.Entities.Orders;
using Domain.Entities.Polls;
using Domain.Entities.Reports;
using Domain.Entities.Settings;
using Domain.Entities.Shops;
using Domain.Entities.Sms;
using Infra.Database.Configs.Auth;
using Microsoft.EntityFrameworkCore;

namespace Infra.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<OrderServeInfo> OrderServeInfos { get; set; }
    public DbSet<OnSiteOrderServeInfo> OnSiteOrderServeInfos { get; set; }
    public DbSet<TakeAwayOrderServeInfo> TakeAwayOrderServeInfos { get; set; }
    public DbSet<DeliveryOrderServeInfo> DeliveryOrderServeInfos { get; set; }
    public DbSet<PosOrder> PosOrders { get; set; }
    public DbSet<CashierOrder> CashierOrders { get; set; }
    public DbSet<WebsiteOrder> WebsiteOrders { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }
    public DbSet<OrderItemChangeHistory> OrderItemChangeHistories { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<PaymentInfo> PaymentInfos { get; set; }
    public DbSet<CashPaymentInfo> CashPaymentInfos { get; set; }
    public DbSet<PosPaymentInfo> PosPaymentInfos { get; set; }
    public DbSet<PGPaymentInfo> PGPaymentInfos { get; set; }
    public DbSet<TransferPaymentInfo> TransferPaymentInfos { get; set; }
    public DbSet<Shop> Shops { get; set; }
    public DbSet<ShopCashbackSetting> ShopCashbackSettings { get; set; }
    public DbSet<ShopUser> ShopUsers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerCredit> CustomerCredits { get; set; }
    public DbSet<CustomerCreditSpendHistory> CustomerCreditSpendHistories { get; set; }
    public DbSet<OrderCustomerCredit> OrderCustomerCredits { get; set; }
    public DbSet<ManualCustomerCredit> ManualCustomerCredits { get; set; }
    public DbSet<ShopCategory> ShopCategories { get; set; }
    public DbSet<ShopCategoryAssignment> ShopCategoryAssignments { get; set; }
    public DbSet<POSDevice> POSDevices { get; set; }
    public DbSet<Poll> Polls { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<PollResponse> PollResponses { get; set; }
    public DbSet<PollToken> PollTokens { get; set; }
    public DbSet<PollQuestion> PollQuestions { get; set; }
    public DbSet<PollQuestionAnswer> PollQuestionAnswers { get; set; }
    public DbSet<PollFoodRating> PollFoodRatings { get; set; }
    public DbSet<CreditHistory> CreditHistories { get; set; }
    public DbSet<InternalPGTransaction> InternalPgTransactions { get; set; }
    public DbSet<SmsRequest> SmsRequests { get; set; }
    public DbSet<SmsRequestReceptor> SmsRequestReceptors { get; set; }
    public DbSet<SmsRequestReceptorStatusHistory> SmsRequestReceptorStatusHistories { get; set; }
    public DbSet<SmsPriceSetting> SmsPriceSettings { get; set; }
    public DbSet<ReportCategory> ReportCategories { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<ShopUserReport> ShopUserReports { get; set; }
    public DbSet<MenuItemPriceHistory> MenuItemPriceHistories { get; set; }

    public new async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await base.SaveChangesAsync(cancellationToken);
    }           
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                     .Where(t => typeof(AggregateRoot).IsAssignableFrom(t.ClrType) && !t.ClrType.IsAbstract))
        {
            var entity = modelBuilder.Entity(entityType.ClrType);

            entity.HasOne(typeof(User), nameof(AggregateRoot.Creator))
                .WithMany()
                .HasForeignKey(nameof(AggregateRoot.CreatorId))
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(typeof(User), nameof(AggregateRoot.Editor))
                .WithMany()
                .HasForeignKey(nameof(AggregateRoot.EditorId))
                .OnDelete(DeleteBehavior.Restrict);
        }
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfig).Assembly);
    }
}