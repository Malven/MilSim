using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MilSim.Data.Migrations
{
    public partial class SteamUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Steam",
                columns: table => new
                {
                    steamid = table.Column<string>(nullable: false),
                    avatar = table.Column<string>(nullable: true),
                    avatarfull = table.Column<string>(nullable: true),
                    avatarmedium = table.Column<string>(nullable: true),
                    communityvisibilitystate = table.Column<int>(nullable: false),
                    gameextrainfo = table.Column<string>(nullable: true),
                    gameid = table.Column<string>(nullable: true),
                    lastlogoff = table.Column<int>(nullable: false),
                    loccountrycode = table.Column<string>(nullable: true),
                    personaname = table.Column<string>(nullable: true),
                    personastate = table.Column<int>(nullable: false),
                    personastateflags = table.Column<int>(nullable: false),
                    primaryclanid = table.Column<string>(nullable: true),
                    profilestate = table.Column<int>(nullable: false),
                    profileurl = table.Column<string>(nullable: true),
                    realname = table.Column<string>(nullable: true),
                    timecreated = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steam", x => x.steamid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Steam");
        }
    }
}
