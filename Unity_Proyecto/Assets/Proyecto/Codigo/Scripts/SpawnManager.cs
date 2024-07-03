using System.Collections;
using System.Collections.Generic;
using Modificaciones;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Generar_SeguirCurva generar_SeguirCurva;
    public float cantidadGenerar;
    public float tiempoEspera = 1f;

    private void Start()
    {
        StartCoroutine(Generar());
    }

    public IEnumerator Generar()
    {
        for (int i = 0; i < cantidadGenerar; i++)
        {
            generar_SeguirCurva.Generar();
            yield return new WaitForSeconds(tiempoEspera);
        }
    }
}
