# ADR-0002: OCP mediante Strategy y NotificationContext

## Estado
Aceptado

## Contexto
El Capítulo 2 introdujo `INotificationChannel` y desacoplό el caso de uso
de las implementaciones concretas. Sin embargo, quedaron dos problemas
estructurales sin resolver:

**Problema 1 — La lógica de routing vive en el lugar equivocado.**
`RegisterUser` decide qué canal activar mediante bloques `if/else` sobre
un `string origin`. Cada canal nuevo obliga a abrir y modificar el caso
de uso, violando OCP.

**Problema 2 — Primitive Obsession en los datos de contexto.**
El origen del registro (`"Web"`, `"Mobile"`, `"Desktop"`) y la condición
VIP viajan como primitivos sueltos dentro de `RegisterUserInput`, mezclados
con datos propios del usuario (`Email`, `DeviceToken`, `PhoneNumber`).
Un `string` no puede expresar la semántica de dominio que esos valores
representan, y `RegisterUserInput` carga responsabilidades que no le
corresponden.

## Decisiones

### Decisión 1: Introducir `NotificationContext`
Se extrae un objeto de valor `NotificationContext` que agrupa exclusivamente
los datos relevantes para la selección y activación de canales: el origen
del registro y la condición VIP.

Esto resuelve dos cosas simultáneamente: elimina la Primitive Obsession
dándole al dominio un tipo propio para expresar el contexto de notificación,
y separa los datos de routing de los datos del usuario dentro de
`RegisterUserInput`.

### Decisión 2: Introducir `Supports(NotificationContext)` en `INotificationChannel`
Cada implementación de `INotificationChannel` incorpora un método
`Supports(NotificationContext context)` que encapsula su propia regla
de activación. El caso de uso deja de tomar esa decisión:

```csharp
foreach (var channel in _channels)
    if (channel.Supports(context))
        await channel.SendAsync(context);
```

Cada canal es el único que conoce con precisión bajo qué condiciones
debe activarse. Mover esa lógica al canal es moverla al lugar correcto.

## Alternativas Descartadas

### Agregar un nuevo bloque condicional en `RegisterUser`
Extender el `if/else` existente con la condición Desktop/WhatsApp.
- **Pros:** Mínimo esfuerzo inmediato.
- **Contras:** Viola OCP. La complejidad ciclomática de `RegisterUser`
  crece con cada canal. Un componente que cambia cada vez que el sistema
  se extiende no está cerrado.

## Consecuencias
- **Positivas:** `RegisterUser` queda cerrado a modificaciones por adición
  de canales. Agregar WhatsApp —o cualquier canal futuro— es una operación
  puramente aditiva. Los canales del Capítulo 2 requieren una migración
  mínima para implementar `Supports()`.
- **Decisión de scope:** La configuración de credenciales de cada canal
  (SMTP, Twilio, Meta API) continúa resolviéndose mediante variables de
  entorno. Introducir `appsettings.json` en este punto violaría KISS: no
  existe aún una necesidad real que lo justifique. Se incorporará cuando
  la complejidad de configuración del sistema lo exija por sí sola.