using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yidong : MonoBehaviour
{
    //声明一个变量，为玩家移动速度
    public float Yidongsudu;

    //声明一个玩家名称，玩家本质为一个2d物体
    //privare 私有
    private Rigidbody2D zhujue;
    
    // Start is called before the first frame update
    // 在第一帧开始前调用
    void Start()
    {
        //获取游戏里玩家的实际物体，给到yj。（物体与名称）
        zhujue = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    // 每一帧调用
    void Update()
    {
        //获取玩家的操作输入
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        //创建一个二维向量（x和y），进行方向操作
        Vector2 yidongFanxiang = new Vector2(moveX, moveY).normalized;

        //移动方向（乘以）移动速度 = 实现移动
        zhujue.velocity = yidongFanxiang * Yidongsudu;
    }
}
