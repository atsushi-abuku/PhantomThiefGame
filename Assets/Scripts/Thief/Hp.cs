using UnityEngine;
[System.Serializable]

public class Hp
{
    //???
    [SerializeField]
    private int value;
    public Hp(int value)
    {
        this.value = value;
    }

    public int GetValue()//??hp
    {
        return value;
    }

    //Thief.cs???Hp????
    //Hp ?console?????????????Thief.cs?console????????????
    public Hp SubHp(Hp hp)//hp????
    {
        if (this.value - hp.GetValue() < 0)
        {
            return new Hp(0);
        }
        return new Hp(this.value - hp.GetValue());
    }
}
