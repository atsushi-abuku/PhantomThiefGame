using UnityEngine;
[System.Serializable]

public class Hp
{
    //ïœêî
    [SerializeField]
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
