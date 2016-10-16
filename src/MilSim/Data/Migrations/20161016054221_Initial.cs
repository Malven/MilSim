using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MilSim.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    OperationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OperationDescription = table.Column<string>(nullable: false),
                    OperationTitle = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.OperationId);
                });

            migrationBuilder.CreateTable(
                name: "Fireteams",
                columns: table => new
                {
                    FireteamId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Member1 = table.Column<string>(nullable: true),
                    Member2 = table.Column<string>(nullable: true),
                    Member3 = table.Column<string>(nullable: true),
                    Member4 = table.Column<string>(nullable: true),
                    Member5 = table.Column<string>(nullable: true),
                    OperationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fireteams", x => x.FireteamId);
                    table.ForeignKey(
                        name: "FK_Fireteams_Operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "OperationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fireteams_OperationId",
                table: "Fireteams",
                column: "OperationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fireteams");

            migrationBuilder.DropTable(
                name: "Operations");
        }
    }
}
