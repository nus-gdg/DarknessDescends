using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GDG;

[System.Serializable]
public class CharacterDeathEvent : UnityEvent<Character, CharacterDeathReason>
{

}

public enum CharacterDeathReason
{
    KILLED_BY_DAMAGE,
    KILLED_BY_KILL_ZONE
}
