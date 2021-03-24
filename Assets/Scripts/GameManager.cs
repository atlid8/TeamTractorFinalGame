using System;
using Pathfinding;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float globalTimeMult = 1f;

    public GameObject slomoScreen;

    private void Awake()
    {
        if (!instance)
            instance = this;
    }
    
    private void GradualSlowDown(Rigidbody2D r)
    {
        var targetTime = r.velocity.magnitude * globalTimeMult;
        StartCoroutine(Commons.DoUntil(() => r.velocity *= 0.98f, 
            () => r.velocity.magnitude < targetTime));
    }
    
    private void GradualSlowDown(AIBase r)
    {
        var targetTime = r.velocity.magnitude * globalTimeMult;
        StartCoroutine(Commons.DoUntil(() => r.maxSpeed *= 0.98f, 
            () => r.maxSpeed < targetTime));
    }
    
    private void GradualSpeedUp(Rigidbody2D r)
    {
        var targetTime = r.velocity.magnitude / globalTimeMult;
        StartCoroutine(Commons.DoUntil(() => r.velocity /= 0.9f, 
            () => r.velocity.magnitude > targetTime));
    }
    
    private void GradualSpeedUp(AIBase r)
    {
        var targetTime = r.velocity.magnitude / globalTimeMult;
        StartCoroutine(Commons.DoUntil(() => r.maxSpeed /= 0.9f, 
            () => r.maxSpeed > targetTime));
    }

    public void ChangeTime(bool slow)
    {
        var bullets = FindObjectsOfType<BulletScript>();
        foreach (var bulletScript in bullets)
        {
            if (bulletScript.playerBullet)
                continue;
            var rigidBody = bulletScript.gameObject.GetComponent<Rigidbody2D>();
            if (slow)
                GradualSlowDown(rigidBody);
            else
                GradualSpeedUp(rigidBody);
        }

        var enemies = FindObjectsOfType<AIPath>();
        foreach (var aiPath in enemies)
        {
            if (slow)
                GradualSlowDown(aiPath);
            else
                GradualSpeedUp(aiPath);
        }
    }

    public void AddTimeEffect(bool slow)
    {
        var screen = slomoScreen.GetComponent<SpriteRenderer>();
        if (slow)
        {
            screen.gameObject.SetActive(true);
            var color = screen.color;
            color = new Color(color.r, color.g, color.b, 0);
            screen.color = color;
            StartCoroutine(Commons.DoUntil(() =>
            {
                var c = screen.color;
                screen.color = new Color(c.r, c.g, c.b, c.a + 0.001f);
            }, () => screen.color.a > 0.12f));
            globalTimeMult = 0.2f;
            ChangeTime(true);
        }
        else
        {
            StartCoroutine(Commons.DoUntil(() =>
            {
                var c = screen.color;
                screen.color = new Color(c.r, c.g, c.b, c.a - 0.001f);
            }, () => screen.color.a < 0, () => screen.gameObject.SetActive(false)));
            ChangeTime(false);
            globalTimeMult = 1f;
        }
    }
    
}
