# 🧪 Guía de Test del Nivel

## Test Rápido del Movimiento del Nivel

### Setup (1 minuto):

1. **Crear Tags necesarios** (Edit > Project Settings > Tags):
   - `Enemy`
   - `Obstacle`

2. **Crear GameObject de Test**:
   - GameObject vacío llamado `LevelTest`
   - Añadir script `LevelTest.cs`
   - Marcar `Auto Start` en el Inspector

3. **Dale Play** - El nivel empezará a moverse automáticamente

---

## 🎮 Controles de Test

- **Espacio**: Pausar/Reanudar movimiento
- **Flecha Arriba**: Aumentar velocidad (+2)
- **Flecha Abajo**: Disminuir velocidad (-2)

### Desde el Inspector (Click derecho en LevelTest):
- **Start Level Test**: Iniciar movimiento
- **Stop Level Test**: Detener movimiento
- **Increase Speed**: +2 velocidad
- **Decrease Speed**: -2 velocidad

---

## ✅ Qué Verificar

### 1. Generación de Segmentos
- ✓ Se generan 3 segmentos al inicio
- ✓ Cada segmento tiene diferentes patrones
- ✓ Los segmentos aparecen correctamente alineados

### 2. Movimiento del Nivel
- ✓ Los segmentos se mueven hacia atrás (Z negativo)
- ✓ El movimiento es suave y constante
- ✓ No hay saltos o stuttering

### 3. Reciclaje de Segmentos
- ✓ Cuando un segmento sale de vista (Z < -20), se destruye
- ✓ Un nuevo segmento aparece adelante automáticamente
- ✓ Siempre hay 3 segmentos activos
- ✓ El nivel nunca se acaba

### 4. Variedad de Patrones
Deberías ver aparecer:
- Segmentos vacíos (solo suelo)
- Torres rojas
- Muros grises bajos
- Bloques flotantes
- Túneles con paredes laterales
- Combinaciones mixtas

---

## 🛡️ Sistema Anti-Referencias Rotas

### ¿Qué se implementó?

1. **ReferenceManager** (Singleton persistente)
   - Encuentra referencias automáticamente
   - Se refresca al cambiar de escena
   - No se destruye entre escenas

2. **Inicialización automática en todos los managers**
   - Todas las referencias se buscan por código
   - No dependen del Inspector
   - Se reinicializan al reiniciar nivel

3. **Método ResetLevel() en LevelManager**
   - Limpia segmentos viejos
   - Reinicializa referencias
   - Regenera el nivel desde cero

### Ventajas:
- ✅ No necesitas arrastrar nada en el Inspector
- ✅ Las referencias no se rompen al reiniciar
- ✅ Funciona aunque cambies de escena
- ✅ Auto-recuperación si algo falla

---

## 🔧 Configuración Opcional

En el Inspector de `LevelTest`:
- **Auto Start**: Si se inicia automáticamente al dar Play
- **Test Scroll Speed**: Velocidad inicial del nivel (default: 5)

En el Inspector de `LevelManager`:
- **Segment Length**: Largo de cada segmento (default: 20)
- **Active Segments**: Cuántos segmentos mantener activos (default: 3)
- **Scroll Speed**: Velocidad del nivel (default: 5)

---

## 🐛 Problemas Comunes

**El nivel no se mueve:**
- Verifica que `scrollSpeed` > 0
- Presiona Espacio para reanudar
- Usa "Start Level Test" del menú contextual

**No se generan segmentos:**
- Verifica que los Tags estén creados
- Mira la consola por errores
- Asegúrate que `LevelManager` tenga `ProceduralSegmentGenerator`

**Los segmentos están morados:**
- El proyecto necesita URP configurado
- O los shaders no se encuentran (ya tiene fallbacks)

**Referencias se pierden al reiniciar:**
- Asegúrate que `ReferenceManager` esté en la escena
- O se crea automáticamente al usar GameManager.RestartGame()

---

## 📊 Valores Recomendados para Testing

| Parámetro | Valor Lento | Valor Normal | Valor Rápido |
|-----------|-------------|--------------|--------------|
| Scroll Speed | 2-3 | 5-7 | 10-15 |
| Segment Length | 20 | 20 | 20 |
| Active Segments | 3 | 3-4 | 4-5 |

---

## 🚀 Siguiente Paso

Una vez que verifiques que el nivel se genera y mueve correctamente:

1. **Desactiva o elimina** `SegmentTester` (ya no lo necesitas)
2. **Desactiva o elimina** `LevelTest` (solo para testing)
3. Continúa con la implementación del jugador usando `GameSetup`

---

## 💡 Tips

- Usa la **Scene View** para ver mejor el movimiento
- Selecciona `LevelManager` en Hierarchy y presiona `F` para seguirlo
- El nivel se regenera infinitamente, no tiene fin
- Puedes cambiar `scrollSpeed` en tiempo real desde el Inspector

---

**¡El sistema está diseñado para ser robusto y no romperse!** 🎯
