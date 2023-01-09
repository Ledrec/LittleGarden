using UnityEditor;
using UnityEngine;

public class TreeTypesController : MonoBehaviour
{
    [SerializeField]private TreeTypeParent[] _parents;
    
    public void ActivateTreeType(TreeType type)
    {
        foreach (TreeTypeParent cur in _parents)
        {
            cur.Parent.SetActive(cur.TreeType == type);
        }
    }
}

[System.Serializable]
public class TreeTypeParent
{
    public TreeType TreeType;
    public GameObject Parent;
}

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(TreeTypeParent))]
    public class TreeTypeParentDrawer: PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float fieldWidth = position.width - EditorGUIUtility.labelWidth;
            EditorGUI.LabelField(position, label);
            EditorGUI.PropertyField(
                new Rect(
                    position.x + EditorGUIUtility.labelWidth,
                    position.y,
                    fieldWidth * 0.5f - 2.0f,
                    EditorGUIUtility.singleLineHeight),
                property.FindPropertyRelative("TreeType"),
                GUIContent.none);
            EditorGUI.PropertyField(
                new Rect(
                    position.x + EditorGUIUtility.labelWidth + fieldWidth * 0.5f + 2.0f,
                    position.y,
                    fieldWidth * 0.5f - 2.0f,
                    EditorGUIUtility.singleLineHeight),
                property.FindPropertyRelative("Parent"),
                GUIContent.none);
        }
    }
#endif