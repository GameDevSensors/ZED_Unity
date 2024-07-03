using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorMusica : MonoBehaviour
{
    public static ControladorMusica instance;

    private AudioSource audioSource;

    public Escalas escalas;
    private int indice;
    
    public Dictionary<Instrumento, bool> cabezasOcupadas = new();

    private void Awake()
    {
        //  Singleton
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    private void Start()
    {
        if (!TryGetComponent(out audioSource))
        {
            Debug.LogError(gameObject.name + " no tiene un audio source");
            Debug.Break();
        }
        else
        {
            indice = 0;
            CrearDiccionario();
        }
    }

    public void Reproducir(Instrumento instrumento)
    {
        int indiceIntrumento = IndiceInstrumento(instrumento);
        if (instrumento == Instrumento.Error)
        {
            indice = 0;
        }
        audioSource.PlayOneShot(escalas.sonidos[indiceIntrumento].clips[indice]);
        indice = (indice + 1) % escalas.sonidos[indiceIntrumento].clips.Count;
    }

    public int IndiceInstrumento(Instrumento instrumento)
    {
        switch (instrumento)
        {
            case Instrumento.Error:
                return 0;
            case Instrumento.Piano:
                return 1;
            case Instrumento.Guitarra:
                return 2;
            default:
                throw new ArgumentException("Instrumento no válido", nameof(instrumento));
        }
    }

    void CrearDiccionario()
    {
        foreach (int i in Enum.GetValues(typeof(Instrumento)))
        {
            cabezasOcupadas.Add((Instrumento)i, false);
        }
        if (cabezasOcupadas.ContainsKey(Instrumento.Error))
        {
            cabezasOcupadas[Instrumento.Error] = true;
        }
    }

    public void AsignarCabeza(CabezaInstrumento cabezaInstrumento)
    {
        foreach (Instrumento instrumento in Enum.GetValues(typeof(Instrumento)))
        {
            if (!cabezasOcupadas[instrumento])
            {
                cabezaInstrumento.instrumento = instrumento;
                cabezasOcupadas[instrumento] = true;
                break;
            }
        }
    }
    public void EliminarCabeza(CabezaInstrumento cabezaInstrumento)
    {
        if (cabezasOcupadas[cabezaInstrumento.instrumento])
        {
            cabezasOcupadas[cabezaInstrumento.instrumento] = false;
        }
    }
}
