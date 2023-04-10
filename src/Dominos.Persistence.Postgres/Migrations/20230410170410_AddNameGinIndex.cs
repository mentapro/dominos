using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dominos.Persistence.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddNameGinIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE EXTENSION pg_trgm;");

            migrationBuilder.CreateIndex(
                name: "IX_vouchers_name_gin",
                table: "vouchers",
                column: "name")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_vouchers_name_gin",
                table: "vouchers");
        }
    }
}
