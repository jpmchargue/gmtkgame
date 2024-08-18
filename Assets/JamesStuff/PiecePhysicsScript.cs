using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePhysicsScript : MonoBehaviour
{
    private bool isHovered;
    private bool isBeingDragged;
    private Plane piecePlane;
    private Rigidbody rb;
    private float xEulerAngle;
    private float yEulerAngle;

    private float lockedZ = 0.0f;
    private float dragAccelStrength = 1.5f;
    private float dragDrag = 10;
    private float zEulerAngle = 0.0f;
    private float dragRotationSpeed = 300.0f;
    public Color noDragColor = new Color(0.6352941f, 0.6352941f, 0.66f);
    private Color dragColor = new Color(0.0f, 0.737255f, 1.0f, 0.6f);
    public Color disabledColor = new Color(1.0f, 0.0f, 1.0f);

    private Vector3 lastPosition;
    private float stabilityMovementThreshold = 0.05f;
    private int stableFrames;
    public bool isStable;
    public bool hasCollided = false;
    private bool hasBeenGrabbed = false;
    private InGameUIManager inGameUIManager;

    private float lossThreshold = 19.5f;

    // Game Flow Objects
    private GameObject buildThreshold;
    private GameObject pieceSpawner;
    
    public MainGameLoop mainGameLoop;
    public bool isActive = true;
    
    // Sounds
    public AudioClip soundHover;
    public AudioClip soundGrab;
    public AudioClip soundDrop;
    public AudioClip soundLockedIn;
    private Vector3 cameraLocation = new Vector3(-16.55f, 23.37f, -9f);
    

    // Start is called before the first frame update
    void Start()
    {
        isHovered = false;
        isBeingDragged = false;
        piecePlane = new Plane(Vector3.forward, 0.0f);
        rb = GetComponent<Rigidbody>();
        GetComponent<MeshRenderer>().material.color = noDragColor;

        xEulerAngle = transform.eulerAngles.x;
        yEulerAngle = transform.eulerAngles.y;

        buildThreshold = GameObject.Find("BuildThreshold");
        pieceSpawner = GameObject.Find("PieceSpawner");
        mainGameLoop = GameObject.Find("MainGameLoop").GetComponent<MainGameLoop>();
        inGameUIManager = GameObject.Find("InGameMenuCanvas").GetComponent<InGameUIManager>();

        inGameUIManager.UpdateScore(10);
        //nextStageManager = GameObject.Find("NextStageManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive) {
            if (!isBeingDragged) {
                if (isHovered && Input.GetMouseButtonDown(0)) {
                    startDrag();
                }
            } else {
                if (Input.GetKey("left") || Input.GetKey(KeyCode.A)) {
                    zEulerAngle += dragRotationSpeed * Time.deltaTime;
                }
                if (Input.GetKey("right") || Input.GetKey(KeyCode.D)) {
                    zEulerAngle -= dragRotationSpeed * Time.deltaTime;
                }

                // Lock rotation while dragging
                transform.eulerAngles = new Vector3(0.0f, 0.0f, zEulerAngle);

                if (Input.GetMouseButtonUp(0)) {
                    stopDrag();
                    AudioSource.PlayClipAtPoint(soundDrop, cameraLocation);
                }
            }

            stabilityCheck();
            lossCheck();

            transform.position = new Vector3(transform.position.x, transform.position.y, lockedZ);
            transform.eulerAngles = new Vector3(xEulerAngle, yEulerAngle, transform.eulerAngles.z);
        }
    }

    void stabilityCheck() {
        if (!isBeingDragged) {
            var movement = transform.position - lastPosition;
            lastPosition = transform.position;
            if (movement.magnitude < stabilityMovementThreshold) {
                stableFrames += 1;
            } else {
                stableFrames = 0;
                isStable = false;
            }
            // isStable may seem redundant but it's useful for doing something exactly once when the block becomes stable
            if (!isStable && stableFrames > 60) {
                isStable = true;
                Debug.Log("I am stable");
                if (transform.position.y > buildThreshold.transform.position.y && hasCollided) {
                    nextStage();
                }
            }
        }
    }

    void lossCheck() {
        if (transform.position.y < lossThreshold) {
            loseGame();
        }
    }

    void startDrag() {
        isBeingDragged = true;
        zEulerAngle = transform.eulerAngles.z;
        rb.drag = dragDrag;
        GetComponent<MeshRenderer>().material.color = dragColor;
        AudioSource.PlayClipAtPoint(soundGrab, cameraLocation);
    }

    void stopDrag() {
        isBeingDragged = false;
        rb.drag = 1;
        rb.angularVelocity = Vector3.zero;
        GetComponent<MeshRenderer>().material.color = noDragColor;
        if (!isHovered) {
            GetComponent<Outline>().enabled = false;
        }
    }

    void nextStage() {
        // Run the next stage animation
        Debug.Log("Stable above line, running next stage check...");
        var allPieces = GameObject.FindGameObjectsWithTag("Piece");

        var piecesLocked = 0;
        foreach (GameObject piece in allPieces) {
            if (!piece.GetComponent<PiecePhysicsScript>().isStable) {
                if (piece.GetComponent<PiecePhysicsScript>().hasCollided) {
                    return;
                }
            }
            piecesLocked += 1;
        }
        // Delete all pieces that haven't landed yet
        foreach (GameObject piece in allPieces) {
            if (!piece.GetComponent<PiecePhysicsScript>().hasCollided) {
                piecesLocked -= 1;
                Destroy(piece);
            }
        }

        Debug.Log($"Pieces Locked: {piecesLocked}");
        // Update score w/ locked pieces
        inGameUIManager.UpdateScore(piecesLocked * 20);
        Debug.Log("NEXT STAGE - ALL STABLE");
        foreach (GameObject piece in allPieces) {
            piece.GetComponent<Rigidbody>().isKinematic = true;
            piece.GetComponent<MeshRenderer>().material.color = disabledColor;
            piece.GetComponent<PiecePhysicsScript>().isActive = false;
        }
        AudioSource.PlayClipAtPoint(soundLockedIn, cameraLocation);
        mainGameLoop.InitiateReset();
    }

    void loseGame() {
        Debug.Log("Game Over");
    }

    void FixedUpdate() {
        if (isBeingDragged) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float enter = 0.0f;
            if (piecePlane.Raycast(ray, out enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                rb.AddForce(
                    (hitPoint.x - transform.position.x) * dragAccelStrength, 
                    (hitPoint.y - transform.position.y) * dragAccelStrength,
                    (hitPoint.z - transform.position.z) * dragAccelStrength,
                    ForceMode.VelocityChange
                );
            }
        }
    }

    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        if (isActive && !isHovered) {
            GetComponent<Outline>().enabled = true;
            AudioSource.PlayClipAtPoint(soundHover, cameraLocation);
            isHovered = true;
        }
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        isHovered = false;
        GetComponent<Outline>().enabled = false;
    }

    void OnCollisionEnter(Collision collision) {
        if (isActive) {
            hasCollided = true;
            stopDrag();
        }
    }
}
