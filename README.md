# Modular Notification Service

Este repositorio contiene la implementación evolutiva de un Servicio de Notificaciones. El objetivo del proyecto es puramente pedagógico: demostrar cómo un sistema de software muta desde una implementación muy simple y concreta hacia una arquitectura modular y escalable a medida que cambian los requerimientos del negocio.

## Propósito del Proyecto

Este software no pretende ser un **boilerplate** listo para producción, ni una librería. Simplemente es una práctica en donde:

1- Se introduce un requerimiento de negocio o una restricción técnica.
2- Se analiza el impacto y la fricción sobre el diseño anterior.
3- Se refactoriza el código aplicando principios de diseño específicos (SOLID, KISS, YAGNI, DRY, Clean Architecture, etc.).
4- Se documenta la justificación técnica mediante un ADR.
5- Se aplica la técnica Baseline Commit para estructurar la transición entre capítulos.

## Glosario Técnico

### ¿En que es la técnica Baseline Commit?

Antes de aplicar algún refactor de código, o un nuevo requerimiento dentro de un **Chapter** (ej. ***chapter 2***), el código fuente del ***chapter 1*** es copiado integramente en una nueva carpeta, y es comiteado a git sin modificar.

### Propósito técnico del Baseline Commit

Aislar las operaciones estructurales de las operaciones funcionales. Si se creara la carpeta y se modificara el código en un mismo bloque, el algoritmo de Git registraría el nuevo capítulo como una adición masiva de archivos creados desde cero.

### Beneficio para el lector del repositorio

Al forzar este commit intermedio se produce un Clean Diff. Esto permite que, al hacer clic en el enlace de comparación de GitHub, el lector pueda visualizar con precisión —línea por línea y archivo por archivo— qué código cambió, qué se eliminó y qué patrones se introdujeron durante la refactorización, eliminando el ruido visual de la migración de carpetas.


