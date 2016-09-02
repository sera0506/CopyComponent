using UnityEngine;
using UnityEditor;
using System.Collections;

public class CopyComponentScript : EditorWindow {

	GameObject source;
	GameObject destination;
	string s_comp;
	Component source_comp;

	[MenuItem("Window/Copy Component")]
	static void Init(){
		CopyComponentScript window = (CopyComponentScript)EditorWindow.GetWindow (typeof(CopyComponentScript));
		window.Show ();
	}

	void OnGUI()
	{
		source = (GameObject) EditorGUILayout.ObjectField ("source obj : " , source, typeof(GameObject), true );
		destination = (GameObject) EditorGUILayout.ObjectField ("destination obj : ", destination, typeof(GameObject), true);
		s_comp = EditorGUILayout.TextField ("component name : ", s_comp) ;

		if (GUILayout.Button ("Copy Component")) {
			//get component by string, then add it on destination object
			CopyComponent (source.GetComponent(s_comp), destination);
		}
	}

	Component CopyComponent(Component source, GameObject dest)
	{
		System.Type type = source.GetType ();
		//add componet to destination gameobject
		Component copy = destination.AddComponent (type);
		//gets the fields of the current type
		System.Reflection.FieldInfo[] fields = type.GetFields ();
		foreach (System.Reflection.FieldInfo field in fields) {
			//sets the value of the field supported by the given object
			field.SetValue (copy, field.GetValue(source));
		}
		Debug.Log (type.ToString());
		return copy;
	}
}
