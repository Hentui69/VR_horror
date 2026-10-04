using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHipHitbox : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = transform.parent.position + (new Vector3(0, -1, 0));
        transform.rotation = new Quaternion(1, 0, 0, 0);
    }
}
