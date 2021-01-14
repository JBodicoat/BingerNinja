using UnityEngine;
using UnityEngine.InputSystem;

public class Test_MarioFernandes : MonoBehaviour
{
    PoisionDefuff_MarioFernandes myPoison;

    bool i = true;
    // Start is called before the first frame update
    void Start()
    {
        myPoison = new PoisionDefuff_MarioFernandes(5, 10);
    }

    // Update is called once per frame
    void Update()
    {
        var curKeyboard= Keyboard.current;        

        if (curKeyboard.spaceKey.isPressed && i)
        {
            gameObject.GetComponent<EffectManager_MarioFernandes>().AddEffect(myPoison);
            i = !i;
		}
    }
}
