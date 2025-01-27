using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AktivCA.Domain.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class AddSettingsCaCert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CaCert",
                table: "Settings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaCert",
                table: "Settings");
        }
    }
}
