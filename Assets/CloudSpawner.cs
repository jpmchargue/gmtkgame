using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloud;
    public Transform cloudSpawnPos;
    public Transform spawnTrigger;
    public Transform deletePoint;
    public float cloudSpeed = 7f;

    public void SpawnCloud()
    {
        GameObject obj = Instantiate(cloud, this.transform);
        obj.transform.position = cloudSpawnPos.position;
        obj.GetComponent<CloudMover>().spawnTrigger = spawnTrigger;
        obj.GetComponent<CloudMover>().deletePoint = deletePoint;
        obj.GetComponent<CloudMover>().speed = cloudSpeed;
    }

}
