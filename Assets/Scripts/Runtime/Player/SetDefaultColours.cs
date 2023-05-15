using System.Collections.Generic;
using UnityEngine;

namespace TerrariaClone.Runtime.Player
{
    public class SetDefaultColours : MonoBehaviour
    {
        public List<SpriteRenderer> parts;
        public Color defColour;

        public void Start()
        {
            SetDefaultColour();
        }

        public void SetDefaultColour()
        {
            foreach (var part in parts)
            {
                var colour = part.material.color;

                colour.r = defColour.r;
                colour.g = defColour.g;
                colour.b = defColour.b;

                part.material.color = colour;
                part.material.SetColor("_EmissionColor", colour);
            }
        }
    }
}
