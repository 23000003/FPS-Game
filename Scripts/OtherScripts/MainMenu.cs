using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // References to UI elements
    [SerializeField] private GameObject startedUI;
    [SerializeField] private GameObject notStartedUI;
    [SerializeField] private RawImage bgChange;
    [SerializeField] private Texture newBG;

    // Slider reference
    private Slider slider;

    void Awake()
    {
        // Initialize slider from the started UI and hide the loading UI initially
        slider = startedUI.GetComponentInChildren<Slider>();
        startedUI.SetActive(false);
    }

    // Method to start the game with a 3-second delay
    public void PlayGame()
    {
        // Start the delay coroutine
        StartCoroutine(PlayGameWithDelay());
    }

    // Coroutine to add a 3-second delay before starting the loading process
    private IEnumerator PlayGameWithDelay()
    {
        // Wait for 3 seconds before executing the next steps
        yield return new WaitForSeconds(3f);

        // Change background texture
        bgChange.texture = newBG;

        // Start the scene loading process
        LoadSceneAsync("MainScene");

        // Show the loading UI and hide the initial UI
        startedUI.SetActive(true);
        notStartedUI.SetActive(false);
    }

    // Coroutine to load the scene asynchronously
    //private IEnumerator LoadSceneAsync(string sceneName)
    //{
    //    // Start loading the scene asynchronously
    //    asyncLoad = SceneManager.LoadSceneAsync(sceneName);

    //    // Prevent the scene from being activated immediately
    //    asyncLoad.allowSceneActivation = false;

    //    // Keep updating the progress bar while loading
    //    while (!asyncLoad.isDone)
    //    {
    //        // Set slider value to show the current progress of loading (between 0 and 0.9)
    //        slider.value = asyncLoad.progress / 0.9f; // Scale progress from 0 to 1

    //        // When the progress reaches 90% (0.9), consider it fully loaded
    //        // Set slider to 100% and activate the scene
    //        if (asyncLoad.progress >= 0.9f)
    //        {
    //            slider.value = 1f;
    //            asyncLoad.allowSceneActivation = true;  // Now activate the scene
    //        }

    //        // Wait for the next frame
    //        yield return null;
    //    }
    //}

    private async void LoadSceneAsync(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);

        scene.allowSceneActivation = false;

        do 
        { 
            await Task.Delay(100);

            if(scene.progress != 0) slider.value = scene.progress;
        }
        while (scene.progress < 0.9f);

        slider.value = 1f;

        await Task.Delay(300);

        scene.allowSceneActivation = true;
    }
}
