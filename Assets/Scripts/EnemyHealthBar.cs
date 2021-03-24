using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;
    public Color Low;
    public Color High;
    public Vector3 Offset;

    public void SetHealth(float health, float maxhealth){
        slider.gameObject.SetActive(health < maxhealth);
        slider.value = health;
        slider.maxValue = maxhealth;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, slider.normalizedValue);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.parent.name != "Boss"){
            slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
        }
    }
}
