using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : StateMachineBehaviour {

    public float speed = 1.0f;

    private Walk walk;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.Log("We have entered the patrol state");
        walk = animator.gameObject.GetComponent<Walk>();
        walk.SetNextPoint();
    }

	// OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //float step = speed * Time.deltaTime;
        //Vector3 playerPosition = animator.gameObject.transform.position;
        //Vector3 nextPosition = new Vector3(animator.GetFloat("targetX"), 0, animator.GetFloat("targetZ"));
        //playerPosition = Vector3.MoveTowards(playerPosition, nextPosition, step);
        //if (animator.GetFloat("distanceFromWaypoint") < 0.001f)
        //{
        //    walk.SetNextPoint();
        //}
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.Log("We have left the patrol state");
    }
}
