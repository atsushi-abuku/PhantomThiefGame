using UnityEngine;

public class Foot : MonoBehaviour
{
    public Thief Thief;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
}
