using UnityEngine;
using UnityEditor;

//custom editor for readme component
[CustomEditor(typeof(ReadMe))]
public class ReadMeEditor : Editor {

	bool editing;
	string editingText;

	public override void OnInspectorGUI() {
		serializedObject.Update();
		SerializedProperty text = serializedObject.FindProperty("text");
		GUILayoutOption[] buttonOptions = { GUILayout.Width(48) };
		if (editing) {
			editingText = EditorGUILayout.TextArea(editingText);
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Apply", EditorStyles.miniButton, buttonOptions)) {
				text.stringValue = editingText;
				editing = false;
			}
			if (GUILayout.Button("Cancel", EditorStyles.miniButton, buttonOptions)) {
				editing = false;
			}
			GUILayout.EndHorizontal();
		}
		else {
			EditorGUILayout.HelpBox(text.stringValue, MessageType.Info);
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Edit", EditorStyles.miniButton, buttonOptions)) {
				editing = true;
				editingText = text.stringValue;
			}
			GUILayout.EndHorizontal();
		}
		serializedObject.ApplyModifiedProperties();
	}

}