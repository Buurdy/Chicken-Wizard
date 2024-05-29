using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
        [SerializeField] Transform target;
        public TextMeshProUGUI text;
        private int timesHit = 0;
        private float cooldown;

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
        agent.SetDestination(target.position);
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

    void  OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && cooldown <= 0)
        {
            timesHit +=1;
            cooldown = 1;
            hittext.Increase();
        }
    }
}
