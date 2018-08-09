using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "List", menuName = "Scriptable/List")]
public abstract class RuntimeSet<T> : ScriptableObject
{
    public List<T> items = new List<T>();

    public virtual void add(T thing)
    {
        if (!items.Contains(thing))
            items.Add(thing);
    }

    public virtual void remove(T thing)
    {
        if (items.Contains(thing))
            items.Remove(thing);
    }
}
