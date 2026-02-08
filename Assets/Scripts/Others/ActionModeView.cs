using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public class ActionModeView : MonoBehaviour
{
    [SerializeField] ActionModeManager actionModeManager;
    [SerializeField] TextMeshProUGUI collectRateTMP;
    [SerializeField] TextMeshProUGUI timeTMP;
    [SerializeField] GameObject hpObject;
    [SerializeField] Thief thief;
    [SerializeField] TextMeshProUGUI gameStateTMP;
    GameObject[] hpObjects;

    [SerializeField] Vector3 beginHpPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hpObjects = new GameObject[thief.hp.GetValue()];
        for(int i = 0; i < hpObjects.Length; i++)
        {
            hpObjects[i] = Instantiate(hpObject);
            hpObjects[i].transform.parent = transform;
            hpObjects[i].transform.localScale = Vector3.one * 15;
            hpObjects[i].transform.localPosition = beginHpPosition + new Vector3(40,0,0) * i;
        }
        gameStateTMP.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        collectRateTMP.text = "ƒqƒ“ƒg‰ñŽû—¦F" + actionModeManager.GetActionCollectRate() + "%";
        for (int i = 0; i < hpObjects.Length; i++)
        {
            if(i < thief.hp.GetValue()) hpObjects[i].SetActive(true);
            else hpObjects[i].SetActive(false);
        }
        if(actionModeManager.GetActionFase() == ActionFase.CLEARED)
        {
            gameStateTMP.text = "CLEAR";
            gameStateTMP.color = Color.yellow;
        }
        else if(actionModeManager.GetActionFase() == ActionFase.GAMEOEVR)
        {
            gameStateTMP.text = "GAME OVER";
            gameStateTMP.color = Color.gray;
        }
    }
}
