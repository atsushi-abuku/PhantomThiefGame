using UnityEditor;
using UnityEngine;

public delegate void GimmickFunc(GameObject gameObject);

public enum GimmickType
{
    NONE,
    VANISH,
    FALL,
    RISE,
}

public class GimmickFuncGenerator
{
    private static GimmickFuncGenerator _instance = new GimmickFuncGenerator();

    private GimmickFuncGenerator()
    {

    }
    public static GimmickFuncGenerator GetInstance()
    {
        return _instance;
    }

    public GimmickFunc Generate(GimmickType type)
    {
        switch (type)
        {
            case GimmickType.NONE:
                return (GameObject gameObject) => {};
            case GimmickType.VANISH:
                return (GameObject gameObject) => { gameObject.SetActive(false);};
            case GimmickType.FALL:
                return (GameObject gameObject) => {
                    if(gameObject.GetComponent<Rigidbody>() == null)
                    {
                        gameObject.AddComponent<Rigidbody>();
                    }
                    gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    gameObject.GetComponent<Rigidbody>().useGravity = true;
                };
            case GimmickType.RISE:
                return (GameObject gameObject) =>
                {
                    if (gameObject.GetComponent<Rigidbody>() == null)
                    {
                        gameObject.AddComponent<Rigidbody>();
                    }
                    gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    gameObject.GetComponent<Rigidbody>().linearVelocity += new Vector3(0,1,0);
                };
            default:
                return (GameObject gameObject) => { };

        }
    }
}
