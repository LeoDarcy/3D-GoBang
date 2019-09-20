using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnGUI()
    {
        const int buttonWidth = 84;
        const int buttonHeight = 60;

        // Determine the button's place on screen
        // Center in X, 2/3 of the height in Y
        Rect buttonStart = new Rect(
            Screen.width / 2 - (buttonWidth / 2),
            (2 * Screen.height / 3) - (buttonHeight / 2),
            buttonWidth,
            buttonHeight
        );

        // Draw a button to start the game
        if (GUI.Button(buttonStart, "Start!"))
        {
            // On Click, load the first level.
            // "Stage1" is the name of the first scene we created.
            Application.LoadLevel("GameScene");
            //SceenManager.
        }
        Rect buttonQuit = new Rect(
            Screen.width / 2 - (buttonWidth / 2),
            (2 * Screen.height / 3) + (buttonHeight / 2),
            buttonWidth,
            buttonHeight
        );
        if (GUI.Button(buttonQuit, "Quit"))
        {
            // On Click, load the first level.
            // "Stage1" is the name of the first scene we created.
            Application.Quit();
            //SceenManager.
        }
    }

}
