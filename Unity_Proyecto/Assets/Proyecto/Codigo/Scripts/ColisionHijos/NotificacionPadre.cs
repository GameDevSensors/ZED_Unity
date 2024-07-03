using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CabezaInstrumento))]
public class NotificacionPadre : MonoBehaviour
{
    CabezaInstrumento cabezaInstrumento;

    public float tiempoSiguienteColision = 1f;
    bool puedeColisionar = true;

    void OnValidate()
    {
        if (!TryGetComponent(out cabezaInstrumento))
        {
            Debug.LogError($"{gameObject.name} no tiene el componente CabezaInstrumento");
        }
    }
    void Start()
    {
        if (!TryGetComponent(out cabezaInstrumento))
        {
            Debug.LogError($"{gameObject.name} no tiene el componente CabezaInstrumento");
        }
        else
        {
            puedeColisionar = true;
        }
    }
    public void HijoTriggerea(Explosion explosion)
    {
        if (puedeColisionar)
        {
            explosion.ExplotarNota(cabezaInstrumento.instrumento);
            StartCoroutine(CambiarEstadoColision());
        }
    }

    IEnumerator CambiarEstadoColision()
    {
        puedeColisionar = false;
        yield return new WaitForSecondsRealtime(tiempoSiguienteColision);
        puedeColisionar = true;
    }

}