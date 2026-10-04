using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class FlashlightControl : MonoBehaviour
{
    public XRRayInteractor leftHandRay;
    public GameObject vLightObj;

    public bool isActive = false;

    void Start()
    {
        // when this is ennabled stop ray of this hand...
        leftHandRay.enabled = false;
    }

    private bool didPress = false;
    // Update is called once per frame
    void Update()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics leftInputDeviceCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftInputDeviceCharacteristics, devices);

        if (devices.Count > 0)
        {
            devices[0].TryGetFeatureValue(CommonUsages.triggerButton, out bool thisFramePressd);

            if (thisFramePressd && !didPress)
            {
                didPress = true;
                isActive = !isActive;
                vLightObj.SetActive(isActive);
            }

            if (!thisFramePressd)
                didPress = false;
        }
    }
}
