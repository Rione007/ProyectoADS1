using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoADS1.Migrations
{
    /// <inheritdoc />
    public partial class nuevamigra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Concesionario",
                columns: table => new
                {
                    IdConcesionario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreComercial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RazonSocial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RUC = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Departamento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Provincia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Distrito = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaRegistro = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    Estado = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Concesio__49EF4257A6FA8DDD", x => x.IdConcesionario);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dni = table.Column<int>(type: "int", nullable: false),
                    nombreUsuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    rol = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "usuario"),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario__645723A6DDB296B6", x => x.idUsuario);
                });

            migrationBuilder.CreateTable(
                name: "FichaInspeccion",
                columns: table => new
                {
                    IdInspeccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdConcesionario = table.Column<int>(type: "int", nullable: false),
                    RUC = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: false),
                    NombreComercial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RazonSocial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Departamento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Provincia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AreaSupervisada = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaProgramada = table.Column<DateOnly>(type: "date", nullable: false),
                    MotivoInspeccion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FechaRegistro = table.Column<DateOnly>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    Estado = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true, defaultValue: "Pendiente"),
                    FechaActualizada = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FichaIns__2929253F94084335", x => x.IdInspeccion);
                    table.ForeignKey(
                        name: "FK__FichaInsp__IdCon__5EBF139D",
                        column: x => x.IdConcesionario,
                        principalTable: "Concesionario",
                        principalColumn: "IdConcesionario");
                });

            migrationBuilder.CreateTable(
                name: "ActaInspeccion",
                columns: table => new
                {
                    IdInspeccion = table.Column<int>(type: "int", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Recomendaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Conclusiones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirmaSupervisor = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    FirmaAdministrado = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ActaInsp__2929253F64C3558B", x => x.IdInspeccion);
                    table.ForeignKey(
                        name: "FK__ActaInspe__IdIns__04E4BC85",
                        column: x => x.IdInspeccion,
                        principalTable: "FichaInspeccion",
                        principalColumn: "IdInspeccion");
                });

            migrationBuilder.CreateTable(
                name: "InformeInspeccion",
                columns: table => new
                {
                    IdInspeccion = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: true),
                    FirmaSupervisor = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    FirmaCoordinador = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__InformeI__2929253F400F5D12", x => x.IdInspeccion);
                    table.ForeignKey(
                        name: "FK__InformeIn__IdIns__2CF2ADDF",
                        column: x => x.IdInspeccion,
                        principalTable: "FichaInspeccion",
                        principalColumn: "IdInspeccion");
                    table.ForeignKey(
                        name: "FK__InformeIn__IdUsu__2DE6D218",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario");
                });

            migrationBuilder.CreateTable(
                name: "MemorandumInspeccion",
                columns: table => new
                {
                    IdMemo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdInspeccion = table.Column<int>(type: "int", nullable: false),
                    Asunto = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Cuerpo = table.Column<string>(type: "text", nullable: true),
                    FirmaCoordinadorGeneral = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    FirmaCoordinador = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    FirmaDirectorGeneral = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    FechaActualizada = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Memorand__4D7D50869C42DFBC", x => x.IdMemo);
                    table.ForeignKey(
                        name: "FK__Memorandu__IdIns__498EEC8D",
                        column: x => x.IdInspeccion,
                        principalTable: "FichaInspeccion",
                        principalColumn: "IdInspeccion");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FichaInspeccion_IdConcesionario",
                table: "FichaInspeccion",
                column: "IdConcesionario");

            migrationBuilder.CreateIndex(
                name: "IX_InformeInspeccion_IdUsuario",
                table: "InformeInspeccion",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "UQ__Memorand__2929253E6844E20C",
                table: "MemorandumInspeccion",
                column: "IdInspeccion",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActaInspeccion");

            migrationBuilder.DropTable(
                name: "InformeInspeccion");

            migrationBuilder.DropTable(
                name: "MemorandumInspeccion");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "FichaInspeccion");

            migrationBuilder.DropTable(
                name: "Concesionario");
        }
    }
}
