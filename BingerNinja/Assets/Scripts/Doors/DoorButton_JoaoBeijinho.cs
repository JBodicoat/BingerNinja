//Joao Beijinho

//Joao Beijinho 29/10/2020 - Created a trigger for the buttons
//Joao Beijinho 02/11/2020 - Replaced m_door GameObject with m_doorCollider Collider2D
//Joao Beijinho 05/11/2020 - Replaced collision.name to collision.tag on triggers
//Joao Beijinho 09/11/2020 - Replaced tags with the tags in the Tags_JoaoBeijinho script
//Chase Wilding 16/11/2020 - Edited to add tilemap removal/drawing for doors, added close functionality

using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// This script is for Buttons that open doors. Either if activated by the player or hit by a projectile
/// </summary>
public class DoorButton_JoaoBeijinho : MonoBehaviour
{
    protected PlayerController_JamieG QA;

    //Reference the door collider to turn if on/off(closed/open)
    public Collider2D QS;

    public Tile QD, QF;
     Tilemap a, b;

    public bool QG = false;
    public bool QH = false;

    public string QJ = "";

     void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag(Tags_JoaoBeijinho.QK))//Collision with Player
        {
            QG = true;//Allow player to open the door
        }
        else if (c.gameObject.CompareTag(Tags_JoaoBeijinho.QL))//Collision with Projectile
        {
            QS.GetComponent<Collider2D>().enabled = false;//Open door
        }
    }

     void OnTriggerExit2D(Collider2D d)
    {
        if (d.gameObject.CompareTag(Tags_JoaoBeijinho.QK))//Collision with Player
        {
            QG = false;//Don't allow player to open the door
        }
    }

    void Awake()
    {
        QA = FindObjectOfType<PlayerController_JamieG>();
        a = GameObject.Find("Walls1_map").GetComponent<Tilemap>();
        b = GameObject.Find("Walls2_map").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        if (QA.m_interact.triggered && QG && QJ == "")//Player interaction with button
        {
            PlayTrack_Jann.Instance.EM(AudioFiles.QL);
            if (!QH)
            {
                QH = true;
                b.SetTile(new Vector3Int(24, 9, 0), null);
                b.SetTile(new Vector3Int(24, 10, 0), null);
                b.SetTile(new Vector3Int(24, 11, 0), null);

                a.SetTile(new Vector3Int(23, 8, 0), null);
                a.SetTile(new Vector3Int(23, 9, 0), null);
                a.SetTile(new Vector3Int(23, 10, 0), null);
              //  m_doorCollider.GetComponent<Collider2D>().enabled = false;//Open door
            }
            else
            {
                b.SetTile(new Vector3Int(24, 9, 0), QF);
                b.SetTile(new Vector3Int(24, 10, 0), QF);
                b.SetTile(new Vector3Int(24, 11, 0), QF);

                a.SetTile(new Vector3Int(23, 8, 0), QD);
                a.SetTile(new Vector3Int(23, 9, 0), QD);
                a.SetTile(new Vector3Int(23, 10, 0), QD);
                QH = false;
            }
        }

        if (QA.m_interact.triggered && QG && QJ == "S11.B1")//Player interaction with button
        {
            if (!QH)
            {
                QH = true;
                b.SetTile(new Vector3Int(12, 20, 0), null);
                b.SetTile(new Vector3Int(13, 20, 0), null);
                b.SetTile(new Vector3Int(14, 20, 0), null);

                a.SetTile(new Vector3Int(13, 19, 0), null);
                a.SetTile(new Vector3Int(12, 19, 0), null);
                a.SetTile(new Vector3Int(11, 19, 0), null);
            }
            else
            {
                b.SetTile(new Vector3Int(12, 20, 0), QF);
                b.SetTile(new Vector3Int(13, 20, 0), QF);
                b.SetTile(new Vector3Int(14, 20, 0), QF);

                a.SetTile(new Vector3Int(13, 19, 0), QD);
                a.SetTile(new Vector3Int(12, 19, 0), QD);
                a.SetTile(new Vector3Int(11, 19, 0), QD);
                QH = false;
            }

        }
        if (QA.m_interact.triggered && QG && QJ == "S11.B2")//Player interaction with button
        {
            if (!QH)
            {

                QH = true;
                b.SetTile(new Vector3Int(20, 20, 0), null);
                b.SetTile(new Vector3Int(19, 20, 0), null);
                b.SetTile(new Vector3Int(18, 20, 0), null);

                a.SetTile(new Vector3Int(19, 19, 0), null);
                a.SetTile(new Vector3Int(18, 19, 0), null);
                a.SetTile(new Vector3Int(17, 19, 0), null);
            }
            else
            {
                b.SetTile(new Vector3Int(20, 20, 0), QF);
                b.SetTile(new Vector3Int(19, 20, 0), QF);
                b.SetTile(new Vector3Int(18, 20, 0), QF);

                a.SetTile(new Vector3Int(19, 19, 0), QD);
                a.SetTile(new Vector3Int(18, 19, 0), QD);
                a.SetTile(new Vector3Int(17, 19, 0), QD);
                QH = false;
            }

        }
        if (QA.m_interact.triggered && QG && QJ == "S11.B3")//Player interaction with button
        {
            if (!QH)
            {
                QH = true;
                b.SetTile(new Vector3Int(26, 20, 0), null);
                b.SetTile(new Vector3Int(25, 20, 0), null);
                b.SetTile(new Vector3Int(24, 20, 0), null);

                a.SetTile(new Vector3Int(25, 19, 0), null);
                a.SetTile(new Vector3Int(24, 19, 0), null);
                a.SetTile(new Vector3Int(23, 19, 0), null);
            }
            else
            {
                b.SetTile(new Vector3Int(26, 20, 0), QF);
                b.SetTile(new Vector3Int(25, 20, 0), QF);
                b.SetTile(new Vector3Int(24, 20, 0), QF);

                a.SetTile(new Vector3Int(25, 19, 0), QD);
                a.SetTile(new Vector3Int(24, 19, 0), QD);
                a.SetTile(new Vector3Int(23, 19, 0), QD);
                QH = false;
            }

        }
    }
}