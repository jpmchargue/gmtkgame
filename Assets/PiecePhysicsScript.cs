using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePhysicsScript : MonoBehaviour
{
    private bool isHovered;
    private bool isBeingDragged;
    private Plane piecePlane;
    private Rigidbody rb;

    private float lockedZ = 0.0f;
    private float dragAccelStrength = 1.5f;
    private float dragDrag = 10;
    private float zEulerAngle = 0.0f;
    private float dragRotationSpeed = 0.1f;
    private Color noDragColor = new Color(0.5f, 0.5f, 0.5f);
    private Color dragColor = new Color(0.0f, 0.737255f, 1.0f, 0.6f);
    

    // Start is called before the first frame update
    void Start()
    {
        isHovered = false;
        isBeingDragged = false;
        piecePlane = new Plane(Vector3.forward, 0.0f);
        rb = GetComponent<Rigidbody>();
        GetComponent<MeshRenderer>().material.color = noDragColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBeingDragged) {
            if (isHovered && Input.GetMouseButtonDown(0)) {
                startDrag();
            }
        } else {
            if (Input.GetKey("left")) {
                zEulerAngle += dragRotationSpeed;
            }
            if (Input.GetKey("right")) {
                zEulerAngle -= dragRotationSpeed;
            }

            // Lock rotation while dragging
            transform.eulerAngles = new Vector3(0.0f, 0.0f, zEulerAngle);

            if (Input.GetMouseButtonUp(0)) {
                stopDrag();
            }
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, lockedZ);
        transform.eulerAngles = new Vector3(0.0f, 0.0f, transform.eulerAngles.z);
    }

    void startDrag() {
        isBeingDragged = true;
        zEulerAngle = transform.eulerAngles.z;
        rb.drag = dragDrag;
        GetComponent<MeshRenderer>().material.color = dragColor;
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
        if (!isHovered) {
            GetComponent<Outline>().enabled = true;
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
        stopDrag();
    }
}
