using UnityEngine;
using UnityEditor;

public static class EditorGUILayoutArrays {

    public class ArrayFieldSettings {

        public string label;
        public bool open;

        public ArrayFieldSettings(string label = "Array", bool open = true) {
            this.label = label;
            this.open = open;
        }
    }

    public static Object[] GameObjectArrayField(ArrayFieldSettings settings, Object[] array, string sizeName, string valueName)
    {
        return GameObjectArrayField(settings.label, ref settings.open, array, sizeName, valueName);
    }

    public static Object[] GameObjectArrayField(string label, ref bool open, Object[] array, string sizeName, string valueName)
    {
        open = EditorGUILayout.Foldout(open, label);
        int newSize = array.Length;

        if (open)
        {
            newSize = EditorGUILayout.IntField(sizeName, newSize);
            newSize = newSize < 0 ? 0 : newSize;

            if (newSize != array.Length)
            {
                array = ResizeArray<Object>(array, newSize);
            }

            for (var i = 0; i < newSize; i++)
            {
                array[i] = EditorGUILayout.ObjectField(valueName + " " + i, array[i], typeof(GameObject));
            }
        }
        return array;
    }

    public static string[] StringArrayField(ArrayFieldSettings settings, string[] array, string sizeName, string valueName) {
        return StringArrayField(settings.label, ref settings.open, array, sizeName, valueName);
    }

    public static string[] StringArrayField(string label, ref bool open, string[] array, string sizeName, string valueName) {
        open = EditorGUILayout.Foldout(open, label);
        int newSize = array.Length;

        if (open) {
            newSize = EditorGUILayout.IntField(sizeName, newSize);
            newSize = newSize < 0 ? 0 : newSize;

            if (newSize != array.Length) {
                array = ResizeArray<string>(array, newSize);
            }

            for (var i = 0; i < newSize; i++) {
                array[i] = EditorGUILayout.TextField(valueName + " " + i, array[i]);
            }
        }
        return array;
    }

    public static int[] IntArrayField(ArrayFieldSettings settings, int[] array, string sizeName, string valueName) {
        return IntArrayField(settings.label, ref settings.open, array, sizeName, valueName);
    }

    public static int[] IntArrayField(string label, ref bool open, int[] array, string sizeName, string valueName) {
        open = EditorGUILayout.Foldout(open, label);
        int newSize = array.Length;

        if (open) {
            newSize = EditorGUILayout.IntField(sizeName, newSize);
            newSize = newSize < 0 ? 0 : newSize;

            if (newSize != array.Length) {
                array = ResizeArray<int>(array, newSize);
            }

            for (var i = 0; i < newSize; i++) {
                array[i] = EditorGUILayout.IntField(valueName + " " + i, array[i]);
            }
        }
        return array;
    }

    public static float[] FloatArrayField(ArrayFieldSettings settings, float[] array, string sizeName, string valueName) {
        return FloatArrayField(settings.label, ref settings.open, array, sizeName, valueName);
    }

    public static float[] FloatArrayField(string label, ref bool open, float[] array, string sizeName, string valueName) {
        open = EditorGUILayout.Foldout(open, label);
        int newSize = array.Length;

        if (open) {
            newSize = EditorGUILayout.IntField(sizeName, newSize);
            newSize = newSize < 0 ? 0 : newSize;

            if (newSize != array.Length) {
                array = ResizeArray<float>(array, newSize);
            }

            for (var i = 0; i < newSize; i++) {
                array[i] = EditorGUILayout.FloatField(valueName + " " + i, array[i]);
            }
        }
        return array;
    }

    public static double[] DoubleArrayField(ArrayFieldSettings settings, double[] array, string sizeName, string valueName) {
        return DoubleArrayField(settings.label, ref settings.open, array, sizeName, valueName);
    }

    public static double[] DoubleArrayField(string label, ref bool open, double[] array, string sizeName, string valueName) {
        open = EditorGUILayout.Foldout(open, label);
        int newSize = array.Length;

        if (open) {
            newSize = EditorGUILayout.IntField(sizeName, newSize);
            newSize = newSize < 0 ? 0 : newSize;

            if (newSize != array.Length) {
                array = ResizeArray<double>(array, newSize);
            }

            for (var i = 0; i < newSize; i++) {
                array[i] = EditorGUILayout.DoubleField(valueName + " " + i, array[i]);
            }
        }
        return array;
    }

    public static Vector2[] Vector2ArrayField(ArrayFieldSettings settings, Vector2[] array, string sizeName, string valueName) {
        return Vector2ArrayField(settings.label, ref settings.open, array, sizeName, valueName);
    }

    public static Vector2[] Vector2ArrayField(string label, ref bool open, Vector2[] array, string sizeName, string valueName) {
        open = EditorGUILayout.Foldout(open, label);
        int newSize = array.Length;

        if (open) {
            newSize = EditorGUILayout.IntField(sizeName, newSize);
            newSize = newSize < 0 ? 0 : newSize;

            if (newSize != array.Length) {
                array = ResizeArray<Vector2>(array, newSize);
            }

            for (var i = 0; i < newSize; i++) {
                array[i] = EditorGUILayout.Vector2Field(valueName + " " + i, array[i]);
            }
        }
        return array;
    }

