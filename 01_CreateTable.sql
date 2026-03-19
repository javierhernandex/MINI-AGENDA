
IF not EXISTS (SELECT name FROM sys.databases WHERE name = 'MiniAgenda')
BEGIN
   create database MiniAgenda
END
go

use MiniAgenda
go
IF not EXISTS (SELECT * FROM sys.tables WHERE name = 'Medico' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
  create table Medico (
	idMedico int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Nombre varchar(50),
	Apellido varchar (50),
	FechaAlta date DEFAULT GETDATE(),
	Estatus bit default 1
)


END
go

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Medico' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
IF NOT EXISTS (SELECT TOP 1 1 FROM Medico)
    BEGIN
  insert into Medico(Nombre,Apellido) values ('Javier','Hernandez'),('Dulce','Fernandez'),('Angela Fernanda','Lopez Perez'),('Mariana Anahi','Ferandez')
  end

END
go 
IF not EXISTS (SELECT * FROM sys.tables WHERE name = 'HorarioAtencion' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
  CREATE TABLE HorarioAtencion (
    idHorario INT PRIMARY KEY IDENTITY(1,1),
    idMedico INT NOT NULL, 
    DiaSemana TINYINT NOT NULL, 
    HoraInicio TIME NOT NULL,  
    HoraFin TIME NOT NULL,    
    Activo BIT DEFAULT 1,      
    
    CONSTRAINT CHK_Horario_Valido CHECK (HoraFin > HoraInicio),
    CONSTRAINT CHK_Dia_Semana CHECK (DiaSemana BETWEEN 1 AND 7)
);

END
go
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'HorarioAtencion' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
IF NOT EXISTS (SELECT TOP 1 1 FROM HorarioAtencion)
    BEGIN
 insert into HorarioAtencion (idMedico,DiaSemana,HoraInicio,HoraFin) values(1,1,'08:00:00','14:00:00'),(1,2,'08:00:00','14:00:00'),(1,3,'08:00:00','14:00:00'),(1,4,'08:00:00','14:00:00'),(1,5,'08:00:00','14:00:00'),(1,6,'09:00:00','12:00:00')
  insert into HorarioAtencion (idMedico,DiaSemana,HoraInicio,HoraFin) values(2,1,'09:00:00','17:00:00'),(2,2,'09:00:00','15:00:00'),(2,3,'09:00:00','12:00:00'),(2,4,'09:00:00','14:00:00'),(2,5,'09:00:00','14:00:00')
  insert into HorarioAtencion (idMedico,DiaSemana,HoraInicio,HoraFin) values(3,2,'09:00:00','17:00:00'),(3,3,'09:00:00','15:00:00'),(3,5,'09:00:00','12:00:00'),(3,6,'09:00:00','14:00:00'),(3,7,'09:00:00','14:00:00')
    insert into HorarioAtencion (idMedico,DiaSemana,HoraInicio,HoraFin) values(4,1,'08:00:00','20:00:00'),(4,2,'14:00:00','20:00:00')
END
end
go

IF not EXISTS (SELECT * FROM sys.tables WHERE name = 'Especialidad' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
create table Especialidad(
	idEspecialidad int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Descripcion varchar(50),
	DuracionCita SMALLINT,
	Estatus bit default 1

)

END
go

IF  EXISTS (SELECT * FROM sys.tables WHERE name = 'Especialidad' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
	IF NOT EXISTS (SELECT TOP 1 1 FROM Especialidad)
	begin
	insert into Especialidad (Descripcion,DuracionCita) values ('Medicina General',20),('Cardiología',30),('Cirugía',45),('Pediatría',20),('Ginecología',30)
	insert into Especialidad (Descripcion,DuracionCita) values ('Urologia',50),('Oncologia',30),('Neumologia',45)
	end

END
go

IF not EXISTS (SELECT * FROM sys.tables WHERE name = 'MedicoEspecialidad' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
create table MedicoEspecialidad(
	idMedico int not null,
	idEspecialidad int not null,
	CONSTRAINT [PK_MedicoEspecialidad] PRIMARY KEY CLUSTERED 
(
	[idMedico] ASC,
	[idEspecialidad] ASC
))

END
go

IF  EXISTS (SELECT * FROM sys.tables WHERE name = 'MedicoEspecialidad' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
IF NOT EXISTS (SELECT TOP 1 1 FROM MedicoEspecialidad)
	begin
	insert into MedicoEspecialidad (idMedico,idEspecialidad) values(1,1),(2,2),(3,8),(4,5)
	end
end
go

IF not EXISTS (SELECT * FROM sys.tables WHERE name = 'Paciente' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
create table Paciente(
		idPaciente int IDENTITY(1,1) NOT NULL PRIMARY KEY,
      nombre varchar (50) not null,
      apellido varchar (50) not null,
      fechaNacimiento date not null,
	  telefono varchar(15) not null,
	  email varchar(50),
	  fechaAlta date default getdate() 
	  )
END
go

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Paciente' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
IF NOT EXISTS (SELECT TOP 1 1 FROM MedicoEspecialidad)
	begin
	insert into Paciente (nombre,apellido,fechaNacimiento,telefono,email) values('Juan Pedro','Valencia Guzman','1993-01-03','9991231212','example@gmail,com')
	insert into Paciente (nombre,apellido,fechaNacimiento,telefono,email) values('Fernanda Maria','Guzman Gonzales','1995-03-17','9996576212','fernanda@gmail,com')
	insert into Paciente (nombre,apellido,fechaNacimiento,telefono,email) values('Jesus','Puc','1990-10-03','99912319292','jpuc@gmail,com')
	end
end
go

IF not EXISTS (SELECT * FROM sys.tables WHERE name = 'Cita' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
create table Cita(
		idCita int IDENTITY(1,1) NOT NULL PRIMARY KEY,
		idPaciente int,
		idMedico int,
		fechaCita date,
		horaCita time,
		estado varchar(20),
		motivo varchar(100),
	  fechaCreacion date default getdate(),
	  fechaCancelacion datetime,
	  motivoCancelacion varchar(100)
	  )
END
go

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Cita' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
IF NOT EXISTS (SELECT TOP 1 1 FROM Cita)
	begin
	insert into Cita (idPaciente,idMedico,fechaCita,horaCita,estado,motivo) values (1,1,'2026-03-23','08:00:00','Activa','Dolor constante') 
	insert into Cita (idPaciente,idMedico,fechaCita,horaCita,estado,motivo) values (2,2,'2026-03-25','09:30:00','Activa','Taticardia') 
	insert into Cita (idPaciente,idMedico,fechaCita,horaCita,estado,motivo) values (3,3,'2026-03-28','09:45:00','Activa','Estornudos Por las noches') 
	insert into Cita (idPaciente,idMedico,fechaCita,horaCita,estado,motivo,fechaCancelacion,motivoCancelacion) values (3,3,'2026-03-28','10:30:00','Cancelada','Estornudos Por las noches','2026-03-18','Viaje de imprevisto')
	end
end
go


DROP PROCEDURE IF EXISTS dbo.sp_DisponibilidadMedico;
GO

create PROCEDURE sp_DisponibilidadMedico
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

