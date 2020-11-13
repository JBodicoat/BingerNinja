// Jann

// Jann - 13/11/20 - Abstraction of MonoBehaviour

using System.Collections;
using UnityEngine;

/// <summary>
/// Derive from this instead of MonoBehaviour
/// </summary>
public class M : MonoBehaviour
{
    public T G<T>() where T : Component
    {
        return GetComponent<T>();
    }

    public T G<T>(GameObject g) where T : Component
    {
        return g.GetComponent<T>();
    }

    public GameObject F(string s)
    {
        return GameObject.Find(s);
    }

    public GameObject FT(string s)
    {
        return GameObject.FindGameObjectWithTag(s);
    }

    public T F<T>() where T : Object
    {
        return FindObjectOfType<T>();
    }

    public T[] Fs<T>() where T : Object
    {
        return FindObjectsOfType<T>();
    }

    public Transform C(Transform g, int i)
    {
        return g.GetChild(i);
    }

    public Coroutine SC(IEnumerator c)
    {
        return StartCoroutine(c);
    }

    public void D(GameObject g)
    {
        Destroy(g);
    }
}