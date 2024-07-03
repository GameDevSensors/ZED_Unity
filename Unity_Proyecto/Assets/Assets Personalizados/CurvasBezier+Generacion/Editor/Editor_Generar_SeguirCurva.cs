using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Modificaciones
{
    [CustomEditor(typeof(Generar_SeguirCurva))]
    public class Editor_Generar_SeguirCurva : Editor
    {
        SerializedProperty objeto;
        SerializedProperty tomarPosicionSpawn;
        SerializedProperty tomarRotacionSpawn;
        SerializedProperty spawn;
        SerializedProperty listaCaminos;

        SerializedProperty velocidadEntrarCurva;
        SerializedProperty velocidadSalirCurva;
        SerializedProperty reproducirEnStart;
        SerializedProperty bucle;
        SerializedProperty rotacionDinamica;

        void OnEnable()
        {
            objeto = serializedObject.FindProperty("objeto");

            tomarPosicionSpawn = serializedObject.FindProperty("tomarPosicionSpawn");
            tomarRotacionSpawn = serializedObject.FindProperty("tomarRotacionSpawn");
            spawn = serializedObject.FindProperty("spawn");
            listaCaminos = serializedObject.FindProperty("listaCaminos");


            velocidadEntrarCurva = serializedObject.FindProperty("velocidadEntrarCurva");
            velocidadSalirCurva = serializedObject.FindProperty("velocidadSalirCurva");
            reproducirEnStart = serializedObject.FindProperty("reproducirEnStart");
            bucle = serializedObject.FindProperty("bucle");
            rotacionDinamica = serializedObject.FindProperty("rotacionDinamica");
        }

        public override void OnInspectorGUI()
        {
            Generar_SeguirCurva generarObjeto = (Generar_SeguirCurva)target;

            serializedObject.Update();

            EditorGUILayout.PropertyField(objeto);
            GUILayout.Space(10f);

            EditorGUILayout.PropertyField(tomarPosicionSpawn);
            EditorGUILayout.PropertyField(tomarRotacionSpawn);

            if (tomarPosicionSpawn.boolValue || tomarRotacionSpawn.boolValue)
            {
                EditorGUILayout.PropertyField(spawn);
            }

            GUILayout.Space(10f);
            EditorGUILayout.PropertyField(listaCaminos);

            GUILayout.Space(10f);
            EditorGUILayout.PropertyField(velocidadEntrarCurva);
            EditorGUILayout.PropertyField(velocidadSalirCurva);
            EditorGUILayout.PropertyField(reproducirEnStart);
            EditorGUILayout.PropertyField(bucle);
            EditorGUILayout.PropertyField(rotacionDinamica);

            serializedObject.ApplyModifiedProperties();
            GUILayout.Space(20f);

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Generar objeto"))
            {
                generarObjeto.Generar();
            }

            GUILayout.EndHorizontal();
        }
    }
}
