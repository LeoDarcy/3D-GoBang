using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Information : MonoBehaviour {

    public int step;
    public string information;
    public  
	// Use this for initialization
	void Start () {
        information = "正在下棋...";
        step = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetStep(int st)
    {
        step = st;
    }
    public void SetInformation(string infor)
    {
        information = infor;
    }
    void OnGUI()
    {
        Debug.Log("OnGUI:" + Screen.width.ToString() + "   " + Screen.height.ToString());
        //标签/
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.black;
        style.fontSize = 40;
        GUI.Label(new Rect(0, 10, 200, 20), information, style);
        GUI.Label(new Rect(Screen.width * 0.5f, 10, 200, 20), "正在第 " + step.ToString() + "步", style);
    }
}
