using UnityEngine;
using TerrariaClone.Runtime.Player;

namespace TerrariaClone.Runtime.UI
{
    public class AssignPlayerParts : MonoBehaviour
    {
        public PlayerColourModifier modifier;
        
        public void ClearList()
        {
            modifier.bodyParts.Clear();
            modifier.red.value = 255;
            modifier.green.value = 255;
            modifier.blue.value = 255;
        }

        public void AddTo(SpriteRenderer toAdd)
        {
            modifier.bodyParts.Add(toAdd);
        }
    }
}
