using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimationTrigger : MonoBehaviour
{

    public void ResetTrigger(string trigger)
    {
        GetComponent<Animator>().ResetTrigger(trigger);
    }

}
