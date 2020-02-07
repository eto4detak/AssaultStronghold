using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float currentHealth;
    public float maxHealth;

    private bool isDie;
    public float CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            if (currentHealth < 0)
            {
                currentHealth = 0;
                OnDie();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
    }


    private void OnDie()
    {
        isDie = true;
        StartCoroutine(Resurrection());
    }

    public IEnumerator Resurrection()
    {
        yield return new WaitForSeconds(2f);
    }
}
