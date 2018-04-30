using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimBehaviour : StateMachineBehaviour {

	override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        animator.SetInteger("idleAnimID", Random.Range(0, 5));
	}
}
