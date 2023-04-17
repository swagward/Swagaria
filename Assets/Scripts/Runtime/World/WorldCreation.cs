using UnityEngine;

namespace PixelWorlds.Runtime.World
{
    public class WorldCreation : MonoBehaviour
    {
        public static TerrainSettings SettingsToUse;

        public void ApplySettings(TerrainSettings settings)
        {
            SettingsToUse = settings;
        }
    }
}