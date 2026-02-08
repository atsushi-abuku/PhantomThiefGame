using UnityEngine;

public class FireCollider : MonoBehaviour
{
    [SerializeField] private SphereCollider sc;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private Color activeColor = Color.orange;
    InvokeGimmickFunc gimmickFunc;

    private void Awake()
    {
        if (sc == null)
        {
            sc = GetComponent<SphereCollider>();
        }
        if (ps == null)
        {
            ps = GetComponent<ParticleSystem>();
        }
    }
    private void Start()
    {
        if (ps == null)
        {
            Debug.LogError("ParticleSystem が見つかりません");
        }
    }

    public void SetGimmickFunc(InvokeGimmickFunc gimmickFunc)
    {
        this.gimmickFunc = gimmickFunc;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Thief")
        {
            gimmickFunc();
            var main = ps.main;
            main.startColor = activeColor;
        }
    }
}
