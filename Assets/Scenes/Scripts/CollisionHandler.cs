using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Ok as long as this is the only script to load scenes.

public class CollisionHandler : MonoBehaviour {
    [Header("Death Sequence")]
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] GameObject explosion;

    /// <summary>
    ///  Handles player collisions with other objects. 
    /// </summary>
    /// <param name="Collider"></param>
    void OnTriggerEnter(Collider other) {
        StartDeathSequence();
    }

    /// <summary>
    /// Communicates with other Player scripts that the player has died.
    /// </summary>
    private void StartDeathSequence() {
        // Notifies other scripts of the Player's death.
        SendMessage("OnPlayerDeath");
        explosion.SetActive(true);
        Invoke("ReloadLevel", levelLoadDelay);

    }

    /// <summary>
    /// Called by string reference.
    /// </summary>
    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }


}
