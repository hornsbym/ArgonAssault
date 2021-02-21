using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour{
    [SerializeField] GameObject explosionFX;
    [SerializeField] Transform explosionParent;
    [SerializeField] int scorePerHit = 10;
    [SerializeField] int healthPoints = 100;

    bool isAlive = true;
    ScoreBoard scoreBoard;

    // Start is called before the first frame update
    void Start() {
        // Prevents the need to add a new collider to each enemy in the script.
        AddNonTriggerBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();

    }

    /// <summary>
    /// Tells the ship to build its own collider.
    /// This prevents us from needing to apply it manually to each enemy via the Unity editor.
    /// </summary>
    private void AddNonTriggerBoxCollider() {
        // Add a new collider to the enemy ship.
        BoxCollider nonTriggerBoxCollider = gameObject.AddComponent<BoxCollider>();

        // Disable the trigger on the new box collider.
        nonTriggerBoxCollider.isTrigger = false;
    }

    /// <summary>
    /// Handles being shot by the player.
    /// </summary>
    /// <param name="other"></param>
    private void OnParticleCollision(GameObject other)
    {
        // Always applies damage upon collision.
        // TODO: Store damage factor on ship/bullets, provide that damage as argument.
        ApplyDamage(10);

        if (isAlive == true) {
            if (healthPoints <= 0){
                // Kill the enemy.
                Die();
            }

            // Award game points.
            scoreBoard.ScoreHit(scorePerHit);
        }
    }

    /// <summary>
    /// Damages the enemy based on the damage factor of the colliding bullets.
    /// </summary>
    private void ApplyDamage(int damage) {
        healthPoints -= 10;
    }

    /// <summary>
    /// Handles death logic for an Enemy unit.
    /// </summary>
    private void Die() {
        // Records death in "isAlive" variable.
        // This prevents score being multiplied by several lasers hitting the target.
        isAlive = false;

        // Plays death explosion animation and destroys the object.
        GameObject explosion = Instantiate(explosionFX, transform.position, Quaternion.identity);
        explosion.transform.parent = explosionParent;
        explosion.SetActive(true);
        Destroy(gameObject);
    }
}
