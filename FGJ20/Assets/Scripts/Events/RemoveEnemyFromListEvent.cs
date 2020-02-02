using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveEnemyFromListEvent : BaseEvent
{
    public ObjectSpawner spawner;

    public override void CallEvent(GameObject interactor = null)
    {
        spawner.RemoveFromEnemies(gameObject);
    }
}
