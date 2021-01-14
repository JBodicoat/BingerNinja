using UnityEngine;

public class TestBossDialog_MarioFernandes : MonoBehaviour
{
    public bool q = false;
    int a = 0;

    // Update is called once per frame
    void Update()
    {
        
        if (q)
        {
            gameObject.GetComponent<BossDialogue_MarioFernandes>().TriggerDialogue(a);
            q = false;
            a++;
		}
    }
}
