using UnityEngine;
using UnityEngine.UIElements;

public class Fire : MonoBehaviour
{
    [SerializeField] private Color hintColor = Color.gray;
    ParticleSystem ps;
    BoxCollider bc;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Thief")
        {
            var thief = collider.GetComponent<Thief>();
            if (thief != null)
            {
                thief.Damage();
            }
        }
    }

    public void Start()
    {
        ps = gameObject.GetComponent<ParticleSystem>();
        ps.Stop(true);
        ps.GetComponent<Renderer>().enabled = false;
        bc = gameObject.GetComponent<BoxCollider>();
        bc.enabled = false;
    }

    public void Activate()
    {
        ps = gameObject.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startColor = hintColor;
        ps.Play(true);
        ps.GetComponent<Renderer>().enabled = true;
        bc = gameObject.GetComponent<BoxCollider>();
        bc.enabled = true;
    }
}
