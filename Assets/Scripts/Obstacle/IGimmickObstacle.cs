using UnityEngine;

public interface IGimmickObstacle
{
    void FaseChange();//盗んだ後のフェーズが始まるときに呼ぶ
    void InvokeGimmick();//ギミック発生するときに呼ぶ
    void VisualizeGimmick();//ヒントが可視化するときに呼ぶ
}
