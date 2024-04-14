using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVuongLT : CharacterObject
{
    [SerializeField] private float _reachY = 5;
    protected override void LateUpdate()
    {
        if(transform.position.y < _reachY)
        {
            ReachTargetBoss();
        }
    }

    private void ReachTargetBoss()
    {

    }
}
