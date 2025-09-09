using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class MoveSpeed
{
    private float value;

    public MoveSpeed(float value)
    {
        this.value = value;
    }

    public float GetValue()
    {
        return value;
    }

    public MoveSpeedÅ@Set(float newValue)
    {
        return new MoveSpeed(newValue);
    }

    public MoveSpeed AddSpeed(MoveSpeed moveSpeed)
    {
        return new MoveSpeed(value + moveSpeed.GetValue());
    }
}
