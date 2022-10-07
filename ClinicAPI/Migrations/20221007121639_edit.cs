using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicAPI.Migrations
{
    public partial class edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Animals_AnimalId",
                table: "Visits");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Procedures_ProcedureId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_AnimalId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_ProcedureId",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "ProcedureId",
                table: "Visits");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProcedureId",
                table: "Visits",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Visits_AnimalId",
                table: "Visits",
                column: "AnimalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visits_ProcedureId",
                table: "Visits",
                column: "ProcedureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Animals_AnimalId",
                table: "Visits",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Procedures_ProcedureId",
                table: "Visits",
                column: "ProcedureId",
                principalTable: "Procedures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
