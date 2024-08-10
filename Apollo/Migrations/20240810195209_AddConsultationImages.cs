using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Apollo.Migrations
{
    /// <inheritdoc />
    public partial class AddConsultationImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RequestedSecondOpinion",
                table: "ConsultationHistories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ConsultationImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConsultationHistoryId = table.Column<int>(type: "integer", nullable: false),
                    ImageData = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultationImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsultationImages_ConsultationHistories_ConsultationHistor~",
                        column: x => x.ConsultationHistoryId,
                        principalTable: "ConsultationHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationImages_ConsultationHistoryId",
                table: "ConsultationImages",
                column: "ConsultationHistoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsultationImages");

            migrationBuilder.DropColumn(
                name: "RequestedSecondOpinion",
                table: "ConsultationHistories");
        }
    }
}
