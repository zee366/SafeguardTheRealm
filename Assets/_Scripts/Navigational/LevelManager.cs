using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int sceneIndex;
    Animator animator;
    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }
    void Update()
    {
        //change scene when user presses Space key
        
    }
    private IEnumerator LoadSceneAFterTransition()
    {
        //show animate out animation
        animator.SetBool("animateOut", true);
        yield return new WaitForSeconds(1f);
        //load the scene we want
        //SceneManager.LoadScene(sceneIndex);
    }

    // Load scene based on name passed; register in console
    public void LoadLevel(string sceneName)
    {
        Debug.Log("New Level Load: " + sceneName);
        StartCoroutine(LoadSceneAFterTransition());
        SceneManager.LoadScene(sceneName);
    }

    //Allow player to return to main menu scene once they click the button on the game over scene
    // This function is associated with it via OnClick() event
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu"); //TODO make sure good name of menu scene
    }
}
