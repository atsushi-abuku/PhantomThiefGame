using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Thief : MonoBehaviour
{
    Hp hp;
    Visual visual;
    Rigidbody rigidBody;
    Animator thiefAnimator;
    CapsuleCollider capsuleCollider;

    public int jumpCount = 0;
    public int maxJumpCount = 1;
    public Foot foot;

    float originalHeight;
    Vector3 originalCenter;
    bool isCrouching = false;

    Move move;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = new Hp(3);
        visual = new Visual(0);
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        thiefAnimator = GetComponent<Animator>();

        originalHeight = capsuleCollider.height;
        originalCenter = capsuleCollider.center;

        move = new Move(rigidBody, jumpCount, maxJumpCount);

        ThiefInput thiefInput = new ThiefInput();
        thiefInput.Enable();
        //Cでしゃがみ切り替え
        thiefInput.Move.Crouch.performed += ctx => Crouch();
}

// Update is called once per frame
void Update()
     {
        move.ApplyMovement();
        jumpCount = move.GetJumpCount();
        //向きの切り替え
        float dir = move.GetDirection();
        if (dir > 0) transform.rotation = Quaternion.Euler(0, 90, 0);
        else if (dir < 0) transform.rotation = Quaternion.Euler(0, 270, 0);
        //アニメーション
        thiefAnimator.SetFloat("speed", move.GetSpeed());
        thiefAnimator.SetInteger("JumpCount", move.GetJumpCount());
        thiefAnimator.SetBool("isCrouching", isCrouching);
    }

    //着地
    public void OnFootTouchGround()
    {
        move.OnFootTouchGround();
    }

    void Crouch()
    {
        //状態を切り替える
        isCrouching = !isCrouching;

        if (isCrouching)
        {
            capsuleCollider.direction = 2;
            capsuleCollider.center = new Vector3(originalCenter.x, originalCenter.y-originalHeight/4, 0.3f);
            foot.SetCrouchState(true);
            Debug.Log("しゃがみ状態");
        }
        else
        {
            capsuleCollider.direction = 1;
            capsuleCollider.center = originalCenter;
            foot.SetCrouchState(false);
            Debug.Log("立ち状態");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Vector3 contactDirection = (other.transform.position - transform.position).normalized;

            // 右に進んでいて右側にぶつかった、または左に進んでいて左側にぶつかった
            if ((move.GetDirection() > 0 && contactDirection.x > 0.5f) ||
                (move.GetDirection() < 0 && contactDirection.x < -0.5f))
            {
                move.SetStuck(true);
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
