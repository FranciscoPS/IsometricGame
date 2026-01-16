# Guía Completa: Implementación de Disparos y Colisiones

## ✅ PASO 1: Configurar Tags en Unity

1. En Unity, ve al menú: **`Edit > Project Settings > Tags and Layers`**
2. En la sección **Tags**, haz clic en el **+** para agregar cada uno:
   - Agregar Tag: `Player`
   - Agregar Tag: `Enemy`
   - Agregar Tag: `Obstacle`
   - Agregar Tag: `Boundary`
   - Agregar Tag: `PlayerBullet`
   - Agregar Tag: `EnemyBullet`
3. Cierra la ventana de Project Settings

---

## ✅ PASO 2: Configurar el GameObject Player

### A. Seleccionar el Player
1. En la **Hierarchy**, busca y selecciona el GameObject llamado **"Player"**

### B. Asignar Tag
1. En el **Inspector**, arriba junto al nombre, verás un dropdown que dice **"Untagged"**
2. Haz clic y selecciona **"Player"**

### C. Agregar Box Collider
1. Con el Player aún seleccionado, en el Inspector haz clic en **`Add Component`**
2. Busca y agrega: **`Box Collider`**
3. En el componente Box Collider que aparece:
   - ✅ **MARCA** el checkbox **`Is Trigger`**
   - Center: (0, 0, 0) - dejar como está
   - Size: (1, 1, 1.5) - cambiar solo el Z a 1.5

### D. Crear Mesh Visual del Player (si no lo tienes)
1. Clic derecho en "Player" en Hierarchy > **`3D Object > Cube`**
2. Este cubo hijo se llama "Cube", déjalo así
3. Selecciona el Cube hijo:
   - Scale: (0.8, 0.5, 1.2)
   - Position: (0, 0, 0)

### E. Verificar que NO necesitas crear bullets manualmente
❌ **NO HAGAS ESTO:** NO necesitas crear prefabs de balas
✅ **El código los crea automáticamente** cuando presionas Play

---

## ✅ PASO 3: Crear Boundaries (Límites Laterales)

### A. Crear GameObject
1. En Hierarchy, clic derecho > **`Create Empty`**
2. Nombrar: **"Boundaries"**
3. Position: (0, 0, 0)

### B. Agregar Script
1. Con "Boundaries" seleccionado, en Inspector: **`Add Component`**
2. Buscar: **`LevelBoundaries`** y agregarlo

### C. Configurar en Inspector
En el componente LevelBoundaries:
- Left Limit: **-9**
- Right Limit: **9**
- ❌ **DESMARCAR** Show Visual Walls (debe estar en **false**)

---

## ✅ PASO 4: Configurar el LevelManager

1. En Hierarchy, selecciona **"LevelManager"**
2. Verifica en Inspector que tenga:
   - `LevelManager` script ✅
   - `ProceduralSegmentGenerator` script ✅
3. En LevelManager, verifica valores:
   - Segment Length: **20**
   - Active Segments: **5**
   - Scroll Speed: **5**

---

## ✅ PASO 5: Verificar el GameManager y UI

### A. Verificar que existan los textos UI
1. En Hierarchy, expande **"Canvas"**
2. Debe tener estos hijos (los creaste antes):
   - **ScoreText** (TextMeshPro)
   - **HealthText** (TextMeshPro)
   - **HeightIndicator** (Slider)
   - **GameOverText** (TextMeshPro, desactivado)

### B. Si NO tienes HealthText, créalo ahora:
1. Clic derecho en Canvas > **`UI > Text - TextMeshPro`**
2. Nombrar: **"HealthText"**
3. En Inspector:
   - Text: "Health: 3"
   - Anchor: Top-Left
   - Pos X: 10, Pos Y: -40
   - Font Size: 24
   - Color: Blanco

### C. Agregar botón Restart al GameOverText
1. Selecciona **"GameOverText"** en Canvas
2. En Inspector, abajo del componente TextMeshPro:
   - **`Add Component > Button`** (UI Button)
3. En el componente Button:
   - Interactable: ✅ Marcado
4. Clic derecho en GameOverText > **`UI > Text - TextMeshPro`** (crear hijo)
5. Nombrar este texto hijo: **"ButtonLabel"**
6. En ButtonLabel:
   - Text: "Click to Restart"
   - Font Size: 18
   - Color: Amarillo
   - Alignment: Center

### D. Conectar el botón Restart
1. Selecciona **"GameOverText"** (el padre con el Button)
2. En el componente **Button**, busca la sección **OnClick()**
3. Haz clic en el **+** para agregar un evento
4. Arrastra **"GameManager"** desde la Hierarchy al campo del evento (donde dice "None (Object)")
5. En el dropdown de la derecha (donde dice "No Function"):
   - Selecciona **`GameManager > RestartGame()`**

