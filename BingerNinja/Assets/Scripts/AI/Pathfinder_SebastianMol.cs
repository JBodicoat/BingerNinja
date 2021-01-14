//sebastian mol
//sebastian mol 21/10/20 started to create the pathfindig class
//sebastian mol 30/10/20 added headers to classes and functions

using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;


/// <summary>
/// node class that reprisents a tile on the tilemap
/// </summary>
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

/// <summary>
/// hold logic that fids paths on the tilemap
/// </summary>
public class Pathfinder_SebastianMol : MonoBehaviour
{
    const float MOVE_COST = 1;
    const float MOVE_COST_DIAG = 1.414f;
    Node[,] m_allTiles;
    public Tile[] m_wallTiles;
    public Tilemap m_tileMap;
    int count = 0;

    public Vector2Int m_startPos;
    public Vector2Int m_targetPos;
    public Tile m_pathTile;

    /// <summary>
    /// creates a list of nodes that represents a path
    /// </summary>
    /// <param name="startPos"> the start of the path</param>
    /// <param name="targetPos">the end of the path</param>
    /// <returns></returns>
    public List<Vector2Int> PathFind(Vector2Int startPos, Vector2Int targetPos)
    {
        Node a = m_allTiles[startPos.x, startPos.y];
        Node b = m_allTiles[targetPos.x, targetPos.y];

        List<Node> q = new List<Node>();
        List<Node> w = new List<Node>();
        List<Node> e = new List<Node>();

        Node r = a;

        if (a == b) return new List<Vector2Int>();

        q.Add(r);

        while (r != b)
        {
            w.Add(r);
            q.Remove(r);

            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    //skip checks
                    if (x == 0 && y == 0) continue; //skips current tile
                    //if (math.abs(x) == math.abs(y)) continue; //skip diagonals
                    Vector2Int worldPos = new Vector2Int(r.m_pos.x + x, r.m_pos.y + y); //curret poss that is begi checked relative to the currentnode
                    if (CheckOutOfBounds(worldPos)) continue; // skips out of bounds
                    if (m_allTiles[worldPos.x, worldPos.y].m_traversable == false) continue; // skips walls
                    if (w.Contains(m_allTiles[worldPos.x, worldPos.y])) continue; //skips nodes already checked
                    if (math.abs(x) == math.abs(y))
                    {
                        if (!m_allTiles[worldPos.x - x, worldPos.y].m_traversable) continue;
                        if (!m_allTiles[worldPos.x, worldPos.y - y].m_traversable) continue;

                    }
                    //==================================================

                    float movementCost = (math.abs(x) == math.abs(y)) ? MOVE_COST_DIAG : MOVE_COST;

                    if (q.Contains(m_allTiles[worldPos.x, worldPos.y]))
                    {
                        if (r.m_g + movementCost < m_allTiles[worldPos.x, worldPos.y].m_g) // find cheaper path
                        {
                            //update node with new data
                            m_allTiles[worldPos.x, worldPos.y].m_parentNode = r;
                            m_allTiles[worldPos.x, worldPos.y].m_g = r.m_g + movementCost;
                            m_allTiles[worldPos.x, worldPos.y].m_f = m_allTiles[worldPos.x, worldPos.y].m_g + m_allTiles[worldPos.x, worldPos.y].m_h;
                        }
                    }
                    else
                    {
                        //update node with new data
                        m_allTiles[worldPos.x, worldPos.y].m_parentNode = r;
                        m_allTiles[worldPos.x, worldPos.y].m_g = r.m_g + movementCost;
                        m_allTiles[worldPos.x, worldPos.y].m_h = math.abs(b.m_pos.x - worldPos.x) + math.abs(b.m_pos.y - worldPos.y);
                        m_allTiles[worldPos.x, worldPos.y].m_f = m_allTiles[worldPos.x, worldPos.y].m_g + m_allTiles[worldPos.x, worldPos.y].m_h;
                        q.Add(m_allTiles[worldPos.x, worldPos.y]);
                    }

                    //have i reached the target
                    if (m_allTiles[worldPos.x, worldPos.y] == b)
                    {
                        w.Add(m_allTiles[worldPos.x, worldPos.y]);
                        r = b;
                    }
                }
            }

            if (r == b) break;
            if (q.Count == 0) break;

            //find the cheapest node
            Node t = q[0];
            foreach (Node node in q)
            {
                if (node.m_f < t.m_f) t = node;
            }
            r = t;
        }

        e = AddNodeToPath(r, e);
        if (e.Count == 0) return new List<Vector2Int>();
        if (e[0] != b) return new List<Vector2Int>();

        List<Vector2Int> m = new List<Vector2Int>();
        for (int i = e.Count - 1; i >= 0; --i)
        {
            m.Add(new Vector2Int(e[i].m_pos.x, e[i].m_pos.y));
        }

        //preapre data for next search
        foreach (Node item in w) item.ResetData();
        foreach (Node item in q) item.ResetData();

        return m;
    }

    /// <summary>
    /// set a tile to be travercible.
    /// </summary>
    /// <param name="pos">postition of the tile</param>
    /// <param name="trav">the travercibilty of the tile</param>
    public void setTravercible(Vector2Int pos, bool trav)
    {
        m_allTiles[pos.x, pos.y].m_traversable = trav;
    }

    /// <summary>
    /// adds a node to a list of nodes with reccursion.
    /// </summary>
    /// <param name="n"></param>
    /// <param name="a"></param>
    /// <returns></returns>
    List<Node> AddNodeToPath(Node n, List<Node> a)
    {
        if (count<60)
        {
            count++;
            a.Add(n);
            if (n.m_parentNode != null) AddNodeToPath(n.m_parentNode, a);
            return a;
        }
        else
        {
            count = 0;
            return a;
        }
    }

    /// <summary>
    /// cheack to see if  atile is out of bounds
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    bool CheckOutOfBounds(Vector2Int p)
    {
        return p.x < 0 || p.x > m_allTiles.GetLength(0) - 1 || p.y < 0 || p.y > m_allTiles.GetLength(1) - 1 ? true : false;
    }

    void Start()
    {
        Vector3Int a = GetComponent<Tilemap>().size;

        //might need to change script order
        m_allTiles = new Node[a.x, a.y];
        for (int y = 0; y < a.y; y++)
            for (int x = 0; x < a.x; x++)
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
                    if (m_tileMap.GetTile(new Vector3Int(x, y, 0)))
                    {
                        m_allTiles[x, y].m_traversable = false;
                    }
                }
            }
        }
    }
}
