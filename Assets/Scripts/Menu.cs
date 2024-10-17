using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string SceneName;

    public void GoToScene()
		{
			SceneManager.LoadScene(SceneName);
		}
    public void Mainmenu()
    {
         SceneManager.LoadScene("Menu");
    }

    public void start()
    {
         SceneManager.LoadScene("Level 4");
    }

    public void options()
    {
        
    }
    
    public void quit()
    {
        Application.Quit();
    }
}
