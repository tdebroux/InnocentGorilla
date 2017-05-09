using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BridgeToFestivalTele : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "MainCharacter")
        {
            SceneManager.LoadScene("SceneWA2b");
        }
    }
}
