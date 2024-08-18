using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTransitionScript : MonoBehaviour
{
    private GameObject pieceSpawner;
    private GameObject ground;
    private float initialJitter = 0.025f;
    private float jitterDuration = 2.5f;
    private float glideSpeed = 1.0f;
    private float glideDuration = 4.0f;
    private int animationStage = 0;

    // Start is called before the first frame update
    void Start()
    {
        ground = GameObject.Find("Ground");
        pieceSpawner = GameObject.Find("PieceSpawner");
        pieceSpawner.GetComponent<PieceSpawnerScript>().isActive = false;
        StartCoroutine(JitterCooldown());
        StartCoroutine(MovementCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        
        if (animationStage == 0) {
            Debug.Log("JITTERING");
            // earthquake
            
            var jitter = new Vector3(
                (Random.value-0.5f) * initialJitter,
                (Random.value-0.5f) * initialJitter,
                (Random.value-0.5f) * initialJitter
            );
            var allPieces = GameObject.FindGameObjectsWithTag("Piece");
            foreach (GameObject piece in allPieces) {
                piece.transform.position = piece.transform.position + jitter;
            }
            ground.transform.position = ground.transform.position + jitter;
            
        } else if (animationStage == 1) {
            Debug.Log("MOVING");
            
            var allPieces = GameObject.FindGameObjectsWithTag("Piece");
            foreach (GameObject piece in allPieces) {
                piece.transform.position = piece.transform.position + (Vector3.down * glideSpeed * Time.deltaTime);
            }
            ground.transform.position = ground.transform.position + (Vector3.down * glideSpeed * Time.deltaTime);
            
        } else if (animationStage == 2) {
            pieceSpawner.GetComponent<PieceSpawnerScript>().isActive = true;
            Destroy(gameObject);
        }
        
    }

    IEnumerator JitterCooldown() {
        yield return new WaitForSeconds(jitterDuration);
        animationStage = 1;
    }

    IEnumerator MovementCooldown() {
        yield return new WaitForSeconds(jitterDuration + glideDuration);
        animationStage = 2;
    }
}