### E. Verificar GameManager
1. Selecciona **"GameManager"** en Hierarchy
2. Verifica que tenga el script `GameManager`
3. ❌ **NO NECESITAS** arrastrar nada al Inspector
4. ✅ El script encuentra los elementos UI automáticamente por nombre

---

## ✅ PASO 6: PRESIONA PLAY - Primera Prueba

### Ahora presiona el botón ▶️ Play

### ¿Qué debería pasar?
1. ✅ El nivel se mueve hacia ti automáticamente
2. ✅ Ves piso verde continuo sin espacios
3. ✅ Aparecen torres rojas (cilindros)
4. ✅ Aparecen obstáculos grises y marrones
5. ✅ En la esquina superior izquierda ves: "Score: 0" y "Health: 3"

### ¿Qué NO debería pasar?
- ❌ Errores en consola sobre NullReferenceException
- ❌ Warnings sobre Input System (ya los arreglamos)
- ❌ Espacios entre segmentos

---

## ✅ PASO 7: Probar Controles del Player

Con el juego en Play:

### Test de Movimiento:
1. Presiona **W** → Player sube
2. Presiona **S** → Player baja
3. Presiona **A** → Player va a la izquierda (se detiene en el borde)
4. Presiona **D** → Player va a la derecha (se detiene en el borde)
5. También prueba las **flechas** ← ↑ → ↓

### Test de Disparos:
1. Presiona **Espacio**
2. ✅ Debe aparecer una **esfera azul/cyan pequeña** que sale hacia adelante
3. Presiona Espacio varias veces rápido
4. ✅ Debes ver múltiples balas azules

### ¿No ves las balas?
- Verifica en la **Scene view** (pestaña al lado de Game)
- Las balas son pequeñas (0.3 unidades) y rápidas
- Si sigues sin verlas, revisa la consola por errores

---

## ✅ PASO 8: Probar Sistema de Torres

### Acércate a una Torre:
1. Mueve el Player cerca de un **cilindro rojo** (torre)
2. Espera 2 segundos
3. ✅ La torre debe **disparar balas rojas** hacia ti
4. Las balas rojas van en tu dirección

### ¿La torre no dispara?
Posibles causas:
- Estás muy lejos (rango = 15 unidades)
- Estás **detrás** de la torre (solo dispara hacia adelante)
- El Player no tiene Tag "Player"

---

## ✅ PASO 9: Probar Sistema de Daño

### Test: Destruir una Torre
1. Colócate frente a una torre roja
2. Presiona **Espacio** 3 veces (apuntando a la torre)
3. ✅ Al tercer disparo, la torre debe:
   - Mostrar una **esfera amarilla** (explosión)
   - **Desaparecer**
   - **Score aumenta** a 50

### Test: Destruir un Obstáculo
1. Dispara a un **cubo gris** (muro) o **cubo marrón** (bloque flotante)
2. Necesitas 2 disparos
3. ✅ Se destruye con explosión amarilla
4. ✅ Score aumenta +15 o +20

### ¿No se destruyen?
- Verifica que las balas azules estén tocando el objetivo
- Revisa la consola por errores
- Verifica que el Player tenga Tag "Player"

---

## ✅ PASO 10: Probar Sistema de Vida del Player

### Test: Recibir Daño
1. Deja que una **bala roja** (de la torre) te golpee
2. ✅ En la UI arriba izquierda: "Health: 3" cambia a "Health: 2"
3. Recibe más daño hasta llegar a 0
4. ✅ Aparece texto grande rojo: **"GAME OVER"**

### Test: Colisión con Obstáculos
1. Choca intencionalmente con un **muro gris**
2. ✅ Pierdes 1 HP
3. ✅ El muro sigue ahí (no se destruye solo por tocarlo)

---

## 📋 CHECKLIST FINAL - Verifica Cada Punto

Antes de reportar un problema, verifica:

**Tags configurados:**
- [ ] Tag "Player" existe
- [ ] Tag "Enemy" existe  
- [ ] Tag "Obstacle" existe
- [ ] Tag "PlayerBullet" existe
- [ ] Tag "EnemyBullet" existe

**GameObject Player:**
- [ ] Tiene Tag "Player" asignado
- [ ] Tiene componente `PlayerController`
- [ ] Tiene componente `Box Collider` con Is Trigger = true
- [ ] Tiene componente `Rigidbody`
- [ ] Tiene un cubo hijo visible (mesh)

**GameObject LevelManager:**
- [ ] Tiene `LevelManager` script
- [ ] Tiene `ProceduralSegmentGenerator` script
- [ ] Segment Length = 20
- [ ] Active Segments = 5
- [ ] El nivel se mueve cuando presionas Play

**GameObject GameManager:**
- [ ] Existe en la escena
- [ ] Tiene script `GameManager`
- [ ] Los campos UI están vacíos en Inspector (se llenan automáticamente)

