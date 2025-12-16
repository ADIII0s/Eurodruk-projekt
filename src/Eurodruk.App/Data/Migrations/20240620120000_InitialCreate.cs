using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eurodruk.App.Data.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Machines",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Line = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Segment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Department = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Machines", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "MaintenanceReports",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MaintenanceReports", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "WorkshopTickets",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                ErrorCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                MachineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ProductionLine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                OperatorName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                OperatorDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ServiceDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                StartedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                ClosedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                Status = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WorkshopTickets", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "AcceptanceDecisions",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                TicketId = table.Column<int>(type: "int", nullable: false),
                Approver = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Accepted = table.Column<bool>(type: "bit", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AcceptanceDecisions", x => x.Id);
                table.ForeignKey(
                    name: "FK_AcceptanceDecisions_WorkshopTickets_TicketId",
                    column: x => x.TicketId,
                    principalTable: "WorkshopTickets",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "MaintenanceReportItems",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ReportId = table.Column<int>(type: "int", nullable: false),
                TicketId = table.Column<int>(type: "int", nullable: false),
                Summary = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MaintenanceReportItems", x => x.Id);
                table.ForeignKey(
                    name: "FK_MaintenanceReportItems_MaintenanceReports_ReportId",
                    column: x => x.ReportId,
                    principalTable: "MaintenanceReports",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_MaintenanceReportItems_WorkshopTickets_TicketId",
                    column: x => x.TicketId,
                    principalTable: "WorkshopTickets",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "TicketActions",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Type = table.Column<int>(type: "int", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                TicketId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TicketActions", x => x.Id);
                table.ForeignKey(
                    name: "FK_TicketActions_WorkshopTickets_TicketId",
                    column: x => x.TicketId,
                    principalTable: "WorkshopTickets",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "TicketPhotos",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Caption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UploadedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                TicketId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TicketPhotos", x => x.Id);
                table.ForeignKey(
                    name: "FK_TicketPhotos_WorkshopTickets_TicketId",
                    column: x => x.TicketId,
                    principalTable: "WorkshopTickets",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_AcceptanceDecisions_TicketId",
            table: "AcceptanceDecisions",
            column: "TicketId");

        migrationBuilder.CreateIndex(
            name: "IX_Machines_Name",
            table: "Machines",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_MaintenanceReportItems_ReportId",
            table: "MaintenanceReportItems",
            column: "ReportId");

        migrationBuilder.CreateIndex(
            name: "IX_MaintenanceReportItems_TicketId",
            table: "MaintenanceReportItems",
            column: "TicketId");

        migrationBuilder.CreateIndex(
            name: "IX_TicketActions_TicketId",
            table: "TicketActions",
            column: "TicketId");

        migrationBuilder.CreateIndex(
            name: "IX_TicketPhotos_TicketId",
            table: "TicketPhotos",
            column: "TicketId");

        migrationBuilder.CreateIndex(
            name: "IX_WorkshopTickets_ErrorCode",
            table: "WorkshopTickets",
            column: "ErrorCode");

        migrationBuilder.CreateIndex(
            name: "IX_WorkshopTickets_Status_CreatedAt",
            table: "WorkshopTickets",
            columns: new[] { "Status", "CreatedAt" });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "AcceptanceDecisions");

        migrationBuilder.DropTable(
            name: "Machines");

        migrationBuilder.DropTable(
            name: "MaintenanceReportItems");

        migrationBuilder.DropTable(
            name: "TicketActions");

        migrationBuilder.DropTable(
            name: "TicketPhotos");

        migrationBuilder.DropTable(
            name: "MaintenanceReports");

        migrationBuilder.DropTable(
            name: "WorkshopTickets");
    }
}
