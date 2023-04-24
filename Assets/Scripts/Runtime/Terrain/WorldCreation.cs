using UnityEngine;

namespace TerrariaClone.Runtime.Terrain
{
    public class WorldCreation : MonoBehaviour
    {
        public static TerrainSettings SettingsToUse;
        public void ApplySettings(TerrainSettings settings)
            => SettingsToUse = settings;
        
    }
}