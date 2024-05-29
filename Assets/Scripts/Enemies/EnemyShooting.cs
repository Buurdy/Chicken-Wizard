using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyShooting : MonoBehaviour
{
    public GameObject projectile;
    public Transform spawnPos;

    public Transform target;

    public GameObject target2;

   
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0)
        {
            GameObject instance = Instantiate(projectile, spawnPos.position, transform.rotation);
            
            instance.GetComponent<EnemyProjectile>().hitText = target2;
            timer = 1;
            //print("e");
        }
        else{
            timer -= Time.deltaTime;
        }
        transform.right = target.position - transform.position;
    }
}
