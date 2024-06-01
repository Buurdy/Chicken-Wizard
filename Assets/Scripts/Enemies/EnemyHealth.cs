using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int Health;

    public EnemiesKilled dead;

    public GameObject[] components;
    public int chanceToDropComponent;
    int rndNum;
    int rndChance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TakeDamage()
    {
        Health -=1;
        if (Health <= 0)
        {
            dead.enemyHasDied();
            rndChance = Random.Range(0, 101);
            if(rndChance <= chanceToDropComponent)
            {
                rndNum = Random.Range(0, components.Length);
                Instantiate(components[rndNum], this.gameObject.transform.position, Quaternion.Euler(0, 0, 0));
            }

            Destroy(gameObject);
        }

    }
    
}
