using UnityEditor;
using UnityEngine;

namespace PlaceMe
{
#if UNITY_EDITOR
    [CustomEditor(typeof(PlaceMe_MovingPlatform3D))]
    public class PlaceMe_MovingPlatform3DEditor: Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            PlaceMe_MovingPlatform3D myScript = (PlaceMe_MovingPlatform3D)target;

            if (GUILayout.Button(
                    "Add POSITION")) // Button to visualize actions (or update actions) in the editor (editor script) while creating levels
            {
                myScript.AddPlatformPosition();
            }
        }
    }
#endif
}