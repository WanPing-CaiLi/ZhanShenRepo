using UnityEngine;

public class BoFangQi : MonoBehaviour
{

    private Rigidbody2D ZhuJue;
    private Animator anim;

    [SerializeField] private float yidongsudu;
    [SerializeField] private float tiaoyuegaodu;

    [Header("Dash info")]
    [SerializeField] private float dashSpeed;//冲刺速度
    [SerializeField] private float dashDuration;
    private float dashTime;
    //private float doubleClickThreshold = 0.3f; // 双击间隔阈值（秒）
    [SerializeField] private float dashCooldown;
    private float dashCooldownTimer;

    [Header("攻击信息")]
    private bool isAttacking;
    private int comboCounter;

    private float YiDong;

    private int ChaoXiang = 1;
    private bool ChaoXiangYou = true;


    [Header("碰撞信息")]
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
        //控制主角横向移动
        YiDongFangfa();
        //检查输入的方法
        CheckInput();

        DiMianjiance();

        dashTime -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;





        //翻转控制
        Fangzhuangkongzhi();
        //动画播放的方法
        DongHuaKongZhiQi();

    }

    private void DiMianjiance()
    {
        ZSdiMian = Physics2D.Raycast(transform.position, Vector2.down, DiMianJianCe, SMSDiMian);
    }

    // 以下是自行创建的方法
    private void TiaoYueFangfa()
    {
        if (ZSdiMian)
            ZhuJue.velocity = new Vector2(ZhuJue.velocity.x, tiaoyuegaodu);
    }    //1.控制主角跳跃判定

    private void DongHuaKongZhiQi()    //2.控制动画播放的动画控制器
    {
        bool ZaiYiDong = ZhuJue.velocity.x != 0;        //使用一个临时真假值，判断是否移动过，对移动播放进行判定

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
    }

    private void YiDongFangfa()
    {
        if (dashTime > 0)
        {
            ZhuJue.velocity = new Vector2(YiDong * dashSpeed, 0);//冲刺
        }
        else
        {
            ZhuJue.velocity = new Vector2(YiDong * yidongsudu, ZhuJue.velocity.y);
        }
    }    //3. 控制主角移动

    private void CheckInput()
    {
        YiDong = Input.GetAxisRaw("Horizontal");        //调用一个移动方法，并赋予

        if(Input.GetKeyDown(KeyCode.J))
        {
            isAttacking = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))        //控制主角跳跃
        {
            TiaoYueFangfa();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }
    }    //4.检查按键输入

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
