using UnityEngine;

public class Fire : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Thief")
        {
            var thief = collider.GetComponentInParent<Thief>();
            if (thief != null)
            {
                thief.Damage();
            }
        }
    }
}
