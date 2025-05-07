using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;

public class FairySpawnners : MonoBehaviour
{
    public int NumberOfFairiesToSpawn;
    public GameObject FairiesPrefab;
    public float height;

    public List<GameObject> SpawnedFairies;

    public int maxNumberOfTry = 100;
    private int currentNumberOfTry = 0;

    public static FairySpawnners instance;


    private void Awake()
    {
        instance = this; 
    }
 

// Start is called before the first frame update
void Start()
    {
        MRUK.Instance.RegisterSceneLoadedCallback(SpawnFairy);
    }

    public void DestroyFairy(GameObject orb)
    {
        SpawnedFairies.Remove(orb);
        Destroy(orb);

        if(SpawnedFairies.Count == 0 )
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    public void SpawnFairy()

    {
        for (int i = 0; i < NumberOfFairiesToSpawn; i++)
            {

            Vector3 randomPosition = Vector3.zero;
            MRUKRoom room = MRUK.Instance.GetCurrentRoom();

            while(currentNumberOfTry < maxNumberOfTry)
            {
                bool hasFound = room.GenerateRandomPositionOnSurface(MRUK.SurfaceType.FACING_UP,
                    1, LabelFilter.Included(MRUKAnchor.SceneLabels.FLOOR), out randomPosition, out Vector3 n);

                if (hasFound)
                    break;

                currentNumberOfTry++;
            }

            randomPosition.y = height;
            GameObject Spawend = Instantiate(FairiesPrefab, randomPosition, Quaternion.identity);
            SpawnedFairies.Add(Spawend);

        }
    }

}