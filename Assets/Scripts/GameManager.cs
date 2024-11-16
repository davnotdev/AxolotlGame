using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public GameManager()
    {
        instance = this;
    }

    public static GameManager Get()
    {
        return instance;
    }

    private uint health = 100;

    public uint GetHealth()
    {
        return health;
    }

    // `health` will be clamped appropriately
    public void SetHealth(int health)
    {
        this.health = (uint)health;
        this.OnDeath();
    }

    private void OnDeath()
    {
        
    }
}
