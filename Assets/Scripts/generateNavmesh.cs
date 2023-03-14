using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class generateNavmesh : MonoBehaviour
{
    public NavMeshSurface[] surfaces;
    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
