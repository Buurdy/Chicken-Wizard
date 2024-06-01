using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using System;

public class IncreaseHitText : MonoBehaviour
{
    public TextMeshProUGUI text;

    public int maxAmountOfHits;
    public GameObject gameOverScreen;

    public PlayerHealth pHealth;
        private int timesHit = 0;
     private float cooldown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0){
            cooldown -= Time.deltaTime;
        }

        if(timesHit >= maxAmountOfHits)
        {
            gameOverScreen.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    public void Increase()
    {
        timesHit += 1;
        text.SetText("Times Hit: " + timesHit);
        /*
        if (cooldown <= 0)
        {
        cooldown = 1;
        timesHit += 1;
        text.SetText("Times Hit: " + timesHit);
        pHealth.TakeDamage();
        }*/
    }

    public static implicit operator IncreaseHitText(GameObject v)
    {
        throw new NotImplementedException();
    }
}
