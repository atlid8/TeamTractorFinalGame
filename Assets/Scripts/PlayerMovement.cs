using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Camera cam;
    public GameObject playerArt;
    private GameObject weapon;
    
    Vector2 movement;
    Vector2 mousePos;

    private void Awake()
    {
        weapon = playerArt.transform.Find("Weapon").gameObject;
    }

    private bool facingRight = false;

    void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (playerArt.transform.position.x > mousePos.x) {
            //Debug.Log("Right");
            playerArt.transform.rotation = Quaternion.identity;
            facingRight = false;
        }
        else {
            //Debug.Log("Left");
            playerArt.transform.rotation = Quaternion.Euler(0,180,0);
            facingRight = true;
        }
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        
        //rb.rotation = angle;
        //weapon.rotation = angle;
        var o = weapon.transform.eulerAngles;
        float offset;
        if (facingRight)
            offset = 85;
        else
            offset = -85;
        var fAngle = angle + offset;
        if (facingRight)
            fAngle = -fAngle;
        weapon.transform.eulerAngles = new Vector3(o.x,o.y,fAngle);
    }
}
