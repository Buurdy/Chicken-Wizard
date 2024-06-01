using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayComponent : MonoBehaviour
{
    TMP_Text compText;
    [SerializeField] int compNum;
    PlayerController playerController;

    private void Awake()
    {
        compText = GetComponent<TMP_Text>();
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(playerController.components.Count > compNum)
        {
            compText.text = playerController.components[compNum];
        }
    }
}
