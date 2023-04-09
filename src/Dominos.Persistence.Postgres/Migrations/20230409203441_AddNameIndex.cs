using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dominos.Persistence.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddNameIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_vouchers_name",
                table: "vouchers",
                column: "name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_vouchers_name",
                table: "vouchers");
        }
    }
}
