using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rebote
{
    public class VelocidadForward : MonoBehaviour
    {
        //// <summary>
        /// Que tan rapido se va a mover
        /// </summary>
        public float escalaVelocidad = 5f;

        Rigidbody rb;
        void Start()
        {
            rb = GetComponent<Rigidbody>();

            rb.velocity += (transform.forward * escalaVelocidad) + (new Vector3(Random.Range(-1, 1), 0, 0).normalized * escalaVelocidad);
        }
    }
}
