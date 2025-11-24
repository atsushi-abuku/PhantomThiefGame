using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Thief : MonoBehaviour
{
    Visual visual;
    ThiefInput thiefInput;
    Rigidbody rigidBody;
    Animator thiefAnimator;
    CapsuleCollider capsuleCollider;
    ThiefAudio thiefAudio;
    //AudioSource audioSource;
    //AudioClip clip;

    public Hp hp;
    public int maxJumpCount = 1;
    public Foot foot;

    float originalHeight;
    Vector3 originalCenter;
    bool isCrouching = false;
    bool isSliding = false;
    float slideTimer = 0f;
    float slideDuration = 0.3f;

    bool isInvincible = false;
    float invincibleTimer = 0f;
    float invincibleDuration = 3f;//無敵時間

    Move move;
    public ThiefTalker thiefTalker;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thiefAudio = GetComponent<ThiefAudio>();
        hp = new Hp(3);
        thiefInput = new ThiefInput();
        thiefInput.Enable();
        visual = new Visual(gameObject, 0, thiefInput, thiefAudio);
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        thiefAnimator = GetComponent<Animator>();

        originalHeight = capsuleCollider.height;
        originalCenter = capsuleCollider.center;

        move = new Move(rigidBody,thiefInput, maxJumpCount, thiefAnimator, thiefAudio);
        thiefTalker = new ThiefTalker(thiefInput);

       
        //Cでスライディング・しゃがみ切り替え
        thiefInput.Move.Crouch.performed += ctx =>
        {
            if (move.GetSpeed() > 3f && !isCrouching) Sliding();
            else Crouch();
        };
}

    // Update is called once per frame
    void Update()
    {
        move.ApplyMovement();
        if (isSliding)
        {
            slideTimer += Time.deltaTime;
            if(slideTimer >= slideDuration)
            {
                isSliding = false;
                Crouch();
            }
        }
        //無敵時間のカウント
        if (isInvincible)
        {
            invincibleTimer += Time.deltaTime;
            if (invincibleTimer >= invincibleDuration)
            {
                isInvincible = false;
                Debug.Log("Im No Longer Invincible");
            }
        }
        //向きの切り替え
        float dir = move.GetDirection();
        if (dir>0 && isSliding) //スライディング
            transform.rotation = Quaternion.Euler(0, 120, 0);
        else if (dir<0 && isSliding) 
            transform.rotation = Quaternion.Euler(0, 290, 0);

        else if (dir > 0 && isCrouching)//しゃがみ
            transform.rotation = Quaternion.Euler(90, 90, 0);
        else if (dir < 0 && isCrouching)
            transform.rotation = Quaternion.Euler(90, 90, 180);

        else if (dir > 0)//その他(歩行)
            transform.rotation = Quaternion.Euler(0, 90, 0);
        else if (dir < 0)
            transform.rotation = Quaternion.Euler(0, 270, 0);

        //アニメーション
        thiefAnimator.SetFloat("speed", move.GetSpeed());
        thiefAnimator.SetInteger("JumpCount", move.GetJumpCount());
        thiefAnimator.SetFloat("VerticalSpeed", rigidBody.linearVelocity.y);
        thiefAnimator.SetBool("isCrouching", isCrouching);
        thiefAnimator.SetBool("isSlidingb", isSliding);
    }

    //着地
    public void OnFootTouchGround()
    {
        move.OnFootTouchGround();
    }

    //スライディング
    void Sliding()
    {
        isSliding = true;
        slideTimer = 0f;
        capsuleCollider.direction = 2;//軸変更
        capsuleCollider.center = new Vector3(originalCenter.x, originalCenter.y - originalHeight / 4, 0.3f);
        foot.SetCrouchState(true);
        thiefAudio.PlaySliding();
        Debug.Log("スライディング");
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") && isSliding)
        {
            Enemy enemy = other.GetComponent<Enemy>();//敵にEnemyというタグ
            if(enemy != null)
            {
                enemy.Damage();
                Debug.Log("スライディングヒット");
            }
        }
    }
    */

    //しゃがみ
    void Crouch()
    {
        //状態を切り替える
        isCrouching = !isCrouching;
        thiefAudio.PlayCrouch();

        if ((isCrouching))
        {
            capsuleCollider.direction = 1;//軸変更
            capsuleCollider.center = originalCenter;
            foot.SetCrouchState(true);
                transform.rotation = Quaternion.Euler(90, 90, 0);
            Debug.Log("しゃがみ状態");
        }
        //立ち
        else
        {
            capsuleCollider.direction = 1;//軸変更
            capsuleCollider.center = originalCenter;
            foot.SetCrouchState(false);
            transform.rotation = Quaternion.Euler(0, 90, 0);
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
        if (isInvincible)
        {
            Debug.Log("Im Invincible");
            return;
        }
        //HP - 1
        hp = hp.SubHp(new Hp(1));
        Debug.Log(hp.GetValue());

        isInvincible = true;
        invincibleTimer = 0f;
    }

    //現在の姿を得る
    public VisualType GetVisualType()
    {
        return visual.GetCurrentVisualType();
    }

  

private void OnDisable()
    {
        thiefInput?.Disable();
    }

    public void EnableInput()
    {
        thiefInput?.Enable();
    }

    public void DisableInput()
    {
        thiefInput?.Disable();
    }
}
