using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMover : MonoBehaviour
{
    public float speed = 60f;
    public Transform spawnTrigger;
    public Transform deletePoint;

    private bool triggerHit = false;


    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (!triggerHit && transform.position.x - spawnTrigger.position.x < 0f)
        {
            triggerHit = true;
            this.GetComponentInParent<CloudSpawner>().SpawnCloud();
        }

        if (transform.position.x - deletePoint.position.x < 0f)
        {
            Destroy(this.gameObject);
        }
    }
}
