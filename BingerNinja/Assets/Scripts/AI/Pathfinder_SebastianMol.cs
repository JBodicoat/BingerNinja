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
    public Node QA = null;
    public float QS = 0, QD = 0, QF = 0;
    public bool QG;
    public Vector2Int QH;
   
    public Node(bool QJ, Vector2Int QK) { QG = QJ; QH = QK; }
    public void QL()
    {
        QS = 0;
        QD = 0;
        QF = 0;
        QA = null;
    }
}

/// <summary>
/// hold logic that fids paths on the tilemap
/// </summary>
public class Pathfinder_SebastianMol : MonoBehaviour
{
    const float QZ = 1;
    const float QX = 1.414f;
    Node[,] QC;
    public Tile[] QV;
    public Tilemap QB;
    int QN = 0;

    public Vector2Int QM;
    public Vector2Int WQ;
    public Tile WW;

    /// <summary>
    /// creates a list of nodes that represents a path
    /// </summary>
    /// <param name="WR"> the start of the path</param>
    /// <param name="WT">the end of the path</param>
    /// <returns></returns>
    public List<Vector2Int> WE(Vector2Int WR, Vector2Int WT)
    {
        Node a = QC[WR.x, WR.y];
        Node b = QC[WT.x, WT.y];

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
                    Vector2Int WU = new Vector2Int(r.QH.x + x, r.QH.y + y); //curret poss that is begi checked relative to the currentnode
                    if (WY(WU)) continue; // skips out of bounds
                    if (QC[WU.x, WU.y].QG == false) continue; // skips walls
                    if (w.Contains(QC[WU.x, WU.y])) continue; //skips nodes already checked
                    if (math.abs(x) == math.abs(y))
                    {
                        if (!QC[WU.x - x, WU.y].QG) continue;
                        if (!QC[WU.x, WU.y - y].QG) continue;

                    }
                    //==================================================

                    float WO = (math.abs(x) == math.abs(y)) ? QX : QZ;

                    if (q.Contains(QC[WU.x, WU.y]))
                    {
                        if (r.QS + WO < QC[WU.x, WU.y].QS) // find cheaper path
                        {
                            //update node with new data
                            QC[WU.x, WU.y].QA = r;
                            QC[WU.x, WU.y].QS = r.QS + WO;
                            QC[WU.x, WU.y].QF = QC[WU.x, WU.y].QS + QC[WU.x, WU.y].QD;
                        }
                    }
                    else
                    {
                        //update node with new data
                        QC[WU.x, WU.y].QA = r;
                        QC[WU.x, WU.y].QS = r.QS + WO;
                        QC[WU.x, WU.y].QD = math.abs(b.QH.x - WU.x) + math.abs(b.QH.y - WU.y);
                        QC[WU.x, WU.y].QF = QC[WU.x, WU.y].QS + QC[WU.x, WU.y].QD;
                        q.Add(QC[WU.x, WU.y]);
                    }

                    //have i reached the target
                    if (QC[WU.x, WU.y] == b)
                    {
                        w.Add(QC[WU.x, WU.y]);
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
                if (node.QF < t.QF) t = node;
            }
            r = t;
        }

        e = WP(r, e);
        if (e.Count == 0) return new List<Vector2Int>();
        if (e[0] != b) return new List<Vector2Int>();

        List<Vector2Int> m = new List<Vector2Int>();
        for (int i = e.Count - 1; i >= 0; --i)
        {
            m.Add(new Vector2Int(e[i].QH.x, e[i].QH.y));
        }

        //preapre data for next search
        foreach (Node item in w) item.QL();
        foreach (Node item in q) item.QL();

        return m;
    }

    /// <summary>
    /// set a tile to be travercible.
    /// </summary>
    /// <param name="pos">postition of the tile</param>
    /// <param name="trav">the travercibilty of the tile</param>
    public void setTravercible(Vector2Int pos, bool trav)
    {
        QC[pos.x, pos.y].QG = trav;
    }

    /// <summary>
    /// adds a node to a list of nodes with reccursion.
    /// </summary>
    /// <param name="n"></param>
    /// <param name="a"></param>
    /// <returns></returns>
    List<Node> WP(Node n, List<Node> a)
    {
        if (QN<60)
        {
            QN++;
            a.Add(n);
            if (n.QA != null) WP(n.QA, a);
            return a;
        }
        else
        {
            QN = 0;
            return a;
        }
    }

    /// <summary>
    /// cheack to see if  atile is out of bounds
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    bool WY(Vector2Int p)
    {
        return p.x < 0 || p.x > QC.GetLength(0) - 1 || p.y < 0 || p.y > QC.GetLength(1) - 1 ? true : false;
    }

    void Start()
    {
        Vector3Int a = GetComponent<Tilemap>().size;

        //might need to change script order
        QC = new Node[a.x, a.y];
        for (int y = 0; y < a.y; y++)
            for (int x = 0; x < a.x; x++)
            {
                QC[x, y] = new Node(true, new Vector2Int(x, y));
            }


        QB = GetComponent<Tilemap>();
        for (int y = 0; y < QB.size.y; y++)
        {
            for (int x = 0; x < QB.size.x; x++)
            {
                for (int i = 0; i < QV.Length; i++)
                {
                    if (QB.GetTile(new Vector3Int(x, y, 0)))
                    {
                        QC[x, y].QG = false;
                    }
                }
            }
        }
    }
}
