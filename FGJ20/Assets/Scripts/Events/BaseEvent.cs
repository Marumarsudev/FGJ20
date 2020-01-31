using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEvent : MonoBehaviour
{
    public virtual void CallEvent()
    {
        Debug.Log("This is an event.");
    }
}
