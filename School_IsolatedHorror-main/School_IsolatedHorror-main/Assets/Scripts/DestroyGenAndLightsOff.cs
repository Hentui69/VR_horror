using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGenAndLightsOff : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Functions.F.turnOffLights();
        Functions.F.destroyGen();
    }
}
