using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Commons
{
    public static IEnumerator DelayedAction(UnityAction lambda, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        lambda.Invoke();
    }
    
    public static IEnumerator DelayedNextFrame(UnityAction lambda)
    {
        yield return new WaitForEndOfFrame();
        lambda.Invoke();
    }

    public static IEnumerator DoForSeconds(UnityAction lambda, float seconds)
    {
        for (var i = seconds; i > 0; i -= Time.deltaTime)
        {
            lambda.Invoke();
            yield return null;
        }
    }

    public static IEnumerator DoUntil(UnityAction lambda, Func<bool> condition, UnityAction end = null)
    {
        while (!condition.Invoke())
        {
            lambda.Invoke();
            yield return null;
        }
        end?.Invoke();
    }
    
    public static IEnumerator FadeAway(SpriteRenderer img, float length)
    {
        for (var i = 1f; i >= 0; i -= Time.deltaTime / length)
        {
            // set color with i as alpha
            var o = img.color;
            img.color = new Color(o.r, o.g, o.b, i);
            yield return null;
        }

        img.enabled = false;
    }
}