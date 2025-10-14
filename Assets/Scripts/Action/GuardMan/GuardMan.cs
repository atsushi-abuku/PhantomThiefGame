using Unity.VisualScripting;
using UnityEngine;

public class GuardMan : MonoBehaviour, IGuardMan
{
    [SerializeField] HitBoxAttack hitBoxAttack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Stanby()
    {
        this.enabled = true;
        this.gameObject.SetActive(true);
    }

    public void Attack()
    {

    }
}
