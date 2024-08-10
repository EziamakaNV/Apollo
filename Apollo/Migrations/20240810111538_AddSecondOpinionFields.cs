using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apollo.Migrations
{
    /// <inheritdoc />
    public partial class AddSecondOpinionFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "ConsultationHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecondOpinion",
                table: "ConsultationHistories",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "ConsultationHistories");

            migrationBuilder.DropColumn(
                name: "SecondOpinion",
                table: "ConsultationHistories");
        }
    }
}
