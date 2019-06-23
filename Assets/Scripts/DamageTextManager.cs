using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageTextManager : MonoBehaviour
{
    public GameObject DamageText;
    public Vector3 Offset;
    public static DamageTextManager Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Two Many Damage Text Managers, destroying this one");
            Destroy(this);
        }
        Instance = this;
    }

    public void OnCharacterInjured(Character character, int damage)
    {
        if (DamageText != null)
        {
            GameObject damageTextObj = Instantiate(
                DamageText,
                character.gameObject.transform.position + Offset,
                Quaternion.identity
            );
            DamageText damageText = damageTextObj.GetComponentInChildren<DamageText>();
            Color damageColor = character.characterType == AllyType.Friendly ?
                Color.blue : Color.red;
            damageText.SetText(damage.ToString(), damageColor);
        }
    }
}
