using UnityEngine;

public class BoFangQi : MonoBehaviour
{

    private Rigidbody2D ZhuJue;
    private Animator anim;

    [SerializeField] private float yidongsudu;
    [SerializeField] private float tiaoyuegaodu;

    [Header("Dash info")]
    [SerializeField] private float dashSpeed;//����ٶ�
    [SerializeField] private float dashDuration;
    private float dashTime;
    //private float doubleClickThreshold = 0.3f; // ˫�������ֵ���룩
    [SerializeField] private float dashCooldown;
    private float dashCooldownTimer;

    [Header("������Ϣ")]
    private float comboTimeWindow;
    [SerializeField] private float comboTime = .3f;
    private bool isAttacking;
    private int comboCounter;

    private float YiDong;

    private int ChaoXiang = 1;
    private bool ChaoXiangYou = true;


    [Header("��ײ��Ϣ")]
    [SerializeField] private float DiMianJianCe;
    [SerializeField] private LayerMask SMSDiMian;
    private bool ZSdiMian;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        ZhuJue = GetComponent<Rigidbody2D>();



    }

    // Update is called once per frame
    void Update()
    {
        //�������Ǻ����ƶ�
        YiDongFangfa();
        //�������ķ���
        CheckInput();

        DiMianjiance();

        dashTime -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;
        comboTimeWindow -= Time.deltaTime;

        



        //��ת����
        Fangzhuangkongzhi();
        //�������ŵķ���
        DongHuaKongZhiQi();

    }

    private void DiMianjiance()
    {
        ZSdiMian = Physics2D.Raycast(transform.position, Vector2.down, DiMianJianCe, SMSDiMian);
    }

    // ���������д����ķ���
    private void TiaoYueFangfa()
    {
        if (ZSdiMian)
            ZhuJue.velocity = new Vector2(ZhuJue.velocity.x, tiaoyuegaodu);
    }    //1.����������Ծ�ж�

    private void DongHuaKongZhiQi()    //2.���ƶ������ŵĶ���������
    {
        bool ZaiYiDong = ZhuJue.velocity.x != 0;        //ʹ��һ����ʱ���ֵ���ж��Ƿ��ƶ��������ƶ����Ž����ж�

        anim.SetFloat("yVelocity", ZhuJue.velocity.y);

        anim.SetBool("ZaiYiDong", ZaiYiDong);
        anim.SetBool("ZaiTiaoYue", ZSdiMian);
        anim.SetBool("isDashing", dashTime > 0);
        anim.SetBool("isAttacking", isAttacking);
        anim.SetInteger("comboCounter", comboCounter);
    }

    public void AttackOver()
    {
        

        isAttacking = false;

        comboCounter++;

        if (comboCounter > 2)
            comboCounter = 0;

       
    }

    private void YiDongFangfa()
    {
        if (dashTime > 0)
        {
            ZhuJue.velocity = new Vector2(YiDong * dashSpeed, 0);//���
        }
        else
        {
            ZhuJue.velocity = new Vector2(YiDong * yidongsudu, ZhuJue.velocity.y);
        }
    }    //3. ���������ƶ�

    private void CheckInput()
    {
        YiDong = Input.GetAxisRaw("Horizontal");        //����һ���ƶ�������������

        if(Input.GetKeyDown(KeyCode.J))
        {
            StarAttackevent();
        }

        if (Input.GetKeyDown(KeyCode.Space))        //����������Ծ
        {
            TiaoYueFangfa();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }
    }    //4.��鰴������

    private void StarAttackevent()
    {
        if (comboTimeWindow < 0)
            comboCounter = 0;

        isAttacking = true;
        comboTimeWindow = comboTime;
    }

    private void DashAbility()
    {
        if (dashCooldownTimer < 0)
        {
            dashCooldownTimer = dashCooldown;
            dashTime = dashDuration;
        }
    }

    private void Fangzhuan()
    {
        ChaoXiang = ChaoXiang * -1;
        ChaoXiangYou = !ChaoXiangYou;
        transform.Rotate(0, 180, 0);
    }

    private void Fangzhuangkongzhi()
    {
        if (ZhuJue.velocity.x > 0 && !ChaoXiangYou)
        {
            Fangzhuan();
        }
        else if (ZhuJue.velocity.x < 0 && ChaoXiangYou)
        {
            Fangzhuan();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - DiMianJianCe));
    }

}
