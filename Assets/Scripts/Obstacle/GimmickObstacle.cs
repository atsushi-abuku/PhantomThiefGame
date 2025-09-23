using UnityEngine;

public abstract class GimmickObstacle : MonoBehaviour
{
    [SerializeField] protected GimmickTrigger trigger;
    protected bool visualizeFlg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        visualizeFlg = false;
    }
}
