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
    bool inFirst = false;

    void Start()
    {
        EndScene();
    }
    void Awake()
    {
        FadeImg.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
        FadeImg.color = Color.clear;
    }

    void Update()
    {
        // If the scene is starting...
       // if (sceneStarting)
            // ... call the StartScene function.
            //StartScene();
    }


    void FadeToClear()
    {
        // Lerp the colour of the image between itself and transparent.
        FadeImg.color = Color.Lerp(Color.black, Color.clear, fadeSpeed);
        if (fadeSpeed < 1)
            fadeSpeed += (Time.deltaTime / (duration)) * (fadeSpeed * 20 + 2);
    }


    void FadeToBlack()
    {
        // Lerp the colour of the image between itself and black.
        FadeImg.color = Color.Lerp(Color.clear, Color.black, fadeSpeed);
        if (fadeSpeed < 1)
            fadeSpeed += (Time.deltaTime / (duration)) * (fadeSpeed * 20 + 2);
    }
    void StartScene()
    {
        //FadeImg.color = Color.clear;
        StartCoroutine("StartSceneRoutine");
        //StartCoroutine("EndSceneRoutine", 1);
    }

    public IEnumerator StartSceneRoutine()
    {
        // Fade the texture to clear.
         while (inFirst)
            yield return new WaitForSeconds(0.1f);
        fadeSpeed = 0.01f;
        do {
              // If the texture is almost black...
              FadeToBlack();
              if (FadeImg.color.a >= 0.95f)
              {
                    // ... set the colour to clear and disable the RawImage.
                  FadeImg.color = Color.black;
                  // The scene is no longer starting.
                  sceneStarting = false;
                yield return new WaitForSeconds(0.5f);
                StartCoroutine("EndSceneRoutine");
                yield break;
              } else {
                  yield return null;
              }
          } while (true);
    }


    public IEnumerator EndSceneRoutine()
    {
        // Make sure the RawImage is enabled.
        inFirst = true;
        fadeSpeed = 0.01f;

        do
        {
            // Start fading towards black.
            FadeToClear();
            // If the screen is almost black...
            if (FadeImg.color.a <= 0.05f)
            {
                FadeImg.color = Color.clear;
                yield break;
            }
            else
            {
                inFirst = false;
                yield return null;
            }
        } while (true);
    }

    public void EndScene()
    {
        sceneStarting = false;
        StartCoroutine("EndSceneRoutine");
    }
}
