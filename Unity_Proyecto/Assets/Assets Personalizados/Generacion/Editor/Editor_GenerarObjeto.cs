using UnityEngine;
using UnityEditor;
using Generacion;

namespace Generacion
{
    [CustomEditor(typeof(GenerarObjeto))]
    public class Editor_GenerarObjeto : Editor
    {
        SerializedProperty objeto;
        SerializedProperty tomarPosicionSpawn;
        SerializedProperty tomarRotacionSpawn;
        SerializedProperty spawn;

        void OnEnable()
        {
            objeto = serializedObject.FindProperty("objeto");

            tomarPosicionSpawn = serializedObject.FindProperty("tomarPosicionSpawn");
            tomarRotacionSpawn = serializedObject.FindProperty("tomarRotacionSpawn");
            spawn = serializedObject.FindProperty("spawn");
        }

        public override void OnInspectorGUI()
        {
            GenerarObjeto generarObjeto = (GenerarObjeto)target;

            serializedObject.Update();

            EditorGUILayout.PropertyField(objeto);
            GUILayout.Space(20f);

            EditorGUILayout.PropertyField(tomarPosicionSpawn);
            EditorGUILayout.PropertyField(tomarRotacionSpawn);

            if (tomarPosicionSpawn.boolValue || tomarRotacionSpawn.boolValue)
            {
                EditorGUILayout.PropertyField(spawn);
            }

            serializedObject.ApplyModifiedProperties();
            GUILayout.Space(10f);

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Generar objeto"))
            {
                generarObjeto.Generar();
            }

            GUILayout.EndHorizontal();
        }
    }
}
