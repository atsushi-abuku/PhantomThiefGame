using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Thief : MonoBehaviour
{
    Hp hp;
    Rigidbody rigidBody;
    Vector3 velocity;
    ThiefInput thiefInput;
    Visual visual;

    public int maxJumpCount = 1;
    public int jumpCount = 0;
    public Animator thiefAnimator;

    private float moveDirection = 0f;
    private MoveSpeed moveSpeed;

    private bool isRightPressed = false;
    private bool isLeftPressed = false;
    private bool isDashPressed = false;

    private CapsuleCollider capsuleCollider;
    private float originalHeight;
    private Vector3 originalCenter;
    private bool isCrouching = false;

    private bool isStuck = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
     void Start()
     {
         hp = new Hp(3);
         rigidBody = GetComponent<Rigidbody>();
         velocity = rigidBody.linearVelocity;
         thiefInput = new ThiefInput();
        visual = new Visual(0);
        moveSpeed = new MoveSpeed(2f);
        //Dで右移動
        thiefInput.Move.MoveRight.performed += ctx =>
        {
            isRightPressed = true;
            moveDirection = 1f;
        };
        thiefInput.Move.MoveRight.canceled += ctx =>
        {
            isRightPressed = false;
            if (moveDirection == 1f && jumpCount == 0) moveDirection = 0f;
        };
        //Aで左移動
        thiefInput.Move.MoveLeft.performed += ctx =>
        {
            isLeftPressed = true;
            moveDirection = -1f;
        };
        thiefInput.Move.MoveLeft.canceled += ctx =>
        {
            isLeftPressed = false;
            if (moveDirection == -1f && jumpCount == 0) moveDirection = 0f;
        };
        //Shiftでダッシュ
        thiefInput.Move.Dash.performed += ctx =>
        {
            isDashPressed = true;
            moveSpeed = moveSpeed.AddSpeed(new MoveSpeed(2f));
        };
        thiefInput.Move.Dash.canceled += ctx =>
        {
            isDashPressed = false;
            if (jumpCount == 0)
                moveSpeed = moveSpeed.SubSpeed(new MoveSpeed(2f));
        };
         //ジャンプ
         thiefInput.Move.Jump.started += Jump;
         thiefInput.Enable();
        //Cでしゃがみ
        capsuleCollider = GetComponent<CapsuleCollider>();
        originalHeight = capsuleCollider.height;
        originalCenter = capsuleCollider.center;
        thiefInput.Move.Crouch.performed += ctx => Crouch();

        thiefAnimator = GetComponent<Animator>();
     }

     // Update is called once per frame
     void Update()
     {
         velocity = rigidBody.linearVelocity;
        //壁に刺さっていないとき
        if (!isStuck) velocity.x = moveDirection * moveSpeed.GetValue();
        //壁に刺さっているとき
        else velocity.x = 0f;
        //向きの切り替え
        if (moveDirection > 0) transform.rotation = Quaternion.Euler(0, 90, 0);
        else if (moveDirection < 0) transform.rotation = Quaternion.Euler(0, 270, 0);
        rigidBody.linearVelocity = velocity;
        thiefAnimator.SetFloat("speed",Mathf.Abs(velocity.x));
        thiefAnimator.SetInteger("JumpCount", jumpCount);
        thiefAnimator.SetFloat("height", capsuleCollider.height);
     }
    //Spaceでジャンプ(1段)
    void Jump(InputAction.CallbackContext context)
    {
        //飛べるとき
        if (jumpCount < maxJumpCount) 
        {
            velocity = rigidBody.linearVelocity;
            velocity.y = 5;
            rigidBody.linearVelocity = velocity;
            jumpCount++;
        }
    }
    //着地
    public void OnFootTouchGround()
    {
        jumpCount = 0;
        isStuck = false;
        //着地した瞬間にキーが押されていなければ
        if(!isRightPressed && !isLeftPressed)
            moveDirection = 0f;
        if (!isDashPressed)
            moveSpeed = moveSpeed.Set(2f);
    }

    void Crouch()
    {
        //状態を切り替える
        isCrouching = !isCrouching;

        if (isCrouching)
        {
            capsuleCollider.height = originalHeight / 2;
            capsuleCollider.center = new Vector3(originalCenter.x, originalCenter.y-originalHeight/4, originalCenter.z);
            Debug.Log("しゃがみ状態");
        }
        else
        {
            capsuleCollider.height = originalHeight;
            capsuleCollider.center = originalCenter;
            Debug.Log("立ち状態");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Vector3 contactDirection = (other.transform.position - transform.position).normalized;

            // 右に進んでいて右側にぶつかった、または左に進んでいて左側にぶつかった
            if ((moveDirection > 0 && contactDirection.x > 0.5f) ||
                (moveDirection < 0 && contactDirection.x < -0.5f))
            {
                moveDirection = 0f;
                isStuck = true;
                Debug.Log("横から刺さった → 移動停止");
            }
        }
    }

    public void Damage()
    {
        //HPを1減らす
        hp = hp.SubHp(new Hp(1));
        Debug.Log(hp.GetValue());
    }

    void OnDestroy()
    {
        visual.Dispose();
    }

}
