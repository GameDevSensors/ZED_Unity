using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabezaInstrumento : MonoBehaviour
{
    public Instrumento instrumento;
    #region Intrumento mallas
    [Space(10f),Header("Skin")]
    public MeshFilter skin;
    [Space(10f),Header("Mallas")]
    public Mesh mallaPiano;
    public Mesh mallaGuitarra;
    #endregion
    void Awake()
    {
        ControladorMusica.instance.AsignarCabeza(this);
        AsignarMalla();
    }

    void OnDestroy()
    {
        ControladorMusica.instance.EliminarCabeza(this);
    }

    void AsignarMalla()
    {
        switch (instrumento)
        {
            case Instrumento.Error:
                skin.mesh = null;
                break;
            case Instrumento.Piano:
                skin.mesh = mallaPiano;
                break;
            case Instrumento.Guitarra:
                skin.mesh = mallaGuitarra;
                break;
            default:
                Debug.LogError("No es posible asignar una malla a una eleccion no valida");
                break;
        }
    }
}
