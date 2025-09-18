using UnityEngine;

public class Foot : MonoBehaviour
{
    public Thief Thief;
    private BoxCollider footCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        footCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //‹ó’†‚É‚ ‚é‘«ê‚É‚àGround‚Æ‚¢‚¤tag‚ğ‚Â‚¯‚é•K—v‚ª‚ ‚é
        if (other.CompareTag("Ground")) 
        {
            Thief.OnFootTouchGround();
        }
    }

    public void SetCrouchState(bool isCrouching) 
    {
        if (isCrouching)
        {
            footCollider.center = new Vector3(0.3f, 0, 0);
            footCollider.size = new Vector3(1f, 0.1f, 1f);
            Debug.Log("syagami");
        }
        else 
        {
            footCollider.center = Vector3.zero;
            footCollider.size = new Vector3(0.1f, 0.1f, 1f);
        }
    }
}
