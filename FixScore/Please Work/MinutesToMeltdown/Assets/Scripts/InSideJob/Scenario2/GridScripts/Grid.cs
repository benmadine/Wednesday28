using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
   // public Transform player;
    public Node[,] grid;
    public Vector2 gridWorldSize;

    public float nodeRadius;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    public LayerMask players;

    bool onNode;

    public GameObject fogPrefab;

    public List<Node> fogNode = new List<Node>();

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    private void Update()
    {
        
    }

    private void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector2 worldBottomLeft = (new Vector2(transform.position.x, transform.position.y)) - Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y / 2; //Getting the bottom left corner
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius); //Start at bottom left then adds nodes from there
                GameObject fog = Instantiate(fogPrefab, new Vector2(worldPoint.x, worldPoint.y), Quaternion.identity);
                grid[x, y] = new Node(worldPoint, fog);
            }
        }
    }

    public Node NodeFromWorldPoint(Vector2 worldPosition) //The node the player is on
    {
        int x = Mathf.RoundToInt((worldPosition.x + gridWorldSize.x / 2 - nodeRadius) / nodeDiameter);
        int y = Mathf.RoundToInt((worldPosition.y + gridWorldSize.y / 2 - nodeRadius) / nodeDiameter);

        x = Mathf.Clamp(x, 0, gridSizeX - 1);
        y = Mathf.Clamp(y, 0, gridSizeY - 1);

        return grid[x, y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(gridWorldSize.x, gridWorldSize.y));

        if(grid != null)
        {
            //Node playerNode = NodeFromWorldPoint(player.position);
            foreach(Node node in grid)
            {
                Gizmos.color = Color.green;
                //if (playerNode == node)
                //{
                //    Gizmos.color = Color.red;
                //}
                Gizmos.DrawWireCube(node.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
            }
        }
    }
}
