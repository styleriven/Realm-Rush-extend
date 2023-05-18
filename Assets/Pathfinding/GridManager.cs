using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize ;
    [Tooltip("World Grid Size - Should match UnityEditor snap settings")]
    [SerializeField] int unitySize = 10;

    public int UnitySize {get {return unitySize;}}

    Dictionary<Vector2Int,Node> grid = new Dictionary<Vector2Int,Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }
    void Awake() {
        createGird();
    }
    public Node GetNode(Vector2Int coordinate)
    {
        if(grid.ContainsKey(coordinate))
        {
            return grid[coordinate];
        }
        return null;
    }
    public void ResetNodes()
    {
        foreach(KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }
    public void BlockNode(Vector2Int coordinate)
    {
        if(grid.ContainsKey(coordinate))
        {
            grid[coordinate].isWalkable = false;
        }
    }
    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x/UnitySize);
        coordinates.y = Mathf.RoundToInt(position.z/UnitySize);
        
        return coordinates;

    }
    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();
        position.x = coordinates.x * UnitySize;
        position.z = coordinates.y * UnitySize;
        return position;
    }
    void createGird() {
        for (int i = 0; i < gridSize.x; i++) 
        {
            for (int j = 0; j < gridSize.y;j++)
            {
                Vector2Int coordinates = new Vector2Int(i,j);
                grid.Add(coordinates,new Node(coordinates,true));
            }
        }
    }
}
