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
    public Node a = null;
    public float q = 0, w = 0, e = 0;
    public bool r;
    public Vector2Int t;
   
    public Node(bool y, Vector2Int u) { r = y; t = u; }
    public void i()
    {
        q = 0;
        w = 0;
        e = 0;
        a = null;
    }
}

/// <summary>
/// hold logic that fids paths on the tilemap
/// </summary>
public class Pathfinder_SebastianMol : MonoBehaviour
{
    const float o = 1;
    const float p = 1.414f;
    Node[,] s;
    public Tile[] d;
    public Tilemap WE;
    int f = 0;

    public Vector2Int g;
    public Vector2Int h;
    public Tile j;

    /// <summary>
    /// creates a list of nodes that represents a path
    /// </summary>
    /// <param name="l"> the start of the path</param>
    /// <param name="z">the end of the path</param>
    /// <returns></returns>
    public List<Vector2Int> k(Vector2Int l, Vector2Int z)
    {
        Node c = s[l.x, l.y];
        Node v = s[z.x, z.y];

        List<Node> b = new List<Node>();
        List<Node> n = new List<Node>();
        List<Node> m = new List<Node>();

        Node Q = c;

        if (c == v) return new List<Vector2Int>();

        b.Add(Q);

        while (Q != v)
        {
            n.Add(Q);
            b.Remove(Q);

            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    //skip checks
                    if (x == 0 && y == 0) continue; //skips current tile
                    //if (math.abs(x) == math.abs(y)) continue; //skip diagonals
                    Vector2Int W = new Vector2Int(Q.t.x + x, Q.t.y + y); //curret poss that is begi checked relative to the currentnode
                    if (F(W)) continue; // skips out of bounds
                    if (s[W.x, W.y].r == false) continue; // skips walls
                    if (n.Contains(s[W.x, W.y])) continue; //skips nodes already checked
                    if (math.abs(x) == math.abs(y))
                    {
                        if (!s[W.x - x, W.y].r) continue;
                        if (!s[W.x, W.y - y].r) continue;

                    }
                    //==================================================

                    float E = (math.abs(x) == math.abs(y)) ? p : o;

                    if (b.Contains(s[W.x, W.y]))
                    {
                        if (Q.q + E < s[W.x, W.y].q) // find cheaper path
                        {
                            //update node with new data
                            s[W.x, W.y].a = Q;
                            s[W.x, W.y].q = Q.q + E;
                            s[W.x, W.y].e = s[W.x, W.y].q + s[W.x, W.y].w;
                        }
                    }
                    else
                    {
                        //update node with new data
                        s[W.x, W.y].a = Q;
                        s[W.x, W.y].q = Q.q + E;
                        s[W.x, W.y].w = math.abs(v.t.x - W.x) + math.abs(v.t.y - W.y);
                        s[W.x, W.y].e = s[W.x, W.y].q + s[W.x, W.y].w;
                        b.Add(s[W.x, W.y]);
                    }

                    //have i reached the target
                    if (s[W.x, W.y] == v)
                    {
                        n.Add(s[W.x, W.y]);
                        Q = v;
                    }
                }
            }

            if (Q == v) break;
            if (b.Count == 0) break;

            //find the cheapest node
            Node R = b[0];
            foreach (Node T in b)
            {
                if (T.e < R.e) R = T;
            }
            Q = R;
        }

        m = Y(Q, m);
        if (m.Count == 0) return new List<Vector2Int>();
        if (m[0] != v) return new List<Vector2Int>();

        List<Vector2Int> U = new List<Vector2Int>();
        for (int i = m.Count - 1; i >= 0; --i)
        {
            U.Add(new Vector2Int(m[i].t.x, m[i].t.y));
        }

        //preapre data for next search
        foreach (Node I in n) I.i();
        foreach (Node O in b) O.i();

        return U;
    }

    /// <summary>
    /// set a tile to be travercible.
    /// </summary>
    /// <param name="A">postition of the tile</param>
    /// <param name="S">the travercibilty of the tile</param>
    public void P(Vector2Int A, bool S)
    {
        s[A.x, A.y].r = S;
    }

    /// <summary>
    /// adds a node to a list of nodes with reccursion.
    /// </summary>
    /// <param name="n"></param>
    /// <param name="D"></param>
    /// <returns></returns>
    private List<Node> Y(Node n, List<Node> D)
    {
        if (f<60)
        {
            f++;
            D.Add(n);
            if (n.a != null) Y(n.a, D);
            return D;
        }
        else
        {
            f = 0;
            return D;
        }
    }

    /// <summary>
    /// cheack to see if  atile is out of bounds
    /// </summary>
    /// <param name="G"></param>
    /// <returns></returns>
    private bool F(Vector2Int G)
    {
        return G.x < 0 || G.x > s.GetLength(0) - 1 || G.y < 0 || G.y > s.GetLength(1) - 1 ? true : false;
    }

    private void Start()
    {
        Vector3Int H = GetComponent<Tilemap>().size;

        //might need to change script order
        s = new Node[H.x, H.y];
        for (int y = 0; y < H.y; y++)
            for (int x = 0; x < H.x; x++)
            {
                s[x, y] = new Node(true, new Vector2Int(x, y));
            }


        WE = GetComponent<Tilemap>();
        for (int y = 0; y < WE.size.y; y++)
        {
            for (int x = 0; x < WE.size.x; x++)
            {
                for (int i = 0; i < d.Length; i++)
                {
                    if (WE.GetTile(new Vector3Int(x, y, 0)))
                    {
                        s[x, y].r = false;
                    }
                }
            }
        }
    }
}
