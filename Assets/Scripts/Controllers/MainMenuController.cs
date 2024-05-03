using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnClickPlayButton()
    {
        SceneManager.LoadScene("Level0");
    }

    public void OnClickAbilityDescriptionsButton()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}