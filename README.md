# API de cuentas de ahorro – Cooperativa

API en .NET 9 con ASP.NET Core, Entity Framework Core y SQLite. Permite consultar una cuenta de ahorro y registrar depósitos.

## Requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Visual Studio Code o Visual Studio (opcional)
- [DB Browser for SQLite](https://sqlitebrowser.org/) o la extensión SQLite Viewer de VS Code (opcional, para ver la base de datos)

Comprueba que el SDK esté instalado:

```powershell
dotnet --list-sdks
```

Debe aparecer una línea que empiece con `9.0`.

## Instalación y ejecución

1. Clona el repositorio:

   ```powershell
   git clone https://github.com/Joel845gg/Nose.git
   cd Nose
   ```

2. Ejecuta la API:

   ```powershell
   dotnet run
   ```

   La primera vez descarga los paquetes NuGet, así que necesita internet. Cuando veas `Now listening on`, la API está activa.

3. Abre Swagger en el navegador usando la dirección que muestra la consola, agregando `/swagger`. Por ejemplo:

   ```
   http://localhost:5295/swagger
   ```

Si el navegador marca el certificado HTTPS como no confiable, ejecuta una sola vez:

```powershell
dotnet dev-certs https --trust
```

## Base de datos

La base `cooperativa.db` se crea sola al arrancar la API, en la carpeta desde donde ejecutas `dotnet run`. Al inicio tiene cuatro cuentas de prueba:

| Número     | Titular        | Saldo   | Estado    |
|------------|----------------|---------|-----------|
| 1234567890 | Juan Perez     | 1000.00 | Activa    |
| 0987654321 | Maria Lopez    | 500.00  | Activa    |
| 1122334455 | Carlos Sanchez | 750.00  | Activa    |
| 5566778899 | Ana Torres     | 1200.00 | Bloqueada |

Para volver a empezar, detén la API y borra `cooperativa.db`. Se volverá a crear con los datos iniciales.

## Endpoints

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/Cuentas/{numero}` | Devuelve número, titular, saldo y estado de la cuenta |
| POST | `/api/Cuentas/{numero}/depositos` | Registra un depósito. Cuerpo: `{ "monto": 100.50 }` |

Ejemplo de respuesta de depósito exitoso:

```json
{
  "mensaje": "Depósito realizado con éxito.",
  "saldo": 1100.5
}
```

## Reglas de negocio

1. El monto de un depósito debe ser mayor que cero.
2. La cuenta debe existir y estar activa para recibir depósitos.
3. Cada depósito actualiza el saldo y guarda un movimiento en la misma operación.

## Errores

Las respuestas de error tienen la forma `{ "mensaje": "..." }`:

| Código | Cuándo |
|--------|--------|
| 400 | Monto menor o igual a cero, o cuenta bloqueada |
| 404 | La cuenta no existe |

## Estructura del proyecto

```
Nose/
├── Controllers/CuentasController.cs   # Endpoints
├── Data/AppDbContext.cs               # Configuración de la base de datos
├── Data/DatosPrueba.cs                # Cuentas de prueba iniciales
├── Models/Cuenta.cs                   # Entidad Cuenta
├── Models/Movimiento.cs               # Entidad Movimiento
├── Properties/launchSettings.json     # Puertos de ejecución
├── Program.cs                         # Arranque de la aplicación
└── CooperativaApi.csproj              # Proyecto y paquetes NuGet
```
