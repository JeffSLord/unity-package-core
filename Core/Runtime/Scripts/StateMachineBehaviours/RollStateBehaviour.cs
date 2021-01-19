using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core
{
    public class RollStateBehaviour : StateMachineBehaviour
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
           animator.GetComponent<CharacterBehavior>().character.CanMove = false;
           animator.GetComponent<CharacterBehavior>().character.CanAttack = false;
           animator.GetComponent<CharacterBehavior>().character.CanBlock = false;
           animator.GetComponent<CharacterBehavior>().character.CanDodge = false;
           animator.GetComponent<CharacterBehavior>().character.StopMovement();

        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.GetComponent<CharacterBehavior>().character.CanMove = true;
            animator.GetComponent<CharacterBehavior>().character.CanAttack = true;
            animator.GetComponent<CharacterBehavior>().character.CanBlock = true;
            animator.GetComponent<CharacterBehavior>().character.CanDodge = true;
            
        }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}