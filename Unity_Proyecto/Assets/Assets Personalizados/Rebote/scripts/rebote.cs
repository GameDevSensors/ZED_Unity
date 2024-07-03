using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rebote
{
    [RequireComponent(typeof(Rigidbody))]
    public class rebote : MonoBehaviour
    {
        /// <summary>
        /// Fuerza con la que rebota el objeto
        /// </summary>
        public float fuerzaRebote = 5f;
        private Rigidbody rb;

        void OnValidate()
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
        }

        void OnCollisionEnter(Collision collision)
        {
            rb = GetComponent<Rigidbody>();
            if (collision.contacts.Length > 0)
            {
                Vector3 direccionRebote = Vector3.Reflect(rb.velocity, collision.contacts[0].normal);
                rb.velocity = direccionRebote.normalized * fuerzaRebote;
            }
            if (collision.gameObject.CompareTag("Nota Rebota"))
            {
                rb.velocity = -1 * fuerzaRebote * new Vector3(Random.Range(-1, 1), 1f, Random.Range(-1, 1));
                collision.gameObject.GetComponent<Rigidbody>().velocity = -1 * fuerzaRebote * new Vector3(Random.Range(-1, 1), -1f, Random.Range(-1, 1));
            }
        }
    }
}
