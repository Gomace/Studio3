#if UNITY_EDITOR
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbilityBase))] // Assuming MyComponent is a MonoBehaviour that has a reference to MyScriptableObject
public class AbilityBaseEditor : Editor
{
    private string[] _fields = new[] { "_name", "_icon", "_description", "_power", "_cooldown", "_resource", "_range" };
    private string[] _scriptObjFields = new [] { "_type", "_targeting", "_abiClass", "_calcNumFrom", "_metric", "_style" };
    private SerializedProperty[] _scriptObjProps = new SerializedProperty[6];

    private void OnEnable()
    {
        for (int i = 0; i < _scriptObjFields.Length; ++i)
            _scriptObjProps[i] = serializedObject.FindProperty(_scriptObjFields[i]);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        DrawFields(0, 3);
        
        DrawScriptObj<Typing>(_scriptObjProps[0], "Type");
        DrawFields(3, _fields.Length);
        
        EditorGUILayout.LabelField("Ability Functionality Details");
        DrawDropDowns();

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawFields(int start, int stop)
    {
        for (int i = start; i < stop; ++i)
            EditorGUILayout.PropertyField(serializedObject.FindProperty(_fields[i]));
    }

    private void DrawDropDowns()
    {
        DrawScriptObj<Targeting>(_scriptObjProps[1], "Targeting Method");
        DrawScriptObj<AbiClass>(_scriptObjProps[2], "Ability Class");
        DrawScriptObj<CalcNumFrom>(_scriptObjProps[3], "Calc Number From");
        DrawScriptObj<CalcMetric>(_scriptObjProps[4], "Metric");
        DrawScriptObj<DmgStyle>(_scriptObjProps[5], "Damage Style");
    }
    
    private void DrawScriptObj<T>(SerializedProperty property, string label) where T : ScriptableObject
    {
        T[] _scriptObjs = GetAllScriptObjs<T>(typeof(T).Name == "Typing" ? "Assets/Resources/Typings" : "Assets/Resources/Abilities/Details/" + typeof(T).Name); // Get all ScriptableObjects of type T in a folder
        
        int i = EditorGUILayout.Popup(label, GetSelectedIndex<T>(property), GetScriptObjNames(_scriptObjs)); // Display a dropdown to select a ScriptableObject
        
        property.objectReferenceValue = _scriptObjs[i]; // Assign the selected ScriptableObject to the property
    }
    
    private int GetSelectedIndex<T>(SerializedProperty property) where T : ScriptableObject
    {
        ScriptableObject _selScriptObj = property.objectReferenceValue as ScriptableObject;

        if (_selScriptObj != null)
        {
            T[] _scriptObjs = GetAllScriptObjs<T>(typeof(T).Name == "Typing" ? "Assets/Resources/Typings" : "Assets/Resources/Abilities/Details/" + typeof(T).Name);

            for (int i = 0; i < _scriptObjs.Length; ++i)
            {
                if (_scriptObjs[i] == _selScriptObj)
                    return i;
            }
        }

        return 0; // Default to "None"
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