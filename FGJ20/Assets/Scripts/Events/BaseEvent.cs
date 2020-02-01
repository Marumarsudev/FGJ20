using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEvent : MonoBehaviour
{

    public virtual void CallEvent(GameObject interactor = null)
    {
        if(interactor != null)
            Debug.Log("This event was triggered by" + interactor.name);
        else
            Debug.Log("This is the default event.");
    }
}
