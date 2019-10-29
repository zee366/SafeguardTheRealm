using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Load scene based on name passed; register in console
    public void LoadLevel(string sceneName)
    {
        Debug.Log("New Level Load: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    // Allow player to return to main menu scene once they click the button on the game over scene
    // This function is associated with it via OnClick() event
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu"); //TODO make sure good name of menu scene
    }
}
