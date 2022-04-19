using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour {

    // Configuration parameters
    [SerializeField] int scoreWhenHit = 15;
    [SerializeField] int scoreWhenKilled = 100;
    [SerializeField] int health = 5;
    [SerializeField] GameObject hitVFX = null;
    [SerializeField] GameObject deathFX = null;
    //[SerializeField] Transform vfxParent = null;

    // Cached references
    Scoreboard scoreboard = null;
    Rigidbody myRigidbody = null;
    GameObject fxParent = null;


    // Start is called before the first frame update
    void Start() {
        scoreboard = FindObjectOfType<Scoreboard>();
        fxParent = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidBody();
    }

    private void AddRigidBody() {
        myRigidbody = gameObject.AddComponent<Rigidbody>();
        myRigidbody.useGravity = false;
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnParticleCollision(GameObject other) {
        ProcessHit();

        if (health <= 0) {
            KillEnemy();
        }
    }

    void ProcessHit() {
        GameObject gotHitVFX = Instantiate(hitVFX, transform.position, Quaternion.identity);
        gotHitVFX.transform.parent = fxParent.transform;
        scoreboard.IncreaseScore(scoreWhenHit);
        health--;
    }


    void KillEnemy() {
        scoreboard.IncreaseScore(scoreWhenKilled);
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = fxParent.transform;

        Destroy(gameObject);
    }


}
