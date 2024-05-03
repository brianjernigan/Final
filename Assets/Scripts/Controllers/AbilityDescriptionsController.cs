using UnityEngine;
using UnityEngine.SceneManagement;

public class AbilityDescriptionsController : MonoBehaviour
{
    public void OnClickPlayButton()
    {
        SceneManager.LoadScene("Level0");
    }

    public void OnClickMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}