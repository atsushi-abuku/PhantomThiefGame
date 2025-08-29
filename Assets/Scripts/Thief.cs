using UnityEngine;
using UnityEngine.InputSystem;

public class Thief : MonoBehaviour
{
    Hp hp;
    Rigidbody rigidBody;
    ThiefInput thiefInput;

    public int maxJumpCount = 1;
    public int jumpCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        hp = new Hp(3);
        rigidBody = GetComponent<Rigidbody>();
        thiefInput = new ThiefInput();
        thiefInput.Move.MoveRight.started += MoveRight;
        thiefInput.Move.MoveLeft.started += MoveLeft;
        thiefInput.Move.Jump.started += Jump;
        thiefInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //Dで右移動
    void MoveRight(InputAction.CallbackContext context)
    {
        rigidBody.linearVelocity += new Vector3(1, 0, 0);
        Debug.Log("D");
    }

    //Aで左移動
    void MoveLeft(InputAction.CallbackContext context) 
    {
        rigidBody.linearVelocity -= new Vector3(1, 0, 0);
        Debug.Log("A");
    }

    //Shiftでジャンプ(1段)
    void Jump(InputAction.CallbackContext context)
    {
        if (jumpCount < maxJumpCount) 
        { 
            rigidBody.linearVelocity += new Vector3(0, 5, 0);
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
