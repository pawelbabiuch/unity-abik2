using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject target;

    public CommandsSet commandSet;

    private void OnEnable()
    {
        commandSet.add(target.transform.position);
    }

    private void OnDisable()
    {
        commandSet.remove(target.transform.position);
    }
}
