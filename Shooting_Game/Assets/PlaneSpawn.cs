using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawn : MonoBehaviour
{
    public BoxCollider2D spawnZoneLeft;
    public BoxCollider2D spawnZoneRight;

    // Initial and minimum wait time
    public float initialWaitTime = 5.0f;
    // The rate at which the wait time decreases
    public float spawnAcceleration = 0.1f;
    // The minimum wait time
    public float minimumWaitTime = 0.3f;

    void Start()
    {
        StartCoroutine(SpawnPlanes());
    }

    IEnumerator SpawnPlanes()
    {
        float waitTime = initialWaitTime;

        while (true)
        {
            // Wait for the calculated amount of time
            yield return new WaitForSeconds(waitTime);

            // Decrease the wait time for the next spawn, but don't let it go below the minimum wait time
            waitTime = Mathf.Max(minimumWaitTime, waitTime - spawnAcceleration);

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
