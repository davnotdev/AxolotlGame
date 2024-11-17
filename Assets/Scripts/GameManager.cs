using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public Transform cameraTransform;

    public const float baguetteBlastForce = 25.0f;
    public const float baguetteBlastTorque = 300.0f;
    public const float baguetteBlastShakingFactor = 0.2f;

    private bool isShaking = false;
    private Vector3 preShakingPosition;

    public GameManager()
    {
        instance = this;
    }

    public static GameManager Get()
    {
        return instance;
    }

    void Update() {
        // Cheats
        if (Input.GetKeyUp(KeyCode.B)) 
        {
            this.IncrementBaguettes();
        }

        // Screen Shake
        if (isShaking) 
        {
            cameraTransform.localPosition = preShakingPosition + Random.insideUnitSphere * GameManager.baguetteBlastShakingFactor;
        }
    }

    private uint health = 100;
    private uint baguettes = 0;

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

    public uint GetBaguettes()
    {
        return baguettes; 
    }

    public void IncrementBaguettes()
    {
        baguettes++; 
    }

    // Returns whether the decrement succeeded
    public bool DecrementBaguettes()
    {
        if (baguettes <= 0)
        {
            return false;
        } 
        else
        {
            baguettes--; 
            return true;
        }
    }

    public void ScreenShake() {
        isShaking = true;
        preShakingPosition = cameraTransform.localPosition;
        StartCoroutine(ResetShaking(0.3f));
    }

    IEnumerator ResetShaking(float secs)
    {
        yield return new WaitForSeconds(secs);
        isShaking = false;
        cameraTransform.localPosition = preShakingPosition;
    }
}
