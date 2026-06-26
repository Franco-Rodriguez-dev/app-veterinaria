# Veterinaria App

Aplicación fullstack para la gestión de clientes, usuarios, mascotas e historial clínico veterinario.

El proyecto separa un backend ASP.NET Core Web API y un frontend Angular standalone. Incluye autenticación con JWT, roles, autorización por permisos, control de pertenencia de datos para clientes, baja lógica y pruebas de integración sobre reglas de seguridad.

## Problema que resuelve

Una veterinaria necesita registrar clientes y mascotas, consultar historiales clínicos y permitir que cada cliente pueda ver sólo su propia información.

La aplicación organiza ese flujo en dos perfiles:

- Administrador: gestiona clientes, usuarios, mascotas, historiales y restablecimiento de contraseñas temporales.
- Cliente: consulta su perfil, sus mascotas y el historial permitido.

## Tecnologías utilizadas

Backend:

- C#
- ASP.NET Core Web API .NET 8
- Entity Framework Core
- SQL Server
- JWT Bearer Authentication
- BCrypt
- AutoMapper
- Swagger / OpenAPI

Frontend:

- Angular standalone
- TypeScript
- Angular Material
- Guards de rutas
- Interceptor JWT

Tests:

- xUnit
- WebApplicationFactory
- SQLite en memoria

Herramientas:

- Git
- GitHub

## Arquitectura general

```text
Usuario
  |
  | HTTP / JSON
  v
Frontend Angular
  - Login
  - Guards por rol
  - Interceptor JWT
  - Bearer Token
  - Vistas Administrador / Cliente
  |
  | HTTP / JSON + Bearer Token
  v
Backend ASP.NET Core Web API
  - Controllers
  - DTOs y AutoMapper
  - Repositories
  - Autenticación/autorización JWT
  - Validación de pertenencia
  |
  v
Entity Framework Core
  |
  v
SQL Server
```

Estructura principal:

```text
App-Veterinaria/
  BE-CRUDMascotas/       Backend ASP.NET Core y tests
  FE-CRUDMascotas/       Frontend Angular
  README.md              Documentación principal
```

## Roles y permisos

### Administrador

Puede:

- Iniciar sesión.
- Ver listado general.
- Crear cliente con usuario y mascota inicial.
- Gestionar clientes, personas y mascotas.
- Crear, editar, ver y eliminar registros de historial clínico.
- Dar de baja clientes.
- Reactivar clientes dados de baja.
- Restablecer contraseñas temporales.
- Consultar endpoints generales protegidos.

### Cliente

Puede:

- Iniciar sesión.
- Ver su perfil.
- Ver sus mascotas.
- Ver el historial clínico de sus propias mascotas.
- Cambiar una contraseña temporal obligatoria.
- Cambiar su propia contraseña voluntariamente.

No puede:

- Ver listados generales.
- Usar CRUD general de personas o mascotas.
- Consultar datos de otros clientes.
- Crear, editar, eliminar o reactivar registros administrativos.
- Restablecer contraseñas de otros usuarios.

## Funcionalidades principales

- Login con JWT.
- Roles: Administrador y Cliente.
- Protección de rutas Angular con guards.
- Interceptor JWT para enviar el token en requests.
- Gestión de clientes, usuarios y mascotas.
- Creación de cliente con usuario y mascota.
- Perfil propio para Cliente.
- Cliente puede ver sólo sus mascotas.
- Historial clínico de mascotas.
- Control de acceso al historial por pertenencia de mascota.
- Baja lógica de cliente, usuario y mascotas.
- Reactivación de clientes.
- Restablecimiento de contraseña temporal por Administrador.
- Cambio obligatorio de contraseña temporal.
- Cambio voluntario de contraseña propia.
- Endpoints generales protegidos para Administrador.
- Swagger configurado con Bearer Token.
- Build Angular de desarrollo y producción funcionando.
- Configuración preparada para variables de entorno y publicación futura.

## Seguridad implementada

- JWT para autenticación.
- Roles para autorización: Administrador y Cliente.
- Endpoints generales restringidos a Administrador.
- Endpoints específicos para Cliente, como perfil propio y mascota propia.
- Control de pertenencia: un Cliente sólo puede consultar mascotas e historiales asociados a su Persona.
- BCrypt para hashear contraseñas.
- Baja lógica para evitar eliminación física de información principal.
- Configuración local segura:
  - `appsettings.json` usa placeholders.
  - `appsettings.Local.json` queda ignorado por Git.
  - Variables de entorno preparadas para publicación futura.

