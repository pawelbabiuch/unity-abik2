using UnityEngine;

public class MoveToTarget : MonoBehaviour {

    public CommandsSet targetsList;
    [Tooltip("How fast prendrive is moving")]
    public FloatValue speed;
    [Tooltip("Error border describing dead zone between target and our position.")]
    public FloatValue radius;

    //public bool isBackTracking = false;

    private int index = 0;

    private int Index
    {
        get
        {
            return index;
        }

        set
        {
            index = value;

            if (index >= targetsList.items.Count)
            {
                index = targetsList.items.Count - 1;
                //index = 0;
            }
            else if (index < 0)
            {
                //index = targetsList.items.Count - 1;
                index = 0;
            }
        }
    }

	// Use this for initialization
	void Start () {
        if (targetsList.items.Count == 0)
            Debug.Log("No targets");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (targetsList.items.Count > 0)
        {
            Vector3 target = targetsList.items[0];
            target.z = 0;

            Vector3 position = transform.position;
            position.z = 0;

            float step = speed.Value * Time.deltaTime;
            Vector3 newPosition = Vector3.MoveTowards(position, target, step);

            if (Vector3.Distance(newPosition, target) <= radius.Value)
            {
                targetsList.remove(0);

                //Index++;

            }

            newPosition.z = transform.position.z;

            transform.position = newPosition;
        }
	}
}
