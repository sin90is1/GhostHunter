using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
using Meta.XR.MRUtilityKit;


public class RuntimeNavMeshBuilderComponent : MonoBehaviour
{
    private NavMeshSurface NavmeshSurface;

    // Start is called before the first frame update
    void Start()
    {
        NavmeshSurface = GetComponent<NavMeshSurface>();
        MRUK.Instance.RegisterSceneLoadedCallback(BuildNavMesh);
    }

    public void BuildNavMesh()
    {
        StartCoroutine(BuildNavMeshRoutine());
    }

    public IEnumerator BuildNavMeshRoutine()
    {
        yield return new WaitForEndOfFrame();
        NavmeshSurface.BuildNavMesh();
    }

}
