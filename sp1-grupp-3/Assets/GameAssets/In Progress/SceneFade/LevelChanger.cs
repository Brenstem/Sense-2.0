using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private Animator animator;

    private int sceneToLoad;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void FadeToLevel(int sceneIndex)
    {
        sceneToLoad = sceneIndex;
        animator.SetTrigger("Fade_Out");
    }

    private void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
