using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using Graphs;

public class DungeonGen : MonoBehaviour
{
    //Big Types of Structure
    enum CellType {
        None,
        Room,
        Hallway
    }    
    //Room Class + anti-overlap
    class Room {
        public RectInt bounds;
        public Room(Vector2Int location, Vector2Int size) {
            bounds = new RectInt(location, size);
        }
        public static bool Intersect(Room a, Room b) {
            return !((a.bounds.position.x >= (b.bounds.position.x + b.bounds.size.x)) || ((a.bounds.position.x + a.bounds.size.x) <= b.bounds.position.x)
                || (a.bounds.position.y >= (b.bounds.position.y + b.bounds.size.y)) || ((a.bounds.position.y + a.bounds.size.y) <= b.bounds.position.y));
        }
    }    
    //Level Customization (Input)
    public int difficulty;
    [SerializeField] Vector2Int levelSize;

    //Room Types
    public List <GameObject> cRooms = new List<GameObject>();//Combat Rooms
    public List <GameObject> lRooms = new List<GameObject>();//Loot Rooms
    public List <GameObject> mRooms = new List<GameObject>();//Misc Rooms

    //Temperory Constants
    private int baseDiffi = 5;
    private double pathProb = 0.125;

    //Stuff Calculated
    private int roomCount;//Total rooms need to generate
    private int cPoints;//amt of Combat rooms
    private int lPoints;//amt of Loot rooms
    private int mPoints;//amt of Misc rooms
    //private int lValue;//Vlaue of Loot (use in loot gen)

    //Looping Variables
    private GameObject theRoom;
    private int roomIndex;
    private int roomSizeX;
    private int roomSizeY;
    //Stuff Used
    Random random;
    List <Room> rooms;
    Grid2D<CellType> grid;
    Delaunay2D delaunay;
    HashSet<Prim.Edge> selectedEdges;

    //? Temperory Variables
    [SerializeField] GameObject cubePrefab;

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        random = new Random(0);
        grid = new Grid2D<CellType>(levelSize, Vector2Int.zero);
        rooms = new List<Room>();

