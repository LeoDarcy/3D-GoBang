using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    void OnBecameInvisible()
    {
        //Destroy(this.gameObject);
    }
	// Use this for initialization
	void Start () {
        float xspeed = Random.Range(-5.0f, 0.0f);
        float yspeed = Random.Range(0.0f, 5.0f);
        this.GetComponent<Rigidbody>().velocity = new Vector3(xspeed, yspeed, 0.0f);
        //this.GetComponent<Rigidbody>().velocity = new Vector3(-8.0f, 8.0f, 0.0f); //设置向左上方的速度
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
