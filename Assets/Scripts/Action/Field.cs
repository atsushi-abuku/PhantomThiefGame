using UnityEngine;
using System.Collections.Generic;

public class Field
{
    List<IGimmickObstacle> gimmickObstacles;
    Treasure treasure;
    Exit exit;
    public Field(GameObject fieldObjects)
    {
        gimmickObstacles = new List<IGimmickObstacle>();
        foreach (Transform child in fieldObjects.transform)
        {
            if (child.GetComponent<IGimmickObstacle>() != null)
            {
                gimmickObstacles.Add(child.GetComponent<IGimmickObstacle>());
            }
            else if (child.GetComponent<Treasure>() != null)
            {
                treasure = child.GetComponent<Treasure>();
            }
            else if(child.GetComponent<Exit>() != null)
            {
                exit = child.GetComponent<Exit>();
            }
        }
    }

    public void FaseChange()
    {
        EnableGimmickObstacles();
    }

    public void SetTreasureFunc(StolenFunc treasureFunc)
    {
        treasure.SetStolenFunc(treasureFunc);
    }

    public void SetExitFunc(ExitFunc exitFunc)
    {
        exit.SetExitFunc(exitFunc);
    }

    private void EnableGimmickObstacles()
    {
        foreach (IGimmickObstacle gObstacles in gimmickObstacles)
        {
            gObstacles.FaseChange();
        }
        exit.gameObject.SetActive(true);
    }
}
