using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generacion
{

    public class GenerarObjeto : MonoBehaviour, IGenerar
    {
        /// <summary>
        /// El objeto que queremos generar
        /// </summary>
        [Header("Objeto a crear"), SerializeField]
        private GameObject objeto;

        /// <summary>
        /// Referencia al objeto creado en el metodo Generar() para poder modificarlo en clases derivadas
        /// </summary>
        protected GameObject objetoCreado;

        #region Spawn

        [Header("Spawn")]

        /// <summary>
        /// Si queremos que aparezca en el spawn
        /// </summary>
        [SerializeField, Tooltip("Si queremos asignarle la posicion donde esta el spawn al objeto")]
        private bool tomarPosicionSpawn = false;

        /// <summary>
        /// Si queremos que tome la rotacion del spawn
        /// </summary>
        [SerializeField, Tooltip("Si queremos asignarle la rotacion del spawn al objeto")]
        private bool tomarRotacionSpawn = false;

        /// <summary>
        /// El lugar donde queremos crear el objeto
        /// </summary>
        [Space(), SerializeField, Tooltip("El objeto donde queremos colocar o asignar rotacion del objeto")]
        private Transform spawn;

        #endregion

        void OnValidate()
        {
            if (objeto == null) Debug.LogWarning("Asigna el gameObject que quieres crear");
            if ((tomarPosicionSpawn == true || tomarRotacionSpawn == true) && spawn == null) Debug.LogWarning("Asigna el spawn");
        }
        public virtual void Generar()
        {
            objetoCreado = Instantiate(objeto);
            //  Asignar posicion y rotacion dependiendo de las opciones que se pongan
            objetoCreado.transform.SetPositionAndRotation(tomarPosicionSpawn ? spawn.position : Vector3.zero, tomarRotacionSpawn ? spawn.rotation : Quaternion.identity);
        }
    }

}