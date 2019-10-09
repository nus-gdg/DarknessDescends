using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace GDG
{
public class DamageTextManager : MonoBehaviour
{
    public GameObject DamageText;
    public Vector2 Offset;
    public Vector2 RandOffset = new Vector2(0.1f, 0.1f);

    [Header("Ally Colour Gradient")]
    public Color topAllyColour = Color.cyan;
    public Color botAllyColour = Color.blue;

    [Header("Enemy Colour Gradient")]
    public Color topEnemyColour = Color.yellow;
    public Color botEnemyColour = Color.red;

    public VertexGradient EnemyColours;

    void Start()
    {
        EventManager.Instance.AddListener<CharacterDamagedEvent>(OnCharacterDamaged);
    }

    void OnDestroy()
    {
        EventManager.Instance.RemoveListener<CharacterDamagedEvent>(OnCharacterDamaged);
    }

    public void OnCharacterDamaged(CharacterDamagedEvent damagedEvent)
    {
        if (DamageText != null)
        {
            Vector3 randOffset = new Vector3(Random.Range(0f, RandOffset.x), Random.Range(0f, RandOffset.y), 0);
            GameObject damageTextObj = Instantiate(
                DamageText,
                damagedEvent.Position + new Vector3(Offset.x, Offset.y, 0) + randOffset,
                Quaternion.identity
            );
            DamageText damageText = damageTextObj.GetComponentInChildren<DamageText>();
            VertexGradient gradient = damagedEvent.CharacterType == AllyType.Friendly ?
                new VertexGradient(topAllyColour, topAllyColour, botAllyColour, botAllyColour) :
                new VertexGradient(topEnemyColour, topEnemyColour, botEnemyColour, botEnemyColour);
            damageText.SetText(damagedEvent.Damage.ToString(), gradient);
        }
    }
}
}
