using Microsoft.EntityFrameworkCore.Migrations;

namespace Covid.Data.Migrations
{
    public partial class CreateCountList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CountList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountList_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CountListDailyCount",
                columns: table => new
                {
                    CountListId = table.Column<int>(nullable: false),
                    DailyCountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountListDailyCount", x => new { x.CountListId, x.DailyCountId });
                    table.ForeignKey(
                        name: "FK_CountListDailyCount_CountList_CountListId",
                        column: x => x.CountListId,
                        principalTable: "CountList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountListDailyCount_DailyCount_DailyCountId",
                        column: x => x.DailyCountId,
                        principalTable: "DailyCount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountList_AppUserId",
                table: "CountList",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CountListDailyCount_DailyCountId",
                table: "CountListDailyCount",
                column: "DailyCountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountListDailyCount");

            migrationBuilder.DropTable(
                name: "CountList");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");
        }
    }
}
