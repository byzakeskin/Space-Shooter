using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGame()
    {
        //set main menu index = 0 rom build settings
        SceneManager.LoadScene(1); //load game scene
    }
}
