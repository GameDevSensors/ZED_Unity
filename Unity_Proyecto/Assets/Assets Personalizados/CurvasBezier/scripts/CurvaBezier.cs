/*
    Autor: Giovani Ramirez
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CurvasBezier
{
    /// <summary>
    /// Clase que permite crear curvas de bezier con nodos rotos para mover objetos siguiento una ruta.
    /// </summary>
    public class CurvaBezier : MonoBehaviour
    {
        /// <summary>
        /// El tipo de curva, puede ser Lineal, Cuadratica o Cubica
        /// </summary>
        public enum TipoCurva
        {
            Lineal,
            Cuadratica,
            Cubica
        }

        [Header("Informacion")]
        /// <summary>
        /// Referencia de los nodos que modifican la curva de bezier
        /// </summary>
        public List<Transform> bezierNodos;

        [Header("Visualizacion")]
        /// <summary>
        /// Color con el que se vera la curva
        /// </summary>
        [Tooltip("Color de la curva de Bezier")]
        public Color colorCurva = Color.white;
        [Tooltip("Color de las distancia de los extremos con los nodos interiores")]
        public Color colorNodos = Color.gray;

        [Header("Suavizado")]
        /// <summary>
        /// El suavizado de la curva; entre mas chico sea este valor mas suave se vera la curva.
        /// </summary>
        [SerializeField, Range(0.05f, 0.1f)]
        float calidadVistaCurva = 0.05f;

        /// <summary>
        /// Tipo de curva que se va a dibujar
        /// </summary>
        TipoCurva tipoCurva;


        #region Dibujar Curvas
        private void OnDrawGizmos()
        {
            AsignarTipoCurva();
            DibujarCurva();
        }

        /// <summary>
        /// Dibuja una curva de bezier dependiendo de que tipo de curva se quiera
        /// </summary>
        private void DibujarCurva()
        {

            switch (tipoCurva)
            {
                case TipoCurva.Lineal:
                    DibujarCurvaLineal();
                    break;
                case TipoCurva.Cuadratica:
                    DibujarCurvaCuadrada();
                    break;
                case TipoCurva.Cubica:
                    DibujarCurvaCubica();
                    break;
            }
        }
        /// <summary>
        /// Se asigna un tipo de curva dependiendo de la longitud de nodos que se hay.
        /// </summary>
        private void AsignarTipoCurva()
        {
            switch (bezierNodos.Count)
            {
                case 2:
                    tipoCurva = TipoCurva.Lineal;
                    break;
                case 3:
                    tipoCurva = TipoCurva.Cuadratica;
                    break;
                case 4:
                    tipoCurva = TipoCurva.Cubica;
                    break;
                default:
                    Debug.LogError("Solo se pueden crear curvas lineales, cuadraticas o cubicas");
                    break;
            }
        }
        /// <summary>
        /// Dibuja una curva linial de bezier
        /// </summary>
        private void DibujarCurvaLineal()
        {
            if (bezierNodos.Count != 2)
            {
                Debug.LogError("Es necesario tener 2 nodos para curvas lineales");
                return;
            }

            Gizmos.color = colorCurva;

            for (float t = 0; t <= 1; t += calidadVistaCurva)
            {
                Vector3 inicio = Curva(bezierNodos[0].position, bezierNodos[1].position, t);
                Vector3 final = Curva(bezierNodos[0].position, bezierNodos[1].position, t + calidadVistaCurva);
                Gizmos.DrawLine(inicio, final);
            }
        }

        /// <summary>
        /// Dibuja una curva cuadratica de bezier
        /// </summary>
        private void DibujarCurvaCuadrada()
        {
            if (bezierNodos.Count != 3)
            {
                Debug.LogError("Es necesario tener 3 nodos para curvas cuadraticas");
                return;
            }

            Gizmos.color = colorCurva;

            for (float t = 0; t <= 1; t += calidadVistaCurva)
            {
                Vector3 inicio = Curva(bezierNodos[0].position, bezierNodos[1].position, bezierNodos[2].position, t);
                Vector3 final = Curva(bezierNodos[0].position, bezierNodos[1].position, bezierNodos[2].position, t + calidadVistaCurva);
                Gizmos.DrawLine(inicio, final);
            }

            Gizmos.color = colorCurva;
            Gizmos.DrawSphere(bezierNodos[1].position, 0.02f);
            Gizmos.color = colorNodos;
            Gizmos.DrawLine(bezierNodos[0].position, bezierNodos[1].position);
            Gizmos.DrawLine(bezierNodos[1].position, bezierNodos[2].position);
        }

        /// <summary>
        /// Dibuja una curva cubica de bezier
        /// </summary>
        private void DibujarCurvaCubica()
        {
            if (bezierNodos.Count != 4)
            {
                Debug.LogError("Es necesario tener 4 nodos para curvas cubicas");
                return;
            }

            Gizmos.color = colorCurva;

            for (float t = 0; t <= 1; t += calidadVistaCurva)
            {
                Vector3 inicio = Curva(bezierNodos[0].position, bezierNodos[1].position, bezierNodos[2].position, bezierNodos[3].position, t);
                Vector3 final = Curva(bezierNodos[0].position, bezierNodos[1].position, bezierNodos[2].position, bezierNodos[3].position, t + calidadVistaCurva);
                Gizmos.DrawLine(inicio, final);
            }

            Gizmos.color = colorCurva;
            Gizmos.DrawSphere(bezierNodos[1].position, 0.02f);
            Gizmos.DrawSphere(bezierNodos[2].position, 0.02f);
            Gizmos.color = colorNodos;
            Gizmos.DrawLine(bezierNodos[0].position, bezierNodos[1].position);
            Gizmos.DrawLine(bezierNodos[2].position, bezierNodos[3].position);

        }
        #endregion

        #region Formula Curvas
        /// <summary>
        /// Formula de la curva parametrizada de grado 3
        /// </summary>
        /// <param name="P0">Punto donde comienza la curva</param>
        /// <param name="P1">Punto donde se dirige la curva desde P0</param>
        /// <param name="P2">Punto donde se dirige la curva desde P1</param>
        /// <param name="P3">Punto donde termina la curva</param>
        /// <param name="t">Variable de la curva</param>
        /// <returns>La posicion en el tiempo de t de la curva</returns>
        public Vector3 Curva(Vector3 P0, Vector3 P1, Vector3 P2, Vector3 P3, float t)
        {
            return (Mathf.Pow(1 - t, 3) * P0)
                    + (3 * t * Mathf.Pow(1 - t, 2) * P1)
                    + (3 * Mathf.Pow(t, 2) * (1 - t) * P2)
                    + (Mathf.Pow(t, 3) * P3);
        }

        /// <summary>
        /// Formula de la curva parametrizada de grado 2
        /// </summary>
        /// <param name="P0">Punto donde comienza la curva</param>
        /// <param name="P1">Punto donde se dirige la curva desde P0</param>
        /// <param name="P2">Punto donde termina la curva</param>
        /// <param name="t">Variable de la curva</param>
        /// <returns>La posicion en el tiempo t de la curva</returns>
        public Vector3 Curva(Vector3 P0, Vector3 P1, Vector3 P2, float t)
        {
            return (Mathf.Pow(1 - t, 2) * P0)
                    + (2 * t * (1 - t) * P1)
                    + (Mathf.Pow(t, 2) * P2);
        }

        /// <summary>
        /// Formula de la curva parametrizada de grado 1
        /// </summary>
        /// <param name="P0">Punto donde se inicia la curva</param>
        /// <param name="P1">Punto donde se termina la curva</param>
        /// <param name="t">Variable de la curva</param>
        /// <returns>La posicion en el tiempo t de la curva</returns>
        public Vector3 Curva(Vector3 P0, Vector3 P1, float t)
        {
            return ((1 - t) * P0) + (t * P1);
        }
        #endregion

        #region Derivada Curva
        /// <summary>
        /// Derivada de la curva parametrizada de grado 3
        /// </summary>
        /// <param name="P0">Punto donde comienza la curva</param>
        /// <param name="P1">Punto donde se dirige la curva desde P0</param>
        /// <param name="P2">Punto donde se dirige la curva desde P1</param>
        /// <param name="P3">Punto donde termina la curva</param>
        /// <param name="t">Variable de la curva</param>
        /// <returns>La derivada</returns>
        public Vector3 DerivarCurva(Vector3 P0, Vector3 P1, Vector3 P2, Vector3 P3, float t)
        {
            return (-3 * Mathf.Pow(1 - t, 2) * P0) +
                    (3 * (t - 1) * (3 * t - 1) * P1) -
                    (3 * t * (3 * t - 2) * P2) +
                    (3 * Mathf.Pow(t, 2) * P3);
        }
        /// <summary>
        /// La derivada de la curva parametrizada de grado 2
        /// </summary>
        /// <param name="P0">Punto donde comienza la curva</param>
        /// <param name="P1">Punto donde se dirige la curva desde P0</param>
        /// <param name="P2">Punto donde termina la curva</param>
        /// <param name="t">Variable de la curva</param>
        /// <returns>La derivada</returns>
        public Vector3 DerivarCurva(Vector3 P0, Vector3 P1, Vector3 P2, float t)
        {
            return (2 * (t - 1) * P0)
                    + (2 * (1 - 2 * t) * P1)
                    + (2 * t * P2);
        }
        /// <summary>
        /// La derivada de la curva parametrizada de grado 1
        /// </summary>
        /// <param name="P0">Punto donde se inicia la curva</param>
        /// <param name="P1">Punto donde se termina la curva</param>
        /// <param name="t">Variable de la curva</param>
        /// <returns>La derivada</returns>
        public Vector3 DerivarCurva(Vector3 P0, Vector3 P1, float t)
        {
            return P1 - P0;
        }
        #endregion
    }
}
