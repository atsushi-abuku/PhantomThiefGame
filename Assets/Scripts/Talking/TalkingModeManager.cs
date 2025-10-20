using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.Universal;

class Choice
{
    public TalkerLine talkerLine;
    public string tag;

    public Choice(string choiceText) {
        string[] choiceDatum = choiceText.Split(" ");
        talkerLine = new TalkerLine(choiceDatum[0]);
        tag = choiceDatum[1];
    }
}

public class TalkingModeManager : MonoBehaviour, IGameMode
{
    [SerializeField] Thief thief;
    private ITalker talker;
    private string[] talkerNames;
    private StringReader reader;

    private List<Choice> thiefChoices;
    private int choiceId;

    private string line;
    private bool isEnd;
    private TalkerLine talkerLine;

    private TalkingInput talkingInput;

    private void Start()
    {
        choiceId = 0;
        talkerLine = new TalkerLine();
        thiefChoices = new List<Choice>();

        isEnd = true;

        talkingInput = new TalkingInput();
        talkingInput.process.decide.started += ctx => {
            if (thiefChoices.Count > 0) DecideChoice();
            else Next();

            Debug.Log(line);
            Debug.Log(talkerLine.name + ":" + "'" + talkerLine.sentence + "'");
        };

        talkingInput.process.up.started += ctx =>
        {
            ChangeChoiceId(false);
        };

        talkingInput.process.down.started += ctx =>
        {
            ChangeChoiceId(true);
        };

    }

    public void LoadTalkingText(string talkingFileName)
    {
        TextAsset talkingText = Resources.Load<TextAsset>(talkingFileName);
        reader = new StringReader(talkingText.text);
        line = reader.ReadLine();
        talkerNames = line.Split(',');
    }

    public void SetTalkingPartner(ITalker talker)
    {
        this.talker = talker;
        LoadTalkingText(talker.GetTalkingFileName());
    }

    public void StartMode()
    {
        isEnd = false;
        talkingInput.Enable();

        Next();
    }

    public void StopMode()
    {
        talkingInput.Disable();

    }

    private void Finish()
    {
        isEnd = true;
        StopMode();
        this.talker = null;
    }

    public string[] GetTalkers()
    {
        return talkerNames;
    }

    public TalkerLine GetTalkerLine()
    {
        return talkerLine;
    }

    public List<TalkerLine> GetChoices()
    {
        List<TalkerLine> choicesText = new List<TalkerLine>();
        foreach(Choice choice in thiefChoices)
        {
            choicesText.Add(choice.talkerLine);
        }
        return choicesText;
    }

    public int GetChoiceId()
    {
        return choiceId;
    }

    private void Next()
    {
        if (isEnd) return;
        Debug.Log("Next");
        if (reader.Peek() == -1) {
            Finish();
            return;
        }
        line = reader.ReadLine();
        if (line == "")
        {
            Next();
            return;
        }
        
        if (line[0] == '@')
        {
            TalkingTagParser(line);
        }
        else
        {
            TalkingLineParser(line);
        }
    }

    private void ChangeChoiceId(bool isDown)//isDownがtrueで+1,falseで-1する．
    {
        if(thiefChoices.Count > 0)
        {
            if(isDown)choiceId = (choiceId + 1) % thiefChoices.Count;
            else choiceId = (choiceId + thiefChoices.Count - 1) % thiefChoices.Count;   
        }
        Debug.Log(choiceId);
    }

    private void DecideChoice()
    {
        talkerLine = thiefChoices[choiceId].talkerLine;
        JumpReadLine(thiefChoices[choiceId].tag);
        //Debug.Log(thiefChoices[choiceId].tag);
        //Debug.Log(line);
        thiefChoices.Clear();
        Next();
    }

    public bool CheckIsEnd()
    {
        return isEnd;
    }

    private void TalkingTagParser(string tag)
    {
        string[] talkingTag = tag.Split(" ");
        switch (talkingTag[0])
        {
            case "@end":
                Finish();
                break;
            case "@ThiefType":
                //thiefのvisualとtalkingTag[1]が一緒かどうか確認
                //一緒ならtalkingTag[2]に飛ぶ
                /*if(thief.GetVisual() == talkingTag[1])
                {
                    JumpReadLine(talkingTag[2]);
                }*/
                break;
            case "@choice":
                line = reader.ReadLine();
                while (line != "@choiceEnd")
                {
                    thiefChoices.Add(new Choice(line));
                    line = reader.ReadLine();
                }
                break;
            case "@eventFlg":
                //ステージのeventFlgを立てる(使わないかも)
                break;
            case "@jump":
                JumpReadLine(talkingTag[1]);
                Next();
                break;
            case "@teachGimmick":
                if(talker != null) talker.TeachGimmick();
                break;
            default:
                Next();
                break;
        }
    }

    private void TalkingLineParser(string line)
    {
        if (line.Split(",").Length >= 3)
        {
            talkerLine = new TalkerLine(line);
        }
        else
        {
            Next();
        }
    }

    private void JumpReadLine(string talkingTag)
    {
        while (line != talkingTag && reader.Peek() != -1)
        {
            line = reader.ReadLine();
        }
    }
}
