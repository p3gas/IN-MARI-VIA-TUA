using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{

    public void QuitTheGame()
    {
        Application.Quit();
    }
    public void StartTheGame(string level)
    {
        SceneManager.LoadScene(level);
    }

}
