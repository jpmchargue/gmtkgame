using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawnerScript : MonoBehaviour
{
    public GameObject[] pieces;

    // Start is called before the first frame update
    void Start()
    {
        var cooldown = 5.0f;
        var coroutine = SpawnPiece(cooldown);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnPiece(float cooldown) {
        for(;;) {
            int index = Random.Range(0, pieces.Length);
            Instantiate(pieces[index], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(cooldown);
        }
    }
}
