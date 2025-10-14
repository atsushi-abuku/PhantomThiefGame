using Unity.VisualScripting;
using UnityEngine;

public interface IGuardMan
{
    void Stanby();//盗んだ後のフェーズが始まるときに呼ぶ
    void Attack();//怪盗に攻撃するときに呼ぶ
}
