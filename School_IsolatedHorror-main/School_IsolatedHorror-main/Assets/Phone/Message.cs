using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Message", menuName = "ScriptableObjects/Message")]
public class Message : ScriptableObject
{
    [Header("Sender (int):\n0: From mother\n1: From unknown\n2: To mother\n3: To unknown")]
    public int sender;
    [Space(20)]
    public string text;
    public Image img;
}
