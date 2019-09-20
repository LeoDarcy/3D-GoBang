using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour {

    private int[] cheese;//保存在上面的棋子
    private int cheese_num = 0;
    public int MaxCheese = 5;
    public GameObject CheesePre;
    private int RowIndex;
    private int ColIndex;
    public bool ready;//判断是否可以下棋
    //发光组件
    private HighlightableObject m_ho;
	// Use this for initialization
	void Start () {
        cheese = new int[MaxCheese];
        for (int i = 0; i < MaxCheese; i++)
            cheese[i] = -1;
        cheese_num = 0;
        ready = true;
	}
	/*void Awake()
    {
        m_ho = GetComponent<HighlightableObject>();
    }*/
	// Update is called once per frame
	void Update () {
		
	}

    void Identify(int[] message)
    {
        RowIndex = (message[0]);
        ColIndex = (message[1]);
    }

    public bool AddCheese(int person)
    {
        
        if (cheese_num >= MaxCheese || person < 0)
            return false;
        cheese[cheese_num] = person;
        cheese_num++;
        GameObject newcy = Instantiate(this.CheesePre);
        
        //修改位置
        Vector3 position = transform.position;
        position.y += 4;
        position.y += cheese_num * 0.5f;
        newcy.transform.position = position;
        if (person == 1)
            newcy.GetComponent<Renderer>().material.color = Color.green;
        else
            newcy.GetComponent<Renderer>().material.color = Color.blue;
        Debug.Log("Add Cheese successful!");
        return true;
    }

    public int GetCheeseNum()
    {
        return cheese_num;
    }
    public int GetCheese(int index)
    {
        if (index >= MaxCheese || index < 0)
            return -1;//表示没有
        return cheese[index];
    }

    //关于高亮部分
    private void OnMouseEnter()
    {
        m_ho = GetComponent<HighlightableObject>();
        m_ho.ConstantOn(Color.red);
    }
    private void OnMouseExit()
    {
        m_ho = GetComponent<HighlightableObject>();
        m_ho.ConstantOff();
    }



    //private bool isLanding = true;
    void OnMouseDown()
    {
        //Debug.Log("MouseDown"  + cheese_num);
        //if (isLanding == false)
        //return;
        //isLanding = false;
        if (ready == false)
            return;
        GameObject controller = GameObject.FindGameObjectWithTag("GameController");
        int[] message = new int[2];
        message[0] = RowIndex;
        message[1] = ColIndex;
        controller.SendMessage("MakeStep", message);
        //if (controller.GetComponent<Laucher>().MakeStep(message))
            //isLanding = true;
    }
    public void SetReady(bool rd)
    {
        ready = rd;
    }
}