    public static Vector3[] Vector3ArrayField(ArrayFieldSettings settings, Vector3[] array, string sizeName, string valueName) {
        return Vector3ArrayField(settings.label, ref settings.open, array, sizeName, valueName);
    }

    public static Vector3[] Vector3ArrayField(string label, ref bool open, Vector3[] array, string sizeName, string valueName) {
        open = EditorGUILayout.Foldout(open, label);
        int newSize = array.Length;

        if (open) {
            newSize = EditorGUILayout.IntField(sizeName, newSize);
            newSize = newSize < 0 ? 0 : newSize;

            if (newSize != array.Length) {
                array = ResizeArray<Vector3>(array, newSize);
            }

            for (var i = 0; i < newSize; i++) {
                array[i] = EditorGUILayout.Vector3Field(valueName + " " + i, array[i]);
            }
        }
        return array;
    }

    public static Vector4[] Vector4ArrayField(ArrayFieldSettings settings, Vector4[] array, string sizeName, string valueName) {
        return Vector4ArrayField(settings.label, ref settings.open, array, sizeName, valueName);
    }

    public static Vector4[] Vector4ArrayField(string label, ref bool open, Vector4[] array, string sizeName, string valueName) {
        open = EditorGUILayout.Foldout(open, label);
        int newSize = array.Length;

        if (open) {
            newSize = EditorGUILayout.IntField(sizeName, newSize);
            newSize = newSize < 0 ? 0 : newSize;

            if (newSize != array.Length) {
                array = ResizeArray<Vector4>(array, newSize);
            }

            for (var i = 0; i < newSize; i++) {
                array[i] = EditorGUILayout.Vector4Field(valueName + " " + i, array[i]);
            }
        }
        return array;
    }

    public static bool[] BooleanArrayField(ArrayFieldSettings settings, bool[] array, string sizeName, string valueName) {
        return BooleanArrayField(settings.label, ref settings.open, array, sizeName, valueName);
    }

    public static bool[] BooleanArrayField(string label, ref bool open, bool[] array, string sizeName, string valueName) {
        open = EditorGUILayout.Foldout(open, label);
        int newSize = array.Length;

        if (open) {
            newSize = EditorGUILayout.IntField(sizeName, newSize);
            newSize = newSize < 0 ? 0 : newSize;

            if (newSize != array.Length) {
                array = ResizeArray<bool>(array, newSize);
            }

            for (var i = 0; i < newSize; i++) {
                array[i] = EditorGUILayout.Toggle(valueName + " " + i, array[i]);
            }
        }
        return array;
    }

    public static Color[] ColorArrayField(ArrayFieldSettings settings, Color[] array, string sizeName, string valueName) {
        return ColorArrayField(settings.label, ref settings.open, array, sizeName, valueName);
    }

    public static Color[] ColorArrayField(string label, ref bool open, Color[] array, string sizeName, string valueName) {
        open = EditorGUILayout.Foldout(open, label);
        int newSize = array.Length;

        if (open) {
            newSize = EditorGUILayout.IntField(sizeName, newSize);
            newSize = newSize < 0 ? 0 : newSize;

            if (newSize != array.Length) {
                array = ResizeArray<Color>(array, newSize);
            }

            for (var i = 0; i < newSize; i++) {
                array[i] = EditorGUILayout.ColorField(valueName + " " + i, array[i]);
            }
        }
        return array;
    }

    private static T[] ResizeArray<T>(T[] array, int size) {
        T[] newArray = new T[size];

        for (var i = 0; i < size; i++) {
            if (i < array.Length) {
                newArray[i] = array[i];
            }
        }

        return newArray;
    }

    //public static GameObject GameObjectField(int value, params GUILayoutOption[] options)
    //{
    //    return GameObjectField(value, EditorStyles.objectField, options);
    //}
//
    //public static GameObject GameObjectField(int value, GUIStyle style, params GUILayoutOption[] options)
    //{
    //    Rect r = s_LastRect = GetControlRect(false, EditorGUI.kSingleLineHeight, style, options);
    //    return EditorGUI.IntField(r, value, style);
    //}
//
    //public static GameObject GameObjectField(string label, int value, params GUILayoutOption[] options)
    //{
    //    return IntField(label, value, EditorStyles.numberField, options);
    //}
//
    //public static GameObject GameObjectField(string label, int value, GUIStyle style, params GUILayoutOption[] options)
    //{
    //    Rect r = s_LastRect = GetControlRect(true, EditorGUI.kSingleLineHeight, style, options);
    //    return GameObjectField(r, label, value, style);
    //}
//
    //public static GameObject GameObjectField(GUIContent label, int value, params GUILayoutOption[] options)
    //{
     //   return IntField(label, value, EditorStyles.objectField, options);
    //}
}
