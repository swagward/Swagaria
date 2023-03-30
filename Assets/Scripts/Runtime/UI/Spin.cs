using UnityEngine;

namespace PixelWorlds.Runtime.UI
{
    public class Spin : MonoBehaviour
    {
        [SerializeField] private float speed;

        private void Update()
            => transform.Rotate(Vector3.forward * (speed * Time.deltaTime));
    }
}