**Canvas y UI:**
- [ ] Existe "ScoreText" (TextMeshPro)
- [ ] Existe "HealthText" (TextMeshPro)
- [ ] Existe "GameOverText" (TextMeshPro, desactivado inicialmente)
- [ ] Existe "HeightIndicator" (Slider)

**GameObject Boundaries:**
- [ ] Existe en la escena
- [ ] Tiene script `LevelBoundaries`
- [ ] Show Visual Walls = false

**Input System:**
- [ ] Archivo `ZaxxonInputActions.inputactions` existe en Assets
- [ ] Tiene "Generate C# Class" marcado
- [ ] Se generó archivo `InputSystem_Actions.cs`

---

## 🎮 CONTROLES FINALES

| Tecla | Acción |
|-------|--------|
| **W / ↑** | Subir |
| **S / ↓** | Bajar |
| **A / ←** | Izquierda |
| **D / →** | Derecha |
| **Espacio** | Disparar |

---

## ⚠️ IMPORTANTE: Cosas que NO Necesitas Crear

❌ **NO NECESITAS:**
- Crear prefabs de balas manualmente
- Arrastrar bullets al Player Inspector
- Crear materiales manualmente
- Configurar Rigidbody del Player (el script lo hace)
- Crear FirePoint manualmente (el script lo crea)
- Arrastrar referencias UI al GameManager (se encuentran por nombre)

✅ **El código crea automáticamente:**
- Balas del jugador (azul/cyan)
- Balas enemigas (rojas)
- Materiales de los objetos
- Explosiones amarillas
- Todos los componentes necesarios

---

## ✅ Sistema de Vidas y Respawn Implementado

### Cómo Funciona:

**Player tiene 3 vidas (HP):**
- Vida inicial: **3 HP**
- Cada colisión o bala enemiga: **-1 HP**

**Cuando recibes daño (pero NO mueres):**
1. ✅ Explosión naranja/roja en tu posición
2. ✅ Player desaparece por 1 segundo
3. ✅ Reaparece en posición inicial (0, 3, 0)
4. ✅ Parpadea durante 2 segundos (invulnerable)
5. ✅ Durante el parpadeo NO puedes recibir daño
6. ✅ El juego continúa moviéndose

**Cuando llegas a 0 HP (Game Over):**
1. ❌ Se detiene el scroll del nivel
2. ❌ Aparece "GAME OVER"
3. ❌ Ya NO puedes moverte ni disparar
4. ❌ Debes reiniciar el juego

### Verificaciones en Unity:

**Si el juego se pausa después de 1 o 2 golpes:**
- Verifica que el Player tenga `Max Health = 3` en Inspector
- Algunos objetos del nivel pueden tener tags incorrectos

**Si los objetos tienen tag "Wall" en lugar de "Obstacle":**
1. Los objetos se crean dinámicamente con el tag correcto
2. Si ves "Wall" como tag en Inspector durante Play, es solo el nombre del GameObject
3. El tag debe decir "Obstacle" arriba en el Inspector

### Colisión Player vs Obstáculos
```
Player toca Obstacle → Player pierde 1 HP → UI se actualiza
Player toca Enemy → Player pierde 1 HP
Player toca EnemyBullet → Player pierde 1 HP → Bala se destruye
```

### Colisión Balas del Player vs Enemigos
```
PlayerBullet toca Enemy → Enemy pierde HP → +50 puntos
PlayerBullet toca Obstacle → Obstacle pierde HP → +20 puntos
Enemy llega a 0 HP → Explosión amarilla → Se destruye
```

### Sistema de Vidas
- Player inicia con **3 HP**
- Al llegar a 0 HP → Game Over
- GameOverText aparece en pantalla

---

## ✅ PASO 6: Testing Completo

### Test 1: Movimiento
1. Presiona **Play**
2. Usa **WASD** o **Flechas** para mover
3. Verifica que no puedes salir de los límites laterales
4. Verifica altura mínima y máxima

### Test 2: Disparos del Player
1. Presiona **Espacio** repetidamente
2. Deberías ver esferas azules saliendo hacia adelante
3. Las balas desaparecen después de 5 segundos

### Test 3: Torres Disparando
1. Acércate a una torre (cilindro rojo)
2. La torre debería disparar balas rojas hacia ti
3. Las balas te persiguen

### Test 4: Destruir Torres
1. Dispara a una torre (3 disparos)
2. Debería aparecer explosión amarilla
3. La torre se destruye
4. Score aumenta +50 puntos

### Test 5: Destruir Obstáculos
1. Dispara a un muro gris o bloque marrón (2 disparos)
2. Se destruye con explosión
3. Score aumenta +15 o +20 puntos

