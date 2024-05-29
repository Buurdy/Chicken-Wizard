using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnEnemy : MonoBehaviour
{

    public GameObject bashEnemy;


    public Transform player;

    public TextMeshProUGUI text;

    public IncreaseHitText hittext;
    // Start is called before the first frame update
    void Awake()
    {
      GameObject enemy =  Instantiate(bashEnemy, gameObject.transform);

      enemy.GetComponent<EnemyNavigation>().target = player;
      enemy.GetComponent<EnemyNavigation>().text = text;
      enemy.GetComponent<EnemyNavigation>().hittext = hittext;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
