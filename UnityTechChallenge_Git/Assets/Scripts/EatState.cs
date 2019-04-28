using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatState : StateMachineBehaviour {

    public float speed = 1.0f;

    private Walk walk;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.Log("We have entered the eat state");
        walk = animator.gameObject.GetComponent<Walk>();
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if(animator.GetFloat("distanceFromWaypoint") < 0.001f)
        {
            var collectibles = GameObject.FindGameObjectsWithTag("Collectible");
            var objectCount = collectibles.Length;
            foreach(var item in collectibles)
            { 
                if((item.GetComponent<Transform>().position.x - animator.GetFloat("targetX")) < 0.001f
                && (item.GetComponent<Transform>().position.z - animator.GetFloat("targetZ")) < 0.001f)
                {
                    Debug.Log("Found it");
                    Destroy(item);
                }
            }
        }
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.Log("We have left the eat state");
    }
}
