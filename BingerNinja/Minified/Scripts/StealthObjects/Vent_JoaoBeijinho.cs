using UnityEngine;public class Vent_JoaoBeijinho : StealthObject_JoaoBeijinho{GameObject a;void OnTriggerEnter2D(Collider2D b){if (b.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag)){if (!B.A){e();}}}void OnTriggerExit2D(Collider2D c){if (c.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag)){if (B.A){f();}}}void e(){W();Q();a.GetComponentInChildren<SpriteRenderer>().enabled = false;Physics2D.IgnoreLayerCollision(0, 10, true);}void f(){W();Q();a.GetComponentInChildren<SpriteRenderer>().enabled = true;gameObject.SetActive(false);Physics2D.IgnoreLayerCollision(0, 10, false);}void Start(){a = F("Player");gameObject.SetActive(false);}}