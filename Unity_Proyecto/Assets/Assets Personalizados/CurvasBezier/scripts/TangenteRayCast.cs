using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CurvasBezier
{
    [RequireComponent(typeof(SeguirCurva))]
    public class TangenteRayCast : MonoBehaviour
    {
        [Header("Gizmos")]
        /// <summary>
        /// Color que queremos asignarle la tangente de la curva para el objeto
        /// </summary>
        public Color colorTangente;
        [Header("Configuracion deteccion colisiones")]
        /// <summary>
        /// El rango de deteccion
        /// </summary>
        public float distanciaDeteccion = 1f;
        /// <summary>
        /// Las mascaras que se detectaran 
        /// </summary>
        public LayerMask mascaraDeteccion;
        /// <summary>
        /// Script SeguirCurva para saber la tangente
        /// </summary>
        SeguirCurva seguirCurva;
        /// <summary>
        /// El evento se llamara una unica vez o se llamara cada FixedUpdate
        /// </summary>
        public bool llamarUnaVez;
        /// <summary>
        /// Acciones que ocurriran al detectarse una colision
        /// </summary>
        public UnityEvent accionDetectar;
        bool llamada = true;
        /// <summary>
        /// Es la linea tangente que usaremos para detectar colisiones
        /// </summary>
        Ray rayoTangente;

        void OnValidate()
        {
            if (!TryGetComponent(out seguirCurva)) Debug.LogError("Es necesario que el gameObject tenga el script SeguirCurva");
            if (!seguirCurva.rotacionDinamica) Debug.LogError("Es necesario que el script SeguirCurva tenga la opcion rotacionDinamica activa");
        }
        void Start()
        {
            if (!TryGetComponent(out seguirCurva)) Debug.LogError("Es necesario que el gameObject tenga el script SeguirCurva");
            if (!seguirCurva.rotacionDinamica)
            {
                Debug.LogWarning("Es necesario que el script SeguirCurva tenga la opcion rotacionDinamica activa");
                seguirCurva.rotacionDinamica = true;
            }
            llamada = true;
            rayoTangente = new Ray(transform.position, transform.forward);
        }
        void FixedUpdate()
        {
            rayoTangente = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(rayoTangente, distanciaDeteccion, mascaraDeteccion))
            {
                if (!llamarUnaVez)
                {
                    accionDetectar?.Invoke();
                }
                else if (llamada)
                {
                    llamada = false;
                    accionDetectar?.Invoke();
                }
                else
                {
                    return;
                }
            }
        }
        void OnDrawGizmos()
        {
            Gizmos.color = colorTangente;
            Gizmos.DrawLine(rayoTangente.origin, rayoTangente.origin + rayoTangente.direction * distanciaDeteccion);
        }
    }
}