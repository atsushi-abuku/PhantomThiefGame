using System.IO;
using UnityEngine;

public class TalkFaseManager
{
    GameObject talkView;
    GameObject leftTalker;
    GameObject rightTalker;
    private StringReader reader;
    public TalkFaseManager(GameObject talkView)
    {
        this.talkView = talkView;
        talkView.SetActive(false);
    }

    public void StartThiefTalking(string talkingFileName, Thief thief)
    {
        LoadTalkingFile(talkingFileName);
        string line = reader.ReadLine();
        string[] talkers = line.Split(",");
        leftTalker = Resources.Load("Prefabs/" + talkers[0]) as GameObject;
        rightTalker = Resources.Load("Prefabs/" + talkers[1]) as GameObject;
    }

    public void StartTalking(string talkingFileName)
    {
        LoadTalkingFile(talkingFileName);

        string line = reader.ReadLine();
        string[] talkers = line.Split(",");
        leftTalker = Resources.Load("Prefabs/" + talkers[0]) as GameObject;
        rightTalker = Resources.Load("Prefabs/" + talkers[1]) as GameObject;

        while (line == "@end")
        {
            line = reader.ReadLine();
            if (line[0] == '@')
            {
                TalkingTagParser(line);
            }
            else
            {
                TalkingLineParser(line);
            }
        }
    }

    private void LoadTalkingFile(string talkingFileName)
    {
        TextAsset talkingText = Resources.Load(talkingFileName) as TextAsset;
        reader = new StringReader(talkingText.text);
    }

    private void TalkingTagParser(string tag)
    {
        string[] talkingTag = tag.Split(" ");
        switch (talkingTag[0])
        {
            case "@Thief":
                break;
        }
    }

    private void TalkingLineParser(string line)
    {
        string[] talkingLine = line.Split(",");
        
    }
}
