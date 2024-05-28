using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class IncreaseHitText : MonoBehaviour
{
    public TextMeshProUGUI text;
        private int timesHit = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Increase()
    {
        timesHit += 1;
        text.SetText("Times Hit: " + timesHit);
    }
}
