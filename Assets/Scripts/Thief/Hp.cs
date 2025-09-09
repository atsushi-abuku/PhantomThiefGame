using UnityEngine;

public class Hp
{
    //•Ï”
    private int value;
    
    public Hp(int value)
    {
        this.value = value;
    }

    public int GetValue()
    {
        return value;
    }

    public Hp SubHp(Hp hp)
    {
        return new Hp(this.value - hp.GetValue());
    }
}
