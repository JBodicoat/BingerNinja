using UnityEngine;public class PlayerAnimation_LouieWilliamson : M{private bool isFacingLeft;private Animator m_playerAnim;private Rigidbody2D m_rb;public void TriggerAttackAnim(){if (isFacingLeft){if (transform.localScale.x < 0){FlipSprite();}}m_playerAnim.SetTrigger("isAttacking");}public void RollAnimation(bool isRolling){m_playerAnim.SetBool("isRolling", isRolling);}public void Crouching(bool isCrouching){m_playerAnim.SetBool("isCrouching", isCrouching);}void Start(){isFacingLeft = true;m_playerAnim = GetComponent<Animator>();m_rb = GetComponent<Rigidbody2D>();}void Update(){if (m_rb.velocity.x != 0 || m_rb.velocity.y != 0){m_playerAnim.SetTrigger("isMoving");}else {m_playerAnim.SetTrigger("isIdle");}if (m_rb.velocity.x < 0){if (!isFacingLeft){FlipSprite();isFacingLeft = true;}}else if (m_rb.velocity.x > 0){if (isFacingLeft){FlipSprite();isFacingLeft = false;}}if (m_rb.velocity.y < 0){m_playerAnim.SetBool("isFacing" , true);}else if (m_rb.velocity.y > 0){m_playerAnim.SetBool("isFacing", false);}}private void FlipSprite(){transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);}}