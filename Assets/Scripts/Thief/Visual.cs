using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum VisualType
{
    Thief = 0,
    GuardMan = 1,
    NPC = 2,
}

public class Visual 
{
    private int visual;
    ThiefInput thiefInput;
    Dictionary<VisualType, GameObject> visuals = new Dictionary<VisualType, GameObject>
    {
        { VisualType.Thief,null },
        { VisualType.GuardMan,null },
        { VisualType.NPC,null },
    };

    public Visual(int value)
    {
        this .visual = value;
        thiefInput = new ThiefInput();
        thiefInput.Enable();
        //Q‚ð‰Ÿ‚³‚ê‚½‚ç
        thiefInput.Visual.Change.performed += ctx => CycleVisual();
    }

    private void CycleVisual()
    {
        visual = (visual + 1) % visuals.Count;
        Debug.Log("Žp" +  (VisualType)visual);
    }
    
    public void Dispose()
    {
        thiefInput.Dispose();
    }
}
