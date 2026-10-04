using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffLights : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Functions.F.turnOffLights();
    }
}
