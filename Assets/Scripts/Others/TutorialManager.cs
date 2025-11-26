using System.IO;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI explainText;
    [SerializeField] GameObject invisibleWall;
    [SerializeField] Needle needle;
    [SerializeField] GimmickDoor door;
    [SerializeField] Thief thief;
    [SerializeField] TextAsset tutorialText;
    string sentence;
    private StringReader reader;

    private int fase;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        reader = new StringReader(tutorialText.text);
        sentence = reader.ReadLine();
        fase = 0;
    }

    // Update is called once per frame
    void Update()
    {
        explainText.text = sentence;
        ChangeFase();
        Debug.Log(fase);
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
                if (door.IsVisualizeGimmick())
                {
                    fase += 1;
                }
                break;
            case 2:
                if (Mathf.Abs(needle.transform.position.x - thief.transform.position.x) < 2.0f)
                {
                    fase += 1;
                }
                break;
            case 3:
                if (needle.transform.position.x > thief.transform.position.x)
                {
                    fase += 1;
                }
                break;
        }
    }
}
