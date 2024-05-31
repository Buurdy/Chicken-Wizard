using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int Health;

    public EnemiesKilled dead;

    private bool kill = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (kill == true)
        {
Destroy(gameObject);
        }
    }

    public void TakeDamage()
    {
        Health -=1;
        if (Health <= 0)
        {
            dead.enemyHasDied();
            kill = true;
        }

    }
    
}
