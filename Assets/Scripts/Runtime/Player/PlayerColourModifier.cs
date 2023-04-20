using UnityEngine;
using UnityEngine.UI;

namespace PixelWorlds.Runtime.Player
{
    public class PlayerColourModifier : MonoBehaviour
    {
        /// <summary>
        /// Made with this:
        /// https://www.youtube.com/watch?v=Uw6XcLImDVk
        /// </summary>
        
        public SpriteRenderer bodyPart;
        public Slider red;
        public Slider green;
        public Slider blue;

        public void OnEdit()
        {
            var colour = bodyPart.material.color;

            colour.r = red.value;
            colour.g = green.value;
            colour.b = blue.value;

            bodyPart.material.color = colour;
            bodyPart.material.SetColor("_EmissionColor", colour);
        }
    }
}
