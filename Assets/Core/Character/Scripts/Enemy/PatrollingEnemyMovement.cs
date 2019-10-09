using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GDG;

[RequireComponent(typeof(Rigidbody2D), typeof(MovementController), typeof(CharacterAnimator))]
public class PatrollingEnemyMovement : MonoBehaviour, InteractsWithPlatformEdgeDemarcator
{
    private CharacterAnimator characterAnimator;
    private MovementController movementController;

    private bool facingDirection; //true means facing/moving right, false means facing/moving left

    [SerializeField]
    public float movementSpeed;

    private void Start()
    {
        characterAnimator = GetComponent<CharacterAnimator>();
        movementController = GetComponent<MovementController>();
        facingDirection = getStartingDirection(); //will change later based on implementation of spawn points

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
        characterAnimator.Move();
        if(facingDirection)
        {
            movementController.MoveRight(movementSpeed);
        }
        else
        {
            movementController.MoveLeft(movementSpeed);
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
}