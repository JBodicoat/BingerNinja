// Jann

// Jann 08/11/20 -  Implementation

using UnityEngine;

/// <summary>
/// Deriving from this turns a class into a Singleton to access an instance of the object via <Classname>.Instance.
/// </summary>
/// <typeparam name="Class"></typeparam>
public class Singleton_Jann<Class> : MonoBehaviour where Class : Singleton_Jann<Class> 
{
    private static Class m_instance;
    public bool isPersistant;
     
    public virtual void Awake() 
    {
        if(isPersistant) 
        {
            if(!m_instance)
            {
                m_instance = this as Class;
            }
            else 
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            m_instance = this as Class;
        }
    }
    
    public static Class Instance => m_instance;
}
