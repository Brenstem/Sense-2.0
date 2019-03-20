using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerDeath : RespawnTrigger {

    [SerializeField]
    Death playerDeath;
    
    void Start () {
        playerDeath.OnMove += PlayerDied;
	}

    private void PlayerDied(GameObject obj)
    {
        TriggerMove(obj);
    }
}
