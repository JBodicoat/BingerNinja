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
     string a = "Player";
     string b = "Crate";
     string c = "MeleeWeapon";
     Tilemap d, e;

    public bool m_activated;
    public WeightedPressurePad_JoaoBeijinho m_otherPressurePad;

     void Start()
    {
        d = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
        e = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
    }

     void OnTriggerEnter2D(Collider2D f)//Open door when an Object is on top of it
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = inactivePressurePad;

        if (SceneManager.GetActiveScene().buildIndex == 17)
        {
            if (f.tag == b)//collision with crates
            {
                m_activated = true;

                if (m_otherPressurePad.m_activated)//Check if the other pressure pad is also activated
                {
                    d.SetTile(new Vector3Int(31, 19, 0), null);
                    d.SetTile(new Vector3Int(31, 20, 0), null);
                    d.SetTile(new Vector3Int(31, 21, 0), null);

                    e.SetTile(new Vector3Int(32, 20, 0), null);
                    e.SetTile(new Vector3Int(32, 21, 0), null);
                    e.SetTile(new Vector3Int(32, 22, 0), null);
                }
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 14)
        {
            if (f.tag == a || f.tag == b || f.tag == c)//collision with every array object except Projectile
            {
                d.SetTile(new Vector3Int(12, 26, 0), null);
                d.SetTile(new Vector3Int(12, 25, 0), null);
                d.SetTile(new Vector3Int(12, 24, 0), null);
                e.SetTile(new Vector3Int(13, 27, 0), null);
                e.SetTile(new Vector3Int(13, 26, 0), null);
                e.SetTile(new Vector3Int(13, 25, 0), null);
            }
        }
        
        //m_doorCollider.GetComponent<Collider2D>().enabled = false;//Open door
    }
        
     void OnTriggerExit2D(Collider2D g)//Close door when an object is removed from it
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = activatedPressurePad;

        if (SceneManager.GetActiveScene().buildIndex == 17)
        {
            if (g.tag == b)//collision with crates
            {
                if (!m_activated)//Check if the pressure pad is deactivated
                {
                    d.SetTile(new Vector3Int(31, 19, 0), m_downDoorPart);
                    d.SetTile(new Vector3Int(31, 20, 0), m_downDoorPart);
                    d.SetTile(new Vector3Int(31, 21, 0), m_downDoorPart);

                    e.SetTile(new Vector3Int(32, 20, 0), m_upDoorPart);
                    e.SetTile(new Vector3Int(32, 21, 0), m_upDoorPart);
                    e.SetTile(new Vector3Int(32, 22, 0), m_upDoorPart);
                }
            }
        }
      
    }
}