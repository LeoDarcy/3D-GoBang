using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("Game Ending....");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public Button buttonRetry;
    public Button buttonExit;

    void Awake()
    {
        // Get the buttons
        //buttons = new GUI.;

        // Disable them
        //HideButtons();
    }
    /*public void HideButtons()
    {
        foreach (var b in buttons)
        {
            b.gameObject.SetActive(false);
        }
    }

    public void ShowButtons()
    {
        foreach (var b in buttons)
        {
            b.gameObject.SetActive(true);
        }
    }*/
    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 3.0f, 60, 200, 20), "Retry"))
        {
            RestartGame();
        }
        if (GUI.Button(new Rect(Screen.width * 2.0f / 3.0f, 60, 200, 20), "Exit"))
        {
            ExitToMenu();
        }

    }

    public void ExitToMenu()
    {
        // Reload the level
        //Application.LoadLevel("UIScene");
        SceneManager.LoadScene("UIScene");
    }

    public void RestartGame()
    {
        // Reload the level
        //Application.LoadLevel("GameScene");
        SceneManager.LoadScene("GameScene");
    }
}
