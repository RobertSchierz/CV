using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animationcontroller : MonoBehaviour {

    #region Attributes

    public Animator animator;
    public bool isInAnimation = false;



    #endregion

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    


    public void animationEnded(){
        this.isInAnimation = false;
       // Animate("idle");
    }


    public void Animate(string boolName)
    {

        if (this.isInAnimation == false)
        {
            this.isInAnimation = true;
            DisableOtherAnimations(animator, boolName);
            animator.SetBool(boolName, true);

        }

      
    }


    private void DisableOtherAnimations(Animator animator, string animation )
    {
        foreach(AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.name != animation)
            {
                animator.SetBool(parameter.name, false);
            }
        }
    }

}