using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class QuickAnim : MonoBehaviour
{

    //mm - Min, Max. 
    //Min - on switch to thumbnail view.
    //Max - on switch to full view. 
    public bool mmIsActive = false;
    public bool mmUseMax = true;
    public bool mmUseMin = true;
    public bool mmSpawnMin = false;
    public Object[] mmBindButtons;
    public bool[] mmBindButtonsInverse;
    public Object[] mmAnotherObj;
    public Object[] mmInvisibleOnMax;
    public Object[] mmInvisibleOnMin;
    public Object[] mmActiveOnMax;
    public Object[] mmActiveOnMin;
    public string mmAVNToMax;
    public string mmAVNToMin;

    //je - Join, Exit. 
    //Join - on open scene.
    //Exit - on exit scene.
    public bool jeIsActive;
    public bool jeUseJoin = true;
    public bool jeUseExit = true;

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

    private EditorGUILayoutArrays.ArrayFieldSettings AFS_mmBindButtons = new EditorGUILayoutArrays.ArrayFieldSettings("    Buttons go this to Max");
    private EditorGUILayoutArrays.ArrayFieldSettings AFS_mmBindButtonsInverse = new EditorGUILayoutArrays.ArrayFieldSettings("    Inverse (go this to Min)");
    private EditorGUILayoutArrays.ArrayFieldSettings AFS_mmAnotherObj = new EditorGUILayoutArrays.ArrayFieldSettings("    Another GameObject's going to Min/Max");
    private EditorGUILayoutArrays.ArrayFieldSettings AFS_mmInvisibleOnMax = new EditorGUILayoutArrays.ArrayFieldSettings("    GameObject's invisible on Max");
    private EditorGUILayoutArrays.ArrayFieldSettings AFS_mmInvisibleOnMin = new EditorGUILayoutArrays.ArrayFieldSettings("    GameObject's invisible on Min");
    private EditorGUILayoutArrays.ArrayFieldSettings AFS_mmActiveOnMax = new EditorGUILayoutArrays.ArrayFieldSettings("    GameObject's active on Max");
    private EditorGUILayoutArrays.ArrayFieldSettings AFS_mmActiveOnMin = new EditorGUILayoutArrays.ArrayFieldSettings("    GameObject's active on Min");

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
		EditorGUILayout.PropertyField(serializedObject.FindProperty("integers"), true);
        _target.mmIsActive = EditorGUILayout.ToggleLeft("Use Min and Max anim's", _target.mmIsActive);
        if (_target.mmIsActive)
        {
            _target.mmUseMax = EditorGUILayout.Toggle("   Use Max animation", _target.mmUseMax);
            _target.mmUseMin = EditorGUILayout.Toggle("   Use Min animation", _target.mmUseMin);
            _target.mmSpawnMin = EditorGUILayout.Toggle("   Spawn in Min", _target.mmSpawnMin);
            _target.mmBindButtons = EditorGUILayoutArrays.GameObjectArrayField(AFS_mmBindButtons, _target.mmBindButtons,"         Size", "        GameObject");
            _target.mmBindButtonsInverse = EditorGUILayoutArrays.BooleanArrayField(AFS_mmBindButtonsInverse, _target.mmBindButtonsInverse, "        Size", "        GameObject");
            _target.mmAnotherObj = EditorGUILayoutArrays.GameObjectArrayField(AFS_mmAnotherObj, _target.mmAnotherObj, "        Size", "        GameObject");
            _target.mmInvisibleOnMax = EditorGUILayoutArrays.GameObjectArrayField(AFS_mmInvisibleOnMax, _target.mmInvisibleOnMax, "        Size", "        GameObject");
            _target.mmInvisibleOnMin = EditorGUILayoutArrays.GameObjectArrayField(AFS_mmInvisibleOnMin, _target.mmInvisibleOnMin, "        Size", "        GameObject");
            _target.mmActiveOnMax = EditorGUILayoutArrays.GameObjectArrayField(AFS_mmActiveOnMax, _target.mmActiveOnMax, "        Size", "        GameObject");
            _target.mmActiveOnMin = EditorGUILayoutArrays.GameObjectArrayField(AFS_mmActiveOnMin, _target.mmActiveOnMin, "        Size", "        GameObject");
            _target.mmAVNToMax = EditorGUILayout.TextField("Anim Value Name, To Max: ", _target.mmAVNToMax);
            _target.mmAVNToMin = EditorGUILayout.TextField("Anim Value Name, To Max: ", _target.mmAVNToMin);
            }
        _target.jeIsActive = EditorGUILayout. ToggleLeft("Use Join and Exit anim's", _target.jeIsActive);
        if(_target.jeIsActive)
        {
            _target.jeUseJoin = EditorGUILayout.Toggle("   Use Join animation", _target.jeUseJoin);
            _target.jeUseExit = EditorGUILayout.Toggle("   Use Exit animation", _target.jeUseExit);
        }
    }
}
#endif
