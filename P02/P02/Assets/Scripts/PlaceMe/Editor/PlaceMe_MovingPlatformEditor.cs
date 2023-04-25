using UnityEngine;
using UnityEditor;

namespace PlaceMe
{
#if UNITY_EDITOR
    [CustomEditor(typeof(PlaceMe_MovingPlatform2D))]
    public class PlaceMe_MovingPlatformEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            PlaceMe_MovingPlatform2D myScript = (PlaceMe_MovingPlatform2D)target;

            if (GUILayout.Button(
                    "Add POSITION")) // Button to visualize actions (or update actions) in the editor (editor script) while creating levels
            {
                myScript.AddPlatformPosition();
            }
        }
    }
#endif
}
