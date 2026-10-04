using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableFlashlightPickup : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Functions.F.FlashlightOn();
    }
}
