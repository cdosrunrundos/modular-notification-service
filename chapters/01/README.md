# Capítulo 1: Implementación Inicial (KISS)

## Contexto y Requerimiento
El sistema requiere enviar un correo electrónico de bienvenida a los usuarios cuando se registran en la plataforma. 

## Análisis de Diseño
Para resolver este problema, se ha aplicado el principio **KISS (Keep It Simple, Stupid)**. El código es directo, modular y no introduce abstracciones prematuras:
* `RegisterUser` (Caso de uso) conoce e instancia directamente a `EmailService`.
* No existen interfaces (`IEmailService`), fábricas ni contenedores de inversión de control (IoC).

### Justificación Técnica (YAGNI)
Introducir una interfaz en este punto violaría el principio **YAGNI (You Ain't Gonna Need It)**. Dado que el negocio solo requiere enviar correos electrónicos y no hay indicios de otros canales, crear una abstracción sería sobreingeniería. El diseño actual es correcto porque resuelve el problema con el menor costo de mantenimiento y la menor complejidad cognitiva posible.

## Limitaciones (El dolor futuro)
Aunque este código es limpio y correcto para el contexto actual, presenta un **alto acoplamiento**. La lógica de negocio está atada a un detalle de infraestructura (el envío de emails por SMTP). Si el negocio exige cambios en los canales de comunicación en el futuro, este diseño comenzará a sufrir fricción.