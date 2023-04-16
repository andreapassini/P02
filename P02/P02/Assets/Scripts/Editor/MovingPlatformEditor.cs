using MovingPlatformAndUI;
using UnityEditor;
using UnityEngine;

namespace Editor
{
#if UNITY_EDITOR
    [CustomEditor(typeof(MovingPlatform))]
    public class MovingPlatformEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            MovingPlatform myScript = (MovingPlatform)target;

            if (GUILayout.Button(
                    "Add POSITION")) // Button to visualize actions (or update actions) in the editor (editor script) while creating levels
            {
                myScript.AddPlatformPosition();
            }

        }
    }
#endif
}
