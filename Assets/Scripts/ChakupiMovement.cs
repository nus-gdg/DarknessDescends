using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(MovementController), typeof(ChakupiCharacterAnimator))]
public class ChakupiMovement : MonoBehaviour, InteractsWithPlatformEdgeDemarcator
{
    private Character character;
    private ChakupiCharacterAnimator characterAnimator;
    private MovementController movementController;

    public GameObject chargingParticleSystem;
    public float movementSpeed;
    public bool facingDirection;
    public GameObject chakupiThunder;
    public float projectileSpeed;

    public GameObject detector;

    enum ChakupiState
    {
        Moving, ChargingTransition, Charging, Fire, BetweenStates
        
    };

    ChakupiState chakupiState;

    void Start()
    {
        chargingParticleSystem.SetActive(false);

        chakupiState = ChakupiState.Moving;

        character = GetComponent<Character>();
        character.characterInjuredEvent.AddListener(GetHit);

        detector.GetComponent<PlayerDetector>().playerDetectedEvent.AddListener(DetectPlayer);

        characterAnimator = GetComponent<ChakupiCharacterAnimator>();
        movementController = GetComponent<MovementController>();

        if(facingDirection)
        {
            characterAnimator.faceRight();
        }
        else
        {
            characterAnimator.faceLeft();
        }
    }

    private void FixedUpdate()
    {
        switch(chakupiState)
        {
            case ChakupiState.Moving:
                characterAnimator.Move();
                if(facingDirection) 
                {
                    movementController.MoveRight(movementSpeed);
                }
                else
                {
                    movementController.MoveLeft(movementSpeed);
                }
                break;
            case ChakupiState.ChargingTransition:
                BeginCharge();
                break;
            case ChakupiState.Charging:
                ContinueCharge();
                break;
            case ChakupiState.Fire:
                Fire();
                break;
            default:
                break;
        }
    }

    private bool getStartingDirection()
    {
        Transform parentTransform = this.transform.parent;

        if(parentTransform == null)
        {
            return false;
        }
        else
        {
            return parentTransform.GetComponent<EnemySpawner>().getStartingDirection();
        }
    }

    public void InteractWithPlatformEdgeDemarcator()
    {
        facingDirection = !facingDirection;

        if(facingDirection)
        {
            //CASE: enemy should start going right
            characterAnimator.faceRight();
            movementController.MoveRight(movementSpeed);
        }
        else
        {
            //CASE: enemy should start going left
            characterAnimator.faceLeft();
            movementController.MoveLeft(movementSpeed);
        }
    }

    public void DetectPlayer()
    {
        if(chakupiState == ChakupiState.Moving)
        {
            chakupiState = ChakupiState.ChargingTransition;
        }
    }

    void BeginCharge()
    {
        characterAnimator.BeginCharge();
        StartCoroutine("BeginChargeRoutine");
        //change state to Charging after a the few milliseconds
    }

    IEnumerator BeginChargeRoutine()
    {
        chakupiState = ChakupiState.BetweenStates;
        yield return new WaitForSeconds(0.2f);
        if(chakupiState == ChakupiState.BetweenStates)
        {
            chakupiState = ChakupiState.Charging;
        }
    }

    void ContinueCharge()
    {
        characterAnimator.ContinueCharge();
        StartCoroutine("ContinueChargeRoutine");
        chargingParticleSystem.SetActive(true);
        //change state to Fire after 1.5 seconds
    }

    IEnumerator ContinueChargeRoutine()
    {
        chakupiState = ChakupiState.BetweenStates;
        yield return new WaitForSeconds(5f);
        if(chakupiState == ChakupiState.BetweenStates)
        {
            chakupiState = ChakupiState.Fire;
        }
    }

    void Fire()
    {
        if(chakupiState == ChakupiState.Fire){
            characterAnimator.Fire();
            ShootProjectile();
            chakupiState = ChakupiState.Moving;
            characterAnimator.Move();
            chargingParticleSystem.SetActive(false);
        }
    }

    void GetHit(Character character)
    {
        chakupiState = ChakupiState.Moving;
        chargingParticleSystem.SetActive(false);
    }

    private void ShootProjectile()
    {
        Vector3 projectilePosition = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
        Rigidbody2D rb = Instantiate(chakupiThunder, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();

        if(facingDirection)
        {
            rb.velocity = new Vector2(projectileSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-projectileSpeed, 0);
        }
    }
}
