using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DungeonGenV2 : MonoBehaviour
{
    class Cell
    {
        public GameObject structure;
        public int cellX, cellZ;
        public Quaternion rotation;
        public Cell(GameObject structure,int x,int z,Quaternion rotation)
        {
            this.structure=structure;
            cellX=x;
            cellZ=z;
            this.rotation=rotation;
        }
        public void place(int cellSize, Transform parent)
        {GameObject.Instantiate(structure,new Vector3((cellX+0.5f)*cellSize,0,(cellZ+0.5f)*cellSize),rotation,parent);}
    }
    [System.Serializable] class RoomType
    {
        public GameObject room;
        public int count;
    }
    [SerializeField] RoomType[] types;
    [SerializeField] private GameObject startRoom;
    [SerializeField] private GameObject endRoom;
    [SerializeField] private Transform parent;
    private Cell[,] grid;
    [Range(2,100)] public int gridX;
    [Range(2,100)] public int gridZ;
    public int cellSize;
    // public int minPathDistance;
    // public int maxRoomDistance;
    public int pathDistance;
    Cell start;
    Cell end;
    void Awake()
    {
        validate();
    }
    void Start()
    {
        Camera.main.transform.position = new Vector3(gridX*cellSize/2,gridX*cellSize/2+gridZ*cellSize/2,gridZ*cellSize/2);
        generate();
    }
    void Update()
    {
        
    }
    public void generate()
    {
        stage1();
    }
    void clear()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                if(grid[x,z]!=null)
                {
                    Destroy(grid[x,z].structure);
                    grid[x,z]=null;
                }
            }
        }
    }
    Cell placeRoom(GameObject g) //place room in random unoccupied cell
    {
        while(true)
        {
            int x = Random.Range(0,gridX), z = Random.Range(0,gridZ);
            if(grid[x,z]==null)
            {
                return new Cell(g,x,z,new Quaternion());
            }
        }
    }
    private void OnDrawGizmos() {
        for (int i = 0; i <= gridX; i++)
            Gizmos.DrawLine(new Vector3(i*cellSize,0,0),new Vector3(i*cellSize,0,gridZ*cellSize));
        for (int i = 0; i <= gridZ; i++)
            Gizmos.DrawLine(new Vector3(0,0,i*cellSize),new Vector3(gridX*cellSize,0,i*cellSize));
    }
    int distanceBetween(Cell from, Cell to)
    {return Mathf.Abs(from.cellX-to.cellX)+Mathf.Abs(from.cellZ-to.cellZ);}
    void validate()
    {
        if(pathDistance>gridX*gridZ/2) {throw new System.Exception("Path distance too far");}
        int a=0;
        foreach (RoomType type in types) a+=type.count;
        if(a+2>gridX*gridZ) {throw new System.Exception("Too many rooms");}
    }
    void stage1() //place rooms
    {
        grid = new Cell[gridX,gridZ];
        bool startendplaced = false;
        while(!startendplaced)
        {
            //place start
            start = placeRoom(startRoom);
            Debug.Log("Start: ["+start.cellX+","+start.cellZ+"]");
            for (int i = 0; i < 10; i++) //10 attempts to place end, else replace start
            {
                end = placeRoom(endRoom);
                if(Mathf.Abs(end.cellX-start.cellX)+Mathf.Abs(end.cellZ-start.cellZ)>pathDistance) //start and end can't be too close
                {
                    grid[start.cellX,start.cellZ] = start;
                    start.place(cellSize,parent);
                    grid[end.cellX,end.cellZ]=end;
                    end.place(cellSize,parent);
                    Debug.Log("End: ["+end.cellX+","+end.cellZ+"]");
                    startendplaced=true;
                    break;
                }
            }
            Debug.LogWarning("Failed to place end, replacing start");
        }
        foreach (RoomType type in types)
        {
            for (int i = 0; i < type.count; i++)
            {
                while(true)
                {
                    Cell c = placeRoom(type.room);
                    grid[c.cellX,c.cellZ]=c;
                    c.place(cellSize,parent);
                    Debug.Log(type.room.name+": ["+c.cellX+","+c.cellZ+"]");
                    break;
                }
            }
        }
    }
}
