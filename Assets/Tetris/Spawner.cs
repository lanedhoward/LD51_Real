using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    // Groups
    public GameObject[] groups;

    public void SpawnNext()
    {
        // Random Index
        int i = Random.Range(0, groups.Length);

        // Spawn Group at current Position
        Instantiate(groups[i],
                    transform.position,
                    Quaternion.identity);
    }

    private void Start()
    {
        SpawnNext();
    }
}
