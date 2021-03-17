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
    private CharacterAnimation playerArtScript;
    public Transform bulletRotation;
    public Transform gunPoint;
    
    Vector2 movement;
    Vector2 mousePos;

    private void Awake()
    {
        playerArtScript = playerArt.GetComponent<CharacterAnimation>();
    }

    void Update() 
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        playerArtScript.Flip(!(playerArt.transform.position.x > mousePos.x));
    }

    void FixedUpdate() 
    {
        if (Math.Abs(movement.x) > 0.001 || Math.Abs(movement.y) > 0.001)
            playerArtScript.SetRunningAnimation(true);
        else 
            playerArtScript.SetRunningAnimation(false);
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
        Vector2 gunPointVector = new Vector2(gunPoint.position.x, gunPoint.position.y);
        Vector2 rbVector = new Vector2(rb.position.x, rb.position.y);
        var lookDir = mousePos - gunPointVector;
        var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        bulletRotation.eulerAngles = new Vector3(0, 0, angle);
        if (Vector2.Distance(rbVector, mousePos) <= Vector2.Distance(gunPointVector, rbVector) + 0.2){
            lookDir = mousePos - rbVector;
            angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

            Debug.Log("Rb");
        }
        playerArtScript.angle = angle;
        
    }
}
