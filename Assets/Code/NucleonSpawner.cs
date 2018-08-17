using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NucleonSpawner : MonoBehaviour {

    public float timeBetweenSpawn;
    public float spawnDistance;

    public Nucleon[] nucleonPrefabs;

    float timeSinceLastSpawn;

    void FixedUpdate () {
        timeSinceLastSpawn += Time.fixedDeltaTime;
        if (timeSinceLastSpawn >= timeBetweenSpawn)
        {
            timeSinceLastSpawn -= timeBetweenSpawn;
            SpawnNucleon();
        }
	}

    void SpawnNucleon()
    {
        Nucleon prefab = nucleonPrefabs[Random.Range(0, nucleonPrefabs.Length)];
        Nucleon spawn = Instantiate<Nucleon>(prefab);
        spawn.transform.localPosition = Random.onUnitSphere * spawnDistance;
        spawn.transform.SetParent(this.transform);
    }
}
