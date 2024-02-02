#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CreatureBase))] // Assuming MyComponent is a MonoBehaviour that has a reference to MyScriptableObject
public class CreatureBaseEditor : Editor
{
    private string[] _fields = new[] { "_name", "_description", "_icon", "_card", "_splash", "_model", "_maxHealth", "_physical", "_magical", "_defense", "_resistance", "_speed", "_learnableAbilities" };
    private string[] _scriptObjFields = new [] { "_type1", "_type2", "_role" };
    private SerializedProperty[] _scriptObjProps = new SerializedProperty[3];

    private void OnEnable()
    {
        for (int i = 0; i < _scriptObjFields.Length; ++i)
            _scriptObjProps[i] = serializedObject.FindProperty(_scriptObjFields[i]);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        DrawFields(0, 7);
        DrawDropDowns();
        
        EditorGUILayout.LabelField("Base Stats");
        DrawFields(7, _fields.Length);

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawFields(int start, int stop)
    {
        for (int i = start; i < stop; ++i)
            EditorGUILayout.PropertyField(serializedObject.FindProperty(_fields[i]));
    }

    private void DrawDropDowns()
    {
        EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 70;
            DrawScriptObj<Typing>(_scriptObjProps[0], "Types", GUILayout.MinWidth(120f));
            DrawScriptObj<Typing>(_scriptObjProps[1], "", GUILayout.MinWidth(10f), GUILayout.MaxWidth(90f));
        EditorGUILayout.EndHorizontal();
        DrawScriptObj<Role>(_scriptObjProps[2], "Role");
        EditorGUIUtility.labelWidth = 120;
    }
    
    private void DrawScriptObj<T>(SerializedProperty property, string label, params GUILayoutOption[] options) where T : ScriptableObject
    {
        T[] _scriptObjs = GetAllScriptObjs<T>("Assets/Resources/" + typeof(T).Name + "s"); // Get all ScriptableObjects of type T in a folder
        
        int i = EditorGUILayout.Popup(label, GetSelectedIndex<T>(property), GetScriptObjNames(_scriptObjs), options); // Display a dropdown to select a ScriptableObject
        
        property.objectReferenceValue = _scriptObjs[i]; // Assign the selected ScriptableObject to the property
    }
    
    private int GetSelectedIndex<T>(SerializedProperty property) where T : ScriptableObject
    {
        ScriptableObject _selScriptObj = property.objectReferenceValue as ScriptableObject;

        if (_selScriptObj != null)
        {
            T[] _scriptObjs = GetAllScriptObjs<T>("Assets/Resources/" + typeof(T).Name + "s");

            for (int i = 0; i < _scriptObjs.Length; ++i)
            {
                if (_scriptObjs[i] == _selScriptObj)
                    return i;
            }
        }

        return 0; // Default to the first item
    }

    private string[] GetScriptObjNames<T>(T[] scriptObjs) where T : ScriptableObject
    {
        string[] _names = new string[scriptObjs.Length + (typeof(T).Name == "Typing" ? 1 : 0)];

        _names[0] = "None";
        
        for (int i = 0; i < scriptObjs.Length; ++i)
            _names[i+(typeof(T).Name == "Typing" ? 1 : 0)] = scriptObjs[i].name;

        return _names;
    }

    private T[] GetAllScriptObjs<T>(string path) where T : ScriptableObject
    {
        string[] _guids = AssetDatabase.FindAssets("t:" + typeof(T).Name, new[] { path });

        T[] _scriptObjs = new T[_guids.Length];

        for (int i = 0; i < _guids.Length; ++i)
            _scriptObjs[i] = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(_guids[i]));

        return _scriptObjs;
    }
}
#endif