using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Node
{
    public Node m_parentNode = null;
    public float m_g = 0, m_h = 0, m_f = 0;
    public bool m_traversable;
    public Vector2Int m_pos;
    public Node(bool trav, Vector2Int newPos) { m_traversable = trav; m_pos = newPos; }
    public void ResetData()
    {
        m_g = 0;
        m_h = 0;
        m_f = 0;
        m_parentNode = null;
    }
}

public class Pathfinder_SebastianMol : MonoBehaviour
{
    const float MOVE_COST = 1;
    const float MOVE_COST_DIAG = 1.414f;
    Node[,] m_allTiles;
    public Tile[] m_wallTiles;
    public bool m_button = false;
    public Vector2Int m_startPos;
    public Vector2Int m_targetPos;
    public Tile m_pathTile;
    public Tilemap m_tileMap;

    private void Start()
    {
        Vector3Int size = GetComponent<Tilemap>().size;

        //might need to change script order
        m_allTiles = new Node[size.x, size.y];
        for (int y = 0; y < size.y; y++)
            for (int x = 0; x < size.x; x++)
            {
                m_allTiles[x, y] = new Node(true, new Vector2Int(x, y));
            }


        m_tileMap = GetComponent<Tilemap>();
        for (int y = 0; y < m_tileMap.size.y; y++)
        {
            for (int x = 0; x < m_tileMap.size.x; x++)
            {
                for (int i = 0; i < m_wallTiles.Length; i++)
                {
                    if (m_tileMap.GetTile(new Vector3Int( x, y, 0)) == m_wallTiles[i])
                    {
                        m_allTiles[x, y].m_traversable = false;
                    }
                }
            }
        }      
    }

    private void Update()
    {
        if (m_button)
        {
            m_button = false;
            List <Vector2Int> path = PathFind(m_startPos, m_targetPos);
            for (int i = 0; i < path.Count; i++)
            {
                m_tileMap.SetTile(new Vector3Int(path[i].x, path[i].y, 0), m_pathTile);

                //m_tileMap.SetTileFlags(new Vector3Int(path[i].x, path[i].y, 0), TileFlags.None);
                //m_tileMap.SetColor(new Vector3Int(path[i].x, path[i].y, 0), Color.green);
                //Debug.Log(m_tileMap.GetCellCenterWorld(new Vector3Int(path[i].x, path[i].y, 0)));
            }
        }
    }

    public List<Vector2Int> PathFind(Vector2Int startPos, Vector2Int targetPos)
    {
        #region prep
        Node startNode = m_allTiles[startPos.x, startPos.y];
        Node targetNode = m_allTiles[targetPos.x, targetPos.y];

        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();
        List<Node> FinalPath = new List<Node>();

        Node currentNode = startNode;

        if (startNode == targetNode) return new List<Vector2Int>();

        openList.Add(currentNode);
        #endregion

        #region algorithm
        while (currentNode != targetNode)
        {
            closedList.Add(currentNode);
            openList.Remove(currentNode);

            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    //skip checks
                    if (x == 0 && y == 0) continue; //skips current tile
                    //if (math.abs(x) == math.abs(y)) continue; //skip diagonals
                    Vector2Int worldPos = new Vector2Int(currentNode.m_pos.x + x, currentNode.m_pos.y + y); //curret poss that is begi checked relative to the currentnode
                    if (CheckOutOfBounds(worldPos)) continue; // skips out of bounds
                    if (m_allTiles[worldPos.x, worldPos.y].m_traversable == false) continue; // skips walls
                    if (closedList.Contains(m_allTiles[worldPos.x, worldPos.y])) continue; //skips nodes already checked
                    if (math.abs(x) == math.abs(y))
                    {
                        if (!m_allTiles[worldPos.x - x, worldPos.y].m_traversable) continue;
                        if (!m_allTiles[worldPos.x, worldPos.y - y].m_traversable) continue;

                    }
                    //==================================================

                    float movementCost = (math.abs(x) == math.abs(y)) ? MOVE_COST_DIAG : MOVE_COST;

                    if (openList.Contains(m_allTiles[worldPos.x, worldPos.y]))
                    {
                        if (currentNode.m_g + movementCost < m_allTiles[worldPos.x, worldPos.y].m_g) // find cheaper path
                        {
                            //update node with new data
                            m_allTiles[worldPos.x, worldPos.y].m_parentNode = currentNode;
                            m_allTiles[worldPos.x, worldPos.y].m_g = currentNode.m_g + movementCost;
                            m_allTiles[worldPos.x, worldPos.y].m_f = m_allTiles[worldPos.x, worldPos.y].m_g + m_allTiles[worldPos.x, worldPos.y].m_h;
                        }
                    }
                    else
                    {
                        //update node with new data
                        m_allTiles[worldPos.x, worldPos.y].m_parentNode = currentNode;
                        m_allTiles[worldPos.x, worldPos.y].m_g = currentNode.m_g + movementCost;
                        m_allTiles[worldPos.x, worldPos.y].m_h = math.abs(targetNode.m_pos.x - worldPos.x) + math.abs(targetNode.m_pos.y - worldPos.y);
                        m_allTiles[worldPos.x, worldPos.y].m_f = m_allTiles[worldPos.x, worldPos.y].m_g + m_allTiles[worldPos.x, worldPos.y].m_h;
                        openList.Add(m_allTiles[worldPos.x, worldPos.y]);
                    }

                    //have i reached the target
                    if (m_allTiles[worldPos.x, worldPos.y] == targetNode)
                    {
                        closedList.Add(m_allTiles[worldPos.x, worldPos.y]);
                        currentNode = targetNode;
                    }
                }
            }

            if (currentNode == targetNode) break;
            if (openList.Count == 0) break;

            //find the cheapest node
            Node lowestCostNode = openList[0];
            foreach (Node node in openList)
            {
                if (node.m_f < lowestCostNode.m_f) lowestCostNode = node;
            }
            currentNode = lowestCostNode;
        }
        #endregion

        #region resolution
        FinalPath = AddNodeToPath(currentNode, FinalPath);
        if (FinalPath.Count == 0) return new List<Vector2Int>();
        if (FinalPath[0] != targetNode) return new List<Vector2Int>();
        #endregion

        #region clean up
        List<Vector2Int> DaPath = new List<Vector2Int>();
        for (int i = FinalPath.Count - 1; i >= 0; --i)
        {
            DaPath.Add(new Vector2Int(FinalPath[i].m_pos.x, FinalPath[i].m_pos.y));
        }

        //preapre data for next search
        foreach (Node item in closedList) item.ResetData();
        foreach (Node item in openList) item.ResetData();
        #endregion
        return DaPath;
    }
    private List<Node> AddNodeToPath(Node n, List<Node> path)
    {
        path.Add(n);
        if (n.m_parentNode != null) AddNodeToPath(n.m_parentNode, path);
        return path;
    }

    bool CheckOutOfBounds(Vector2Int pos)
    {
        return pos.x < 0 || pos.x > m_allTiles.GetLength(0) - 1 || pos.y < 0 || pos.y > m_allTiles.GetLength(1) - 1 ? true : false;
    }

    public void setTravercible(Vector2Int pos, bool trav)
    {
        m_allTiles[pos.x, pos.y].m_traversable = trav;
    }
}
