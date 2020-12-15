//Joao Beijinho

//Joao Beijinho 29/10/2020 - Created triggers for weighted pressure pads
//Joao Beijinho 02/11/2020 - Replaced m_door GameObject with m_doorCollider Collider2D
//Joao Beijinho 06/12/2020 - Added //Door 2 section and bools for individual doors of Level 17
//                           Created RespawnDoor() and added it to the OnTriggerExit2D along with public TileBases
//                           Moved contents of RespawnDoor() to OnTriggerExit2D and removed the function, check for collision inside
//                           each SceneManager check, both on OnTriggerEnter2D and OnTriggerExit2D
//Joao Beijinho 14/12/2020 - Created Level 17 Settings
//                           On OnTriggerEnter2D, toggle this pressure pad as active and check if the other pressure pad is active
//                           On OnTriggerExit2D, toggle this pressure pad as deactivated
//                           Make both pressure pads on level 17 open only one door

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
    public TileBase m_downDoorPart, m_upDoorPart;
    private string m_playerTag = "Player";
    private string m_crateTag = "Crate";
    private string m_meleeWeaponTag = "MeleeWeapon";
    private Tilemap walls1, walls2;

    [Header("Level 17")]
    public bool m_activated;
    public WeightedPressurePad_JoaoBeijinho m_otherPressurePad;

    private void Start()
    {
        walls1 = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
        walls2 = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collision)//Open door when an Object is on top of it
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = activatedPressurePad;

        if(SceneManager.GetActiveScene().buildIndex == 17)
        {
            if (collision.tag == m_crateTag)//collision with crates
            {
                m_activated = true;

                if (m_otherPressurePad.m_activated)//Check if the other pressure pad is also activated
                {
                    walls1.SetTile(new Vector3Int(31, 19, 0), null);
                    walls1.SetTile(new Vector3Int(31, 20, 0), null);
                    walls1.SetTile(new Vector3Int(31, 21, 0), null);

                    walls2.SetTile(new Vector3Int(32, 20, 0), null);
                    walls2.SetTile(new Vector3Int(32, 21, 0), null);
                    walls2.SetTile(new Vector3Int(32, 22, 0), null);
                }
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 14)
        {
            if (collision.tag == m_playerTag || collision.tag == m_crateTag || collision.tag == m_meleeWeaponTag)//collision with every array object except Projectile
            {
                walls1.SetTile(new Vector3Int(12, 26, 0), null);
                walls1.SetTile(new Vector3Int(12, 25, 0), null);
                walls1.SetTile(new Vector3Int(12, 24, 0), null);
                walls2.SetTile(new Vector3Int(13, 27, 0), null);
                walls2.SetTile(new Vector3Int(13, 26, 0), null);
                walls2.SetTile(new Vector3Int(13, 25, 0), null);
            }
        }
        
        Debug.Log("yay");
        //m_doorCollider.GetComponent<Collider2D>().enabled = false;//Open door
    }
        
    private void OnTriggerExit2D(Collider2D collision)//Close door when an object is removed from it
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = activatedPressurePad;

        if (SceneManager.GetActiveScene().buildIndex == 17)
        {
            if (collision.tag == m_crateTag)//collision with crates
            {
                if (!m_activated)//Check if the pressure pad is deactivated
                {
                    walls1.SetTile(new Vector3Int(31, 19, 0), m_downDoorPart);
                    walls1.SetTile(new Vector3Int(31, 20, 0), m_downDoorPart);
                    walls1.SetTile(new Vector3Int(31, 21, 0), m_downDoorPart);

                    walls2.SetTile(new Vector3Int(32, 20, 0), m_upDoorPart);
                    walls2.SetTile(new Vector3Int(32, 21, 0), m_upDoorPart);
                    walls2.SetTile(new Vector3Int(32, 22, 0), m_upDoorPart);
                }
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 14)
        {
            if (collision.tag == m_playerTag || collision.tag == m_crateTag || collision.tag == m_meleeWeaponTag)//collision with every array object except Projectile
            {
                //walls1.SetTile(new Vector3Int(12, 26, 0), m_downDoorPart);
                //walls1.SetTile(new Vector3Int(12, 25, 0), m_downDoorPart);
                //walls1.SetTile(new Vector3Int(12, 24, 0), m_downDoorPart);
                //walls2.SetTile(new Vector3Int(13, 27, 0), m_upDoorPart);
                //walls2.SetTile(new Vector3Int(13, 26, 0), m_upDoorPart);
                //walls2.SetTile(new Vector3Int(13, 25, 0), m_upDoorPart);
            }
            // m_doorCollider.GetComponent<Collider2D>().enabled = true;//Close door
        }
    }
}