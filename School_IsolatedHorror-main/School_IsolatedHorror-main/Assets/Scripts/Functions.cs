using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Functions : MonoBehaviour
{
    public GameObject[] lightGameObjects;
    public GameObject TV;

    public GameObject GeneratorParticle;

    public DoorEvent de;
    public BoxCollider genButton;

    public AudioSource generatorDown;
    public AudioSource skatAudio;
    public AudioListener skatAudioL;

    public GameObject XR;

    public XRSimpleInteractable flashlightPickupable;

    public GameObject monsterBody;
    public GameObject monsterEyes;

    public void PlayIntro()
    {
        skatAudio.Play();
    }

    public void EnableVR()
    {
        XR.SetActive(true);
        skatAudioL.enabled = false;
    }

    public void turnOffLights()
    {
        foreach (GameObject g in lightGameObjects)
        {
            g.SetActive(false);
        }
        TV.SetActive(false);
        generatorDown.Play();
    }

    public void turnOnLights()
    {
        foreach (GameObject g in lightGameObjects)
        {
            g.SetActive(true);
        }
    }

    public void destroyGen()
    {
        GeneratorParticle.SetActive(true);
    }

    public void SetupNextEventAndDissableGen()
    {
        genButton.enabled = false;
        de.genState = 1;
    }

    public void FlashlightOn()
    {
        flashlightPickupable.enabled = true;
    }

    public void ToggleMonsterVisible()
    {
        monsterBody.SetActive(!monsterBody.activeSelf);
    }




    public static Functions F;
    private void Awake()
    {
        F = this;
    }
}
