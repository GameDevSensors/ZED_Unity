using System.Collections;
using System.Collections.Generic;
using Generacion;
using UnityEngine;

public class SpawnManagerRebote : MonoBehaviour
{
    public GenerarObjeto generar;
    public float cantidadGenerarPorTiempoEspera;
    public float tiempoEspera = 1f;

    private void Start()
    {
        StartCoroutine(Generar());
    }

    public IEnumerator Generar()
    {
        while (true)
        {
            for (int i = 0; i < cantidadGenerarPorTiempoEspera; i++)
            {
                generar.Generar();
            }
            yield return new WaitForSecondsRealtime(tiempoEspera);
        }
    }
}
