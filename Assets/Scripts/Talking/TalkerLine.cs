using UnityEngine;

public class TalkerLine
{
    public string name;
    public string emotion;
    public string sentence;

    public TalkerLine(string talkerLine)
    {
        if (talkerLine.Split(",").Length < 3)
        {
            name = "";
            emotion = "";
            sentence = "";
        }
        else
        {
            string[] talkingLine = talkerLine.Split(",");
            name = talkingLine[0];
            emotion = talkingLine[1];
            sentence = talkingLine[2];
        }
    }

    public TalkerLine()
    {
        name = "";
        emotion = "";
        sentence = "";
    }

    public TalkerLine(string name,string emotion,string sentence)
    {
        this.name = name;
        this.emotion = emotion;
        this.sentence = sentence;
    }
}
