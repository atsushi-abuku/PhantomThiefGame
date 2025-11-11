using UnityEngine;

public class FieldObjectsManager
{
    private Field field;
    private Characters characters;
    public FieldObjectsManager(GameObject fieldObjects)
    {
        field = new Field(fieldObjects);
        characters = new Characters(fieldObjects);
    }

    public void SetTreasureFunc(StolenFunc treasureFunc)
    {
        field.SetTreasureFunc(treasureFunc);
    }

    public void SetExitFunc(ExitFunc exitFunc)
    {
        field.SetExitFunc(exitFunc);
    }

    public void FaseChange()
    {
        field.FaseChange();
        characters.FaseChange();
    }
}
