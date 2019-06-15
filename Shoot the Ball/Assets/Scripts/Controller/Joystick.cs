using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private PlayerScripts player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScripts>();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.name == "Left")
        {
            //Debug.Log("left");
            player.setMoveLeft(true);
        }
        else if (gameObject.name == "Right")
        {
            //Debug.Log("right");
            player.setMoveLeft(false);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.stopMoving();
    }
}
