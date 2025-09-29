using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class HitBoxAttack : HitBox
{
    [SerializeField] int startupFrame;
    [SerializeField] int activeFrame;
    private int activateFrame;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activateFrame = activeFrame;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(activateFrame < 0)
        {
            activateFrame++;
        }
        else if(activateFrame < activeFrame)
        {
            activateFrame++;
            this.gameObject.SetActive(true);
        }
        else 
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Activate() {
        activateFrame = -startupFrame;
        this.gameObject.SetActive(false);
    }
}
