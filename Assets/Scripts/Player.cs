using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject offRoadMarkerLower;
    public GameObject offRoadMarkerHigher;
    public Transform cameraTransform;
    public float moveBackSpeedX = 1.0f;

    private Rigidbody2D rb;
    private GameManager gameManager;
    private ParticleSystem particleSys;

    private float verticalSpeed = 0.2f;
    private float startingX;
    private bool canUseItem = true;
    private float baguetteCooldown = 1.0f;

    void Start()
    {
        gameManager = GameManager.Get();

        rb = GetComponent<Rigidbody2D>();
        particleSys = GetComponent<ParticleSystem>();

        startingX = transform.position.x;
    }

    void Update() 
    {
        if (
            (transform.position.y <= offRoadMarkerLower.transform.position.y ||
            transform.position.y >= offRoadMarkerHigher.transform.position.y) &&
            gameManager.GetHealth() != 0
        )
        {
            gameManager.SetHealth(0);
        }
    }

    void FixedUpdate()
    {
        float inputAxisY = Input.GetAxisRaw("Vertical");
        rb.MovePosition(new Vector2(
            transform.position.x,
            transform.position.y + inputAxisY * verticalSpeed
        ));

        if (Mathf.Abs(transform.position.x - startingX) >= 0.1f) 
        {
            Vector2 targetPosition = new Vector2(startingX, transform.position.y);
            Vector2 sourcePosition = new Vector2(transform.position.x, transform.position.y);
            float direction = (startingX - transform.position.x) / Mathf.Abs(startingX - transform.position.x);
            rb.MovePosition(sourcePosition - targetPosition + Vector2.right * moveBackSpeedX * direction);
        }

        if (canUseItem && Input.GetKeyUp(KeyCode.Space))
        {
            if (gameManager.DecrementBaguettes())
            {
                BaugetteShockWave(); 
                canUseItem = false;
                StartCoroutine(ResetCanUseItem());
            }
        }
    }

    void OnCollisionEnter2D() 
    {
        Debug.Log("owie");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other)
        {
            switch (other.tag) {
                case "Substance":
                    gameManager.IncrementScore();
                    break;
                case "Baguette":
                    gameManager.IncrementBaguettes();
                    break;
                case "Toolbox":
                    gameManager.SetHealth((int)gameManager.GetHealth() + 1);
                    break;
            }

            Destroy(other.gameObject);
        }
    }

    void BaugetteShockWave() 
    {
        Vector3 blastPosition = transform.position;
        EnemyMovement[] hitColliders = Object.FindObjectsOfType<EnemyMovement>();

        particleSys.Play();

        foreach (EnemyMovement enemyMovement in hitColliders)
        {
            GameObject enemy = enemyMovement.gameObject;
            Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
            if (enemyRb && enemy.GetComponent<EnemyMovement>()) 
            {
                enemyRb.AddForce(
                    (enemy.transform.position - blastPosition).normalized * 
                    GameManager.baguetteBlastForce,
                    ForceMode2D.Impulse
                );
                enemyRb.AddTorque(GameManager.baguetteBlastTorque);
            }
            enemyMovement.DisableMovement();
        }

        gameManager.ScreenShake();
    }

    IEnumerator ResetCanUseItem()
    {
        yield return new WaitForSeconds(baguetteCooldown);
        canUseItem = true;
    }
}
