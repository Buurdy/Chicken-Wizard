using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesKilled : MonoBehaviour
{

    public int requiredKills;

    public int kills;

    public SpawnEnemy[] enemiespawners;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void enemyHasDied()
    {
        kills+=1;
        if(kills==requiredKills)
        {
            for (int i = 0; i < enemiespawners.Length; i++){
                enemiespawners[i].SpawnonKill();
                
            }
            kills = 0;
        }
    }
}
