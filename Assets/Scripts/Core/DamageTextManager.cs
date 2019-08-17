using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class DamageTextManager : MonoBehaviour
{
    public GameObject DamageText;
    public Vector2 Offset;
    public Vector2 RandOffset = new Vector2(0.1f, 0.1f);
    public static DamageTextManager Instance;

    [Header("Ally Colour Gradient")]
    public Color topAllyColour = Color.cyan;
    public Color botAllyColour = Color.blue;

    [Header("Enemy Colour Gradient")]
    public Color topEnemyColour = Color.yellow;
    public Color botEnemyColour = Color.red;

    public VertexGradient EnemyColours;
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
            Vector3 randOffset = new Vector3(Random.Range(0f, RandOffset.x), Random.Range(0f, RandOffset.y), 0);
            GameObject damageTextObj = Instantiate(
                DamageText,
                character.gameObject.transform.position + new Vector3(Offset.x, Offset.y, 0) + randOffset,
                Quaternion.identity
            );
            DamageText damageText = damageTextObj.GetComponentInChildren<DamageText>();
            VertexGradient gradient = character.characterType == AllyType.Friendly ?
                new VertexGradient(topAllyColour, topAllyColour, botAllyColour, botAllyColour) :
                new VertexGradient(topEnemyColour, topEnemyColour, botEnemyColour, botEnemyColour);
            damageText.SetText(damage.ToString(), gradient);
        }
    }
}
