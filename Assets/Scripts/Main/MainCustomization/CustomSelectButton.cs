using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Customization;
using UnityEditor;

public class CustomSelectButton : MonoBehaviour
{
    [Header("Part")]
    public Part part;

    [Header("Current")]
    public Head head;
    [Header("Current")]
    public Body body;
    [Header("Current")] 
    public Leg leg;

    [SerializeField] HeadSelect h_sel;
    [SerializeField] BodySelect b_sel;
    [SerializeField] LegSelect l_sel;

    void Start()
    {

    }

    void Update()
    {

    }

    /// <summary>
    /// 2022.12.28 / LJ
    /// 버튼을 클릭 했을 때 다음 목록을 보여줌
    /// </summary>
    public void OnClick()
    {
        switch (part)
        {
            case Part.Head:
                if ((int)head == System.Enum.GetValues(typeof(Head)).Length - 1) head = Head.선택안함;
                else head += 1;
                h_sel.Change.Invoke();
                break;
            case Part.Body:
                if ((int)body == System.Enum.GetValues(typeof(Body)).Length - 1) body = Body.선택안함;
                else body += 1;
                b_sel.Change.Invoke();
                break;
            case Part.Leg:
                if ((int)leg == System.Enum.GetValues(typeof(Leg)).Length - 1) leg = Leg.선택안함;
                else leg += 1;
                l_sel.Change.Invoke();
                break;
        }
    }
}

/// <summary>
/// 2022.12.28 / LJ
/// 인스펙터 창에서 각 파츠를 선택했을때 그 파츠에 필요한 프로퍼티만 보여줌
/// </summary>

#if UNITY_EDITOR
[CustomEditor(typeof(CustomSelectButton))]
class CustomInspectorEditor : Editor
{
    SerializedProperty part_prop;
    SerializedProperty head_prop;
    SerializedProperty body_prop;
    SerializedProperty leg_prop;
    SerializedProperty h_sel_prop;
    SerializedProperty b_sel_prop;
    SerializedProperty l_sel_prop;

private void Awake()
    {
        part_prop = serializedObject.FindProperty("part");
        head_prop = serializedObject.FindProperty("head");
        body_prop = serializedObject.FindProperty("body");
        leg_prop = serializedObject.FindProperty("leg");
        h_sel_prop = serializedObject.FindProperty("h_sel");
        b_sel_prop = serializedObject.FindProperty("b_sel");
        l_sel_prop = serializedObject.FindProperty("l_sel");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(part_prop);
        if ((Part)part_prop.enumValueIndex == Part.Head)
        {
            EditorGUILayout.PropertyField(head_prop);
            EditorGUILayout.PropertyField(h_sel_prop);
        }
        else if ((Part)part_prop.enumValueIndex == Part.Body)
        {
            EditorGUILayout.PropertyField(body_prop);
            EditorGUILayout.PropertyField(b_sel_prop);
        }
        else if ((Part)part_prop.enumValueIndex == Part.Leg)
        {
            EditorGUILayout.PropertyField(leg_prop);
            EditorGUILayout.PropertyField(l_sel_prop);
        }

        serializedObject.ApplyModifiedProperties();
    }
}

#endif
