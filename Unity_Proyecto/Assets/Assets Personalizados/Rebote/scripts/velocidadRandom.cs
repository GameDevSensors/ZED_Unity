using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Rebote
{
    [RequireComponent(typeof(Rigidbody))]
    public class velocidadEjes : MonoBehaviour
    {
        public enum Ejes
        {
            X,
            Y,
            Z
        }

        /// <summary>
        /// En que ejes se va a mover
        /// </summary>
        public Ejes ejes;

        /// <summary>
        /// Que tan rapido se va a mover
        /// </summary>
        public float escalaVelocidad = 5f;

        Rigidbody rb;

        Vector3 velocidad;

        /// <summary>
        /// Direccion donde se va a mover el objeto
        /// </summary>
        void AsignarVelocidad()
        {
            switch (ejes)
            {
                case Ejes.X:
                    velocidad = new Vector3(1, Random.Range(-1, 1), Random.Range(-1, 1));
                    break;
                case Ejes.Y:
                    velocidad = new Vector3(Random.Range(-1, 1), 1, Random.Range(-1, 1));
                    break;
                case Ejes.Z:
                    velocidad = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 1);
                    break;
                default:
                    Debug.LogError("Solo es posible estas tres opciones");
                    break;
            }
        }
        void Start()
        {
            rb = GetComponent<Rigidbody>();

            AsignarVelocidad();
            rb.velocity += velocidad * escalaVelocidad;
        }
    }
}
