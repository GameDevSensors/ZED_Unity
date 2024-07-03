using System.Collections;
using UnityEngine;

public class DeteccionColisionHijo : MonoBehaviour
{
    public NotificacionPadre padre;

    void Start()
    {
        padre = GetComponentInParent<NotificacionPadre>();
        if (padre == null)
        {
            Debug.LogError($"{padre.name} no tiene el script NotificacionPadre");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Explosion explosion))
        {
            padre.HijoTriggerea(explosion);
        }
    }
}