using UnityEngine;

[CreateAssetMenu]
public class CommandsSet : RuntimeSet<Vector3>
{
    public override void add(Vector3 thing)
    {
        items.Add(thing);
    }

    public override void remove(Vector3 thing)
    {
        items.Remove(thing);
    }

    public void remove(int index)
    {
        items.RemoveAt(index);
    }
}
