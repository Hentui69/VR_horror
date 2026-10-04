using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualGame : MonoBehaviour
{
    public static ActualGame AG;

    private void Awake()
    {
        AG = this;
    }

    public Animator monsterAnimator;

    private bool gameActive = false;

    public BoxCollider[] windows;

    public Transform flashlight;
    public FlashlightControl FC;

    private int lastChosenWindow = -1;
    private int chosenWindow = -1;

    private float cooldown = 0;

    private float jumpscareTimer = 5;
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            return;
        }

        if (!gameActive)
            return;

        jumpscareTimer -= Time.deltaTime;

        if (jumpscareTimer <= 0)
        {
            gameActive = false;
            return; // jumpscare
        }

        if (chosenWindow == -1)
        {
            chosenWindow = Random.Range(0, 3);
            Debug.Log(chosenWindow + " | " + lastChosenWindow);
            //while (chosenWindow != lastChosenWindow)
            //{
            //    Debug.Log(chosenWindow + " | " + lastChosenWindow);
            //    chosenWindow = Random.Range(0, 3);
            //}
            lastChosenWindow = chosenWindow;
            monsterAnimator.SetInteger("CurWindow", chosenWindow);
        }

        if (FC.isActive)
        {
            Ray ray = new Ray();

            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(flashlight.position, flashlight.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Did Hit on: " + hit.collider.name);

                if (hit.collider.gameObject == windows[chosenWindow].gameObject)
                {
                    monsterAnimator.SetTrigger("FlashedLights");
                    cooldown = 1;
                    chosenWindow = -1;
                }
            }
        }
    }

    public void StartGame()
    {
        gameActive = true;
        monsterAnimator.SetTrigger("StartGame");
    }
}
