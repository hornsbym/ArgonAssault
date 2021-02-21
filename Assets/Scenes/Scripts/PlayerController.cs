using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {
    [Header("General")]
    [Tooltip("m/sec")] [SerializeField] float xSpeed = 20;
    [Tooltip("m/sec")] [SerializeField] float ySpeed = 20;
    [SerializeField] float xBounds = 20;
    [SerializeField] float yBounds = 10;

    [Header("Screen position")]
    [SerializeField] float positionPitchFactor = -2;
    [SerializeField] float positionYawFactor = 1.3333f;


    [Header("Control throw")]
    [SerializeField] float controlPitchFactor = -5;
    [SerializeField] float controlYawFactor = 5;
    [SerializeField] float controlRollFactor = -45;
    bool isControlEnabled = true;

    [Header("Projectiles")]
    [SerializeField] ParticleSystem laser1;
    [SerializeField] ParticleSystem laser2;


    /// Start is called before the first frame update
    void Start() {
        
    }

    /// Update is called once per frame
    void Update() {
        if (isControlEnabled)
        {
            RespondToInput();
        }
    }

    /// <summary>
    /// Recieves player input and takes necessary action.
    /// </summary>
    void RespondToInput() {
        // Get input here.
        // Throw = float from -1 -> 1 representing joystick distance 
        // from resting center position
        float horizontalThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float verticalThrow = CrossPlatformInputManager.GetAxis("Vertical");

        // Respond to input here.
        ApplyTranslations(horizontalThrow, verticalThrow);
        ApplyRotations(horizontalThrow, verticalThrow);
        Shoot();
    }

    /// <summary>
    /// Shoots lasers when the player presses spacebar.
    /// </summary>
    private void Shoot()
    {
        if (CrossPlatformInputManager.GetButton("Jump")) {
            if (!laser1.isEmitting && !laser2.isEmitting) { 
                print("Firing lasers.");
                laser1.Play();
                laser2.Play();
            }
        } else {
            if (laser1.isEmitting && laser2.isEmitting) {
                print("Stopping lasers.");
                laser1.Stop();
                laser2.Stop();
            }
        }
    }

    /// Moves the ship along the x and y planes.
    private void ApplyTranslations(float xThrow, float yThrow) {
        // Calculate how far the ship should move (per frame)
        float xOffset = xSpeed * xThrow * Time.deltaTime;
        float yOffset = ySpeed * yThrow * Time.deltaTime;

        // Apply movement relative to current position
        float newXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -xBounds, xBounds);
        float newYPos = Mathf.Clamp(transform.localPosition.y + yOffset, -yBounds, yBounds);
        transform.localPosition = new Vector3(newXPos, newYPos, transform.localPosition.z);
    }

    /// Rotates the ship around its x, y, and z axes.
    private void ApplyRotations(float xThrow, float yThrow) {
        // Pitch = x rotation;
        // Yaw = y rotation
        // Roll = z rotation;
        float pitch = transform.localPosition.y * positionPitchFactor + (yThrow * controlPitchFactor);
        float yaw = transform.localPosition.x * positionYawFactor + (xThrow * controlYawFactor);
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    /// <summary>
    ///  Fires in the event that the player dies.
    ///  Called by string reference.
    /// </summary>
    private void OnPlayerDeath() {
        isControlEnabled = false;
    }
}
