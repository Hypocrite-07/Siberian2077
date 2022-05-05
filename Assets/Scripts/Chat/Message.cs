using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Message
{
    [TextArea(4, 10)]
    public string messageText, notificationText;
    public string messageFinalTextName, nameNpc;
    public bool isNPC, isAuthor;
    public Color color;
    
    /*
    public static Message getFormatMessage(string text, bool isNPC)
    {
        Message message = new Message();
        message.messageText = text;
        message.isNPC = isNPC;
        return message;
    }
    */
}
