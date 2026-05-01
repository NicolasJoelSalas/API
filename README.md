🎟️ Sistema de Venta de Tickets de Eventos

Este repositorio contiene una aplicación web orientada a la gestión y venta de tickets para eventos. La solución está compuesta por un backend desarrollado en .NET utilizando Entity Framework Core para el acceso a datos y una interfaz que permite la interacción con los usuarios.

Para poder ejecutar correctamente el proyecto en un entorno local, es necesario realizar una serie de configuraciones iniciales. En primer lugar, se debe verificar el string de conexión a la base de datos, el cual se encuentra en el archivo TPAPIdeProyectoSoftware/appsettings.Development.json. Es fundamental que el valor del servidor esté correctamente definido.

Luego, utilizando SQLSERVER debe crear una base de datos llamado "PROYECTO". Las tablas y los datos a precargar se generaran con la carpeta "Migration". Para ello, se debe abrir la Consola del Administrador de Paquetes en Visual Studio, seleccionar como proyecto predeterminado la capa "Infrastructure" y ejecutar el comando "Update-Database" para recargar los datos de la migracion a la base de datos. 
Una vez configurada la base de datos, es necesario establecer los proyectos de inicio. Se debe configurar la solución para ejecutar múltiples proyectos al mismo tiempo, seleccionando como proyectos de inicio "TPAPIdeProyectoSoftware" y "ApiFront". Esto se realiza desde las propiedades de la solución, en la opción "Configurar proyectos de inicio", eligiendo "Varios proyectos de inicio" y asignando la acción "Inicio" a ambos.

Al ejecutar la aplicación, el frontend permitirá navegar entre las distintas funcionalidades del sistema, como la visualización de eventos, sectores y asientos. Primeramente, se debe loguear con usuario o puede registrarse, por otro lado, le permitira realizar una reserva del asiento o asientos que desee. Es importante tener en cuenta que la navegación dentro del frontend no debe realizarse mediante recargas manuales del navegador, ya que esto provocará la pérdida de la sesión iniciada (login).

Se recomienda además verificar que SQL Server esté en ejecución antes de levantar el proyecto y que las migraciones se hayan aplicado correctamente para evitar errores de conexión o de estructura de base de datos.
