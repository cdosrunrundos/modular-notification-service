# ADR-0001: Desacoplamiento de Canales de Notificación mediante Inversión de Dependencias

## Estado
Aceptado

## Contexto
El sistema del Capítulo 1 acoplaba directamente el caso de uso `RegisterUser` con la clase concreta `EmailService`. Ante el nuevo requerimiento de soportar múltiples canales de comunicación (Email, Push, SMS) con reglas condicionales variables, mantener el diseño actual obligaría a modificar el núcleo de la lógica de negocio ante cada nuevo canal, violando los principios SRP y OCP.

## Alternativas Evaluadas

### Alternativa 1: Orquestación mediante condicionales en el Caso de Uso (Descartada)
Consiste en importar `EmailService`, `PushService` y `SmsService` dentro de `RegisterUser` y decidir mediante un `switch` cuál ejecutar.
* **Pros:** Rápido de implementar a corto plazo.
* **Contras:** Alto acoplamiento. Modificar o añadir un canal rompe el caso de uso. Complejidad ciclomática ascendente.

### Alternativa 2: Introducción de una Abstracción de Canal (Seleccionada)
Definir una interfaz común (`INotificationChannel`) que encapsule la acción de enviar un mensaje. El caso de uso dependerá únicamente de esta interfaz.
* **Pros:** Desacoplamiento total. El caso de uso se vuelve agnóstico a la infraestructura. Cumple con DIP y OCP.
* **Contras:** Introduce una ligera complejidad cognitiva inicial debido a la creación de nuevos archivos y contratos.

## Decisión
Adoptar la **Alternativa 2**. Se creará la interfaz `INotificationChannel` en C# y se utilizará una factoría o inyección de dependencias para proveer la implementación requerida en tiempo de ejecución.

## Consecuencias
* **Positivas:** El caso de uso `RegisterUser` queda cerrado a modificaciones si el día de mañana se cambia el proveedor de SMS o se agrega WhatsApp. Las pruebas unitarias se vuelven viables mediante el uso de *Mocks* o *Stubs*.
* **Negativas:** Incremento en el número de clases y archivos en la solución.