//Joao Beijinho

//Joao Beijinho 29/10/2020 - Created triggers for weighted pressure pads
//Joao Beijinho 02/11/2020 - Replaced m_door GameObject with m_doorCollider Collider2D

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

/// <summary>
/// This script is for Weighted Pressure Pads. An object needs to stay on top of it so that the door can remain open
/// </summary>
public class WeightedPressurePad_JoaoBeijinho : MonoBehaviour
{
    //Reference the door collider to turn if on/off(closed/open)
    public Collider2D m_doorCollider;
    public Sprite activatedPressurePad, inactivePressurePad;
    private string m_playerTag = "Player";
    private string m_crateTag = "Crate";
    private string m_meleeWeaponTag = "MeleeWeapon";
    private Tilemap walls1, walls2;

    private void Start()
    {
        walls1 = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
        walls2 = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collision)//Open door when an Object is on top of it
    {
        if (collision.tag == m_playerTag || collision.tag == m_crateTag || collision.tag == m_meleeWeaponTag)//collision with every array object except Projectile
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = activatedPressurePad;
            if(SceneManager.GetActiveScene().buildIndex == 17)
            {
                walls1.SetTile(new Vector3Int(32, 19, 0), null);
                walls1.SetTile(new Vector3Int(32, 20, 0), null);
                walls1.SetTile(new Vector3Int(32, 21, 0), null);
                walls1.SetTile(new Vector3Int(32, 22, 0), null);

                walls2.SetTile(new Vector3Int(33, 20, 0), null);
                walls2.SetTile(new Vector3Int(33, 21, 0), null);
                walls2.SetTile(new Vector3Int(33, 22, 0), null);
                walls2.SetTile(new Vector3Int(33, 23, 0), null);

            }
            if (SceneManager.GetActiveScene().buildIndex == 14)
            {
                walls1.SetTile(new Vector3Int(12, 26, 0), null);
                walls1.SetTile(new Vector3Int(12, 25, 0), null);
                walls1.SetTile(new Vector3Int(12, 24, 0), null);
                walls2.SetTile(new Vector3Int(13, 27, 0), null);
                walls2.SetTile(new Vector3Int(13, 26, 0), null);
                walls2.SetTile(new Vector3Int(13, 25, 0), null);

            }
            
            Debug.Log("yay");
            //m_doorCollider.GetComponent<Collider2D>().enabled = false;//Open door
        }
    }
        
    private void OnTriggerExit2D(Collider2D collision)//Close door when an object is removed from it
    {
        if (collision.tag == m_playerTag || collision.tag == m_crateTag || collision.tag == m_meleeWeaponTag)//if this scirpt is in a Pressure Pad
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = inactivePressurePad;
           // m_doorCollider.GetComponent<Collider2D>().enabled = true;//Close door
        }
    }
}