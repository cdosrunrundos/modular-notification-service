# Capítulo 3: Extensibilidad sin Modificación (OCP)

## El Cambio del Negocio (Requerimiento)
El equipo de producto lanza soporte para usuarios de escritorio (Desktop).
El sistema debe enviar una notificación por **WhatsApp** cuando el origen
del registro sea `"Desktop"`. Las reglas existentes no cambian:

| Origen  | Canal    |
|---------|----------|
| Web     | Email    |
| Mobile  | Push     |
| Desktop | WhatsApp |
| VIP     | SMS (independiente del origen) |

## El Problema del Diseño Anterior (Fricción Técnica)
El Capítulo 2 resolvió correctamente el acoplamiento a implementaciones
concretas. Sin embargo, **trasladó el problema**: la lógica que decide
*qué canal usar* quedó dentro de `RegisterUser`.

Eso significa que cada vez que el negocio introduce un canal nuevo,
el caso de uso debe abrirse y modificarse. Un componente que cambia
cada vez que el sistema crece no está bien diseñado — está acoplado
al crecimiento mismo del sistema.

## El Principio (OCP)
El **Principio Abierto/Cerrado** establece que una entidad de software
debe estar **abierta para extensión** pero **cerrada para modificación**.

En términos concretos: agregar comportamiento nuevo no debería requerir
editar código que ya funciona. El riesgo de regresión, la fricción del
cambio y la complejidad cognitiva acumulada son exactamente los síntomas
que OCP busca eliminar.

## Solución Aplicada
La solución no consiste en escribir un `if/else` más prolijo — consiste
en **mover la responsabilidad al lugar correcto**. Cada canal es el único
que sabe con precisión bajo qué condiciones debe activarse. Por lo tanto,
cada canal debe declararlo.

Se introduce el **patrón Strategy** como mecanismo para materializar OCP:
cada implementación de `INotificationChannel` incorpora un método
`Supports(NotificationContext context)` que encapsula su propia regla
de activación. El caso de uso itera la colección y delega:

```csharp
foreach (var channel in _channels)
    if (channel.Supports(context))
        await channel.SendAsync(context);
```

A partir de este punto, `RegisterUser` está **cerrado**. Agregar WhatsApp,
o cualquier canal futuro, es una operación puramente aditiva: una clase
nueva, cero modificaciones al núcleo.

La justificación técnica detallada está documentada en el
[ADR-0002](../../../adrs/0002-strategy-seleccion-canal.md).