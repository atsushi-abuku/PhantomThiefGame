using UnityEngine;
public delegate void StolenFunc();

public class Treasure : MonoBehaviour
{
    [SerializeField] private Fire fire;
    StolenFunc stolenFunc;

    void Start()
    {
        if (fire == null)
        {
            fire = GetComponent<Fire>();
        }
    }

    public void SetStolenFunc(StolenFunc stolenFunc)
    {
        this.stolenFunc = stolenFunc;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Thief")
        {
                stolenFunc();
                Destroy(this.gameObject);
        }
    }
}
