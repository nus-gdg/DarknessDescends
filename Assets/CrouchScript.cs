using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrouchScript : MonoBehaviour
{
    public float ShieldVal;
    private float ShieldMem;
    private bool ShieldState = false;
    public float ShieldCD;
    private float ShieldCDMem;
    protected Animator animator;
    public Text Timer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        ShieldMem = ShieldVal;
        ShieldCDMem = ShieldCD;
        ShieldCD = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShieldCD <= 0)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                ShieldState = !ShieldState;
                animator.SetBool("Crouch", ShieldState);
                ShieldCD = ShieldCDMem;
            }

            //if (Input.GetKeyUp(KeyCode.X))
            //{
            //    ShieldState = false;
            //    animator.SetBool("Crouch", ShieldState);
            //    ShieldCD = ShieldCDMem;
            //}
        }




        if (ShieldState == true)
        {
            ShieldVal -= Time.deltaTime * 25;
        }

        if (ShieldState == false)
        {
            ShieldVal += Time.deltaTime*50;
        }

        if (ShieldVal >= ShieldMem)
        {
            ShieldVal = ShieldMem;
        }

        if (ShieldVal <= 0)
        {
            ShieldVal = 0;
            ShieldState = false;
            animator.SetBool("Crouch", false);
            ShieldCD = ShieldCDMem;
        }

        Timer.text = ShieldVal.ToString();
        ShieldCD -= Time.deltaTime;
    }
}
