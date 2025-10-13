using Unity.VisualScripting;
using UnityEngine;

public interface ITalker
{
    void Talk();

    void TeachGimmick();

    void DisableTalk();

    string GetTalkingFileName();
}
