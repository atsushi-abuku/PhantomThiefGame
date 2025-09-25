using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private float smoothSpeed = 5f;
    public ActionFase actionFase;

    void Update()
    {
        if(target == null) return;
        Vector3 desiredPosition;
        //â°à⁄ìÆí«è]
        if (actionFase == ActionFase.GO)
        {
            desiredPosition = new Vector3(target.position.x + 3f, transform.position.y, transform.position.z);
        }
        else if(actionFase == ActionFase.BACK)
        {
            desiredPosition = new Vector3(target.position.x - 3f, transform.position.y, transform.position.z);
        }
        else
        {
            desiredPosition = new Vector3(target.position.x , transform.position.y, transform.position.z);
        }
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
