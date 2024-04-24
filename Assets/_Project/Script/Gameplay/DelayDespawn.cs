using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDespawn : MonoBehaviour
{
    [SerializeField] private float _delay;
    private void OnEnable()
    {
        StartCoroutine(Co_DelayDespawn());
    }

    private IEnumerator Co_DelayDespawn()
    {
        yield return new WaitForSeconds(_delay);
        LeanPool.Despawn(this);
    }
}
