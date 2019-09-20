using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    protected float jump_speed = 5.0f;//起跳速度
    public bool is_landing = false;
	// Use this for initialization
	void Start () {
        this.is_landing = false;
        HighlightableObject obj = GetComponent<HighlightableObject>();
        obj.ConstantOn(Color.black);
    }
	
	// Update is called once per frame
	void Update () {
        /*if (this.is_landing)
        {
            if (Input.GetMouseButtonDown(0))//0表示鼠标左键
            {
                this.is_landing = false;
                this.GetComponent<Rigidbody>().velocity = Vector3.up * this.jump_speed;
            }
        }*/
	}
    /*void OnCollisionEnter(Collision collision)
    {
        this.is_landing = true;

    }*/

}
