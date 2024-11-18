using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public Transform cameraTransform;
    public TMP_Text healthCounter;
    public TMP_Text baguetteCounter;
    public TMP_Text scoreCounter;

    public const float baguetteBlastForce = 25.0f;
    public const float baguetteBlastTorque = 300.0f;
    public const float baguetteBlastShakingFactor = 0.2f;

    private bool isShaking = false;
    private Vector3 preShakingPosition;

    private float t = 0;

    private uint health = 5;
    private uint baguettes = 0;
    private uint score = 0;

    public GameManager()
    {
        instance = this;
    }

    public static GameManager Get()
    {
        return instance;
    }

    void Awake()
    {
        t = 0;
        EnemySpawner._minimumSpawnTime = 2;
        EnemySpawner._maximumSpawnTime = 5;

        EnemySpawner._minPerSpawn = 3;
        EnemySpawner._maxPerSpawn = 5;
        
        healthCounter.text = this.health.ToString();
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



        t += Time.deltaTime;

        if (t > 50.0f)
            Phase5();
        else if (t > 40.0f)
            Phase4();
        else if (t > 30.0f)
            Phase3();
        else if (t > 20.0f)
            Phase2();
        else if (t > 10.0f)
            Phase1();


    }

    public int GetHealth()
    {
        return (int)health;
    }

    // `health` will be clamped appropriately
    public void SetHealth(int health)
    {
        if (health - this.health < 0)
        {
            ScreenShake();
        }

        this.health = (uint)health;
        healthCounter.text = this.health.ToString();

        if (this.health == 0) 
        {
            this.OnDeath();
        }
    }

    private void OnDeath()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public uint GetBaguettes()
    {
        return baguettes; 
    }

    public void IncrementBaguettes()
    {
        baguettes++; 
        baguetteCounter.text = this.baguettes.ToString();
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
            baguetteCounter.text = this.baguettes.ToString();
            return true;
        }
    }

    public uint GetScore()
    {
        return score; 
    }

    public void IncrementScore()
    {
        score++;
        scoreCounter.text = this.score.ToString();
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

    public static uint RandomWithWeights(List<uint> weights)
    {
        uint sum = 0;
        foreach (var weight in weights) 
        {
            sum += weight;
        }

        int random = Random.Range((int)0, (int)sum);

        uint accum = 0;
        for (int i = 0; i < weights.Count; i++)
        {
            accum += weights[i];
            if (random < accum)
            {
                return (uint)i;
            }
        }
        return (uint)(weights.Count - 1);
    }


    ///////  PHASES  /////////////////////////////////////////////////
    
    public void Phase1()
    {
        EnemySpawner._minimumSpawnTime = 2;
        EnemySpawner._maximumSpawnTime = 4;
    }

    public void Phase2()
    {
        EnemySpawner._minimumSpawnTime = 1;
        EnemySpawner._maximumSpawnTime = 3;
    }

    public void Phase3()
    {
        EnemySpawner._minimumSpawnTime = 1;
        EnemySpawner._maximumSpawnTime = 2;
    }

    public void Phase4()
    {
        EnemySpawner._minimumSpawnTime = 1;
        EnemySpawner._maximumSpawnTime = 1;
    }

    public void Phase5()
    {
        EnemySpawner._minimumSpawnTime = 0;
        EnemySpawner._maximumSpawnTime = 1;
        EnemySpawner._minPerSpawn = 2;
        EnemySpawner._maxPerSpawn = 3;
    }

}
