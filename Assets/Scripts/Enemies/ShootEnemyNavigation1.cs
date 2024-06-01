using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.AI;

public class ShootEnemyNavigation : MonoBehaviour
{
        //[SerializeField] Transform target;
        public Transform target;
        public TextMeshProUGUI text;
        //private int timesHit = 0;
        private float cooldown;

        public Transform wandsprite;

        public IncreaseHitText hittext;
        
     NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        
      RaycastHit2D hit =  Physics2D.Raycast(wandsprite.position, gameObject.transform.position - target.position, 10f);

      if (hit.transform != target)
      {
        ToPlayer();
        print("playernotfound");
      }
      else{
        agent.SetDestination(gameObject.transform.position);
        print("playerfound");
      }
    }

    
    public void ToPlayer()
    {
        agent.SetDestination(target.position);
        

    }
}
