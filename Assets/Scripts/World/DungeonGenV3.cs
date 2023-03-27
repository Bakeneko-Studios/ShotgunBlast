using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenV3 : MonoBehaviour
{
    class Cell
    {
        public Room room;
        public int x, z;
        public Cell(Room structure,int cellX,int cellZ,int cellSize)
        {
            room=structure;
            x=cellX;
            z=cellZ;
            doors = new Vector3[4];
            room.eastDoor+=new Vector3((x+0.5f)*cellSize,0,(z+0.5f)*cellSize);
            room.westDoor+=new Vector3((x+0.5f)*cellSize,0,(z+0.5f)*cellSize);
            room.northDoor+=new Vector3((x+0.5f)*cellSize,0,(z+0.5f)*cellSize);
            room.southDoor+=new Vector3((x+0.5f)*cellSize,0,(z+0.5f)*cellSize);
        }
        public Vector3[] doors;
        public void place(int cellSize, Transform parent)
        {
            GameObject g = GameObject.Instantiate(room.structure,new Vector3((x+0.5f)*cellSize,0,(z+0.5f)*cellSize),new Quaternion(),parent);
            transforms.Add(g.transform);
        }
    }

    [System.Serializable] class RoomType
    {
        public GameObject roomObject;
        public int count;
        public Room room;
    }

    private bool valid=true;
    private readonly int maxAttempts = 100;
    [SerializeField] RoomType[] types;
    [SerializeField] private Room startRoom;
    [SerializeField] private Room endRoom;
    [SerializeField] private Transform parent;
    [SerializeField] private Transform hallwayParent;
    [SerializeField] private GameObject hallwaySegment;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject block;
    public int segmentLength;
    [SerializeField] private Cell[,] grid;
    [Range(2,100)] public int gridX;
    [Range(2,100)] public int gridZ;
    public int cellSize;
    [Range(0.25f,1)]public float doorSpawnRate = 0.5f;
    Cell start,end,lastRoom;
    List<Room> rooms = new List<Room>();
    [SerializeField] List<Room> placedRooms = new List<Room>();
    List<Vector3> hallways = new List<Vector3>();
    public static List<Transform> transforms = new List<Transform>();

    void Awake()
    {
        validate();
    }
    void Start()
    {
        clear();
        generate();
    }

    void validate()
    {
        int a=0;
        foreach (RoomType type in types) a+=type.count;
        if(a+2>gridX*gridZ/2) {Debug.LogError("Too many rooms (max = gridsize/2"); valid=false;}
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

    public void generate()
    {
        if(!valid) {Debug.LogWarning("invalid config, can't generate"); return;}
        int attempts = 0;

        attempt:
        attempts++;
        if(attempts>maxAttempts)
        {
            Debug.LogError("Exceeded max failed attempts, exiting");
            return;
        }
        
        //initialize rooms list
        foreach (RoomType type in types)
        {
            type.room = type.roomObject.GetComponent<RoomMaster>().theRoom;
            for (int i = 0; i < type.count; i++)
                rooms.Add(new Room(type.room));
        }

        //initialize grid
        grid = new Cell[gridX,gridZ];
        Cell room1;

        //place start room
        start = new Cell(new Room(startRoom),Random.Range(0,gridX),Random.Range(0,gridZ),cellSize);
        grid[start.x,start.z]=start;
        start.place(cellSize,parent);
        placedRooms.Add(start.room);
        Debug.Log($"Start: [{start.x},{start.z}]");
        
        //place first room
        int d = validPaths(start)[Random.Range(0,validPaths(start).Count)];
        int r = Random.Range(0,rooms.Count);

        switch(d) //case for each direction
        {
            case 0: room1 = new Cell(rooms[r], start.x, start.z+1, cellSize); hallways.Add(start.room.northDoor); hallways.Add(room1.room.southDoor); break;
            case 1: room1 = new Cell(rooms[r], start.x+1, start.z, cellSize); hallways.Add(start.room.eastDoor);  hallways.Add(room1.room.westDoor);  break;
            case 2: room1 = new Cell(rooms[r], start.x, start.z-1, cellSize); hallways.Add(start.room.southDoor); hallways.Add(room1.room.northDoor); break;
            case 3: room1 = new Cell(rooms[r], start.x-1, start.z, cellSize); hallways.Add(start.room.westDoor);  hallways.Add(room1.room.eastDoor);  break;
            default: clear(); Debug.LogWarning("restarting (1)"); goto attempt;
        }
        placedRooms.Add(room1.room);

        //start pathfinding from first room
        bool e = pathfind(room1,r);
        if(!e||rooms.Count!=0)
        {
            clear();
            Debug.LogWarning("restarting (2)");
            goto attempt;
        }

        //find valid end placement
        List<int> b = validPaths(lastRoom);
        if(b==null) {clear(); Debug.LogWarning("restarting (3)"); goto attempt;}
        d = b[Random.Range(0,b.Count)];

        //place end
        switch(d)
        {
            case 0: end = new Cell(new Room(endRoom), lastRoom.x, lastRoom.z+1, cellSize); hallways.Add(lastRoom.room.northDoor); hallways.Add(end.room.southDoor); break;
            case 1: end = new Cell(new Room(endRoom), lastRoom.x+1, lastRoom.z, cellSize); hallways.Add(lastRoom.room.eastDoor);  hallways.Add(end.room.westDoor);  break;
            case 2: end = new Cell(new Room(endRoom), lastRoom.x, lastRoom.z-1, cellSize); hallways.Add(lastRoom.room.southDoor); hallways.Add(end.room.northDoor); break;
            case 3: end = new Cell(new Room(endRoom), lastRoom.x-1, lastRoom.z, cellSize); hallways.Add(lastRoom.room.westDoor);  hallways.Add(end.room.eastDoor);  break;
            default: clear(); Debug.LogWarning("restrting (4)"); goto attempt;
        }
        grid[end.x,end.z]=end;
        end.place(cellSize,parent);
        placedRooms.Add(end.room);

        //place hallways
        if(!connect()) {clear(); Debug.LogWarning("restarting (5)"); goto attempt;}

        //place doors
        if(!doors()) {clear(); Debug.LogWarning("restarting (6)"); goto attempt;}

        Debug.Log("dungeon generated sucessfully");

        movement.instance.transform.position = new Vector3(cellSize*(start.x+0.5f),movement.instance.height/2,cellSize*(start.z+0.5f));
    }

    bool pathfind(Cell c, int r)
    {
        if(rooms.Count==1) lastRoom=c;
        
        //place previous room
        rooms.RemoveAt(r);
        grid[c.x,c.z]=c;
        c.place(cellSize,parent);
        placedRooms.Add(c.room);
        Debug.Log($"{c.room.structure.name}: [{c.x},{c.z}]");

        //find valid paths for next rooms(s)
        List<int> doors = validPaths(c);
        if(doors==null) {Debug.LogWarning("no valid paths"); return false;}
        for (int i = 0; i < doors.Count; i++) {if(Random.value<doorSpawnRate) {doors.RemoveAt(i);i--;}}

        //continue pathfinding in each chosen direction
        foreach (int i in doors)
        {
            if(rooms.Count==0) {Debug.Log("no more rooms"); return true;}
            int rr = Random.Range(0,rooms.Count);
            switch(i)
            {
                case 0: hallways.Add(c.room.northDoor); hallways.Add(new Vector3((c.x+0.5f)*cellSize, 0, (c.z+1.5f)*cellSize)+rooms[rr].southDoor); pathfind(new Cell(rooms[rr], c.x, c.z+1, cellSize),rr); break;
                case 1: hallways.Add(c.room.eastDoor);  hallways.Add(new Vector3((c.x+1.5f)*cellSize, 0, (c.z+0.5f)*cellSize)+rooms[rr].westDoor);  pathfind(new Cell(rooms[rr], c.x+1, c.z, cellSize),rr); break;
                case 2: hallways.Add(c.room.southDoor); hallways.Add(new Vector3((c.x+0.5f)*cellSize, 0, (c.z-0.5f)*cellSize)+rooms[rr].northDoor); pathfind(new Cell(rooms[rr], c.x, c.z-1, cellSize),rr); break;
                case 3: hallways.Add(c.room.westDoor);  hallways.Add(new Vector3((c.x-0.5f)*cellSize, 0, (c.z+0.5f)*cellSize)+rooms[rr].eastDoor);  pathfind(new Cell(rooms[rr], c.x-1, c.z, cellSize),rr); break;
                default: return false;
            }
        }
        Debug.Log("path finished");
        return true;
    }

    bool connect()
    {
        if(hallwaySegment==null) throw new System.Exception("Missing hallway object");
        //takes pairs of Vector3s and places hallway segments between them
        if(hallways.Count%2!=0) {throw new System.Exception("Hallway missing start/end point");}
        for (int i = 0; i < hallways.Count; i+=2)
        {
            switch (hallways[i+1]-hallways[i])
            {
                case Vector3 v1 when v1.x>0: for (int j = 0; j <= v1.x; j+=segmentLength)  {GameObject.Instantiate(hallwaySegment, hallways[i] + new Vector3(j+segmentLength/2, 0, segmentLength/2),  Quaternion.Euler(0,90,0), hallwayParent);} break;
                case Vector3 v2 when v2.x<0: for (int j = 0; j <= -v2.x; j+=segmentLength) {GameObject.Instantiate(hallwaySegment, hallways[i] + new Vector3(-j+segmentLength/2, 0, segmentLength/2), Quaternion.Euler(0,-90,0), hallwayParent);} break;
                case Vector3 v3 when v3.z>0: for (int j = 0; j <= v3.z; j+=segmentLength)  {GameObject.Instantiate(hallwaySegment, hallways[i] + new Vector3(segmentLength/2, 0, j+segmentLength/2),  Quaternion.Euler(0,0,0), hallwayParent);} break;
                case Vector3 v4 when v4.z<0: for (int j = 0; j <= -v4.z; j+=segmentLength) {GameObject.Instantiate(hallwaySegment, hallways[i] + new Vector3(segmentLength/2, 0, -j+segmentLength/2), Quaternion.Euler(0,180,0), hallwayParent);} break;
                default: throw new System.Exception("VEIFWEFGUOYDHIUYG#OEWDUOMIYWEGFFUCKKKAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            }
        }
        return true;
    }

    // bool doors()
    // {
    //     if(door==null) throw new System.Exception("Missing door object");
    //     for (int i = 0; i < hallways.Count; i+=2)
    //     {
    //         switch (hallways[i+1]-hallways[i])
    //         {
    //             case Vector3 v1 when v1.x>0: GameObject g1 = GameObject.Instantiate(door, hallways[i], Quaternion.Euler(0,90,0),  hallwayParent); GameObject.Instantiate(door, hallways[i+1], Quaternion.Euler(0,90,0), hallwayParent);  break;
    //             case Vector3 v2 when v2.x<0: GameObject g2 = GameObject.Instantiate(door, hallways[i], Quaternion.Euler(0,-90,0), hallwayParent); GameObject.Instantiate(door, hallways[i+1], Quaternion.Euler(0,-90,0), hallwayParent); break;
    //             case Vector3 v3 when v3.z>0: GameObject g3 = GameObject.Instantiate(door, hallways[i], Quaternion.Euler(0,0,0),   hallwayParent); GameObject.Instantiate(door, hallways[i+1], Quaternion.Euler(0,0,0), hallwayParent);   break;
    //             case Vector3 v4 when v4.z<0: GameObject g4 = GameObject.Instantiate(door, hallways[i], Quaternion.Euler(0,180,0), hallwayParent); GameObject.Instantiate(door, hallways[i+1], Quaternion.Euler(0,180,0), hallwayParent); break;
    //             default: throw new System.Exception("VEIFWEFGUOYDHIUYG#OEWDUOMIYWEGFFUCKKKAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
    //         }
    //     }
    //     return true;
    // }

    bool doors()
    {
        if(door==null) throw new System.Exception("Missing door object");
        if(block==null) throw new System.Exception("Missing wall object");

        if(!hallways.Contains(start.room.northDoor)) {GameObject.Instantiate(block, start.room.northDoor, Quaternion.Euler(0,180,0), hallwayParent);}
        if(!hallways.Contains(start.room.eastDoor))  {GameObject.Instantiate(block, start.room.eastDoor,  Quaternion.Euler(0,-90,0), hallwayParent);}
        if(!hallways.Contains(start.room.southDoor)) {GameObject.Instantiate(block, start.room.southDoor, Quaternion.Euler(0,0,0),   hallwayParent);}
        if(!hallways.Contains(start.room.westDoor))  {GameObject.Instantiate(block, start.room.westDoor,  Quaternion.Euler(0,90,0),  hallwayParent);}
        
        if(!hallways.Contains(end.room.northDoor)) {GameObject.Instantiate(block, end.room.northDoor, Quaternion.Euler(0,180,0), hallwayParent);}
        if(!hallways.Contains(end.room.eastDoor))  {GameObject.Instantiate(block, end.room.eastDoor,  Quaternion.Euler(0,-90,0), hallwayParent);}
        if(!hallways.Contains(end.room.southDoor)) {GameObject.Instantiate(block, end.room.southDoor, Quaternion.Euler(0,0,0),   hallwayParent);}
        if(!hallways.Contains(end.room.westDoor))  {GameObject.Instantiate(block, end.room.westDoor,  Quaternion.Euler(0,90,0),  hallwayParent);}
        
        foreach (Transform t in transforms)
        {
            if(t.TryGetComponent<RoomMaster>(out RoomMaster rm))
            {
                Debug.Log(t.position);
                if(hallways.Contains(t.transform.position+rm.theRoom.northDoor)) {GameObject g = GameObject.Instantiate(door, t.transform.position+rm.theRoom.northDoor, Quaternion.Euler(0,180,0), hallwayParent); rm.lockers.Add(g.GetComponent<Locker>());} else {GameObject.Instantiate(block, t.transform.position+rm.theRoom.northDoor, Quaternion.Euler(0,180,0), hallwayParent);}
                if(hallways.Contains(t.transform.position+rm.theRoom.eastDoor))  {GameObject g = GameObject.Instantiate(door, t.transform.position+rm.theRoom.eastDoor,  Quaternion.Euler(0,-90,0), hallwayParent); rm.lockers.Add(g.GetComponent<Locker>());} else {GameObject.Instantiate(block, t.transform.position+rm.theRoom.eastDoor,  Quaternion.Euler(0,-90,0), hallwayParent);}
                if(hallways.Contains(t.transform.position+rm.theRoom.southDoor)) {GameObject g = GameObject.Instantiate(door, t.transform.position+rm.theRoom.southDoor, Quaternion.Euler(0,0,0),   hallwayParent); rm.lockers.Add(g.GetComponent<Locker>());} else {GameObject.Instantiate(block, t.transform.position+rm.theRoom.southDoor, Quaternion.Euler(0,0,0),   hallwayParent);}
                if(hallways.Contains(t.transform.position+rm.theRoom.westDoor))  {GameObject g = GameObject.Instantiate(door, t.transform.position+rm.theRoom.westDoor,  Quaternion.Euler(0,90,0),  hallwayParent); rm.lockers.Add(g.GetComponent<Locker>());} else {GameObject.Instantiate(block, t.transform.position+rm.theRoom.westDoor,  Quaternion.Euler(0,90,0),  hallwayParent);}
                rm.freeRoom();
            }
        }
        return true;
    }

    public void clear()
    {
        foreach (Transform t in parent) {if(t.name!=hallwayParent.name) Destroy(t.gameObject);}
        foreach (Transform t in hallwayParent) {Destroy(t.gameObject);}
        transforms.Clear();
        rooms.Clear();
        placedRooms.Clear();
        hallways.Clear();
        grid=null;
    }
    
    private void OnDrawGizmos()
    {
        for (int i = 0; i <= gridX; i++)
            Gizmos.DrawLine(new Vector3(i*cellSize,0,0),new Vector3(i*cellSize,0,gridZ*cellSize));
        for (int i = 0; i <= gridZ; i++)
            Gizmos.DrawLine(new Vector3(0,0,i*cellSize),new Vector3(gridX*cellSize,0,i*cellSize));
        // Gizmos.color = Color.green;
        // for (int i = 0; i < hallways.Count-1; i+=2)
        // {
        //     Gizmos.DrawLine(hallways[i],hallways[i+1]);
        // }
    }
}
