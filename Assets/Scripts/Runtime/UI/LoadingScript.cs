using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace PixelWorlds.Runtime.UI
{
    public class LoadingScript : MonoBehaviour
    {
        [SerializeField] private GameObject loadScreen;
        [SerializeField] private Slider slider;
        [SerializeField] private TMP_Text text;
        [SerializeField] private float loadSpeed = .9f;
        
        public void LoadLevel(string scene)
            => StartCoroutine(LoadAsync(scene));

        private IEnumerator LoadAsync(string scene)
        {
            var operation = SceneManager.LoadSceneAsync(scene);
            loadScreen.SetActive(true);

            while (!operation.isDone)
            {
                var progress = Mathf.Clamp01(operation.progress / loadSpeed);
                slider.value = progress;
                text.text = $"{progress * 100}%";
                
                yield return null;
            }
            
        }
    }
}
