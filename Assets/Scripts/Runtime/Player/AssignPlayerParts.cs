using System.Collections.Generic;
using UnityEngine;

namespace PixelWorlds.Runtime.Player
{
    public class AssignPlayerParts : MonoBehaviour
    {
        public PlayerColourModifier modifier;
        
        public void ClearList()
        {
            modifier.bodyParts.Clear();
        }
    }
}
