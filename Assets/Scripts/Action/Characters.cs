using System.Collections.Generic;
using UnityEngine;

public class Characters
{
    List<ITalker> talkers;
    List<IGuardMan> guardMen;
    public Characters(GameObject fieldObjects)
    {
        talkers = new List<ITalker>();
        guardMen = new List<IGuardMan>();
        foreach (Transform child in fieldObjects.transform)
        {
            if (child.GetComponent<ITalker>() != null)
            {
                talkers.Add(child.GetComponent<ITalker>());
            }
            if (child.GetComponent<IGuardMan>() != null)
            {
                guardMen.Add(child.GetComponent<IGuardMan>());
            }
        }
    }
    public void FaseChange()
    {
        foreach (ITalker talker in talkers)
        {
            talker.DisableTalk();
        }

        foreach (IGuardMan guardMan in guardMen)
        {
            guardMan.Stanby();
        }
    }
}
