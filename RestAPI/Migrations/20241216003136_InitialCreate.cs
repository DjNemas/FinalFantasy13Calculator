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
                name: "Item",
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
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_Item_UpgradeToAccessoireId",
                        column: x => x.UpgradeToAccessoireId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Item_Item_UpgradeToWeaponId",
                        column: x => x.UpgradeToWeaponId,
                        principalTable: "Item",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_Name",
                table: "Item",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_UpgradeToAccessoireId",
                table: "Item",
                column: "UpgradeToAccessoireId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_UpgradeToWeaponId",
                table: "Item",
                column: "UpgradeToWeaponId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");
        }
    }
}
