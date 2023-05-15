using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TerrariaClone.Runtime.Player
{
    public class PlayerColourModifier : MonoBehaviour
    {
        /// <summary>
        /// Made with this:
        /// https://www.youtube.com/watch?v=Uw6XcLImDVk
        /// </summary>
        
        public List<SpriteRenderer> bodyParts;
        public Slider red;
        public Slider green;
        public Slider blue;


        public void OnEdit()
        {
            foreach (var part in bodyParts)
            {
                var colour = part.material.color;

                colour.r = red.value;
                colour.g = green.value;
                colour.b = blue.value;

                part.material.color = colour;
                part.material.SetColor("_EmissionColor", colour);
            }
        }
    }
}
