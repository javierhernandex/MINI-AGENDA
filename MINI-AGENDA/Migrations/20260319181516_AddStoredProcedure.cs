using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MINI_AGENDA.Migrations
{
    /// <inheritdoc />
    public partial class AddStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        CREATE OR ALTER PROCEDURE sp_DisponibilidadMedico
            @idMedico INT,
            @fecha DATE
        AS
        BEGIN
            SET NOCOUNT ON;

            DECLARE @duracion INT;
            DECLARE @horaInicio TIME;
            DECLARE @horaFin TIME;

            SET DATEFIRST 1;

            SELECT @duracion = e.DuracionCita
            FROM MedicoEspecialidad m
            INNER JOIN Especialidad e ON m.idEspecialidad = e.idEspecialidad
            WHERE m.idMedico = @idMedico;

            SELECT 
                @horaInicio = h.horainicio,
                @horaFin = h.horafin
            FROM HorarioAtencion h
            WHERE h.idMedico = @idMedico
              AND h.diasemana = DATEPART(WEEKDAY, @fecha)
              AND h.Activo = 1;

            DECLARE @Horas TABLE (hora TIME);

            DECLARE @horaActual TIME = @horaInicio;

            WHILE @horaActual < @horaFin
            BEGIN
                INSERT INTO @Horas VALUES (@horaActual);
                SET @horaActual = DATEADD(MINUTE, @duracion, @horaActual);
            END

            SELECT h.hora
            FROM @Horas h
            WHERE NOT EXISTS (
                SELECT 1
                FROM Cita c
                WHERE c.idmedico = @idMedico
                  AND c.fechaCita = @fecha
                  AND c.estado = 'Activa'
                  AND h.hora < DATEADD(MINUTE, @duracion, c.horaCita)
                  AND DATEADD(MINUTE, @duracion, h.hora) > c.horaCita
            )
            ORDER BY h.hora;
        END
    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_DisponibilidadMedico");
        }
    }
}
