using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class Explosion : MonoBehaviour, IEstadosNota
{
    #region Particulas
    [Header("Particulas")]
    public VisualEffect visualEffect;
    public VisualEffectAsset particulaNormal;
    public VisualEffectAsset particulaMuerte;
    #endregion

    [Space(10f)]
    public float tiempoRegresar = 1f;   //  tiempo para que la nota vuelva a aparecer

    #region Rebote
    [Space(10f), Header("Rebote")]
    public GameObject objetoDestruierRebote;
    public bool notaRebote = false;
    #endregion



    public void EstadoNormal()
    {
        visualEffect.visualEffectAsset = particulaNormal;   //  Regresa la nota
        visualEffect.Play();
    }
    public void EstadoExplosion()
    {
        ////  Comportamiento de colision    ////
        visualEffect.visualEffectAsset = particulaMuerte;   //  Efecto de explosion
        visualEffect.SendEvent("OnDead");
    }
    public void ExplotarNota(Instrumento instrumento)
    {
        if (notaRebote == true)
        {
            ControladorMusica.instance.Reproducir(Instrumento.Error);
            Destroy(objetoDestruierRebote);
        }
        else
        {
            ControladorMusica.instance.Reproducir(instrumento);
            EstadoExplosion();
            StartCoroutine(RegresarEstadoNormal());
        }
    }

    public IEnumerator RegresarEstadoNormal()
    {
        yield return new WaitForSeconds(tiempoRegresar);
        EstadoNormal();
    }
}
