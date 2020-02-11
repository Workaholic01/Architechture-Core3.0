using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LVT.Data.Migrations
{
    public partial class makeDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "vehicle_alarms",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    registration_no = table.Column<string>(maxLength: 60, nullable: false),
                    color = table.Column<string>(nullable: true),
                    engine_no = table.Column<string>(nullable: true),
                    chassis_no = table.Column<string>(nullable: true),
                    owner_name = table.Column<string>(nullable: true),
                    model = table.Column<string>(nullable: true),
                    reported_no = table.Column<string>(nullable: true),
                    fir_police_station = table.Column<string>(nullable: true),
                    fir_no = table.Column<string>(nullable: true),
                    investigator_name = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    alarm_cam_id = table.Column<string>(nullable: true),
                    alarm_location = table.Column<string>(nullable: true),
                    alarm_generation_time = table.Column<string>(nullable: true),
                    image_path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_alarms", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "vehicle_alarms");
        }
    }
}
