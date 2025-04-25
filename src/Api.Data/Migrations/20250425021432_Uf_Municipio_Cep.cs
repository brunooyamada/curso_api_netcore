using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Uf_Municipio_Cep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Uf",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Sigla = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    Nome = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uf", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Municipio",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    CodIBGE = table.Column<int>(type: "integer", nullable: false),
                    UfId = table.Column<long>(type: "bigint", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Municipio_Uf_UfId",
                        column: x => x.UfId,
                        principalTable: "Uf",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cep",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cep = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Logradouro = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Numero = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    MunicipioId = table.Column<long>(type: "bigint", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cep_Municipio_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Uf",
                columns: new[] { "Id", "CreateAt", "Nome", "Sigla", "UpdateAt" },
                values: new object[,]
                {
                    { 1L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9449), "Acre", "AC", null },
                    { 2L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9451), "Alagoas", "AL", null },
                    { 3L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9452), "Amapá", "AP", null },
                    { 4L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9454), "Amazonas", "AM", null },
                    { 5L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9455), "Bahia", "BA", null },
                    { 6L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9456), "Ceará", "CE", null },
                    { 7L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9458), "Distrito Federal", "DF", null },
                    { 8L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9459), "Espírito Santo", "ES", null },
                    { 9L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9461), "Goiás", "GO", null },
                    { 10L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9463), "Maranhão", "MA", null },
                    { 11L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9464), "Mato Grosso", "MT", null },
                    { 12L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9465), "Mato Grosso do Sul", "MS", null },
                    { 13L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9466), "Minas Gerais", "MG", null },
                    { 14L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9468), "Pará", "PA", null },
                    { 15L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9469), "Paraíba", "PB", null },
                    { 16L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9470), "Paraná", "PR", null },
                    { 17L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9471), "Pernambuco", "PE", null },
                    { 18L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9472), "Piauí", "PI", null },
                    { 19L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9473), "Rio de Janeiro", "RJ", null },
                    { 20L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9475), "Rio Grande do Norte", "RN", null },
                    { 21L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9476), "Rio Grande do Sul", "RS", null },
                    { 22L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9477), "Rondônia", "RO", null },
                    { 23L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9478), "Roraima", "RR", null },
                    { 24L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9479), "Santa Catarina", "SC", null },
                    { 25L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9481), "São Paulo", "SP", null },
                    { 26L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9482), "Sergipe", "SE", null },
                    { 27L, new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9483), "Tocantins", "TO", null }
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateAt", "UpdateAt" },
                values: new object[] { new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9179), new DateTime(2025, 4, 25, 2, 14, 31, 940, DateTimeKind.Utc).AddTicks(9183) });

            migrationBuilder.CreateIndex(
                name: "IX_Cep_Cep",
                table: "Cep",
                column: "Cep");

            migrationBuilder.CreateIndex(
                name: "IX_Cep_MunicipioId",
                table: "Cep",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_Municipio_CodIBGE",
                table: "Municipio",
                column: "CodIBGE");

            migrationBuilder.CreateIndex(
                name: "IX_Municipio_UfId",
                table: "Municipio",
                column: "UfId");

            migrationBuilder.CreateIndex(
                name: "IX_Uf_Sigla",
                table: "Uf",
                column: "Sigla",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cep");

            migrationBuilder.DropTable(
                name: "Municipio");

            migrationBuilder.DropTable(
                name: "Uf");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateAt", "UpdateAt" },
                values: new object[] { new DateTime(2025, 4, 13, 2, 19, 24, 210, DateTimeKind.Utc).AddTicks(423), new DateTime(2025, 4, 13, 2, 19, 24, 210, DateTimeKind.Utc).AddTicks(427) });
        }
    }
}
