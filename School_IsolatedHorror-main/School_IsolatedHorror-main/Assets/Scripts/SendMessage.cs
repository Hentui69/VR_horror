using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMessage : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PhoneController.PS.NextMessage();
    }
}
