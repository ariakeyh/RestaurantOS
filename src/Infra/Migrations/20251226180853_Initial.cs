using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AdminPasswordHashed = table.Column<string>(type: "varchar(2056)", maxLength: 2056, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Provinces_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Provinces_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Questions_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ReportCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportCategories_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportCategories_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ShopCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopCategories_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShopCategories_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactInfo = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImageName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumberOfTables = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    Credit = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shops_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shops_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SmsPriceSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(type: "int", nullable: false),
                    BasePrice = table.Column<int>(type: "int", nullable: false),
                    SellPrice = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsPriceSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmsPriceSettings_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SmsPriceSettings_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Latitude = table.Column<double>(type: "double", nullable: false),
                    Longitude = table.Column<double>(type: "double", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cities_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cities_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ReportCategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReportType = table.Column<int>(type: "int", nullable: false),
                    ApiUrl = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_ReportCategories_ReportCategoryId",
                        column: x => x.ReportCategoryId,
                        principalTable: "ReportCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reports_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImageName = table.Column<string>(type: "varchar(2056)", maxLength: 2056, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShopId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Categories_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CreditHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShopId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditHistories_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreditHistories_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CreditHistories_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Polls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShopId = table.Column<int>(type: "int", nullable: false),
                    ShouldRateFoods = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    NotifyAfterDays = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Polls_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Polls_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Polls_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ShopCashbackSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShopId = table.Column<int>(type: "int", nullable: false),
                    CashbackPercentage = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    MaxCashbackAmount = table.Column<int>(type: "int", nullable: false),
                    ExpirationInDays = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopCashbackSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopCashbackSettings_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopCashbackSettings_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShopCashbackSettings_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ShopCategoryAssignments",
                columns: table => new
                {
                    ShopId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopCategoryAssignments", x => new { x.ShopId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ShopCategoryAssignments_ShopCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ShopCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopCategoryAssignments_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopCategoryAssignments_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShopCategoryAssignments_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ShopUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopUsers_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopUsers_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShopUsers_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShopUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(2056)", maxLength: 2056, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ingredients = table.Column<string>(type: "varchar(2056)", maxLength: 2056, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImageName = table.Column<string>(type: "varchar(2056)", maxLength: 2056, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    IsUnAvailable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItems_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuItems_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuItems_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InternalPgTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PGProvider = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    StatusComment = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Payload = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Authority = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShopId = table.Column<int>(type: "int", nullable: false),
                    CreditHistoryId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalPgTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternalPgTransactions_CreditHistories_CreditHistoryId",
                        column: x => x.CreditHistoryId,
                        principalTable: "CreditHistories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InternalPgTransactions_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InternalPgTransactions_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternalPgTransactions_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SmsRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShopId = table.Column<int>(type: "int", nullable: false),
                    CreditHistoryId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    ReceptorsCount = table.Column<int>(type: "int", nullable: false),
                    TotalDelivered = table.Column<int>(type: "int", nullable: false),
                    TotalFailed = table.Column<int>(type: "int", nullable: false),
                    TotalSent = table.Column<int>(type: "int", nullable: false),
                    BasePrice = table.Column<int>(type: "int", nullable: false),
                    SellPrice = table.Column<int>(type: "int", nullable: false),
                    FinalProviderPrice = table.Column<int>(type: "int", nullable: false),
                    Payload = table.Column<string>(type: "varchar(2064)", maxLength: 2064, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmsRequests_CreditHistories_CreditHistoryId",
                        column: x => x.CreditHistoryId,
                        principalTable: "CreditHistories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SmsRequests_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SmsRequests_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SmsRequests_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PollQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PollId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PollQuestions_Polls_PollId",
                        column: x => x.PollId,
                        principalTable: "Polls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PollQuestions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PollQuestions_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PollQuestions_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Badge = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_ShopUsers_Id",
                        column: x => x.Id,
                        principalTable: "ShopUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "POSDevices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IMEI = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SerialNumber = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POSDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POSDevices_ShopUsers_Id",
                        column: x => x.Id,
                        principalTable: "ShopUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ShopUserReports",
                columns: table => new
                {
                    ShopUserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ReportId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopUserReports", x => new { x.ShopUserId, x.ReportId });
                    table.ForeignKey(
                        name: "FK_ShopUserReports_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopUserReports_ShopUsers_ShopUserId",
                        column: x => x.ShopUserId,
                        principalTable: "ShopUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopUserReports_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShopUserReports_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SmsRequestReceptors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SmsRequestId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusComment = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SendDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ReceptorNumber = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Message = table.Column<string>(type: "varchar(2064)", maxLength: 2064, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BasePrice = table.Column<int>(type: "int", nullable: false),
                    SellPrice = table.Column<int>(type: "int", nullable: false),
                    FinalProviderPrice = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsRequestReceptors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmsRequestReceptors_SmsRequests_SmsRequestId",
                        column: x => x.SmsRequestId,
                        principalTable: "SmsRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SmsRequestReceptors_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SmsRequestReceptors_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ShopId = table.Column<int>(type: "int", nullable: false),
                    Source = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    ExpectedPreparation = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ActualPreparation = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    OrderVersion = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    OrderNumber = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalPrice = table.Column<int>(type: "int", nullable: false),
                    TotalPriceWithDiscount = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SmsRequestReceptorStatusHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SmsRequestReceptorId = table.Column<int>(type: "int", nullable: false),
                    PrevStatus = table.Column<int>(type: "int", nullable: false),
                    PrevStatusComment = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsRequestReceptorStatusHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmsRequestReceptorStatusHistories_SmsRequestReceptors_SmsReq~",
                        column: x => x.SmsRequestReceptorId,
                        principalTable: "SmsRequestReceptors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SmsRequestReceptorStatusHistories_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SmsRequestReceptorStatusHistories_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CashierOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CashierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashierOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashierOrders_Orders_Id",
                        column: x => x.Id,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CashierOrders_Users_CashierId",
                        column: x => x.CashierId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CustomerCredits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    SpentAmount = table.Column<int>(type: "int", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "varchar(2056)", maxLength: 2056, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    CashbackSettingId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCredits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerCredits_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerCredits_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerCredits_ShopCashbackSettings_CashbackSettingId",
                        column: x => x.CashbackSettingId,
                        principalTable: "ShopCashbackSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerCredits_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerCredits_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrderItemChangeHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    PriceWithDiscount = table.Column<int>(type: "int", nullable: false),
                    OrderVersion = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemChangeHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItemChangeHistories_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItemChangeHistories_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItemChangeHistories_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItemChangeHistories_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    PriceWithDiscount = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrderServeInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    Address = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TableNumber = table.Column<byte>(type: "tinyint unsigned", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderServeInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderServeInfos_Orders_Id",
                        column: x => x.Id,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderServeInfos_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderServeInfos_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrderStatusHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatusHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderStatusHistories_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderStatusHistories_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderStatusHistories_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PaymentInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Type = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentInfos_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentInfos_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentInfos_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PollResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    StarRate = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    Note = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PollResponses_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PollResponses_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PollResponses_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PollTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Token = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PollTokens_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PollTokens_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PollTokens_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PosOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    POSDeviceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PosOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PosOrders_Orders_Id",
                        column: x => x.Id,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PosOrders_POSDevices_POSDeviceId",
                        column: x => x.POSDeviceId,
                        principalTable: "POSDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WebsiteOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebsiteOrders_Orders_Id",
                        column: x => x.Id,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CustomerCreditSpendHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerCreditId = table.Column<int>(type: "int", nullable: false),
                    SpendAmount = table.Column<int>(type: "int", nullable: false),
                    BalanceAmount = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCreditSpendHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerCreditSpendHistories_CustomerCredits_CustomerCreditId",
                        column: x => x.CustomerCreditId,
                        principalTable: "CustomerCredits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerCreditSpendHistories_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerCreditSpendHistories_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerCreditSpendHistories_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CashPaymentInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashPaymentInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashPaymentInfos_PaymentInfos_Id",
                        column: x => x.Id,
                        principalTable: "PaymentInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PGPaymentInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ReferenceNo = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaskedCardNo = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AdditionalData = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PGPaymentInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PGPaymentInfos_PaymentInfos_Id",
                        column: x => x.Id,
                        principalTable: "PaymentInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PosPaymentInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TerminalNo = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AcceptorCode = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TraceNo = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaskedCardNo = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PosPaymentInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PosPaymentInfos_PaymentInfos_Id",
                        column: x => x.Id,
                        principalTable: "PaymentInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TransferPaymentInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DestinationCardNo = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReferenceNo = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Reason = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferPaymentInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferPaymentInfos_PaymentInfos_Id",
                        column: x => x.Id,
                        principalTable: "PaymentInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PollFoodRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    ResponseId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<byte>(type: "tinyint unsigned", maxLength: 512, nullable: false),
                    Note = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollFoodRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PollFoodRatings_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PollFoodRatings_PollResponses_ResponseId",
                        column: x => x.ResponseId,
                        principalTable: "PollResponses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PollFoodRatings_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PollFoodRatings_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PollQuestionAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    ResponseId = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollQuestionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PollQuestionAnswers_PollQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "PollQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PollQuestionAnswers_PollResponses_ResponseId",
                        column: x => x.ResponseId,
                        principalTable: "PollResponses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PollQuestionAnswers_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PollQuestionAnswers_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CashierOrders_CashierId",
                table: "CashierOrders",
                column: "CashierId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatorId",
                table: "Categories",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_EditorId",
                table: "Categories",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ShopId",
                table: "Categories",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CreatorId",
                table: "Cities",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_EditorId",
                table: "Cities",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ProvinceId",
                table: "Cities",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditHistories_CreatorId",
                table: "CreditHistories",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditHistories_EditorId",
                table: "CreditHistories",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditHistories_ShopId",
                table: "CreditHistories",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCredits_CashbackSettingId",
                table: "CustomerCredits",
                column: "CashbackSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCredits_CreatorId",
                table: "CustomerCredits",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCredits_CustomerId",
                table: "CustomerCredits",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCredits_EditorId",
                table: "CustomerCredits",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCredits_OrderId",
                table: "CustomerCredits",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCreditSpendHistories_CreatorId",
                table: "CustomerCreditSpendHistories",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCreditSpendHistories_CustomerCreditId",
                table: "CustomerCreditSpendHistories",
                column: "CustomerCreditId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCreditSpendHistories_EditorId",
                table: "CustomerCreditSpendHistories",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCreditSpendHistories_OrderId",
                table: "CustomerCreditSpendHistories",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalPgTransactions_CreatorId",
                table: "InternalPgTransactions",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalPgTransactions_CreditHistoryId",
                table: "InternalPgTransactions",
                column: "CreditHistoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InternalPgTransactions_EditorId",
                table: "InternalPgTransactions",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalPgTransactions_PGProvider_Authority",
                table: "InternalPgTransactions",
                columns: new[] { "PGProvider", "Authority" });

            migrationBuilder.CreateIndex(
                name: "IX_InternalPgTransactions_ShopId",
                table: "InternalPgTransactions",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_CategoryId",
                table: "MenuItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_CreatorId",
                table: "MenuItems",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_EditorId",
                table: "MenuItems",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemChangeHistories_CreatorId",
                table: "OrderItemChangeHistories",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemChangeHistories_EditorId",
                table: "OrderItemChangeHistories",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemChangeHistories_MenuItemId",
                table: "OrderItemChangeHistories",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemChangeHistories_OrderId",
                table: "OrderItemChangeHistories",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_CreatorId",
                table: "OrderItems",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_EditorId",
                table: "OrderItems",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_MenuItemId",
                table: "OrderItems",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatorId",
                table: "Orders",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EditorId",
                table: "Orders",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderNumber",
                table: "Orders",
                column: "OrderNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShopId",
                table: "Orders",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderServeInfos_CreatorId",
                table: "OrderServeInfos",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderServeInfos_EditorId",
                table: "OrderServeInfos",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatusHistories_CreatorId",
                table: "OrderStatusHistories",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatusHistories_EditorId",
                table: "OrderStatusHistories",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatusHistories_OrderId",
                table: "OrderStatusHistories",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentInfos_CreatorId",
                table: "PaymentInfos",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentInfos_EditorId",
                table: "PaymentInfos",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentInfos_OrderId",
                table: "PaymentInfos",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PollFoodRatings_CreatorId",
                table: "PollFoodRatings",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_PollFoodRatings_EditorId",
                table: "PollFoodRatings",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_PollFoodRatings_MenuItemId",
                table: "PollFoodRatings",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PollFoodRatings_ResponseId",
                table: "PollFoodRatings",
                column: "ResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_PollQuestionAnswers_CreatorId",
                table: "PollQuestionAnswers",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_PollQuestionAnswers_EditorId",
                table: "PollQuestionAnswers",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_PollQuestionAnswers_QuestionId",
                table: "PollQuestionAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_PollQuestionAnswers_ResponseId",
                table: "PollQuestionAnswers",
                column: "ResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_PollQuestions_CreatorId",
                table: "PollQuestions",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_PollQuestions_EditorId",
                table: "PollQuestions",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_PollQuestions_PollId",
                table: "PollQuestions",
                column: "PollId");

            migrationBuilder.CreateIndex(
                name: "IX_PollQuestions_QuestionId",
                table: "PollQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_PollResponses_CreatorId",
                table: "PollResponses",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_PollResponses_EditorId",
                table: "PollResponses",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_PollResponses_OrderId",
                table: "PollResponses",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Polls_CreatorId",
                table: "Polls",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Polls_EditorId",
                table: "Polls",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_Polls_ShopId",
                table: "Polls",
                column: "ShopId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PollTokens_CreatorId",
                table: "PollTokens",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_PollTokens_EditorId",
                table: "PollTokens",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_PollTokens_OrderId",
                table: "PollTokens",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PollTokens_Token",
                table: "PollTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_POSDevices_IMEI",
                table: "POSDevices",
                column: "IMEI",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_POSDevices_SerialNumber",
                table: "POSDevices",
                column: "SerialNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PosOrders_POSDeviceId",
                table: "PosOrders",
                column: "POSDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_CreatorId",
                table: "Provinces",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_EditorId",
                table: "Provinces",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CreatorId",
                table: "Questions",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_EditorId",
                table: "Questions",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCategories_CreatorId",
                table: "ReportCategories",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCategories_EditorId",
                table: "ReportCategories",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_CreatorId",
                table: "Reports",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_EditorId",
                table: "Reports",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReportCategoryId",
                table: "Reports",
                column: "ReportCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopCashbackSettings_CreatorId",
                table: "ShopCashbackSettings",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopCashbackSettings_EditorId",
                table: "ShopCashbackSettings",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopCashbackSettings_ShopId",
                table: "ShopCashbackSettings",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopCategories_CreatorId",
                table: "ShopCategories",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopCategories_EditorId",
                table: "ShopCategories",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopCategoryAssignments_CategoryId",
                table: "ShopCategoryAssignments",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopCategoryAssignments_CreatorId",
                table: "ShopCategoryAssignments",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopCategoryAssignments_EditorId",
                table: "ShopCategoryAssignments",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_CreatorId",
                table: "Shops",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_EditorId",
                table: "Shops",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopUserReports_CreatorId",
                table: "ShopUserReports",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopUserReports_EditorId",
                table: "ShopUserReports",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopUserReports_ReportId",
                table: "ShopUserReports",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopUsers_CreatorId",
                table: "ShopUsers",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopUsers_EditorId",
                table: "ShopUsers",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopUsers_ShopId",
                table: "ShopUsers",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopUsers_UserId",
                table: "ShopUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SmsPriceSettings_CreatorId",
                table: "SmsPriceSettings",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_SmsPriceSettings_EditorId",
                table: "SmsPriceSettings",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_SmsRequestReceptors_CreatorId",
                table: "SmsRequestReceptors",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_SmsRequestReceptors_EditorId",
                table: "SmsRequestReceptors",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_SmsRequestReceptors_SmsRequestId",
                table: "SmsRequestReceptors",
                column: "SmsRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_SmsRequestReceptorStatusHistories_CreatorId",
                table: "SmsRequestReceptorStatusHistories",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_SmsRequestReceptorStatusHistories_EditorId",
                table: "SmsRequestReceptorStatusHistories",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_SmsRequestReceptorStatusHistories_SmsRequestReceptorId",
                table: "SmsRequestReceptorStatusHistories",
                column: "SmsRequestReceptorId");

            migrationBuilder.CreateIndex(
                name: "IX_SmsRequests_CreatorId",
                table: "SmsRequests",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_SmsRequests_CreditHistoryId",
                table: "SmsRequests",
                column: "CreditHistoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SmsRequests_EditorId",
                table: "SmsRequests",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_SmsRequests_ShopId",
                table: "SmsRequests",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatorId",
                table: "Users",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EditorId",
                table: "Users",
                column: "EditorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashierOrders");

            migrationBuilder.DropTable(
                name: "CashPaymentInfos");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "CustomerCreditSpendHistories");

            migrationBuilder.DropTable(
                name: "InternalPgTransactions");

            migrationBuilder.DropTable(
                name: "OrderItemChangeHistories");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "OrderServeInfos");

            migrationBuilder.DropTable(
                name: "OrderStatusHistories");

            migrationBuilder.DropTable(
                name: "PGPaymentInfos");

            migrationBuilder.DropTable(
                name: "PollFoodRatings");

            migrationBuilder.DropTable(
                name: "PollQuestionAnswers");

            migrationBuilder.DropTable(
                name: "PollTokens");

            migrationBuilder.DropTable(
                name: "PosOrders");

            migrationBuilder.DropTable(
                name: "PosPaymentInfos");

            migrationBuilder.DropTable(
                name: "ShopCategoryAssignments");

            migrationBuilder.DropTable(
                name: "ShopUserReports");

            migrationBuilder.DropTable(
                name: "SmsPriceSettings");

            migrationBuilder.DropTable(
                name: "SmsRequestReceptorStatusHistories");

            migrationBuilder.DropTable(
                name: "TransferPaymentInfos");

            migrationBuilder.DropTable(
                name: "WebsiteOrders");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "CustomerCredits");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "PollQuestions");

            migrationBuilder.DropTable(
                name: "PollResponses");

            migrationBuilder.DropTable(
                name: "POSDevices");

            migrationBuilder.DropTable(
                name: "ShopCategories");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "SmsRequestReceptors");

            migrationBuilder.DropTable(
                name: "PaymentInfos");

            migrationBuilder.DropTable(
                name: "ShopCashbackSettings");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Polls");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "ReportCategories");

            migrationBuilder.DropTable(
                name: "SmsRequests");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "CreditHistories");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "ShopUsers");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