### Test 6: Recibir Daño
1. Deja que una bala enemiga te golpee
2. Health debe bajar en UI (arriba izquierda)
3. Al llegar a 0: "GAME OVER" aparece

### Test 7: Colisión con Escenario
1. Choca intencionalmente con un muro
2. Pierdes 1 HP
3. El muro sigue ahí (no se destruye por colisión)

---

## 🎮 CONTROLES FINALES

| Tecla | Acción |
|-------|--------|
| **W / ↑** | Subir |
| **S / ↓** | Bajar |
| **A / ←** | Izquierda |
| **D / →** | Derecha |
| **Espacio** | Disparar |

---

## 📊 VALORES DE BALANCE ACTUAL

### Player
- Velocidad: 10 unidades/s
- Vida: 3 HP
- Cadencia: 0.3s (3.3 disparos/segundo)
- Daño por disparo: 1

### Torres (Enemigos)
- Vida: 3 HP
- Cadencia: 2s (0.5 disparos/segundo)
- Rango detección: 15 unidades
- Daño por disparo: 1
- Puntos al destruir: 50

### Obstáculos
- Vida: 2 HP
- Puntos al destruir: 15-20
- Daño por colisión: 1

### Nivel
- Velocidad scroll: 5 unidades/s
- Segmentos activos: 5
- Longitud segmento: 20 unidades

---

## 🔧 AJUSTES OPCIONALES

### Para hacer el juego más difícil:
1. En **LevelManager**: Aumentar `scrollSpeed` a 7-10
2. En **ProceduralSegmentGenerator**: Torres con más HP
3. En **EnemyTurret**: Reducir `fireRate` a 1.5s

### Para hacer el juego más fácil:
1. En **PlayerController**: Aumentar `maxHealth` a 5
2. En **LevelManager**: Reducir `scrollSpeed` a 3
3. En **EnemyTurret**: Aumentar `fireRate` a 3s

---

## ⚠️ PROBLEMAS COMUNES Y SOLUCIONES

### "Algunos cubos no me hacen daño / Los atravieso"

**Causa:** El Player o los obstáculos no tienen colliders correctamente configurados.

**Solución paso a paso:**

1. **Verificar el Player:**
   - Selecciona "Player" en Hierarchy
   - En Inspector, verifica:
     - ✅ **Box Collider** existe
     - ✅ **Is Trigger = TRUE** (DEBE estar marcado)
     - ✅ Size: (1, 1, 1.5)
     - ✅ **Rigidbody** existe
     - ✅ Use Gravity = FALSE
     - ✅ Is Kinematic = FALSE

2. **Diagnosticar colisiones:**
   - Agrega el script **`CollisionDebugger`** al Player
   - Presiona Play
   - Abre la ventana **Console** (Ctrl+Shift+C o Cmd+Shift+C)
   - Choca con objetos
   - Deberías ver: `[COLLISION] Player hit: Wall | Tag: Obstacle | IsTrigger: true`
   - Si NO ves mensajes → El collider del Player está mal configurado

3. **Verificar obstáculos durante Play:**
   - Con el juego corriendo, en Hierarchy expande "LevelManager"
   - Expande un "Segment_X"
   - Selecciona un "Wall" o "FloatingBlock"
   - En Inspector verifica:
     - Tag: "Obstacle" ✅
     - Box Collider: Is Trigger = true ✅

### "Después de Game Over aún puedo moverme"

**Arreglado:** Ahora el movimiento y disparo se desactivan cuando `isDead = true`.

### "Chocar con enemigos (torres) no me hace daño"

**Arreglado:** Ahora las colisiones con Tag "Enemy" también quitan 1 HP.

### "Las balas no hacen daño"
- ✅ Verifica que los Tags estén asignados
- ✅ Verifica que los Colliders tengan `Is Trigger = true`

### "Las torres no disparan"
- ✅ Asegúrate que el Player tenga Tag "Player"
- ✅ Verifica que estás dentro del rango (15 unidades)
- ✅ Debes estar adelante de la torre (menor Z)

### "El Player no recibe daño"
- ✅ Player debe tener Box Collider con `Is Trigger = true`
- ✅ Balas enemigas deben tener Tag "EnemyBullet"

### "No veo el score/health en UI"
- ✅ Asegúrate de haber creado los TextMeshPro con nombres exactos
- ✅ GameManager debe estar en la escena

---

## ✨ SIGUIENTE NIVEL

Ya tienes un juego completamente funcional. Para mejorarlo:

1. **Sonidos:** Agregar audio para disparos y explosiones
2. **Efectos:** Partículas para explosiones y disparos
3. **Power-ups:** Vida extra, escudo, disparo triple
4. **Boss Fight:** Segmento especial con jefe final
5. **Menú:** Pantalla de inicio y selección de dificultad
6. **Persistencia:** Guardar high score con PlayerPrefs

