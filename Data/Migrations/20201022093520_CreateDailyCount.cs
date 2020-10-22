using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Covid.Data.Migrations
{
    public partial class CreateDailyCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyCount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    County = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Fips = table.Column<float>(nullable: false),
                    Cases = table.Column<int>(nullable: false),
                    Deaths = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyCount", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyCount");
        }
    }
}
