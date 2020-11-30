using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TESTING_JW : MonoBehaviour
{
    //DELETE THIS ON BUILD - ONLY USED FOR TESTING

    Tilemap walls1, walls2;
    Tile bottomDoorTile, topDoorTile;

    private void Awake()
    {
        walls1 = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
        walls2 = GameObject.Find("Walls2_map").GetComponent<Tilemap>();

        bottomDoorTile = walls1.GetTile<Tile>(new Vector3Int(12, 26, 0));
        topDoorTile = walls1.GetTile<Tile>(new Vector3Int(13, 27, 0));
    }

    private void Start()
    {
        walls1.SetTile(new Vector3Int(12, 26, 0), null);
        walls1.SetTile(new Vector3Int(12, 25, 0), null);
        walls1.SetTile(new Vector3Int(12, 24, 0), null);
        walls2.SetTile(new Vector3Int(13, 27, 0), null);
        walls2.SetTile(new Vector3Int(13, 26, 0), null);
        walls2.SetTile(new Vector3Int(13, 25, 0), null);

    }

    private void Update()
    {
        
        
        walls1.SetTile(new Vector3Int(12, 26, 0), bottomDoorTile);
    }

}
