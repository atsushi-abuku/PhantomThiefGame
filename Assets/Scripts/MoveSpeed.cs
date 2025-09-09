using UnityEngine;

public class MoveSpeed
{
    private float value;

    public MoveSpeed(float value)
    {
        this.value = value;
    }

    public float getValue()
    {
        return value;
    }

    public MoveSpeedÅ@Set(float newValue)
    {
        return new MoveSpeed(newValue);
    }
}
