using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopOffScreen : MonoBehaviour
{
    Camera cam;
    Vector3 objectWidth, objectHeight;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        objectWidth = Vector3.right * (transform.localScale.x / 2);
        objectHeight = Vector3.up * (transform.localScale.y / 2);
    }

    // Update is called once per frame
    void Update()
    {
        CheckScreenPos();
    }

    //This function is used to see the current position of the object containing this script, and loops it to the other side of the screen when it leaves view
    void CheckScreenPos()
    {
        //Checks to see if the whole object has gone past the left side of the screen
        if (cam.WorldToScreenPoint(transform.position + objectWidth).x < 0)
        {
            //Sets the position of the object to just off the right side of the screen
            transform.position = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.WorldToScreenPoint(transform.position).y, 1)) + objectWidth;
        }
        //Checks to see if the whole object has gone past the right side of the screen
        else if (cam.WorldToScreenPoint(transform.position - objectWidth).x > cam.pixelWidth)
        {
            //Sets the positiong of the object to just off the left side of the screen
            transform.position = cam.ScreenToWorldPoint(new Vector3(0, cam.WorldToScreenPoint(transform.position).y, 1)) - objectWidth;
        }

        //Checks to see if the whole object has gone off the bottom of the screen
        if (cam.WorldToScreenPoint(transform.position + objectHeight).y < 0)
        {
            //Sets the position of the object to just off the top of the screen
            transform.position = cam.ScreenToWorldPoint(new Vector3(cam.WorldToScreenPoint(transform.position).x, cam.pixelHeight, 1)) + objectHeight;
        }
        //Checks to see if the whole object has gone off the top of the screen
        else if (cam.WorldToScreenPoint(transform.position - objectHeight).y > cam.pixelHeight)
        {
            //Sets the position of the object to just off the bottom of the screen
            transform.position = cam.ScreenToWorldPoint(new Vector3(cam.WorldToScreenPoint(transform.position).x, 0, 1)) - objectHeight;
        }
    }
}
