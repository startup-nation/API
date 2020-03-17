using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookMyMealAPI.Migrations
{
    public partial class ResturantAndResturantRequestTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Restaurent",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantEmail = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Location = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    OwnerEmail = table.Column<string>(nullable: true),
                    applicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurent", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Restaurent_AspNetUsers_applicationUserId",
                        column: x => x.applicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RestaurentRequest",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationDateTime = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    IsVerified = table.Column<bool>(nullable: false),
                    RestaurentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurentRequest", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RestaurentRequest_Restaurent_RestaurentID",
                        column: x => x.RestaurentID,
                        principalTable: "Restaurent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Restaurent_applicationUserId",
                table: "Restaurent",
                column: "applicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurentRequest_RestaurentID",
                table: "RestaurentRequest",
                column: "RestaurentID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RestaurentRequest");

            migrationBuilder.DropTable(
                name: "Restaurent");
        }
    }
}
