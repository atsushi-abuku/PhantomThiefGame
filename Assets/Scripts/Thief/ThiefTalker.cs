using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThiefTalker
{
    ITalker talkableCharacter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public ThiefTalker(ThiefInput thiefInput)
    {
        thiefInput.Talk.Talk.performed += StartTalking;
    }

    private void StartTalking(InputAction.CallbackContext context)
    {
        if (talkableCharacter != null)
        {
            talkableCharacter.Talk();
        }
    }

    public void SetTalkableCharacter(ITalker talkableCharacter)
    {
        this.talkableCharacter = talkableCharacter;
    }
}
