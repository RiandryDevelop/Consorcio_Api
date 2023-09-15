using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Consorcio_Api.Migrations
{
    /// <inheritdoc />
    public partial class mssql_migration_749 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    IdDepartment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Departme__DF1E6E4BC4AB4F85", x => x.IdDepartment);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    IdEmployee = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Full_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IdDepartment = table.Column<int>(type: "int", nullable: true),
                    Salary = table.Column<int>(type: "int", nullable: true),
                    contract_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employee__51C8DD7A4193F9AD", x => x.IdEmployee);
                    table.ForeignKey(
                        name: "FK__Employee__IdDepa__3A81B327",
                        column: x => x.IdDepartment,
                        principalTable: "Department",
                        principalColumn: "IdDepartment");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_IdDepartment",
                table: "Employee",
                column: "IdDepartment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
