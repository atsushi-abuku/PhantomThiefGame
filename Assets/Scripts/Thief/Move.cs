using UnityEngine;
using UnityEngine.InputSystem;

public class Move
{
    Rigidbody rigidBody;
    Vector3 velocity;
    ThiefInput thiefInput;

    private float moveDirection = 0;
    private MoveSpeed moveSpeed;

    private int jumpCount = 0;
    int maxJumpCount;

    private bool isRightPressed = false;
    private bool isLeftPressed = false;
    private bool isDashPressed = false;
    private bool isStuck = false;

    public Move(Rigidbody rb,ThiefInput input, int maxJumpCount)
    {
        rigidBody = rb;
        thiefInput = input;
        this.maxJumpCount = maxJumpCount;
        velocity = rigidBody.linearVelocity;
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
            { moveSpeed = moveSpeed.SubSpeed(new MoveSpeed(2f)); Debug.Log("motoni"); }
        };
        //Spaceでジャンプ    
        thiefInput.Move.Jump.started += Jump;
    }

    //ジャンプ
    private void Jump(InputAction.CallbackContext context)
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
        if (!isRightPressed && !isLeftPressed)
            moveDirection = 0f;
        if (!isDashPressed)
        moveSpeed = moveSpeed.Set(2f);

    }


    public void ApplyMovement()
    {
        velocity = rigidBody.linearVelocity;
        //壁に刺さっていないとき
        if (!isStuck) velocity.x = moveDirection * moveSpeed.GetValue();
        //壁に刺さっているとき
        else velocity.x = 0f;
        rigidBody.linearVelocity = velocity;
    }

    public void Dispose()
    {
        thiefInput?.Disable();
    }

    
    public void SetStuck(bool value)
    {
        isStuck = value;
    }

    public float GetDirection()
    {
        return moveDirection;
    }

    public int GetJumpCount()
    {
        return jumpCount;
    }

    public float GetSpeed()
    {
        return Mathf.Abs(rigidBody.linearVelocity.x);
    }
    
}
