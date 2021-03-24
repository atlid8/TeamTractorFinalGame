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
    
    public static IEnumerator FadeAway(SpriteRenderer img, float length)
    {
        // loop over 1 second backwards
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