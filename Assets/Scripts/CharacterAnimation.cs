using System;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private GameObject weapon;
    private bool facingRight;
    public float angle { get; set; }
    private Animator legAnimator;
    private bool running;
    private Transform gunPoint;
    
    private void Awake()
    {
        weapon = transform.Find("Weapon").gameObject;
        legAnimator = transform.Find("Legs").GetComponent<Animator>();
        gunPoint = weapon.transform.Find("GunPoint");
    }

    public Transform GetGunPoint()
    {
        return gunPoint;
    }

    public void Flip(bool right)
    {
        facingRight = right;
        transform.rotation = right ? Quaternion.Euler(0,180,0) : Quaternion.identity;
    }

    public void SetRunningAnimation(bool play)
    {
        if (play && !running)
        {
            legAnimator.Play("LegsAnimation");
            running = true;
            //Debug.Log("stop");
        }
        else if (!play && running)
        {
            legAnimator.Play("Idle");
            running = false;
            //Debug.Log("running");
        }
        else
        {
            //Debug.Log("nothing");
        }
    }

    private void FixedUpdate()
    {
        float offset;
        if (facingRight)
            offset = 85;
        else
            offset = -85;
        var fAngle = angle + offset;
        if (facingRight)
            fAngle = -fAngle;
        var o = weapon.transform.eulerAngles;
        weapon.transform.eulerAngles = new Vector3(o.x,o.y,fAngle);
    }
}
