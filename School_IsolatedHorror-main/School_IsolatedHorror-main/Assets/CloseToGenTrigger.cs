using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseToGenTrigger : MonoBehaviour
{
    public Animator MainAnimator;
    private void OnTriggerEnter(Collider other)
    {
        MainAnimator.SetBool("Close to gen", true);
    }

    private void OnTriggerExit(Collider other)
    {
        MainAnimator.SetBool("Close to gen", false);
    }
}
