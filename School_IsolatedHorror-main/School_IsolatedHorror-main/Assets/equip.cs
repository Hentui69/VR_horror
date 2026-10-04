using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class equip : MonoBehaviour
{
    public BoxCollider box;

    public AudioSource inHand;
    public AudioSource offHand;

    public XRRayInteractor rayInteractor;

    public Canvas canvas;
    public MeshRenderer meshRenderer;

    public PhoneController pc;

    public Transform rightHand;

    public GameObject grabNotification;

    private bool justGribbed = false;
    // Update is called once per frame
    void Update()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightInputDeviceCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightInputDeviceCharacteristics, devices);

        if (devices.Count > 0)
        {
            InputDevice inp = devices[0];

            inp.TryGetFeatureValue(CommonUsages.gripButton, out bool gripped);

            if (gripped == false)
            {
                if (justGribbed == true)
                {
                    // just released the phone
                    pc.audioSource = offHand;
                    inHand.enabled = false;
                    offHand.enabled = true;

                    rayInteractor.enabled = true;

                    canvas.enabled = false;
                    meshRenderer.enabled = false;
                }

                justGribbed = false;
            }

            if (gripped && !justGribbed)
            {
                justGribbed = true;

                if (box.bounds.Contains(rightHand.position))
                {
                    // dissable grab mechanic
                    grabNotification.SetActive(false);
                    // just picked up the phone
                    pc.audioSource = inHand;
                    offHand.enabled = false;
                    inHand.enabled = true;

                    rayInteractor.enabled = false;

                    canvas.enabled = true;
                    meshRenderer.enabled = true;
                }
            }
        }
    }
}
