using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yidong : MonoBehaviour
{
    //����һ��������Ϊ����ƶ��ٶ�
    public float Yidongsudu;

    //����һ��������ƣ���ұ���Ϊһ��2d����
    //privare ˽��
    private Rigidbody2D zhujue;
    
    // Start is called before the first frame update
    // �ڵ�һ֡��ʼǰ����
    void Start()
    {
        //��ȡ��Ϸ����ҵ�ʵ�����壬����yj�������������ƣ�
        zhujue = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    // ÿһ֡����
    void Update()
    {
        //��ȡ��ҵĲ�������
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        //����һ����ά������x��y�������з������
        Vector2 yidongFanxiang = new Vector2(moveX, moveY).normalized;

        //�ƶ����򣨳��ԣ��ƶ��ٶ� = ʵ���ƶ�
        zhujue.velocity = yidongFanxiang * Yidongsudu;
    }
}