        PlaceRooms();
        Triangulate();
        CreateHallways();
        PathfindHallways();
    }
    void calcDifficulty()
    {
        //How difficult is the layer
        cPoints = baseDiffi + (2*difficulty);
        lPoints = (int) Mathf.Ceil((baseDiffi + difficulty)/5);
        mPoints = 1;
        //lValue = difficulty;
        roomCount = cPoints + lPoints + mPoints;
    }
    void PlaceRooms()
    {
        calcDifficulty();
        //TODO: Room Rotation
        int roomRotate = random.Next(0, 3);
        //Vector3 target = new Vector3();
        //Room Selection
        for (int i = 0; i < roomCount; i++) {
            Debug.Log("i: " + i);
            //Determin room type
            if (i+1 <= cPoints){
                Debug.Log("combat");
                //Select a type of combat room
                roomIndex = random.Next(0, cRooms.Count-1);
                theRoom = cRooms[roomIndex];
            }
            if (i+1 > cPoints && i+1 <= cPoints+lPoints){
                Debug.Log("loot");
                //If no more cRooms, pick from lRooms
                roomIndex = random.Next(0, (lRooms.Count)-1);
                theRoom = lRooms[roomIndex];
            }
            if (i+1 > cPoints+lPoints && i+1 <= cPoints+lPoints+mPoints){
                Debug.Log("Misc");
                //If no more cRooms and lRooms, pick from mRooms
                roomIndex = random.Next(0, (mRooms.Count)-1);
                theRoom = mRooms[roomIndex];
            }
            //Random Room Location
            Vector2Int location = new Vector2Int(
                random.Next(0, levelSize.x),
                random.Next(0, levelSize.y)
            );
            //Calculate Room Size
            bool foundPosi = false;
            foreach (Transform child in theRoom.transform)
            {
                if (child.gameObject.tag == "RoomPosi")
                {
                    foundPosi = true;
                    if (roomRotate==0 || roomRotate==2)
                    {
                        roomSizeX = (int) Mathf.Ceil(child.position.x);
                        roomSizeY = (int) Mathf.Ceil(child.position.z);
                    }
                    if (roomRotate==1 || roomRotate==3)
                    {
                        roomSizeX = (int) Mathf.Ceil(child.position.z);
                        roomSizeY = (int) Mathf.Ceil(child.position.x);                        
                    }
                }
            }
            if (!foundPosi)
            {
                Debug.Log("RoomPosi Not Found");
            }
            Vector2Int roomSize = new Vector2Int(roomSizeX, roomSizeY);
            Debug.Log(roomSize);
            //I dont know what's this shit
            bool add = true;
            Room newRoom = new Room(location, roomSize);
            Room buffer = new Room(location + new Vector2Int(-1, -1), roomSize + new Vector2Int(2, 2));
            //Overlap prevention
            foreach (var room in rooms)
            {
                if (Room.Intersect(room, buffer)) {
                    Debug.Log("Intersected");
                    add = false;
                    break;
                }
            }
            //Spawn within borders
            if (newRoom.bounds.xMin < 0 || newRoom.bounds.xMax >= levelSize.x
                || newRoom.bounds.yMin < 0 || newRoom.bounds.yMax >= levelSize.y) {
                add = false;
            } 
            //Spawning the room
            if (add)
            {
                Debug.Log("RoomAdded");
                rooms.Add(newRoom);
                //! Might have to give center location
                //TODO: Quaternion Rotatoion
                Instantiate(theRoom, new Vector3(location.x, 0, location.y), Quaternion.LookRotation(transform.position));
                //idk what this means, prob get rid of this spawning space
                foreach (var pos in newRoom.bounds.allPositionsWithin) {
                    grid[pos] = CellType.Room;
                }
            }
            else
            {
                i--;
            }
        }
    }
    //Triangulate between rooms, make mesh
    void Triangulate() 
    {
        List<Vertex> vertices = new List<Vertex>();
        foreach (var room in rooms) {
            vertices.Add(new Vertex<Room>((Vector2)room.bounds.position + ((Vector2)room.bounds.size) / 2, room));
        }
        delaunay = Delaunay2D.Triangulate(vertices);
    }
    //Minium Spanning Tree
    void CreateHallways()
    {
        List<Prim.Edge> edges = new List<Prim.Edge>();

        foreach (var edge in delaunay.Edges) {
            edges.Add(new Prim.Edge(edge.U, edge.V));
        }

        List<Prim.Edge> mst = Prim.MinimumSpanningTree(edges, edges[0].U);

        selectedEdges = new HashSet<Prim.Edge>(mst);
        var remainingEdges = new HashSet<Prim.Edge>(edges);
        remainingEdges.ExceptWith(selectedEdges);
        //Add extra paths
        foreach (var edge in remainingEdges) {
            if (random.NextDouble() < pathProb) {
                selectedEdges.Add(edge);
            }
        }        
    }

    void PathfindHallways()
    {
        DungeonPathfinder2D aStar = new DungeonPathfinder2D(levelSize);
        //At this point, I give up trying to understand this
        foreach (var edge in selectedEdges) {
            var startRoom = (edge.U as Vertex<Room>).Item;
            var endRoom = (edge.V as Vertex<Room>).Item;

            var startPosf = startRoom.bounds.center;
            var endPosf = endRoom.bounds.center;
            var startPos = new Vector2Int((int)startPosf.x, (int)startPosf.y);
            var endPos = new Vector2Int((int)endPosf.x, (int)endPosf.y);

            var path = aStar.FindPath(startPos, endPos, (DungeonPathfinder2D.Node a, DungeonPathfinder2D.Node b) => {
                var pathCost = new DungeonPathfinder2D.PathCost();
                
                pathCost.cost = Vector2Int.Distance(b.Position, endPos);    //heuristic

                if (grid[b.Position] == CellType.Room) {
                    pathCost.cost += 10;
                } else if (grid[b.Position] == CellType.None) {
                    pathCost.cost += 5;
                } else if (grid[b.Position] == CellType.Hallway) {
                    pathCost.cost += 1;
                }

                pathCost.traversable = true;

                return pathCost;
            });

            if (path != null) {
                for (int i = 0; i < path.Count; i++) {
                    var current = path[i];

                    if (grid[current] == CellType.None) {
                        grid[current] = CellType.Hallway;
                    }

                    if (i > 0) {
                        var prev = path[i - 1];

                        var delta = current - prev;
                    }
                }
                foreach (var pos in path) {
                    if (grid[pos] == CellType.Hallway) {
                        PlaceHallway(pos);
                    }
                }

            }
        }    
    }


    //dunno how to place hallway, so here
    void PlaceHallway(Vector2Int location)
    {
        PlaceCube(location, new Vector2Int(100, 100));
    }
    void PlaceCube(Vector2Int location, Vector2Int size) {
        GameObject go = Instantiate(cubePrefab, new Vector3(location.x, 0, location.y), Quaternion.identity);
        go.GetComponent<Transform>().localScale = new Vector3(size.x, 1, size.y);
    }


}
