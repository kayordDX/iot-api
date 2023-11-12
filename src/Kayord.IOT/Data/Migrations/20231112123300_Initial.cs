using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.IOT.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entity",
                columns: table => new
                {
                    Time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entity_Name",
                table: "Entity",
                column: "Name");

            migrationBuilder.Sql(
                "SELECT create_hypertable( '\"Entity\"', 'Time');\n" +
                "CREATE INDEX ix_name_time ON \"Entity\" (\"Name\", \"Time\" DESC)"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entity");
        }
    }
}
