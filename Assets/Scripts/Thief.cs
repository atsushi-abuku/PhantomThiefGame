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
    public float jumpPower = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

     void Start()
     {
         hp = new Hp(3);
         rigidBody = GetComponent<Rigidbody>();
         thiefInput = new ThiefInput();
         //Dで右移動
         thiefInput.Move.MoveRight.performed += ctx => moveDirection = 1f;
        thiefInput.Move.MoveRight.canceled += ctx =>
        {
            if (moveDirection == 1f) moveDirection = 0f;
        };
         //Aで左移動
         thiefInput.Move.MoveLeft.performed += ctx => moveDirection = -1f;

        thiefInput.Move.MoveLeft.canceled += ctx =>
        {
            if (moveDirection == -1f) moveDirection = 0f;
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
    
    //Shiftでジャンプ(1段)
    void Jump(InputAction.CallbackContext context)
    {
        if (jumpCount < maxJumpCount) 
        {
            Vector3 velocity = rigidBody.linearVelocity;
            velocity.y = jumpPower;
            rigidBody.linearVelocity = velocity;
            jumpCount++;
            Debug.Log("Shift");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))  //空中にある足場にもGroundというtagをつける必要がある
        {
            jumpCount = 0;
        }
    }
} 
