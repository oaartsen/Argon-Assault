using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    // Configuration paremeters
    [Header("Player movement parameters")]
    [Tooltip("Speed of space ship vertically and horizontally based on player input")] [SerializeField] float controlSpeed = 20f;
    [Tooltip("Horizontal range of player movement")] [SerializeField] float xRange = 20f;
    [Tooltip("Vertical range of player movement")] [SerializeField] float yRange = 7f;

    [Header("Space ship rotation parameters")]
    [Tooltip("Amount of rotation up and down (around the x-axis) based on the y-position of the player")] [SerializeField] float positionPitchFactor = -2f;
    [Tooltip("Speed of rotation up and down (around the x-axis) based on player's vertical movement")] [SerializeField] float controlPitchFactor = -10f;
    [Tooltip("Amount of rotation left and right (around the y-axis) based on the x-position of the player")] [SerializeField] float positionYawFactor = 1f;
    [Tooltip("Speed of rolling rotation (around the z-axis) based on the player's horizontal movement")] [SerializeField] float controlRollFactor = -10f;

    [Header("Laser gun array")]
    [Tooltip("Add all the player's laser guns here")] [SerializeField] GameObject[] lasers = null;

    // State variables
    float xThrow, yThrow;
    float xOffset, yOffset;
    float rawXPos, rawYPos;
    float clampedXPos, clampedYPos;

    float pitch, yaw, roll;
    float pitchDueToPosition, pitchDueToControlThrow;

    // Cached references

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessTranslation() {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        xOffset = xThrow * Time.deltaTime * controlSpeed;
        yOffset = yThrow * Time.deltaTime * controlSpeed;

        rawXPos = transform.localPosition.x + xOffset;
        rawYPos = transform.localPosition.y + yOffset;

        clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessRotation() {

        pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        pitchDueToControlThrow = yThrow * controlPitchFactor;
        pitch = pitchDueToPosition + pitchDueToControlThrow;

        yaw = transform.localPosition.x * positionYawFactor;

        roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring() {

        if (Input.GetButton("Fire1")) {
            SetLasersActive(true);
        }

        else {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive) {
        foreach (GameObject laser in lasers) {
            //laser.GetComponent<ParticleSystem>().emission.enabled = true;
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

}
