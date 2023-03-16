using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Arms _arms;

    public void Attack()
    {
        StartCoroutine(Attacking());
    }

    private IEnumerator Attacking()
    {
        _arms.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        _arms.gameObject.SetActive(false);
    }
}
