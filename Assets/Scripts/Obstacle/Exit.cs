using UnityEngine;
public delegate void ExitFunc();

public class Exit : MonoBehaviour
{
    ExitFunc exitFunc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetExitFunc(ExitFunc func)
    {
        exitFunc = func;
    }

    private void OnTriggerEnter(Collider other)
    {
        exitFunc();
        this.gameObject.SetActive(false);
    }
}
