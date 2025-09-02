using Unity.VisualScripting;
using UnityEngine;

public sealed class HitBoxGenerator
{
    private static HitBoxGenerator _instance = new HitBoxGenerator();

    private HitBoxGenerator()
    {

    }
    public static HitBoxGenerator GetInstance()
    {
        return _instance;
    }

    public GameObject Generate(Vector3 position, Vector3 hitRange)
    {
        GameObject generatedObject = new GameObject();
        generatedObject.AddComponent<HitBox>();
        generatedObject.GetComponent<HitBox>().SetHitRange(hitRange);
        return generatedObject;

    }
}
