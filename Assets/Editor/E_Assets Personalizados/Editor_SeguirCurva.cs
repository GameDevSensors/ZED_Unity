using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CurvasBezier
{
    [CustomEditor(typeof(SeguirCurva))]
    public class Editor_SeguirCurva : Editor
    {
        SerializedProperty pausa;
        SerializedProperty siguiendoRuta;
        SerializedProperty reproducirEnStart;

        void OnEnable()
        {

            pausa = serializedObject.FindProperty("pausa");
            siguiendoRuta = serializedObject.FindProperty("siguiendoRuta");
            reproducirEnStart = serializedObject.FindProperty("reproducirEnStart");
        }

        public override void OnInspectorGUI()
        {
            SeguirCurva seguirObjeto = (SeguirCurva)target;
            serializedObject.Update();

            base.OnInspectorGUI();
            GUILayout.Space(20f);

            EditorGUILayout.LabelField("Control de Movimiento en Curva", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();

            if (pausa.boolValue || !reproducirEnStart.boolValue)
            {
                if (GUILayout.Button("Reproducir"))
                {
                    seguirObjeto.ReproducirMovimiento();
                }
            }

            if (!pausa.boolValue)
            {
                if (GUILayout.Button("Pausar"))
                {
                    seguirObjeto.PausarMovimiento();
                }
            }
            if (!pausa.boolValue)
            {
                if (siguiendoRuta.boolValue)
                {
                    if (GUILayout.Button("Salir del camino"))
                    {
                        seguirObjeto.SalirCamino();
                    }
                }
            }

            GUILayout.EndHorizontal();
            GUILayout.Space(10f);

            EditorGUILayout.LabelField("Control de Movimiento", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Reiniciar"))
            {
                seguirObjeto.ReiniciarMovimiento();
            }
            GUILayout.EndHorizontal();
            serializedObject.ApplyModifiedProperties();
        }
    }
}