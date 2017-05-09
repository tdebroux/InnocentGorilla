using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BridgeToFestivalTele : MonoBehaviour
{
    public string sceneName;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "MainCharacter")
        {
            SceneManager.LoadScene(sceneName.Trim());
        }
    }
}
