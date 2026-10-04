using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Functions.F.PlayIntro();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Functions.F.EnableVR();
    }
}
