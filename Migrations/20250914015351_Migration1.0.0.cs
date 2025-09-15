using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace agendaAulas.Migrations
{
    /// <inheritdoc />
    public partial class Migration100 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    TipoPlano = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoAulas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoAulas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Aulas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TipoAulaId = table.Column<int>(type: "integer", nullable: false),
                    DataHora = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CapacidadeMax = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aulas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aulas_TipoAulas_TipoAulaId",
                        column: x => x.TipoAulaId,
                        principalTable: "TipoAulas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Agendamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AlunoId = table.Column<int>(type: "integer", nullable: false),
                    AulaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agendamentos_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agendamentos_Aulas_AulaId",
                        column: x => x.AulaId,
                        principalTable: "Aulas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "Nome", "TipoPlano" },
                values: new object[,]
                {
                    { 1, "Ana Silva", 1 },
                    { 2, "Bruno Costa", 3 },
                    { 3, "Carla Souza", 12 },
                    { 4, "Diego Rocha", 1 },
                    { 5, "Eduarda Lima", 3 },
                    { 6, "Felipe Martins", 12 },
                    { 7, "Gabriela Almeida", 1 },
                    { 8, "Henrique Santos", 3 },
                    { 9, "Isabela Nunes", 12 },
                    { 10, "João Pedro", 1 }
                });

            migrationBuilder.InsertData(
                table: "TipoAulas",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Pilates" },
                    { 2, "Cross" },
                    { 3, "Funcional" }
                });

            migrationBuilder.InsertData(
                table: "Aulas",
                columns: new[] { "Id", "CapacidadeMax", "DataHora", "TipoAulaId" },
                values: new object[,]
                {
                    { 1, 10, new DateTimeOffset(new DateTime(2025, 9, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 2, 12, new DateTimeOffset(new DateTime(2025, 9, 16, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 3, 15, new DateTimeOffset(new DateTime(2025, 9, 17, 11, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 4, 8, new DateTimeOffset(new DateTime(2025, 9, 18, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 5, 20, new DateTimeOffset(new DateTime(2025, 9, 19, 13, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2 },
                    { 6, 10, new DateTimeOffset(new DateTime(2025, 9, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2 },
                    { 7, 18, new DateTimeOffset(new DateTime(2025, 9, 16, 15, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2 },
                    { 8, 12, new DateTimeOffset(new DateTime(2025, 9, 17, 16, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3 },
                    { 9, 14, new DateTimeOffset(new DateTime(2025, 9, 18, 17, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3 },
                    { 10, 16, new DateTimeOffset(new DateTime(2025, 9, 19, 18, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3 }
                });

            migrationBuilder.InsertData(
                table: "Agendamentos",
                columns: new[] { "Id", "AlunoId", "AulaId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 },
                    { 4, 4, 4 },
                    { 5, 5, 5 },
                    { 6, 6, 6 },
                    { 7, 7, 7 },
                    { 8, 8, 8 },
                    { 9, 9, 9 },
                    { 10, 10, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_AlunoId",
                table: "Agendamentos",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_AulaId",
                table: "Agendamentos",
                column: "AulaId");

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_TipoAulaId",
                table: "Aulas",
                column: "TipoAulaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agendamentos");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Aulas");

            migrationBuilder.DropTable(
                name: "TipoAulas");
        }
    }
}
