using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ATempObstacle : MonoBehaviour
{
    [SerializeField]
    public NavMeshSurface[] surfaces;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        foreach (var surface in surfaces)
        {
            surface.BuildNavMesh();
        }
    }
}
