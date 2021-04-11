using System;
using Pathfinding;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float globalTimeMult = 1f;

    public int level;

    public GameObject slomoScreen;

    public float gameVolume = 0.5f;
    
    private void Awake()
    {
        level = 1;
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
}
