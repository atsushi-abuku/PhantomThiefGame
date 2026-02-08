using UnityEngine;

public interface IGimmickObstacle
{
    void FaseChange();//盗んだ後のフェーズが始まるときに呼ぶ
    void InvokeGimmick();//ギミックが発生するときに呼ぶ
    void VisualizeGimmick();//ヒントが可視化するときに呼ぶ
    bool IsVisualizeGimmick();//ヒントが可視化されているならtrue
}
