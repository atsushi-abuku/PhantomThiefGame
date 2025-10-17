using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Thief")
        {
            Debug.Log("Damage");
        }
    }
}
