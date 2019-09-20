using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laucher : MonoBehaviour {
    public GameObject CylinderPrefab;//表示柱子
    public int Row = 5;
    public float CylinderDistance = 3;
    private GameObject[] cylinders;

    private int step = 0;
    private int person = 0;
    //private bool isLanding = true;
	// Use this for initialization
	void Start () {
        cylinders = new GameObject[Row * Row];
        float offsetx = 0 - (Row - 1) * CylinderDistance / 2.0f;
        float offsetz = 0 - (Row - 1) * CylinderDistance / 2.0f;
        for (int i = 0; i < Row; i++)
        {
            for(int j = 0; j < Row; j++)
            {
                GameObject newcy = Instantiate(this.CylinderPrefab);
                //对柱子标号
                int[] message = new int[2];
                message[0] = i;
                message[1] = j;
                newcy.SendMessage("Identify", message);
                //修改位置
                Vector3 position = newcy.transform.position;
                position.x = offsetx + j * CylinderDistance;
                position.z = offsetz + i * CylinderDistance;
                newcy.transform.position = position;
                cylinders[i * Row + j] = newcy;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void StopAllCylinders()
    {
        for (int i = 0; i < Row; i++)
        {
            for (int j = 0; j < Row; j++)
            {
                cylinders[i * Row + j].GetComponent<Cylinder>().SetReady(false);
            }
        }
    }
    //接受柱子的信息
    public bool  MakeStep(int[] message)
    {
        //if (isLanding == false)
            //return false;
        //isLanding = false;
        Debug.Log("Make Step");
        //cylinders[message[0] * Row + message[1]].SendMessage("AddCheese", person);
        bool flag = cylinders[message[0] * Row + message[1]].GetComponent<Cylinder>().AddCheese(person);
        if (flag == false)
            return false;
        
        if (EndGame(message[0], message[1]))
        {
            Debug.Log("Game End!");
            StopAllCylinders();
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            camera.GetComponent<Information>().SetInformation("胜利者：" + person.ToString());
            //Time.timeScale = 0;
            Invoke("GameEndMenu", 1.5f);
            return true;
            //GameEndMenu();
        }

        if (person == 1)
            person = 0;
        else
            person = 1;
        step++;
        UpdateMessage();
        //Invoke("UpdateMessage", 1.5f);
        //isLanding = true;
        return true;
    }
    void UpdateMessage()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.GetComponent<Information>().SetStep(step);
        
    }
    void GameEndMenu()
    {
        transform.gameObject.AddComponent<GameOver>();
    }
    private bool isMatch(int r, int c, int index)
    {
        if(r >= 0 && r < Row && c >= 0 && c < Row)
        {
            if (cylinders[r * Row + c].GetComponent<Cylinder>().GetCheese(index) == person)
                return true;
        }
        return false;
    }
    public bool EndGame(int row ,int col)
    {
        //Debug.Log("now is Person: " + person + " and step : " + step);
        //Debug.Log("now is Cylinder: " + row + " and col : " + col);
        Cylinder cylind = cylinders[row * Row + col].GetComponent<Cylinder>();
        int index = cylind.GetCheeseNum() - 1;
        //竖向对比，向上
        int count = 1;
        int tmp = index + 1;
        while(cylind.GetCheese(tmp) == person)
        {
            count++;
            tmp++;
        }
        //竖向向下
        tmp = index - 1;
        while (cylind.GetCheese(tmp) == person)
        {
            count++;
            tmp--;
        }
        if (count >= 5)
            return true;
        //Debug.Log("the cylinder:" + row + "  " + col);
        //Debug.Log("the count : " + count);
        //横向平面对比,part1,平面竖直方向
        count = 1;
        tmp = index;
        int tmpr = row + 1;
        int tmpc = col;
        while (isMatch(tmpr, tmpc, tmp))
        {
            count++;
            tmpr++;
        }
        //竖向向下
        tmpr = row - 1;
        while (isMatch(tmpr, tmpc, index))
        {
            count++;
            tmpr--;
        }
        if (count >= 5)
            return true;

        //斜方向，撇
        count = 1;
        tmp = index;
        tmpr = row + 1;
        tmpc = col + 1;
        while (isMatch(tmpr, tmpc, index))
        {
            count++;
            tmpc++;
            tmpr++;
        }
        tmpr = row - 1;
        tmpc = col - 1;
        while (isMatch(tmpr, tmpc, index))
        {
            count++;
            tmpc--;
            tmpr--;
        }
        if (count >= 5)
            return true;


        //斜方向，捺
        count = 1;
        tmp = index;
        tmpr = row + 1;
        tmpc = col - 1;
        while (isMatch(tmpr, tmpc, index))
        {
            count++;
            tmpc--;
            tmpr++;
        }
        tmpr = row - 1;
        tmpc = col + 1;
        while (isMatch(tmpr, tmpc, index))
        {
            count++;
            tmpc++;
            tmpr--;
        }
        if (count >= 5)
            return true;


        //四个斜方向，从左下down到右上up
        count = 1;
        tmp = index + 1;
        tmpr = row + 1;
        tmpc = col + 1;
        while (isMatch(tmpr, tmpc, tmp))
        {
            count++;
            tmpc++;
            tmpr++;
            tmp++;
        }
        tmpr = row - 1;
        tmpc = col - 1;
        tmp = index - 1;
        while (isMatch(tmpr, tmpc, tmp))
        {
            count++;
            tmpc--;
            tmpr--;
            tmp--;
        }
        if (count >= 5)
            return true;

        //四个斜方向，从左下up到右上down
        count = 1;
        tmp = index - 1;
        tmpr = row + 1;
        tmpc = col + 1;
        while (isMatch(tmpr, tmpc, tmp))
        {
            count++;
            tmpc++;
            tmpr++;
            tmp--;
        }
        tmpr = row - 1;
        tmpc = col - 1;
        tmp = index + 1;
        while (isMatch(tmpr, tmpc, tmp))
        {
            count++;
            tmpc--;
            tmpr--;
            tmp++;
        }
        if (count >= 5)
            return true;

        //四个斜方向，从左上down到右下up
        count = 1;
        tmp = index - 1;
        tmpr = row + 1;
        tmpc = col - 1;
        while (isMatch(tmpr, tmpc, tmp))
        {
            count++;
            tmpc--;
            tmpr++;
            tmp--;
        }
        tmpr = row - 1;
        tmpc = col + 1;
        tmp = index + 1;
        while (isMatch(tmpr, tmpc, tmp))
        {
            count++;
            tmpc++;
            tmpr--;
            tmp++;
        }
        if (count >= 5)
            return true;

        //四个斜方向，从左上up到右下down
        count = 1;
        tmp = index + 1;
        tmpr = row - 1;
        tmpc = col + 1;
        while (isMatch(tmpr, tmpc, tmp))
        {
            count++;
            tmpc++;
            tmpr--;
            tmp++;
        }
        tmpr = row - 1;
        tmpc = col + 1;
        tmp = index - 1;
        while (isMatch(tmpr, tmpc, tmp))
        {
            count++;
            tmpc++;
            tmpr--;
            tmp--;
        }
        if (count >= 5)
            return true;

        return false;
    }
}
