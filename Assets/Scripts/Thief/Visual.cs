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
    ThiefAudio thiefAudio;
    Dictionary<VisualType, GameObject> visuals;

    public Visual(GameObject parent, int value, ThiefInput input, ThiefAudio thiefAudio)
    {
        this.visual = value;
        this.thiefAudio = thiefAudio;
        thiefInput = input;
        //Q‚ð‰Ÿ‚³‚ê‚½‚ç
        thiefInput.Visual.Change.performed += ctx => CycleVisual();

        visuals = new Dictionary<VisualType, GameObject>
        {
            { VisualType.Thief, parent.transform.Find("ƒeƒBƒY_Thief").gameObject},
            { VisualType.GuardMan, parent.transform.Find("•ºŽm_Thief").gameObject},
            { VisualType.NPC, parent.transform.Find("’b–è‰®_Thief").gameObject},
        };

        SetActiveVisual((VisualType)value);
    }

    private void SetActiveVisual(VisualType activeType)
    {
        foreach (var kvp in visuals)
        {
            kvp.Value.SetActive(kvp.Key == activeType);
        }

    }

    private void CycleVisual()
    {
        visual = (visual + 1) % visuals.Count;
        VisualType type = (VisualType)visual;

        SetActiveVisual(type);

        thiefAudio.PlayTransform();
        Debug.Log("Žp" + (VisualType)visual);
    }



    public VisualType GetCurrentVisualType()
    {
        return (VisualType)visual;
    }
}

