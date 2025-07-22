
#  Survival Multiplayer – Proyecto Unity

**Survival Multiplayer** es un juego multijugador estilizado, de estética low-poly, centrado en la supervivencia en un mundo natural hostil. Los jugadores se mueven libremente, recolectan recursos, se enfrentan entre sí en combate cuerpo a cuerpo y exploran un paisaje dinámico que evoluciona según sus acciones.

##  Características principales

- **Movimiento y animaciones personalizadas** sincronizadas con las entradas del jugador.
- **Recolección de recursos**: madera, piedra y fibra interactuables mediante zonas activas.
- **Sistema de combate** básico entre jugadores con animaciones de ataque y daño.
- **Gestión dinámica del paisaje** que genera nuevos recursos cuando se agotan.
- **Interacción en red con Mirror**, sincronizando estado de jugadores y recursos.

##  Arquitectura de código

### PlayerBehabeour.cs  
Controla el comportamiento del jugador local y en red:
- Movimiento, rotación y salto.
- Ataque e interacción.
- Animaciones adaptadas según dirección de movimiento.
- Sincronización de vida y daño.
- Activación de zonas (`attackZone`, `interactZone`) para realizar acciones.

### SurvivalNetworkManager.cs  
Extiende `NetworkManager` de Mirror:
- Control de conexión de clientes y servidores.
- Cambios de escena automáticos.
- Hooks personalizados para manejar eventos de red.

### MaterialManager.cs  
Gestión de recursos recolectables:
- Vida del material (`resourceLife`) y destrucción al ser recolectado.
- Comunicación con `LandscapeManager` para actualizar el entorno.

### LandscapeManager.cs  
Supervisión y regeneración del entorno:
- Crea nuevos recursos si la cantidad cae por debajo del mínimo.
- Utiliza vértices de la malla para distribuir los objetos en el paisaje.
- Controla el delay de creación para mantener fluidez en el juego.

### InteractZoneTrigger.cs  
Detecta colisiones dentro de zonas de interacción:
- Al tocar un objeto con tag `"HarvestMaterial"`, aplica daño usando el valor del jugador.

## 🖼 Estilo visual

- Estética **low-poly** clara y vibrante.
- Personajes con siluetas definidas y colores contrastantes.
- Entorno modular diseñado para destacar recolección y navegación.

##  Ideas futuras

- Sistema de crafteo e inventario.
- Construcción de refugios.
- Estados del jugador (vida, energía, hambre).
- UI para interacciones, salud y recursos.

##  Tecnologías utilizadas

| Herramienta      | Uso                         |
|------------------|-----------------------------|
| Unity            | Motor de desarrollo principal |
| Mirror           | Gestión de red multijugador |
| Cinemachine      | Cámara personalizada por jugador |
| TextMeshPro      | Etiquetas y textos elegantes |
| InputSystem      | Captura de entradas |

---

¿Quieres que le agregue una sección sobre la narrativa o el tono humorístico que te gusta transmitir? También puedo ayudarte a adaptarlo para GitHub con badges o instrucciones de compilación si lo necesitas.
