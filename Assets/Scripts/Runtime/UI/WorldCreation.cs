using UnityEngine;
using TerrariaClone.Runtime.Terrain;

namespace TerrariaClone.Runtime.UI
{
    public class WorldCreation : MonoBehaviour
    {
        public static TerrainSettings SettingsToUse;
        public void ApplySettings(TerrainSettings settings)
            => SettingsToUse = settings;
        
    }
}