using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;

public class EnemySpawner : MonoBehaviour
{
    public float spawnTimer = 1;
    public GameObject prefabToSpawn;

    public float minEdgeDistance = 0;
    public MRUKAnchor.SceneLabels SpawnLabel;
    public float NormalOffset = 0;

    public int SpawnTry = 100;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!MRUK.Instance && !MRUK.Instance.IsInitialized)
            return;

        timer += Time.deltaTime;
        if (timer > spawnTimer)
        {
            SpawnGhost();
            timer -= spawnTimer;
        }
    }

 
    public void SpawnGhost()
    {
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();
        if (!room) return;

        for (int i = 0; i < SpawnTry; i++)
        {
            if (room.GenerateRandomPositionOnSurface(
                MRUK.SurfaceType.FACING_UP,
                minEdgeDistance,
                LabelFilter.Included(SpawnLabel),
                out Vector3 pos,
                out Vector3 norm))
            {
                Vector3 spawnPos = pos + norm * NormalOffset;
                // Consider keeping y position for vertical surfaces
                Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
                return;
            }
        }
    }
    
}
