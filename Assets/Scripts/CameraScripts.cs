using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScripts : MonoBehaviour {

    //跟随目标，一般为空
    public Transform target;
    private int MouswWheelSensitivity = 1;//滑轮灵敏度
    private int MouseZoomMin = 1; //相机距离的最小值
    private int MouseZoomMax = 50; //相机距离的最大值

    private float moveSpeed = 10;//相机的跟随速度，越大越平滑

    private float xSpeed = 250.0f; // 旋转视角时相机X轴的转速
    private float ySpeed = 120.0f; // 旋转视角时相机y轴的转速

    private int yMinLimit = -360;
    private int yMaxLimit = 360;

    private float x = 0.0f;//相机的欧拉角
    private float y = 0.0f;//相机的欧拉角

    private float Distance = 20;

    private Vector3 targetOnScreenPosition;//目标的屏幕坐标，第三个值为z轴
    private Quaternion storeRotation;//储存相机的姿态四元数
    private Vector3 CameraTargetPosition;
    private Vector3 initPosition;
    private Vector3 cameraX;
    private Vector3 cameraY;
    private Vector3 cameraZ;

    private Vector3 initScreenPos;
    private Vector3 curScreenPos;


    // Use this for initialization
    void Start () {
        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        CameraTargetPosition = target.position;
        storeRotation = Quaternion.Euler(y + 60, x, 0);
        transform.rotation = storeRotation;//设置相机
        Vector3 position = storeRotation * new Vector3(0.0f, 0.0f - Distance) + CameraTargetPosition;
        transform.position = storeRotation * new Vector3(0, 0.0f, -Distance) + CameraTargetPosition;

        //Debug.Log("camera x: " + transform.right);
        //Debug.Log


	}
	
	// Update is called once per frame
	void Update () {
		//右键旋转功能
        if(Input.GetMouseButton(1))
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            y = ClampAngle(y, yMinLimit, yMaxLimit);

            storeRotation = Quaternion.Euler(y + 60, x, 0);
            var position = storeRotation * new Vector3(0.0f, 0.0f, -Distance) + CameraTargetPosition;

            transform.rotation = storeRotation;
            transform.position = position;

        }
        else if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if(Distance >= MouseZoomMin && Distance <= MouseZoomMax)
            {
                Distance -= Input.GetAxis("Mouse ScrollWheel") * MouswWheelSensitivity;
            }
            if (Distance < MouseZoomMin)
                Distance = MouseZoomMin;
            else if (Distance > MouseZoomMax)
                Distance = MouseZoomMax;
            var rotation = transform.rotation;
            transform.position = storeRotation * new Vector3(0, 0, -Distance) + CameraTargetPosition;
        }
        if(Input.GetMouseButtonDown(2))
        {
            cameraX = transform.right;
            cameraY = transform.up;
            cameraZ = transform.forward;

            targetOnScreenPosition = Camera.main.WorldToScreenPoint(CameraTargetPosition);

            initScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetOnScreenPosition.z);

            //Debug.Log("MouseDown : x:" +  initPosition.x);
            initPosition = CameraTargetPosition;

        }
        if(Input.GetMouseButton(2))
        {
            curScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetOnScreenPosition.z);

            target.position = initPosition - 0.01f * ((curScreenPos.x - initScreenPos.x) * cameraX + (curScreenPos.y - initScreenPos.y) * cameraY);
            //Debug.Log("MouseDown : x:" + curScreenPos.x + "  , last:" + initPosition.x);
            //Debug.Log("MouseDown : y:" + curScreenPos.y + "  , last:" + initPosition.y);
            
            //重新计算位置
            Vector3 mPosition = storeRotation * new Vector3(0.0f, 0.0f, -Distance) + target.position;
            transform.position = mPosition;
            //CameraTargetPosition = target.position;
        }
        if (Input.GetMouseButtonUp(2))
        {
            Debug.Log("upOnce");
            //平移结束把cameraTargetPosition的位置更新一下，不然会影响缩放与旋转功能
            CameraTargetPosition = target.position;
        }
    }
    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
