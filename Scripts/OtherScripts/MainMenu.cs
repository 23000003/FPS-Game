using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject startedUI;
    [SerializeField] private GameObject notStartedUI;
    [SerializeField] private RawImage bgChange;
    [SerializeField] private Texture newBG;

    private Slider slider;

    private AsyncOperation asyncLoad;

    void Awake()
    {
        slider = startedUI.GetComponentInChildren<Slider>();
        startedUI.SetActive(false);
    }

    public void PlayGame()
    {
        StartCoroutine(PlayGameWithDelay());
    }

    private IEnumerator PlayGameWithDelay()
    {
        yield return new WaitForSeconds(3f);

        bgChange.texture = newBG;

        StartCoroutine(LoadSceneAsync("MainScene"));

        startedUI.SetActive(true);
        notStartedUI.SetActive(false);
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            slider.value = asyncLoad.progress;

            if (asyncLoad.progress >= 0.9f)
            {
                slider.value = 1f;
                asyncLoad.allowSceneActivation = true;  // Now activate the scene
            }

            yield return null;
        }
    }
}
