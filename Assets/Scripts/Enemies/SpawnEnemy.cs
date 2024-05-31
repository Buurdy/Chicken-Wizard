using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnEnemy : MonoBehaviour
{

    public GameObject bashEnemy;

    public GameObject shootEnemy;


    public Transform player;

    public TextMeshProUGUI text;

    public IncreaseHitText hittext;

    public GameObject damageManager;
    public EnemiesKilled killed;

    public bool killbased;


    public bool timerbased;

    public float basetimer;

    private float timer;

    private float randomNumber;
    // Start is called before the first frame update
    void Awake()
    {
      randomNumber = Random.Range(0, 3);
      if(randomNumber > 1)
      {
      SpawnBash();

      }
      else
      {
      SpawnShoot();
      }
      timer = basetimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerbased == true){
          timer -= Time.deltaTime;

          if(timer <= 0){
            timer = basetimer;
            randomNumber = Random.Range(0, 3);
      if(randomNumber > 1)
      {
      SpawnBash();

      }
      else
      {
      SpawnShoot();
      }
          }
        }
    }

    public void SpawnonKill(){
      if (killbased == true)
      {
        randomNumber = Random.Range(0, 3);
      if(randomNumber > 1)
      {
      SpawnBash();

      }
      else
      {
      SpawnShoot();
      }
      }
    }

    public void SpawnBash()
    {
      GameObject enemy =  Instantiate(bashEnemy, gameObject.transform);
      enemy.GetComponent<EnemyNavigation>().target = player;
      enemy.GetComponent<EnemyNavigation>().text = text;
      enemy.GetComponent<EnemyNavigation>().hittext = hittext;
      enemy.GetComponent<EnemyHealth>().dead = killed;
    }

    public void SpawnShoot()
    {
GameObject enemy =  Instantiate(shootEnemy, gameObject.transform);
        /*
      enemy.GetComponent<EnemyShooting>().target = player;
      enemy.GetComponent<EnemyShooting>().target2 = damageManager;*/
      enemy.GetComponent<EnemyHealth>().dead = killed;
    }
}
