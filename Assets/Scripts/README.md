# Zaxxon Prototype - Unity 6
## Guía de Setup Rápido (3 Días)

### ✅ Scripts Creados

Todos los scripts necesarios han sido generados con código procedural. No necesitas diseñar nada manualmente en Unity.

#### Scripts de Jugador:
- `PlayerController.cs` - Movimiento y disparo del jugador
- `PlayerShadow.cs` - Sombra proyectada en el suelo
- `HeightIndicator.cs` - UI del indicador de altura

#### Scripts de Nivel:
- `ProceduralSegmentGenerator.cs` - Genera segmentos con código (7 patrones diferentes)
- `LevelManager.cs` - Gestiona el reciclaje y movimiento de segmentos
- `LevelBoundaries.cs` - Crea muros invisibles laterales

#### Scripts de Enemigos y Combate:
- `EnemyTurret.cs` - Torres que disparan al jugador
- `Bullet.cs` - Proyectiles del jugador y enemigos
- `Destructible.cs` - Sistema de vida para obstáculos
- `BulletFactory.cs` - Crea prefabs de balas proceduralmente

#### Scripts de Gestión:
- `GameManager.cs` - Puntuación, vida, game over
- `CameraSetup.cs` - Cámara isométrica
- `GameSetup.cs` - **Script automático que configura toda la escena**

---

## 🚀 PASOS PARA CONFIGURAR EL JUEGO

### Paso 1: Crear Tags Necesarios
En Unity, ve a: **Edit > Project Settings > Tags and Layers**

Añade estos tags:
1. `Player`
2. `Enemy`
3. `Obstacle`
4. `Boundary`
5. `PlayerBullet`
6. `EnemyBullet`

### Paso 2: Configuración Automática
1. Crea un GameObject vacío en tu escena
2. Nómbralo `GameSetup`
3. Añádele el script `GameSetup.cs`
4. En el Inspector, asegúrate que `Auto Setup Scene` esté marcado
5. **Da Play** - La escena se configurará automáticamente

**O manualmente:**
- Click derecho en `GameSetup` en el Hierarchy
- Selecciona: **Setup Complete Scene** del menú contextual

### Paso 3: Configurar Input (si es necesario)
Ve a: **Edit > Project Settings > Input Manager**

Asegúrate que existan:
- **Horizontal**: A/D o Flechas Izquierda/Derecha
- **Vertical**: W/S o Flechas Arriba/Abajo  
- **Fire1**: Space o Click Izquierdo

(Por defecto Unity ya tiene estos configurados)

---

## 🎮 CONTROLES

- **A/D o ←/→**: Mover horizontalmente
- **W/S o ↑/↓**: Mover verticalmente (altura)
- **Espacio o Click Izquierdo**: Disparar

---

## 📋 PATRONES DE SEGMENTOS GENERADOS

El sistema genera 7 tipos de segmentos proceduralmente:

1. **Vacío** - Solo suelo, área de descanso
2. **Torres Simples** - 2 torres enemigas
3. **Muros Bajos** - Obstáculos a nivel del suelo
4. **Bloques Flotantes** - Obstáculos en el aire
5. **Túnel** - Sección cerrada con techo
6. **Mixto** - Combinación de obstáculos
7. **Zigzag** - Bloques en patrón zigzag vertical

Los segmentos se reciclan automáticamente y la dificultad aumenta progresivamente.

---

## 🔧 CARACTERÍSTICAS IMPLEMENTADAS

✅ Nave con movimiento 2D (horizontal y vertical)  
✅ Nivel que avanza automáticamente  
✅ Indicador de altura visual  
✅ Sombra proyectada en el suelo  
✅ Generación procedural de niveles  
✅ Torres enemigas que disparan  
✅ Sistema de disparo del jugador  
✅ Colisiones y daño  
✅ Sistema de puntuación  
✅ Game Over con reinicio  
✅ Muros invisibles laterales  
✅ Velocidad progresiva  
✅ Cámara isométrica  

