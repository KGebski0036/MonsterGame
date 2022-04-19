using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetIsJumping : StateMachineBehaviour
{
    private LocomotionMenager m_Menager;

    private void Awake()
    {
        m_Menager = FindObjectOfType<LocomotionMenager>();

    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isJumping", false);
        m_Menager.isJumping = false;
    }    
}
