using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class WalkSoundPlayer : MonoBehaviour
{
    public AudioSource walkSound;

    public BoxCollider bc1;
    public BoxCollider bc2;

    void Update()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightInputDeviceCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightInputDeviceCharacteristics, devices);

        if (devices.Count > 0)
        {
            InputDevice inp = devices[0];

            inp.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 vec);
            
            if (vec.magnitude > 0.1)
            {
                if (bc1.bounds.Intersects(bc2.bounds))
                {
                    if (walkSound.isPlaying)
                        walkSound.Pause();
                    return;
                }

                if (!walkSound.isPlaying)
                    walkSound.Play();
                return;
            }
        }

        // failsafe...
        if (walkSound.isPlaying)
            walkSound.Pause();
    }
}
