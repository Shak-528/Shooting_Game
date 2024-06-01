using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneDespawn : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the plane entered the despawn zone
        if (other.gameObject.CompareTag("Plane"))
        {
            // Return the plane to the pool
            PlanePool.Instance.ReturnPlane(other.gameObject);

            // Decrease player health
            Player.Instance.TakeDamage(10);
        }
    }
}
