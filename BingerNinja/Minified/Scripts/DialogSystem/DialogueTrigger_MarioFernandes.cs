using UnityEngine;public class DialogueTrigger_MarioFernandes : M{public Dialogue m_dialogue;bool m_state = true;public void TriggerDialogue (){FOT<DialogueManager_MarioFernandes>().StartDialogue(m_dialogue);}private void OnTriggerEnter2D(Collider2D other) {if(other.tag == "Player" && m_state){TriggerDialogue();m_state = false;gameObject.SetActive(false);}}}