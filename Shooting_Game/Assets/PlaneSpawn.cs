using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawn : MonoBehaviour
{
    public BoxCollider2D spawnZoneLeft;
    public BoxCollider2D spawnZoneRight;

    // Minimum and maximum wait time
    public float minWaitTime = 1.0f;
    public float maxWaitTime = 5.0f;

    // The rate at which the wait times decrease
    public float decreaseRate = 0.1f;

    // The time for the next decrease
    private float nextDecreaseTime = 10.0f;

    public static PlaneSpawn Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple instances of PlaneSpawn!");
        }
    }

    void Start()
    {
        StartCoroutine(SpawnPlanes());
    }

    IEnumerator SpawnPlanes()
    {
        float elapsedTime = 0.0f;

        while (true)
        {
            // Stop spawning planes if the player is dead
            yield return new WaitForSeconds(0.01f);
            if (Player.Instance.currentHealth <= 0)
            {
                Debug.Log("Player is dead");
                yield break;
            }

            // Calculate a random wait time
            float waitTime = Random.Range(minWaitTime, maxWaitTime);

            // Wait for the calculated amount of time
            yield return new WaitForSeconds(waitTime);

            elapsedTime += waitTime;

            // Decrease the wait times every 10 seconds
            if (elapsedTime >= nextDecreaseTime && minWaitTime > 0.3f)
            {
                minWaitTime = minWaitTime - decreaseRate;
                maxWaitTime = maxWaitTime - (decreaseRate * 6.0f);
                nextDecreaseTime += 10.0f;
            }

            // Choose a random spawn zone
            BoxCollider2D spawnZone = Random.value < 0.5f ? spawnZoneLeft : spawnZoneRight;

            // Calculate a random position within the spawn zone
            Vector2 spawnPosition = new Vector2(
                Random.Range(spawnZone.bounds.min.x, spawnZone.bounds.max.x),
                Random.Range(spawnZone.bounds.min.y, spawnZone.bounds.max.y)
            );

            // Get a plane from the pool and set its position
            GameObject plane = PlanePool.Instance.GetPlane();
            plane.transform.position = spawnPosition;

            // Get the active SpriteRenderer
            SpriteRenderer[] sprites = plane.GetComponentsInChildren<SpriteRenderer>();
            SpriteRenderer activeSprite = null;
            foreach (SpriteRenderer sprite in sprites)
            {
                if (sprite.enabled)
                {
                    activeSprite = sprite;
                    break;
                }
            }

            // Flip the x axis of the active SpriteRenderer if spawning from the right zone
            if (activeSprite != null)
            {
                activeSprite.flipX = spawnZone == spawnZoneRight;
            }
        }
    }
}
