using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldgenv3 : MonoBehaviour
{
    class Cell
    {
        public GameObject structure;
        public int x, z;
        public Cell(GameObject structure,int cellX,int cellZ)
        {
            this.structure=structure;
            x=cellX;
            z=cellZ;
            doors = new Vector3[4];
        }
        public Vector3[] doors;
        public void place(int cellSize, Transform parent)
        {GameObject.Instantiate(structure,new Vector3((x+0.5f)*cellSize,0,(z+0.5f)*cellSize),new Quaternion(),parent);}
    }
    [System.Serializable] class RoomType
    {
        public GameObject room;
        public int count;
    }
    [System.Serializable] class Room
    {
        GameObject structure;
        Vector3 northDoor;
        Vector3 eastDoor;
        Vector3 southDoor;
        Vector3 westDoor;
    }
    enum Direction
    {north,east,south,west}
    [SerializeField] RoomType[] types;
    [SerializeField] private GameObject startRoom;
    [SerializeField] private GameObject endRoom;
    [SerializeField] private Transform parent;
    private Cell[,] grid;
    [Range(2,100)] public int gridX;
    [Range(2,100)] public int gridZ;
    public int cellSize;
    Cell start,end;
    List<GameObject> rooms = new List<GameObject>();
    void Awake()
    {
        validate();
    }
    void Start()
    {
        generate();
    }
    void validate()
    {
        int a=0;
        foreach (RoomType type in types) a+=type.count;
        if(a+2>gridX*gridZ/2) {throw new System.Exception("Too many rooms (max = gridsize/2");}
    }
    List<int> validPaths(Cell c)
    {
        List<int> e = new List<int>();
        if(c.z<gridZ-1 && grid[c.x,c.z+1]==null) e.Add(0);
        if(c.x<gridX-1 && grid[c.x+1,c.z]==null) e.Add(1);
        if(c.z>0 && grid[c.x,c.z-1]==null) e.Add(2);
        if(c.x>0 && grid[c.x-1,c.z]==null) e.Add(3);
        if(e.Count==0) return null;
        return e;
    }
    void generate()
    {
        e:
        foreach (RoomType type in types){for (int i = 0; i < type.count; i++){rooms.Add(type.room);}}
        grid = new Cell[gridX,gridZ];
        Cell room1;
        start = new Cell(startRoom,Random.Range(0,gridX),Random.Range(0,gridZ));
        grid[start.x,start.z]=start;
        start.place(cellSize,parent);
        Debug.Log($"Start: [{start.x},{start.z}]");
        int d = validPaths(start)[Random.Range(0,validPaths(start).Count)];
        int r = Random.Range(0,rooms.Count);
        switch(d)
        {
            case 0: room1 = new Cell(rooms[r],start.x,start.z+1); break;
            case 1: room1 = new Cell(rooms[r],start.x+1,start.z); break;
            case 2: room1 = new Cell(rooms[r],start.x,start.z-1); break;
            case 3: room1 = new Cell(rooms[r],start.x-1,start.z); break;
            default: clear(); goto e;
        }
        rooms.RemoveAt(r);
        Debug.Log($"{room1.structure.name}: [{room1.x},{room1.z}]");
        bool e = pathfind(room1);
        if(!e){clear(); goto e;}
    }
    bool pathfind(Cell c)
    {
        grid[c.x,c.z]=c;
        c.place(cellSize,parent);
        Debug.Log($"{c.structure.name}: [{c.x},{c.z}]");
        if(rooms.Count==0) {Debug.Log("no more rooms"); return true;}
        List<int> doors = validPaths(c);
        if(doors==null) {Debug.Log("no valid paths"); return false;}
        foreach (int i in doors)
        {
            if(Random.Range(0,2)==0)
            {
                int r = Random.Range(0,rooms.Count);
                switch(i)
                {
                    case 0: pathfind(new Cell(rooms[r],start.x,start.z+1)); break;
                    case 1: pathfind(new Cell(rooms[r],start.x+1,start.z)); break;
                    case 2: pathfind(new Cell(rooms[r],start.x,start.z-1)); break;
                    case 3: pathfind(new Cell(rooms[r],start.x-1,start.z)); break;
                    default: return false;
                }
                rooms.RemoveAt(r);
            }
        }
        Debug.Log("path finsihed");
        return true;
    }
    void clear()
    {
        foreach (Transform g in parent.GetComponentsInChildren<Transform>()) Destroy(g.gameObject);
        grid=null;
    }
    private void OnDrawGizmos() {
        for (int i = 0; i <= gridX; i++)
            Gizmos.DrawLine(new Vector3(i*cellSize,0,0),new Vector3(i*cellSize,0,gridZ*cellSize));
        for (int i = 0; i <= gridZ; i++)
            Gizmos.DrawLine(new Vector3(0,0,i*cellSize),new Vector3(gridX*cellSize,0,i*cellSize));
    }
}
