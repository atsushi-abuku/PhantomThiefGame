using UnityEngine;
using UnityEngine.InputSystem;

public class Thief : MonoBehaviour
{
    Hp hp;
    Rigidbody rigidBody;
    ThiefInput thiefInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        hp = new Hp(3);
        rigidBody = GetComponent<Rigidbody>();
        thiefInput = new ThiefInput();
        thiefInput.Move.MoveRight.performed += MoveRight;
        thiefInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    void MoveRight(InputAction.CallbackContext context)
    {
        rigidBody.linearVelocity += new Vector3(1, 0, 0);
        Debug.Log("D");
    }
}
