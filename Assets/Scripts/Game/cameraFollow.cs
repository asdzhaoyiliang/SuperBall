using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public float smooth = 1.5f;
    private Transform player;
    private Vector3 relCameraPos; //相对位置摄像机对人物
    private float relCameraPosMag; //摄像机和人物的距离
    private Vector3 newPos;  //摄像机试着抵达的位置
    private Vector3 offset;
    private Vector3 backPos;
    private float magnitude;
    private Vector3 topPos;
    private Vector3 resPos;
 
    void Awake()
    {
        player = GameObject.FindWithTag("player").transform;
        offset = transform.position - player.position;
        magnitude = offset.magnitude - 0.5f;

    }
 
    void FixedUpdate()
    {
        backPos = player.position + offset;
        topPos = player.position + Vector3.up * magnitude;

        Vector3[] pos = new Vector3[5];
        pos[0] = backPos;

        pos[1] = Vector3.Lerp(backPos, topPos, 0.25f);
        pos[2] = Vector3.Lerp(backPos, topPos, 0.5f);
        pos[3] = Vector3.Lerp(backPos, topPos, 0.75f);
        pos[4] = topPos;
        for (int i = 0; i < pos.Length; i++)
        {
            if (ViewPosCheck(pos[i]))
            {
                break;
            }
        }

        transform.position = Vector3.Lerp(transform.position,resPos,1.5f*Time.deltaTime);

        LookAt();

    }

    //使用光线投射的方法检测摄像机能否投射碰撞到玩家 
    bool ViewPosCheck(Vector3 pos)
    {
        Vector3 direction = player.position - pos;
        RaycastHit hit;
        if (Physics.Raycast(pos, direction, out hit, magnitude))
        {
            if (hit.transform != player)
            {
                return false;
            }
        }

        resPos = pos;
        return true;
    }
    
    //使摄像机函数在移动过程中始终面对玩家
    void LookAt()
    {
        Vector3 direction = player.position - transform.position;
        Quaternion lookAt = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookAt, 1.5f * Time.deltaTime);
    }
}
