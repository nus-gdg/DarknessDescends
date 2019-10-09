using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrouchScript : MonoBehaviour
{
    public float ShieldVal;
    public float ShieldMem;
    private bool ShieldState = false;
    public float ShieldCD;
    private float ShieldCDMem;
    protected Animator animator;
    public Text Timer;
    public GameObject shieldBar;
    public GameObject shieldBarGreen;
    public float shieldBarLength;

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
                if (ShieldState == true)
                {
                    this.GetComponent<MovementController>().MovementEnabled = false;
                }
                if (ShieldState == false)
                {
                    ShieldCD = 1;
                }
                //ShieldCD = ShieldCDMem;
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
            ShieldCD = 0;
        }

        if (ShieldVal <= 0)
        {
            ShieldVal = 0;
            ShieldState = false;
            animator.SetBool("Crouch", false);
            ShieldCD = 1;
        }

        Timer.text = ShieldVal.ToString();

        Vector3 temp = shieldBarGreen.transform.localScale;
        temp.x = (ShieldVal / ShieldMem);
        shieldBarGreen.transform.localScale = temp;

        temp = shieldBarGreen.transform.localPosition;
        temp.x = shieldBarLength * (1 - (ShieldVal / ShieldMem)) * -0.5f;
        shieldBarGreen.transform.localPosition = temp;
        
    }
}
