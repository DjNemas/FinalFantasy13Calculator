using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rank = table.Column<long>(type: "bigint", nullable: false),
                    MaxLevel = table.Column<long>(type: "bigint", nullable: false),
                    BaseEXP = table.Column<long>(type: "bigint", nullable: false),
                    IncreaseEXP = table.Column<long>(type: "bigint", nullable: false),
                    SellPrice = table.Column<long>(type: "bigint", nullable: false),
                    NexusGroup = table.Column<int>(type: "int", nullable: false),
                    Catalysator = table.Column<int>(type: "int", nullable: false),
                    SpecialEffect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Buyable = table.Column<bool>(type: "bit", nullable: false),
                    BuyPrice = table.Column<long>(type: "bigint", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    MinValue = table.Column<long>(type: "bigint", nullable: true),
                    MaxValue = table.Column<long>(type: "bigint", nullable: true),
                    UpgradeToAccessoireId = table.Column<long>(type: "bigint", nullable: true),
                    MinPhysicalDamage = table.Column<long>(type: "bigint", nullable: true),
                    MaxPhysicalDamage = table.Column<long>(type: "bigint", nullable: true),
                    MinMagicDamage = table.Column<long>(type: "bigint", nullable: true),
                    MaxMagicDamage = table.Column<long>(type: "bigint", nullable: true),
                    UpgradeToWeaponId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Items_UpgradeToAccessoireId",
                        column: x => x.UpgradeToAccessoireId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_Items_UpgradeToWeaponId",
                        column: x => x.UpgradeToWeaponId,
                        principalTable: "Items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    BearerToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDateBearerToken = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ResetToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDateResetToken = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_Name",
                table: "Items",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_UpgradeToAccessoireId",
                table: "Items",
                column: "UpgradeToAccessoireId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UpgradeToWeaponId",
                table: "Items",
                column: "UpgradeToWeaponId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_UserId",
                table: "Sessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}
