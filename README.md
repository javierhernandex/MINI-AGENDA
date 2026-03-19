Mini Agenda API

-API desarrollada en .NET para la gestión de médicos, pacientes y citas médicas.


Tecnologías

.NET 8
-Entity Framework Core
-SQL Server
-Arquitectura en capas (Controller, Service, Repository)


Estructura del proyecto

-Controllers/ → Endpoints
-Services/ → Lógica de negocio
-Repository/ → Acceso a datos
-Models/ → Entidades
-Database/ → Scripts SQL


Usar docker para dezplegar y probar las api's.
- descargar docker desktop https://www.docker.com/products/docker-desktop/ (segun el sistema y arquitectura de tu equipo)
- Una vez intalado ubicar la carpeta del proyecto y abrir la terminar en la ruta de este
- ejecutar el siguiente comando en la terminal para construir la imagen y ejecutar el contenedor:
	docker compose up --build


Si se requiere probar la aplicacion creando la base de datos manualmente, se debe seguir los siguientes pasos:
  Base de Datos
	Ejecutar el script:
		-Database/01_CreateBase.sql
	Configuración
		Editar appsettings.json con los datos del servidor donde se creo la base.
		"ConnectionStrings": {
		  "DefaultConnection": "Server=.;Database=MiniAgenda;Trusted_Connection=True;"
		}


 Endpoints 

 Citas
  Gestion de Citas
	Get /api/Citas/Citas   
		devulve la lista de todas las citas existente
	Get /api/Citas/CitasPaciente/{idpaciente} -
		devuelve las citas de un paciente ingresando su idpaciente
	Get /api/Citas/CitasMedico/{idmedico}  
		devuelve las citas de dicho medico ingresando idmedico
	Post /api/Citas/CrearCitas 
		crea una nueva cita médica validando que la fecha no sea en el pasado, que el médico tenga una especialidad asignada, que la cita esté dentro del horario de atención del médico y que el horario esté disponible.
	Put /api/Citas/CancelarCita/{idcita}/{motivo} 
		Cancela una cita validando que no esté ya cancelada, que no sea una cita pasada y registrando el motivo de cancelación.
	Get /api/Citas/DisponibilidadMedico/{idmedico}/{fecha} 
		muestra la disponibilidad de un médico para una fecha específica, devolviendo los horarios disponibles para agendar citas.

 Medicos
   Gestión de Médicos
	GET /api/Medicos/Medicos 	
		Busca información de los médicos registrados en el sistema (ID, nombre, apellido, fecha de alta y estatus).
	POST /api/Medicos/Medicos   
		Crea un nuevo médico en el sistema.
	PUT /api/Medicos/Medicos/{id} 
		Actualiza la información de un médico existente.
	DELETE /api/Medicos/Medicos/{id} 
		Elimina un médico por su ID.

  Especialidades
	GET /api/Medicos/Especialidades
		Trae información de todas las especialidades registradas (ID, descripción, duración de cita y estatus).
	GET /api/Medicos/Especialidad/{idEspecialidad}
		Trae información detallada de una especialidad específica.
	POST /api/Medicos/Especialidad
		Crea una nueva especialidad médica.
	PUT /api/Medicos/Especialidad/{idEspecialidad}
		Actualiza una especialidad existente.
	DELETE /api/Medicos/Especialidad/{idEspecialidad}
		Elimina una especialidad del sistema.

  Relación Médico-Especialidad
	GET /api/Medicos/MedicoEspecialidad
		Trae información de las especialidades asignadas a los médicos (ID médico e ID especialidad).
	POST /api/Medicos/MedicoEspecialidad
		Crea una nueva relación entre un médico y una especialidad.

  Horarios de Atención
	GET /api/Medicos/HorarioAtencion  
		Trae los horarios registrados (ID horario, ID médico, hora inicio, hora fin y estatus).
	POST /api/Medicos/HorarioAtencion 
		Crea un nuevo horario de atención para un médico.
	PUT /api/Medicos/HorarioAtencion/{id} 
		Actualiza un horario de atención existente.
	DELETE /api/Medicos/HorarioAtencion/{id} 
		Elimina un horario de atención.

Pciente
  Gestión de Pacientes
	GET /api/Pacientes/Pacientes
		Busca y devuelve una lista de todos los pacientes desde la base de datos.
	POST /api/Pacientes/Pacientes
		Crea un nuevo paciente en el sistema.
	PUT /api/Pacientes/Pacientes
		Actualiza la información de un paciente existente.
	GET /api/Pacientes/PacientesId/{idpaciente}
		Busca y devuelve un paciente específico desde la base de datos, filtrando por su ID.
	DELETE /api/Pacientes/Pacientes/{idpaciente}
		Elimina un paciente existente. Nota: El sistema valida si cuenta con citas próximas asociadas activas; si es así, no permite eliminarlo.



 Reglas de negocio

-No se pude crear cita en el mismo horario para el mismo medico en la misma fecha
-No se pueden cancelar citas pasadas
-Pacientes con múltiples cancelaciones generan advertencia
-No se pueden eliminar pacientes con citas activas
-No se pueden eliminar medicos con citas activas
-No se pueden agendar citas fuera del horario que tiene configurado el medico
-No se pueden agendar citas para medicos sin especialidad asignada
-No se pueden agendar citas en fechas pasadas
-Si la fecha introducida para agendar una cita, te devulve las proximas 5 citas disponibles para ese medico en esa fecha, si no hay citas disponibles te devulve un mensaje indicando que no hay citas disponibles para ese medico en esa fecha.





 Autor

Francisco Hernandez