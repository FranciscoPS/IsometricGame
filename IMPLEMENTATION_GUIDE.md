# Guía de Implementación - Zaxxon Prototype

## Configuración Inicial de la Escena

### 1. Crear el Player
1. Crear GameObject vacío: `GameObject > Create Empty`
2. Nombrar: "Player"
3. Tag: "Player"
4. Position: (0, 3, 0)
5. Agregar componentes:
   - `PlayerController` script
   - `PlayerShadow` script
   - Crear hijo Cube para visualización:
     - Scale: (0.8, 0.5, 1.2)
     - Agregar Rigidbody (se configura automáticamente)

### 2. Crear el Level Manager
1. Crear GameObject vacío: "LevelManager"
2. Position: (0, 0, 0)
3. Agregar componentes:
   - `LevelManager` script
   - `ProceduralSegmentGenerator` script (se agrega automáticamente)
4. Configuración en Inspector:
   - Segment Length: 20
   - Active Segments: 3
   - Scroll Speed: 5

### 3. Crear la Cámara
1. Seleccionar Main Camera
2. Agregar `CameraSetup` script
3. El script configura automáticamente:
   - Position: (10, 15, -10)
   - Rotation: (35, -45, 0)
   - Projection: Perspective

### 4. Crear UI
1. `GameObject > UI > Canvas`
2. Dentro del Canvas crear:
   - **Text - TextMeshPro** nombrado "ScoreText" 
     - Anchor: Top-Left
     - Position: (10, -10) desde esquina
     - Text: "Score: 0"
   - **Slider** nombrado "HeightIndicator"
     - Anchor: Right-Center
     - Width: 200, Height: 30
   - **Text - TextMeshPro** nombrado "GameOverText"
     - Anchor: Center
     - Font Size: 48
     - Color: Rojo
     - Text: "GAME OVER"
     - Desactivar inicialmente
   - **Text - TextMeshPro** nombrado "HealthText"
     - Anchor: Top-Left
     - Position: (10, -40) desde esquina
     - Text: "Health: 3"

> **Nota:** Si Unity pregunta "Import TMP Essentials", haz clic en "Import TMP Essentials" primero.

### 5. Crear Game Manager
1. Crear GameObject vacío: "GameManager"
2. Agregar `GameManager` script
3. El script encuentra automáticamente los elementos UI por nombre

### 6. Configurar Input Actions
1. En Project, el archivo `ZaxxonInputActions.inputactions` ya existe
2. Seleccionarlo y asegurarse que esté configurado:
   - WASD para movimiento
   - Flechas para movimiento
   - Espacio para disparar

### 7. Crear Bullet Factory
1. Crear GameObject vacío: "BulletFactory"
2. Agregar `BulletFactory` script
3. Se crean automáticamente los prefabs de balas

## Controles

- **WASD / Flechas**: Mover nave (horizontal y vertical)
- **Espacio**: Disparar

## Mecánicas Implementadas

### Sistema de Nivel
- ✅ Generación procedural de 8 patrones diferentes
- ✅ Scroll automático continuo
- ✅ Reciclaje de segmentos (optimización)
- ✅ Segmentos pegados sin espacios
- ✅ Velocidad ajustable

### Jugador
- ✅ Movimiento horizontal y vertical
- ✅ Límites de movimiento
- ✅ Altura mínima/máxima
- ✅ Sistema de disparo
- ✅ Sombra en el suelo
- ✅ Sistema de vida (3 HP)

### Enemigos y Obstáculos
- ✅ Torres enemigas
- ✅ Paredes y obstáculos
- ✅ Sistema de daño
- ✅ Colisiones

### UI
- ✅ Indicador de puntuación
- ✅ Indicador de altura
- ✅ Pantalla de Game Over
- ✅ Sistema de restart

## Patrones de Nivel

1. **Empty**: Segmento vacío
2. **SimpleTowers**: Torres básicas
3. **LowWalls**: Muros bajos
4. **FloatingBlocks**: Bloques flotantes
5. **Tunnel**: Túnel estrecho
6. **Mixed**: Combinación de obstáculos
7. **Zigzag**: Patrón zigzag
8. **Random**: Generación aleatoria

## Progresión de Dificultad

- Segmentos 0-2: Patrones fáciles (0-1)
- Segmentos 3-9: Patrones medios (0-3)
- Segmentos 10+: Todos los patrones (1-6)

## Testing

### Probar Generación de Nivel
1. Agregar `LevelTest` script al LevelManager
2. Play
3. Usar menú contextual (clic derecho en componente):
   - Start Level Test
   - Stop Level Test
   - Increase/Decrease Speed

### Probar Juego Completo
1. Presiona Play
2. Usa WASD o flechas para mover
3. Espacio para disparar
4. Evita obstáculos
5. Destruye torres enemigas

## Notas Técnicas

- **URP Compatible**: Shaders configurados para Universal Render Pipeline
- **Input System**: Usa el nuevo Unity Input System
- **Sin Referencias Rotas**: Sistema automático de búsqueda de referencias
- **Optimizado**: Reciclaje de segmentos, sin instantiate excesivo

## Próximos Pasos Sugeridos

1. Ajustar velocidades y dificultad
2. Agregar efectos visuales (partículas)
3. Agregar sonidos
4. Crear más patrones de nivel
5. Sistema de power-ups
6. Menú principal
