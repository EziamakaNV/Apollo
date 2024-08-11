using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Apollo.Migrations
{
    /// <inheritdoc />
    public partial class SeedDoctorData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "ConsultationHistories");

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "ConsultationHistories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Rating = table.Column<double>(type: "double precision", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Name", "Price", "Rating" },
                values: new object[,]
                {
                    { 1, "Dr. John Smith", 100.0, 4.7999999999999998 },
                    { 2, "Dr. Emily Johnson", 120.0, 4.7000000000000002 },
                    { 3, "Dr. Michael Brown", 150.0, 4.9000000000000004 },
                    { 4, "Dr. Linda Davis", 80.0, 4.5999999999999996 },
                    { 5, "Dr. Robert Wilson", 110.0, 4.5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationHistories_DoctorId",
                table: "ConsultationHistories",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultationHistories_Doctors_DoctorId",
                table: "ConsultationHistories",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsultationHistories_Doctors_DoctorId",
                table: "ConsultationHistories");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_ConsultationHistories_DoctorId",
                table: "ConsultationHistories");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "ConsultationHistories");

            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "ConsultationHistories",
                type: "text",
                nullable: true);
        }
    }
}
