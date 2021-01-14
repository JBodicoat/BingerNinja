using UnityEngine;public class HideBehindable_JoaoBeijinho : StealthObject_JoaoBeijinho{private SpriteRenderer m_changeLayer;private SpriteRenderer m_playerSprite;private string m_playerSpriteLayer;private GameObject m_player;private ParticleSystem m_smokeParticleSystem;private bool m_canHide = false;private bool m_isHiding = false;private string m_playerTag = "Player";private void OnTriggerEnter2D(Collider2D collision){if (collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag)){m_canHide = true;}}private void OnTriggerExit2D(Collider2D collision){if (collision.gameObject.CompareTag(Tags_JoaoBeijinho.m_playerTag)){m_canHide = false;}}void Start(){m_player = F("Player");m_playerSprite = m_player.GetComponent<SpriteRenderer>();m_playerSpriteLayer = m_playerSprite.sortingLayerName;m_changeLayer = GetComponent<SpriteRenderer>();m_smokeParticleSystem = GetComponentInChildren<ParticleSystem>();}void Update(){if (m_playerControllerScript.m_interact.triggered && m_isHiding == true){m_canHide = true;m_isHiding = false;m_playerSprite.sortingOrder = 10;m_playerSprite.sortingLayerName = m_playerSpriteLayer;Hide();m_playerControllerScript.m_movement.Enable();m_playerControllerScript.m_crouch.Enable();gameObject.layer = 2;}else if (m_playerControllerScript.m_interact.triggered && m_canHide == true && !m_playerStealthScript.m_crouched){m_canHide = false;m_isHiding = true;m_playerSprite.sortingOrder = 8;m_playerSprite.sortingLayerName = m_changeLayer.sortingLayerName;m_player.transform.position = new Vector3( gameObject.transform.position.x, gameObject.transform.position.y, m_player.transform.position.z);Hide();m_playerControllerScript.m_movement.Disable();m_playerControllerScript.m_crouch.Disable();m_smokeParticleSystem.Play();gameObject.layer = 0;}}}