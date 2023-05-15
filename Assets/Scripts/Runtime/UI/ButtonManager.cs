using TerrariaClone.Runtime.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelWorlds.Runtime.UI
{
    public class ButtonManager : MonoBehaviour
    {
        public SetDefaultColours[] set;

        public void LoadScene(string scene)
            => SceneManager.LoadScene(scene);

        public void Quit()
            => Application.Quit();

        private void Start()
        {
            for (int i = 0;i < set.Length; i++)
                set[i].PlayerColorsInit();
        }
    }
}
