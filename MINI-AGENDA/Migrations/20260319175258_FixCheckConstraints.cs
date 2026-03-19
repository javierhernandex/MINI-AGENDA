using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MINI_AGENDA.Migrations
{
    /// <inheritdoc />
    public partial class FixCheckConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hora");

            migrationBuilder.AlterColumn<string>(
                name: "telefono",
                table: "Paciente",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "nombre",
                table: "Paciente",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaAlta",
                table: "Paciente",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "Paciente",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "apellido",
                table: "Paciente",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "nombre",
                table: "Medico",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaAlta",
                table: "Medico",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "Estatus",
                table: "Medico",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "Activo",
                table: "HorarioAtencion",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "Estatus",
                table: "Especialidad",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Especialidad",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "motivoCancelacion",
                table: "Cita",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "motivo",
                table: "Cita",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaCreacion",
                table: "Cita",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "estado",
                table: "Cita",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicoEspecialidad_idEspecialidad",
                table: "MedicoEspecialidad",
                column: "idEspecialidad");

            migrationBuilder.CreateIndex(
                name: "IX_HorarioAtencion_idMedico",
                table: "HorarioAtencion",
                column: "idMedico");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Dia_Semana",
                table: "HorarioAtencion",
                sql: "DiaSemana BETWEEN 1 AND 7");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Horario_Valido",
                table: "HorarioAtencion",
                sql: "HoraFin > HoraInicio");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_idpaciente",
                table: "Cita",
                column: "idpaciente");

            migrationBuilder.AddForeignKey(
                name: "FK_Cita_Medico_idpaciente",
                table: "Cita",
                column: "idpaciente",
                principalTable: "Medico",
                principalColumn: "idMedico",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cita_Paciente_idpaciente",
                table: "Cita",
                column: "idpaciente",
                principalTable: "Paciente",
                principalColumn: "idPaciente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HorarioAtencion_Medico_idMedico",
                table: "HorarioAtencion",
                column: "idMedico",
                principalTable: "Medico",
                principalColumn: "idMedico",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicoEspecialidad_Especialidad_idEspecialidad",
                table: "MedicoEspecialidad",
                column: "idEspecialidad",
                principalTable: "Especialidad",
                principalColumn: "idEspecialidad",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicoEspecialidad_Medico_idMedico",
                table: "MedicoEspecialidad",
                column: "idMedico",
                principalTable: "Medico",
                principalColumn: "idMedico",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cita_Medico_idpaciente",
                table: "Cita");

            migrationBuilder.DropForeignKey(
                name: "FK_Cita_Paciente_idpaciente",
                table: "Cita");

            migrationBuilder.DropForeignKey(
                name: "FK_HorarioAtencion_Medico_idMedico",
                table: "HorarioAtencion");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicoEspecialidad_Especialidad_idEspecialidad",
                table: "MedicoEspecialidad");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicoEspecialidad_Medico_idMedico",
                table: "MedicoEspecialidad");

            migrationBuilder.DropIndex(
                name: "IX_MedicoEspecialidad_idEspecialidad",
                table: "MedicoEspecialidad");

            migrationBuilder.DropIndex(
                name: "IX_HorarioAtencion_idMedico",
                table: "HorarioAtencion");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Dia_Semana",
                table: "HorarioAtencion");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Horario_Valido",
                table: "HorarioAtencion");

            migrationBuilder.DropIndex(
                name: "IX_Cita_idpaciente",
                table: "Cita");

            migrationBuilder.AlterColumn<string>(
                name: "telefono",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "nombre",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaAlta",
                table: "Paciente",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "apellido",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "nombre",
                table: "Medico",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaAlta",
                table: "Medico",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<bool>(
                name: "Estatus",
                table: "Medico",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Activo",
                table: "HorarioAtencion",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Estatus",
                table: "Especialidad",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Especialidad",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "motivoCancelacion",
                table: "Cita",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "motivo",
                table: "Cita",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaCreacion",
                table: "Cita",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "estado",
                table: "Cita",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Hora",
                columns: table => new
                {
                    hora = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                });
        }
    }
}
