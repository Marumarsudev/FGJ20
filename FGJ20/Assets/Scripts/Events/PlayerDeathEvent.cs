using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathEvent : BaseEvent
{
    public override void CallEvent(GameObject interactor = null)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
