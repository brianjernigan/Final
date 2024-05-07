//////////////////////////////////////////////
//Assignment/Lab/Project: Final
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 05/06/2024
/////////////////////////////////////////////

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