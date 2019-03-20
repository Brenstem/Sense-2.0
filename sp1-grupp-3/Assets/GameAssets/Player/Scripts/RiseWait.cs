using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseWait : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        //Debug.Log(animator.gameObject.GetComponent<PlayerMovement>());
        //GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
        //GameObject.Find("Player").GetComponent<PlayerJump>().enabled = false;
        if (stateinfo.IsName("Player_Rise"))
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
            GameObject.Find("Player").GetComponent<PlayerJump>().enabled = false;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        if (stateinfo.IsName("Player_Rise"))
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
            GameObject.Find("Player").GetComponent<PlayerJump>().enabled = true;
        }
        //Debug.Log(animator.gameObject.GetComponent<PlayerMovement>());
        //GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
        //GameObject.Find("Player").GetComponent<PlayerJump>().enabled = true;
    }
    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
