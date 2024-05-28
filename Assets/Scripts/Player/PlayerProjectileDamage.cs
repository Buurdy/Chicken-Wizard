using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage();
        }
    }
}
