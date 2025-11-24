using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI explainText;
    [SerializeField] GameObject invisibleWall;
    [SerializeField] Citizen talker1;
    [SerializeField] Citizen talker2;
    [SerializeField] Needle needle;
    [SerializeField] Thief thief;

    private int fase;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fase = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeFase();
    }

    void ChangeFase()
    {
        switch (fase)
        {
            case 0:
                if (needle.IsVisualizeGimmick())
                {
                    invisibleWall.SetActive(false);
                    fase += 1;
                }
                break;
            case 1:
                if (needle.IsVisualizeGimmick())
                {
                    invisibleWall.SetActive(false);
                    fase += 1;
                }
                break;
        }
    }
}
