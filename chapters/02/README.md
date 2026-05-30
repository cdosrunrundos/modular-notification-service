# Capítulo 2: Abstracción e Inversión de Dependencias (DIP y OCP)

## El Cambio del Negocio (Requerimiento)
El departamento de marketing exige expandir los canales de comunicación. Ahora, el sistema debe ser capaz de enviar notificaciones por múltiples vías según el contexto del registro:
1. **Web:** Se envía un **Email** de bienvenida.
2. **App Móvil:** Se envía una notificación **Push**.
3. **Usuario VIP:** Independientemente del canal de origen, se debe enviar un **SMS** prioritario.

## El Problema del Diseño Anterior (Fricción Técnica)
Si intentáramos resolver este requerimiento utilizando la estructura del **Capítulo 1 (KISS)**, nos enfrentaríamos a los siguientes problemas de diseño:
* **Violación de SRP (Single Responsibility Principle):** El caso de uso `RegisterUser` tendría que encargarse de instanciar tres clientes de infraestructura distintos (`EmailService`, `PushService`, `SmsService`).
* **Violación de OCP (Open/Closed Principle):** Cada vez que el negocio quiera agregar un nuevo canal (ej. WhatsApp), estaríamos obligados a modificar el código interno del caso de uso agregando bloques condicionales (`if/else` o `switch`).
* **Alto Acoplamiento:** La lógica de negocio central sigue dependiendo de detalles de implementación volátiles.

## Solución Aplicada
Para resolver este problema de forma escalable, se decidió desacoplar el caso de uso de las implementaciones concretas mediante la introducción de una abstracción (`INotificationChannel`) y la aplicación del **Principio de Inversión de Dependencias (DIP)**.

La justificación técnica detallada, las alternativas evaluadas y las consecuencias de este cambio de arquitectura están documentadas en el [ADR-0001](../../../adrs/0001-desacoplamiento-canales-notificacion.md).