using UnityEngine;

public class Thief : MonoBehaviour
{
    Hp hp;
    Rigidbody rigidBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = new Hp(3);
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        MoveRight();
    }

    void MoveRight()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            rigidBody.linearVelocity += new Vector3(1, 0, 0);
        }
    }
}
