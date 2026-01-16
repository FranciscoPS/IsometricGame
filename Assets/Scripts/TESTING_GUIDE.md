# 🧪 Testing de Generación de Segmentos

## Prueba Rápida - Solo Generación de Mapa

### Setup Mínimo (2 minutos):

1. **Crear Tags Básicos** (Edit > Project Settings > Tags):

   - `Enemy`
   - `Obstacle`

2. **Crear GameObject de Test**:

   - Click derecho en Hierarchy → Create Empty
   - Nómbralo: `SegmentTest`
   - Añade el script: `SegmentTester.cs`

3. **Configurar Cámara**:

   - Selecciona Main Camera
   - Posición: `(10, 15, -10)`
   - Rotación: `(35, -45, 0)`

4. **¡Dale Play!**

---

## 🎮 Opciones de Testing

### Opción 1: Ver Todos los Patrones (Automático)

En el Inspector del `SegmentTest`:

- ✅ Marca: `Generate All Patterns On Start`
- Click Play
- Verás los 8 patrones generados en línea

### Opción 2: Generar Patrones Manualmente

En el Inspector del `SegmentTest`:

- ❌ Desmarca: `Generate All Patterns On Start`
- Click Play
- Click derecho en `SegmentTest` en Hierarchy
- Selecciona una opción:
  - **Generate All Patterns** → Genera todos
  - **Generate Single Pattern** → Genera el patrón del slider
  - **Generate Random Pattern** → Genera uno aleatorio
  - **Clear All Segments** → Limpia la escena

### Opción 3: Probar Patrones Específicos

1. Desmarca `Generate All Patterns On Start`
2. En `Pattern To Generate`, elige un número (0-7):
   - **0** = Vacío (solo suelo)
   - **1** = Torres simples
   - **2** = Muros bajos
   - **3** = Bloques flotantes
   - **4** = Túnel
   - **5** = Mixto
   - **6** = Zigzag
   - **7** = Aleatorio
3. Click derecho → `Generate Single Pattern`

---

## 🎥 Navegación de Cámara

**En el Editor (Scene View):**

- Click derecho + arrastrar = Rotar cámara
- Rueda del mouse = Zoom
- Click medio + arrastrar = Desplazar (pan)
- F con objeto seleccionado = Enfocar

**Ver todos los segmentos:**

- Selecciona `SegmentTest` en Hierarchy
- Presiona `F` para enfocar todos los segmentos

---

## 📊 Lista de Patrones Generados

Cuando generas todos los patrones, verás:

```
Z=0   → Segment_0_Empty (solo suelo)
Z=25  → Segment_1_SimpleTowers (2 torres)
Z=50  → Segment_2_LowWalls (muros bajos)
Z=75  → Segment_3_FloatingBlocks (bloques en el aire)
Z=100 → Segment_4_Tunnel (túnel cerrado)
Z=125 → Segment_5_Mixed (combinado)
Z=150 → Segment_6_Zigzag (patrón zigzag)
Z=175 → Segment_7_Random (aleatorio)
```

---

## 🔧 Ajustes Disponibles

En el Inspector de `SegmentTest`:

- **Generate All Patterns On Start**: Genera todos al iniciar
- **Spacing**: Distancia entre segmentos (default: 25)
- **Pattern To Generate**: Patrón específico para generar

---

## 🎨 Colores de los Elementos

- **Verde oscuro** = Suelo
- **Gris** = Muros/Obstáculos
- **Rojo** = Torres enemigas
- **Azul oscuro** = Techos
- **Café** = Bloques flotantes

---

## ✅ Verificación Visual

**Lo que deberías ver:**

✓ Suelo verde en todos los segmentos  
✓ Torres rojas cilíndricas  
✓ Muros grises cúbicos  
✓ Bloques flotantes en el aire  
✓ Túnel con techo azul  
✓ Variedad de configuraciones

---

## 🐛 Si algo no funciona:

**No se genera nada:**

- Verifica que `SegmentTester` esté en el GameObject
- Mira la Consola por errores

**No veo los segmentos:**

- Usa Scene View en lugar de Game View
- Presiona `F` con `SegmentTest` seleccionado
- Ajusta la posición de la cámara

**Colores extraños:**

- Es normal, son materiales procedurales simples

---

## 💡 Próximo Paso

Una vez que veas que los segmentos se generan correctamente:

1. Usa `Clear All Segments` para limpiar
2. Desactiva o elimina el `SegmentTest`
3. Continúa con el setup completo del juego usando `GameSetup.cs`

---

**¡Listo para probar!** 🚀