## Tests

El backend incluye 8 tests de integración con xUnit, WebApplicationFactory y SQLite en memoria.

Validan, de forma resumida:

- Usuario inactivo no puede iniciar sesión.
- Sin token no se puede acceder a perfil protegido.
- Cliente no puede acceder a endpoint exclusivo de Administrador.
- Cliente puede consultar su propio perfil.
- Cliente no puede consultar una mascota ajena.
- Cliente no puede ver historial de una mascota ajena.
- Administrador puede consultar una mascota desde endpoint general.
- Baja lógica desactiva persona, usuario y mascotas, manteniendo historiales.

Ejecutar tests:

```bash
cd BE-CRUDMascotas
dotnet test
```

## Cómo ejecutar localmente

### 1. Backend

Entrar a la carpeta del backend:

```bash
cd BE-CRUDMascotas/BE-CRUDMascotas
```

Crear configuración local:

1. Copiar `appsettings.Local.example.json`.
2. Renombrar la copia a `appsettings.Local.json`.
3. Completar los valores locales necesarios.

> Importante: `appsettings.Local.json` contiene configuración local, está ignorado por Git y no debe subirse al repositorio.

Ejemplo de estructura:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "COMPLETAR_CONNECTION_STRING_LOCAL"
  },
  "Jwt": {
    "Key": "COMPLETAR_JWT_KEY_LOCAL",
    "Issuer": "COMPLETAR_JWT_ISSUER_LOCAL",
    "Audience": "COMPLETAR_JWT_AUDIENCE_LOCAL",
    "ExpireMinutes": 60
  },
  "Cors": {
    "AllowedOrigins": [
      "http://localhost:4200",
      "https://localhost:4200"
    ]
  }
}
```

Ejecutar backend:

```bash
dotnet run
```

Swagger queda disponible al ejecutar el perfil de desarrollo configurado por Visual Studio o `dotnet run`, según el puerto usado localmente.

### 2. Frontend

Entrar a la carpeta del frontend:

```bash
cd FE-CRUDMascotas
```

Instalar dependencias:

```bash
npm install
```

Ejecutar en desarrollo:

```bash
npm start
```

Build de desarrollo:

```bash
npm run build -- --configuration development
```

Build de producción:

```bash
npm run build
```

Antes de publicar el frontend, reemplazar en `src/environments/environment.prod.ts`:

```ts
apiUrl: 'https://API_URL_PENDIENTE/api'
```

por la URL real de la API publicada.

## Variables de entorno para deploy

Configurar en el hosting del backend:

```text
Jwt__Key
Jwt__Issuer
Jwt__Audience
Jwt__ExpireMinutes
ConnectionStrings__DefaultConnection
Cors__AllowedOrigins__0
```

Notas:

- No guardar secretos reales en archivos versionados.
- `ConnectionStrings__DefaultConnection` debe apuntar a la base SQL Server del entorno publicado.
- `Cors__AllowedOrigins__0` debe apuntar a la URL real del frontend publicado.

## Estado del proyecto

Implementado:

- Backend ASP.NET Core con JWT, roles y autorización.
- Frontend Angular con guards, interceptor y vistas por rol.
- Flujo de Administrador.
- Flujo de Cliente.
- Historial clínico.
- Baja lógica y reactivación.
- Restablecimiento de contraseñas temporales y cambio de contraseña.
- Tests de integración.
- Configuración segura para desarrollo local y publicación futura.
- Build Angular de producción funcionando.

Mejoras futuras:

- Flujo automático de restablecimiento de contraseñas por email.
- Mejoras visuales adicionales.
- Más cobertura de tests.
- Preparación final de hosting cuando se defina un entorno de publicación.

## Capturas y demo

Pendiente de agregar:

- Capturas del login.
- Capturas del panel Administrador.
- Capturas del perfil Cliente.
- Capturas del historial clínico.
- Video corto de demo.

## Licencia y uso

Proyecto desarrollado con fines de aprendizaje y portfolio.

## Autor

Franco Rodríguez

GitHub: [Franco-Rodriguez-dev](https://github.com/Franco-Rodriguez-dev)
