using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvent : MonoBehaviour
{
    public Animator mainAnimator;

    public GameObject monster;

    public BoxCollider bigCollider;
    public BoxCollider smallCollider;

    public BoxCollider plrCollider;
    /// <summary>
    /// 0 = nothing
    /// 1 = ready
    /// 2 = done
    /// </summary>
    public int genState = 0;

    void Update()
    {
        if (genState == 1)
        {
            if (plrCollider.bounds.Intersects(bigCollider.bounds))
            {
                monster.SetActive(true);
                if (plrCollider.bounds.Intersects(smallCollider.bounds))
                {
                    monster.GetComponent<Animator>().enabled = true;
                    genState = 2;

                    mainAnimator.SetTrigger("MonsterAppearance");
                }
            }
            else
            {
                monster.SetActive(false);
            }
        }
    }
}
