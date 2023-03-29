using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void LoadScene(string scene)
        => SceneManager.LoadScene(scene);

    public void Quit()
        => Application.Quit();
}
