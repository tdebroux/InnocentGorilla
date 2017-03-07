using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{
    public Image FadeImg;
    public float fadeSpeed;
    public bool sceneStarting = true;
    private float duration = 10;

    void Awake()
    {
        FadeImg.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
    }

    void Update()
    {
        // If the scene is starting...
        if (sceneStarting)
            // ... call the StartScene function.
            StartScene();
    }


    void FadeToClear()
    {
        // Lerp the colour of the image between itself and transparent.
        FadeImg.color = Color.Lerp(Color.black, Color.clear, fadeSpeed);
        if (fadeSpeed < 1)
            fadeSpeed += (Time.deltaTime/(duration))*(fadeSpeed*10 + 2);
    }


    void FadeToBlack()
    {
        // Lerp the colour of the image between itself and black.
        FadeImg.color = Color.Lerp(Color.clear, Color.black, fadeSpeed);
        if (fadeSpeed < 1)
            fadeSpeed += (Time.deltaTime / (duration)) * (fadeSpeed * 10 + 2);
    }


    void StartScene()
    {
        // Fade the texture to clear.
        do
        {
            fadeSpeed = 0;
            FadeToBlack();
            // If the texture is almost clear...
            print("ALPHA " + FadeImg.color.a);
            if (FadeImg.color.a >= 0.95f)
            {
                // ... set the colour to clear and disable the RawImage.
                FadeImg.color = Color.black;
                FadeImg.enabled = false;

                // The scene is no longer starting.
                sceneStarting = false;
                break;
            }
        } while (true);
        EndScene(1);
    }


    public IEnumerator EndSceneRoutine(int SceneNumber)
    {
        // Make sure the RawImage is enabled.
        //FadeImg.enabled = true;
        do
        {
            // Start fading towards black.
            FadeToClear();
            // If the screen is almost black...
            if (FadeImg.color.a <= 0.05f)
            {
                FadeImg.color = Color.clear;
                // ... reload the level
                //SceneManager.LoadScene(SceneNumber);
                yield break;
            }
            else
            {
                yield return null;
            }
        } while (true);
    }

    public void EndScene(int SceneNumber)
    {
        sceneStarting = false;
        StartCoroutine("EndSceneRoutine", SceneNumber);
    }
}
