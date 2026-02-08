using UnityEngine;
using UnityEngine.UIElements;

public class Fire : GimmickObstacle, IGimmickObstacle
{
    [SerializeField] private Color hintColor = Color.gray;
    ParticleSystem ps;
    BoxCollider bc;

    [SerializeField] FireCollider fireCollider;
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
        fireCollider.gameObject.SetActive(false);
        bc = gameObject.GetComponent<BoxCollider>();
        bc.enabled = false;
        fireCollider.SetGimmickFunc(InvokeGimmick);
    }

    public void FaseChange()
    {
        fireCollider.gameObject.SetActive(true);
    }
    public void InvokeGimmick()
    {
        ps = gameObject.GetComponent<ParticleSystem>();
        ps.Play(true);
        ps.GetComponent<Renderer>().enabled = true;
        bc.enabled = true;
    }
    public void VisualizeGimmick()
    {
        visualizeFlg = true;
        ps = gameObject.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startColor = hintColor;
        ps.Play(true);
        ps.GetComponent<Renderer>().enabled = true;
    }
}
