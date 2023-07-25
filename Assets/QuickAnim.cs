using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class QuickAnim : MonoBehaviour
{

    public bool minOrMax = false;
    public bool mmSpawnMin = false;
    public Object[] mmBindButtons;
    public bool[] mmBindButtonsInverse;
    public Object[] mmAnotherObj;
    public Object[] mmInvisibleOnMax;
    public Object[] mmInvisibleOnMin;
    public Object[] mmActiveOnMax;
    public Object[] mmActiveOnMin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(QuickAnim))]
public class CustomGUIEditor : Editor
{
    QuickAnim _target;
    //public Object source;

    private void OnEnable()
    {
        _target = target as QuickAnim;
        
    }

    private EditorGUILayoutArrays.ArrayFieldSettings aSettingsB = new EditorGUILayoutArrays.ArrayFieldSettings("    Inverse (go this to Min)");
    private EditorGUILayoutArrays.ArrayFieldSettings aSettingsGO = new EditorGUILayoutArrays.ArrayFieldSettings("    Buttons go this to Max");
    private EditorGUILayoutArrays.ArrayFieldSettings aSettingsGOAO = new EditorGUILayoutArrays.ArrayFieldSettings("    Another GameObject's going to Min/Max");
    private EditorGUILayoutArrays.ArrayFieldSettings aSettingsGOIGOOMax = new EditorGUILayoutArrays.ArrayFieldSettings("    GameObject's invisible on Max");
    private EditorGUILayoutArrays.ArrayFieldSettings aSettingsGOIGOOMin = new EditorGUILayoutArrays.ArrayFieldSettings("    GameObject's invisible on Min");
    private EditorGUILayoutArrays.ArrayFieldSettings aSettingsGOAGOOMax = new EditorGUILayoutArrays.ArrayFieldSettings("    GameObject's active on Max");
    private EditorGUILayoutArrays.ArrayFieldSettings aSettingsGOAGOOMin = new EditorGUILayoutArrays.ArrayFieldSettings("    GameObject's active on Min");

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
		EditorGUILayout.PropertyField(serializedObject.FindProperty("integers"), true);
        _target.minOrMax = EditorGUILayout.Toggle("Use min or max", _target.minOrMax);
        if (_target.minOrMax)
        {
            _target.mmSpawnMin = EditorGUILayout.Toggle("   Spawn in Min", _target.mmSpawnMin);
            //Rect r = EditorGUILayout.BeginHorizontal("button");
            _target.mmBindButtons = EditorGUILayoutArrays.GameObjectArrayField(aSettingsGO, _target.mmBindButtons, "        Size", "        GameObject");
            //source = EditorGUILayout.ObjectField(source, typeof(GameObject), true);
            _target.mmBindButtonsInverse = EditorGUILayoutArrays.BooleanArrayField(aSettingsB, _target.mmBindButtonsInverse, "        Size", "        GameObject");
            //GUILayout.Label("So am I");
            //EditorGUILayout.EndHorizontal();
            _target.mmAnotherObj = EditorGUILayoutArrays.GameObjectArrayField(aSettingsGOAO, _target.mmAnotherObj, "        Size", "        GameObject");
            _target.mmInvisibleOnMax = EditorGUILayoutArrays.GameObjectArrayField(aSettingsGOIGOOMax, _target.mmInvisibleOnMax, "        Size", "        GameObject");
            _target.mmInvisibleOnMin = EditorGUILayoutArrays.GameObjectArrayField(aSettingsGOIGOOMin, _target.mmInvisibleOnMin, "        Size", "        GameObject");
            _target.mmActiveOnMax = EditorGUILayoutArrays.GameObjectArrayField(aSettingsGOAGOOMax, _target.mmActiveOnMax, "        Size", "        GameObject");
            _target.mmActiveOnMin = EditorGUILayoutArrays.GameObjectArrayField(aSettingsGOAGOOMin, _target.mmActiveOnMin, "        Size", "        GameObject");
            }
        else
        {
            //_target.SecondString = EditorGUILayout.TextField("Второе поле: ", _target.SecondString);
        }
    }
}
#endif
