using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDespawn : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private bool _isDestroy;
    private void OnEnable()
    {
        StartCoroutine(Co_DelayDespawn());
    }

    private IEnumerator Co_DelayDespawn()
    {
        yield return new WaitForSeconds(_delay);
        if(_isDestroy )
            Destroy(gameObject);
        else
            LeanPool.Despawn(this);
    }
}
