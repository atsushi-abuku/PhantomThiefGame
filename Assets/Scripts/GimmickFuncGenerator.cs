using UnityEditor;
using UnityEngine;

public delegate void GimmickFunc(GameObject gameObject);

public enum GimmickType
{
    Vanish,
    Fall,
    Up,
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
            case GimmickType.Vanish:
                return (GameObject gameObject) => { gameObject.SetActive(false);};
            case GimmickType.Fall:
                return (GameObject gameObject) => {
                    if(gameObject.GetComponent<Rigidbody>() == null)
                    {
                        gameObject.AddComponent<Rigidbody>();
                    }
                    gameObject.GetComponent<Rigidbody>().useGravity = true;
                };
            case GimmickType.Up:
                return (GameObject gameObject) =>
                {
                    if (gameObject.GetComponent<Rigidbody>() == null)
                    {
                        gameObject.AddComponent<Rigidbody>();
                    }
                    gameObject.GetComponent<Rigidbody>().linearVelocity += new Vector3(0,1,0);
                };
            default:
                return (GameObject gameObject) => { };

        }
    }
}
