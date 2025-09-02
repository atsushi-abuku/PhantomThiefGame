using UnityEngine;
using UnityEngine.InputSystem;

public class Thief : MonoBehaviour
{
    Hp hp;
    Rigidbody rigidBody;
    ThiefInput thiefInput;

    public int maxJumpCount = 1;
    public int jumpCount = 0;

    private float moveDirection = 0f;
    public float moveSpeed = 3f;

    private bool isRightPressed = false;
    private bool isLeftPressed = false;
    private bool isDashPressed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

     void Start()
     {
         hp = new Hp(3);
         rigidBody = GetComponent<Rigidbody>();
         thiefInput = new ThiefInput();
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
            moveSpeed = 4f;
        };
        thiefInput.Move.Dash.canceled += ctx =>
        {
            isDashPressed = false;
            if(jumpCount == 0) 
                moveSpeed = 2f;
        };
         //ジャンプ
         thiefInput.Move.Jump.started += Jump;
         thiefInput.Enable();
     }

     // Update is called once per frame
     void Update()
     {
         Vector3 velocity = rigidBody.linearVelocity;
         velocity.x = moveDirection * moveSpeed;
         rigidBody.linearVelocity = velocity;
     }
    
    //Spaceでジャンプ(1段)
    void Jump(InputAction.CallbackContext context)
    {
        //飛べるとき
        if (jumpCount < maxJumpCount) 
        {
            Vector3 velocity = rigidBody.linearVelocity;
            velocity.y = 8;
            rigidBody.linearVelocity = velocity;
            jumpCount++;
        }
    }

    //着地
    public void OnFootTouchGround()
    {
        jumpCount = 0;
        //着地した瞬間にキーが押されていなければ停止
        if(!isRightPressed && !isLeftPressed)
            moveDirection = 0f;
        if (!isDashPressed)
            moveSpeed = 2f;
    }
}
