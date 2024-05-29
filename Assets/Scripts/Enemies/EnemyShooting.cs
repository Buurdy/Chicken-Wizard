using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AI;
using Unity.Collections;

public class EnemyShooting : MonoBehaviour
{
    public GameObject projectile;
    public Transform spawnPos;

    public Transform target;

    public GameObject target2;

   public NavMeshAgent agent;
    private float timer = 0;
   

    public LayerMask layermask;
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //agent.updateRotation = false;
        if(timer <= 0)
        {
           
            
        }
        else{
            timer -= Time.deltaTime;
        }
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position , transform.TransformDirection(Vector3.up));
transform.rotation = new Quaternion( 0 , 0 , rotation.z , rotation.w );
        RaycastHit2D hit =  Physics2D.Raycast(transform.position, target.position - transform.position,10f, layermask);
        Debug.DrawRay(transform.position, transform.right * 10f, Color.red);
    if (hit.collider != null)
    {
        

      if ( Physics2D.Raycast(transform.position, target.position - transform.position,10f, layermask))
      {
        if(hit.collider.gameObject.CompareTag("Player"))
        {
         if(timer <= 0)
        {
            GameObject instance = Instantiate(projectile, spawnPos.position, transform.rotation);
            
            instance.GetComponent<EnemyProjectile>().hitText = target2;
            timer = 1;
            print("shot");
        }
        agent.SetDestination(gameObject.transform.position);
        print("playerfound");
        }
      else{
        
        ToPlayer();
        print("playernotfound");
      }
      }
    }
        
    }

    public void ToPlayer()
    {
        agent.SetDestination(target.position);
        

    }
}
