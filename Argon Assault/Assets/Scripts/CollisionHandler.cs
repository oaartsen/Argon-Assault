using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {

    // Configuration parameters
    [SerializeField] float loadDelayForCrash = 1f;
    [SerializeField] ParticleSystem crashVFX = null;

    // State variables
    int currentSceneIndex;

    // Cached references
    PlayerControls myPlayerControls = null;
    MeshRenderer[] childMeshRendererArray = null;

    // Start is called before the first frame update
    void Start() {
        myPlayerControls = GetComponent<PlayerControls>();
        childMeshRendererArray = GetComponentsInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void OnTriggerEnter(Collider other) {
        StartCrashSequence();
    }

    void StartCrashSequence() {
        myPlayerControls.enabled = false;

        foreach (MeshRenderer childMeshRenderer in childMeshRendererArray) { // With this foreach() loop I disable all children of the spaceship that have a meshrenderer components. This way, also the colliders of these objects are disabled (so that there is no possibility of multiple explosions after the player has died once)
            childMeshRenderer.gameObject.SetActive(false);
        }

        crashVFX.Play();

        Invoke("ReloadLevel", loadDelayForCrash);
    }

    void ReloadLevel() {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}
