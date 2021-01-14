// Jann

// Jann 08/11/20 -  Implementation

using UnityEngine;

/// <summary>
/// Deriving from this turns a class into a Singleton to access an instance of the object via <Classname>.Instance.
/// </summary>
/// <typeparam name="Class"></typeparam>
public class Singleton_Jann<Class> : MonoBehaviour where Class : Singleton_Jann<Class> 
{
    private static Class ins;
    public bool isPersistant;
     
    public virtual void Awake() 
    {
        if(isPersistant) 
        {
            if(!ins)
            {
                ins = this as Class;
            }
            else 
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            ins = this as Class;
        }
    }
    
    public static Class Instance => ins;
}
