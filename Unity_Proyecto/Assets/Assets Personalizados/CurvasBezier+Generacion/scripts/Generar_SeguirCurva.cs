using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CurvasBezier;
using Generacion;

namespace Modificaciones
{
    [RequireComponent(typeof(ListaCaminos))]

    public class Generar_SeguirCurva : GenerarObjeto
    {
        /// <summary>
        /// El objeto que deberia de tener el componente SeguirCurva
        /// </summary>
        SeguirCurva objetoSeguirCurva;
        [Header("Seguir camino")]
        /// <summary>
        /// El componente ListaCamnios que debe de tener el Generador
        /// </summary>
        public ListaCaminos listaCaminos;
        [Header("Configuraciones")]
        [Range(0.01f, 20f)]
        public float velocidadEntrarCurva = 0.3f;
        [Range(0.01f, 20f)]
        public float velocidadSalirCurva = 1f;
        public bool reproducirEnStart;
        public bool bucle;
        public bool rotacionDinamica;

        void OnValidate()
        {
            if (!TryGetComponent(out listaCaminos)) Debug.LogError("El Gameobject no tiene el componente ListaCaminos");
        }
        void Start()
        {
            if (!TryGetComponent(out listaCaminos))
            {
                Debug.LogError("El Gameobject no tiene el componente ListaCaminos");
                Debug.Break();
            }
        }
        public override void Generar()
        {
            base.Generar();
            //  Asignacion de parametros de SeguirCurva
            if (!objetoCreado.TryGetComponent(out objetoSeguirCurva))
            {
                Debug.LogError($"{objetoSeguirCurva.gameObject.name} no tiene el componente SeguirCurva");
                Debug.Break();
            }
            else if (listaCaminos.caminos.Count == 0)
            {
                Debug.LogError($"{listaCaminos.gameObject.name} no contiene caminos en su lista");
                Debug.Break();
            }
            else
            {
                objetoSeguirCurva.AsignarCamino(listaCaminos);
                objetoSeguirCurva.AsignarConfiguraciones(velocidadEntrarCurva, velocidadSalirCurva, reproducirEnStart, bucle, rotacionDinamica);
            }
        }
    }
}
