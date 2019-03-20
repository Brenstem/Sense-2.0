using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LeverObject)), CanEditMultipleObjects]
public class LeverObjectEditor : Editor
{


    public SerializedProperty actionProperty;

    private LeverObject leverObject;


    private void OnEnable()
    {
        actionProperty = serializedObject.FindProperty("objectAction");
        leverObject = (LeverObject)target;

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Space();

        if (leverObject.GetComponent<Platform>() == null) {
            actionProperty.enumValueIndex = EditorGUILayout.Popup(actionProperty.enumValueIndex, actionProperty.enumDisplayNames);

            LeverObject.ObjectAction objectAction = (LeverObject.ObjectAction)actionProperty.enumValueIndex;


            switch (objectAction) {
                case LeverObject.ObjectAction.DoNothing:
                    break;
                case LeverObject.ObjectAction.MoveToPosition:
                    leverObject.moveTo = EditorGUILayout.Vector2Field("Move To", leverObject.moveTo);
                    leverObject.moveSpeed = EditorGUILayout.FloatField("Move Speed", leverObject.moveSpeed);
                    break;
                case LeverObject.ObjectAction.MoveByDistance:
                    leverObject.moveBy = EditorGUILayout.Vector2Field("Move By", leverObject.moveBy);
                    leverObject.moveSpeed = EditorGUILayout.FloatField("Move Speed", leverObject.moveSpeed);
                    break;
                case LeverObject.ObjectAction.ActivatePhysics:
                    break;
                default:
                    break;
            }

            serializedObject.ApplyModifiedProperties();
        }
        else {
            EditorGUILayout.LabelField("Platform script attached!");
        }
    }



}
