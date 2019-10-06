using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GDG;

[System.Serializable]
public class CharacterInjuredEvent : UnityEvent<Character, int>
{
}
