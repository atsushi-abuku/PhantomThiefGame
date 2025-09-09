using UnityEngine;
using UnityEngine.InputSystem;

public class Visual : MonoBehaviour
{
    VisualInput visualInput;
    public int[] visuals = { 0, 1, 2 };
    private int currentIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        visualInput = new VisualInput();
        visualInput.Enable();
        //Q‚ð‰Ÿ‚³‚ê‚½‚ç
        visualInput.VisualChange.Change.performed += ctx => CycleVisual();
    }

    void CycleVisual()
    {
        currentIndex = (currentIndex + 1) % visuals.Length;
        Debug.Log("Žp" +  currentIndex);
    }
    
    void OnDestroy()
    {
        visualInput.Dispose();
    }
}