---

## ⚙️ AJUSTES OPCIONALES

### Modificar Velocidad del Nivel:
En `LevelManager`:
- `scrollSpeed` - Velocidad inicial (default: 5)

En `GameManager`:
- `startSpeed` - Velocidad inicial (default: 5)
- `maxSpeed` - Velocidad máxima (default: 15)
- `speedIncreaseRate` - Qué tan rápido aumenta (default: 0.5)

### Modificar Dificultad:
En `PlayerController`:
- `maxHealth` - Vida inicial (default: 3)
- `moveSpeed` - Velocidad de movimiento (default: 10)
- `fireRate` - Velocidad de disparo (default: 0.3)

En `EnemyTurret`:
- `fireRate` - Velocidad de disparo enemigo (default: 2)
- `detectionRange` - Rango de detección (default: 15)

### Modificar Generación de Nivel:
En `ProceduralSegmentGenerator`:
- `segmentLength` - Largo de cada segmento (default: 20)
- `segmentWidth` - Ancho del nivel (default: 18)

En `LevelManager`:
- Modifica el método `ChoosePattern()` para cambiar qué patrones aparecen y cuándo

---

## 🐛 SOLUCIÓN DE PROBLEMAS

**Problema: El jugador no se mueve**
- Verifica que el tag `Player` esté asignado
- Revisa que `PlayerController` esté en el GameObject

**Problema: No se generan segmentos**
- Verifica que `LevelManager` tenga el componente `ProceduralSegmentGenerator`
- Revisa la consola por errores de materiales

**Problema: Las balas no funcionan**
- Asegúrate que todos los tags estén creados
- Verifica que los colliders sean `Trigger`

**Problema: Game Over no funciona**
- Verifica que `GameManager` tenga las referencias UI asignadas
- Revisa que el panel `GameOverPanel` exista en el Canvas

**Problema: Las torres no disparan**
- Asegúrate que el jugador tenga el tag `Player`
- Verifica que `EnemyTurret` esté en los objetos torre

---

## 📝 NOTAS DE DESARROLLO

- Todo está hecho con **primitivas 3D** (Cube, Sphere, Cylinder, Quad)
- Los segmentos se generan **100% con código**, no necesitas diseñar nada manualmente
- El sistema **recicla segmentos** automáticamente para optimizar performance
- Los materiales se crean **proceduralmente** con colores simples
- El script `GameSetup` puede configurar **toda la escena automáticamente**

---

## 🎯 PRÓXIMOS PASOS OPCIONALES (Si tienes tiempo extra)

1. **Audio**: Añadir efectos de sonido simples
2. **Partículas**: Mejorar las explosiones con particle systems
3. **Power-ups**: Añadir coleccionables que den beneficios
4. **Boss Fight**: Crear un segmento especial con un jefe
5. **Menu**: Añadir menú principal
6. **Diferentes tipos de disparo**: Triple shot, spread, etc.

---

## 📦 ESTRUCTURA DE ARCHIVOS

```
Assets/
└── Scripts/
    ├── PlayerController.cs
    ├── PlayerShadow.cs
    ├── HeightIndicator.cs
    ├── ProceduralSegmentGenerator.cs
    ├── LevelManager.cs
    ├── LevelBoundaries.cs
    ├── EnemyTurret.cs
    ├── Bullet.cs
    ├── Destructible.cs
    ├── BulletFactory.cs
    ├── GameManager.cs
    ├── CameraSetup.cs
    └── GameSetup.cs
```

---

## ✨ ¡Listo para Probar!

1. Abre Unity
2. Crea los Tags necesarios
3. Crea un GameObject con el script `GameSetup`
4. Da Play
5. ¡Disfruta tu prototipo de Zaxxon!

---

**Tiempo estimado de setup: 15-30 minutos**  
**Desarrollo total: 3 días** ✅
