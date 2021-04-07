using System;
using Pathfinding;
using UnityEngine;

public class SlowMotionPickupScript : MonoBehaviour
{
    public GameObject slomoScreen;
    private SpriteRenderer spriteRenderer;
    private GameObject overlay;
    private AudioSource audioSource;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GameObject.Find("Audio").GetComponent<AudioSource>();
    }

    private void GradualSlowDown(Rigidbody2D r)
    {
        var targetTime = r.velocity.magnitude * GameManager.instance.globalTimeMult;
        StartCoroutine(Commons.DoUntil(() => r.velocity *= 0.98f, 
            () => r.velocity.magnitude < targetTime));
    }
    
    private void GradualSlowDown(AIBase r)
    {
        var targetTime = r.velocity.magnitude * GameManager.instance.globalTimeMult;
        StartCoroutine(Commons.DoUntil(() => r.maxSpeed *= 0.98f, 
            () => r.maxSpeed < targetTime));
    }
    
    private void GradualSpeedUp(Rigidbody2D r)
    {
        var targetTime = r.velocity.magnitude / GameManager.instance.globalTimeMult;
        StartCoroutine(Commons.DoUntil(() => r.velocity /= 0.9f, 
            () => r.velocity.magnitude > targetTime));
    }
    
    private void GradualSpeedUp(AIBase r)
    {
        var targetTime = r.velocity.magnitude / GameManager.instance.globalTimeMult;
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
        const float audioSpeedMult = 0.01f;
        if (slow)
        {
            overlay = Instantiate(slomoScreen, transform.position, transform.rotation);
            overlay.SetActive(true);
            var screen = overlay.GetComponent<SpriteRenderer>();
            var color = screen.color;
            color = new Color(color.r, color.g, color.b, 0);
            screen.color = color;
            StartCoroutine(Commons.DoUntil(() =>
            {
                var c = screen.color;
                screen.color = new Color(c.r, c.g, c.b, c.a + 0.001f);
            }, () => screen.color.a > 0.12f));
            StartCoroutine(Commons.DoUntil(() =>
            {
                audioSource.pitch -= audioSpeedMult;
            }, () => audioSource.pitch <= 0.5f));
            GameManager.instance.globalTimeMult = 0.2f;
            ChangeTime(true);
        }
        else
        {
            var screen = overlay.GetComponent<SpriteRenderer>();
            StartCoroutine(Commons.DoUntil(() =>
            {
                var c = screen.color;
                screen.color = new Color(c.r, c.g, c.b, c.a - 0.001f);
            }, () => screen.color.a <= 0, () =>
            {
                overlay.SetActive(false);
                Destroy(overlay);
                Destroy(gameObject);
            }));
            StartCoroutine(Commons.DoUntil(() =>
            {
                audioSource.pitch += audioSpeedMult;
            }, () => audioSource.pitch >= 1.0f));
            ChangeTime(false);
            GameManager.instance.globalTimeMult = 1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AddTimeEffect(true);
            StartCoroutine(Commons.DelayedAction(() =>
            {
                AddTimeEffect(false);
            }, 5f));
            spriteRenderer.enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
