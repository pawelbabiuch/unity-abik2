using UnityEditor;

public class YourClassAsset
{
    [MenuItem("Assets/Task/Add task")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<TaskData>();
    }
}