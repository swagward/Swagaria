using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelWorlds.Runtime.UI
{
    public class ButtonManager : MonoBehaviour
    {
        public void LoadScene(string scene)
            => SceneManager.LoadScene(scene);

        public void Quit()
            => Application.Quit();
    }
}
