using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Notification", menuName = "ScriptableObjects/Notification")]
public class Notification : ScriptableObject
{
    public string source;
    public string text;
}
