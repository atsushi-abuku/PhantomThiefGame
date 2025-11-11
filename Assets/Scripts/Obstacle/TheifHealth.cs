using UnityEngine;
//このスクリプトは使ってないので後で削除する

public class ThiefHealth : MonoBehaviour
{
    [SerializeField] private int initialHp = 100;
    private Hp hp;

    private void Awake()
    {
        hp = new Hp(initialHp);
    }
    public void TakeDamage(int amount)
    {
        hp = hp.SubHp(new Hp(amount));
        Debug.Log("今のHP: " + hp.GetValue());
        if (hp.GetValue() <= 0)
        {
            Debug.Log("やられちゃったね、おつ");
            //質問　ダメージ判定に関して質問
        }
    }
    public int GetHp()
    {
        return hp.GetValue();
    }
}
