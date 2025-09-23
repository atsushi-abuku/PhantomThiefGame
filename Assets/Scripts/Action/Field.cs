using UnityEngine;
using System.Collections.Generic;

public class Field
{
    List<IGimmickObstacle> gimmickObstacles;
    Treasure treasure;
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

    private void EnableGimmickObstacles()
    {
        foreach (IGimmickObstacle gObstacles in gimmickObstacles)
        {
            gObstacles.FaseChange();
        }
    }
}
