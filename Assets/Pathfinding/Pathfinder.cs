using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{

    [SerializeField] public Vector2Int StartCoordinates;
    [SerializeField] public Vector2Int destinateCoordinates;

    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

    Node startNode;
    Node destinationNode;
    Node currentSearchNode;
    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.Grid;
            startNode = grid[StartCoordinates];
            destinationNode = grid[destinateCoordinates];
            startNode.isWalkable = true;
            destinationNode.isWalkable = true;
            
        }
        
    }

    void Start()
    {
        
        GetNewPath();
    }
    public List<Node> GetNewPath()
    {
        return GetNewPath(StartCoordinates);
    }
    
    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(coordinates);
        return BuildPath();
    }

    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoords = currentSearchNode.coordinates + direction;

            if (grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);
                grid[neighborCoords].isExplored = true;
               
            }
            foreach (Node neighbor in neighbors)
            {
                if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
                {
                    neighbor.connectedTo = currentSearchNode;
                    reached.Add(neighbor.coordinates, neighbor);
                    frontier.Enqueue(neighbor);
                }
            }

        }
    }
    void BreadthFirstSearch(Vector2Int coordinates)
    {
        frontier.Clear();
        reached.Clear();
        bool isRunning = true;
        frontier.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);
        while (frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            ExploreNeighbors();
            if (currentSearchNode.coordinates == destinateCoordinates)
            {
                isRunning = false;
            }
        }
    }
    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();

        return path;

    }
    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;

            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = previousState;

            if(newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }

        }
        
        return false;
    }
    public void NotifyReceiver()
    {
        BroadcastMessage("RecalculatePath",false,SendMessageOptions.RequireReceiver);
    }
}
