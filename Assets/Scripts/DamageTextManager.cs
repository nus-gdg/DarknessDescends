using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageTextManager : MonoBehaviour
{
    public GameObject DamageText;
    public Vector3 Offset;
    public static DamageTextManager Instance;
    public Color allyColor = Color.blue;
    public Color enemyColor = Color.red;
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
            Vector3 randOffset = new Vector3(Random.Range(0, 1), Random.Range(0, 1), 0);
            GameObject damageTextObj = Instantiate(
                DamageText,
                character.gameObject.transform.position + Offset + randOffset,
                Quaternion.identity
            );
            DamageText damageText = damageTextObj.GetComponentInChildren<DamageText>();
            Color damageColor = character.characterType == AllyType.Friendly ?
                allyColor : enemyColor;
            damageText.SetText(damage.ToString(), damageColor);
        }
    }
}
