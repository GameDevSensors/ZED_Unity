using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rebote
{
    public class PlataformaRebote : MonoBehaviour
    {
        /// <summary>
        /// Fuerza con la que rebota el objeto
        /// </summary>
        public float fuerzaRebote = 5f;
        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Rigidbody rb))
            {
                rb.velocity = new Vector3(Random.Range(0f, 1f), -1f, Random.Range(0f, 1f)).normalized * fuerzaRebote;
            }
        }
    }
}
