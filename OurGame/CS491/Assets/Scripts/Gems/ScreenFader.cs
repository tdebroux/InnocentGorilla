﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{
    public Image FadeImg;
    Animator animator;
    void Awake()
    {
        FadeImg.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
        animator = GetComponent<Animator>();
    }

    public void FadeToBlack()
    {
        print("Fading to black");
        animator.SetInteger("FadeState", 1);
        print("fadestate: " + animator.GetInteger("FadeState"));
    }
    public void FadeToTrans()
    {
        animator.SetInteger("FadeState", 2);
    }
}
