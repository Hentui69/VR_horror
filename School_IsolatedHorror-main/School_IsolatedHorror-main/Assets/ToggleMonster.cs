using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMonster : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Functions.F.ToggleMonsterVisible();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Functions.F.ToggleMonsterVisible();
    }
}
