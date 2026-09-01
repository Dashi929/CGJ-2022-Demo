using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework;
using GameFramework;
//using GameMain;
using GameMain;
public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;


    float m_inputForce = 10;

    [SerializeField]
    HingeJoint2D hinge;
    [SerializeField]
    Rigidbody2D hingeRb;

    Animator anim;

    [SerializeField, Header("最大移速度")]
    float MaxMoveForce = 10;
    [SerializeField, Header("最大移动速度")]
    float MaxMoveSpeed = 10;
    [SerializeField, Header("移动力增加系数")]
    int forceaddval = 10;
    [SerializeField, Header("最大移动速度增加系数")]
    int speedAddVal=10;

    float refSpeed = 0;
    float refForce = 0;

    float m_addForce = 0;

    public float HingeAngle=0;

    public float InputForce
    {
        get { return m_inputForce; }
        set
        {
            m_inputForce = value;
        }
    }


    [SerializeField, Header("转动转矩")]
    private float m_platformToque;

    public float PlatformToque
    {
        get { return m_platformToque; }
        set { m_platformToque = value; }
    }

    public float rotateToque;

    //[SerializeField]
    [SerializeField]
    int SoundID=0;
    
    private PIDController rotatePID;
    [SerializeField,Range(-100, 100)]
    private float pval, ival, dval;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (hinge == null)
        {
            Debug.LogError("HingeIsEmpty");
            return;
        }
        if (hingeRb == null)
        {

            Debug.LogError("HingeRBIsEmpty");
            return;
        }
        anim = GetComponent<Animator>();

        rotatePID = new PIDController(pval,ival,dval);
    }

    private void FixedUpdate()
    {
        //int valu

        rb.AddForce(new Vector2(m_addForce, 0));

        hingeRb.AddTorque(rotateToque);
        HingeAngle = hinge.jointAngle;

        float error = HingeAngle - 0;
        float correction = rotatePID.GetOutput(error, Time.fixedDeltaTime);

        if (rotateToque==0)
        {
            hingeRb.AddTorque(correction);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveController();
        UpdateSpeed();
        rotatePID.UpPIDdateVal(pval,ival,dval);
    }

    public void PlaySound()
    {
        if (rb.linearVelocity.magnitude!=0)
        {
            GameEntry.Sound.PlaySound(SoundID);
        }
    }

    public void UpdateSpeed()
    {
        //int floors = GameEntry.Utils.curLevel;
        int floors = 0;
        refSpeed = MaxMoveSpeed + speedAddVal * floors;
        refForce = MaxMoveForce + forceaddval * floors;

    }

    public void MoveController()
    {
        var velocity = rb.linearVelocity;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (velocity.x < refSpeed)
            {
                m_addForce = refForce;
                rotateToque = m_platformToque;
            }
            else
            {
                m_addForce = 0;
                rotateToque = 0;

            }

            transform.localScale = new Vector3(-1, 1, 1);//偏向
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {



            if (velocity.x > -refSpeed)
            {
                m_addForce = -refForce;
                rotateToque = -m_platformToque;
            }
            else
            {
                m_addForce = 0;
                rotateToque = 0;
            }


            transform.localScale = new Vector3(1,1, 1);//偏向
        }
        else
        {
            rotateToque = 0;
            m_addForce = 0;
        }
        if (anim != null)
        {
            anim.speed = (velocity.magnitude / MaxMoveSpeed) * 4;
        }




    }
}
