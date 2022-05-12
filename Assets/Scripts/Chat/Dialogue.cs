using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    public Message[] messages;
    public bool isLoop;
    public bool isFinal;
    [HideInInspector]
    public bool isReadable;


}
