using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    Animator animator;
    Boolean mAnimation = false;
    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }
    void Update()
    {
        //change scene when user presses Space key
        animator.SetBool("animateOut", mAnimation);
    }
    public void changeLevel(string sceneName) {
        Debug.Log("boolean");
        mAnimation = true;
        
        StartCoroutine(LoadSceneAFterTransition(sceneName));
    }
    IEnumerator LoadSceneAFterTransition(string sceneName)
    {
        Debug.Log("2secs");
        yield return new WaitForSeconds(1);
        Debug.Log("2secs");
        
        LoadLevel(sceneName);
    }

    
    // Load scene based on name passed; register in console
    public void LoadLevel(string sceneName)
    {
        Debug.Log("New Level Load: " + sceneName);
        mAnimation = false;
        SceneManager.LoadScene(sceneName);
    }

    //Allow player to return to main menu scene once they click the button on the game over scene
    // This function is associated with it via OnClick() event
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu"); //TODO make sure good name of menu scene
    }
}
