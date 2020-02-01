using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    void OnTriggerStay(Collider col)
    {
        if(col.GetComponent<Rigidbody>())
        {
            col.GetComponent<Rigidbody>().AddForce(transform.right * 0.25f, ForceMode.Impulse);
        }
        else
        {
            col.transform.Translate(transform.right * 0.25f);
        }
    }
}
