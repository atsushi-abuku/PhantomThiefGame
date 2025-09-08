using UnityEngine;

enum StageFase
{
    GO,
    BACK
}

public class Stage : MonoBehaviour
{
    private Timer timer;
    [SerializeField] float limitTime;
    [SerializeField] StageFase stageFase;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = new Timer(limitTime, FaseChange);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FaseChange()
    {

    }
}
