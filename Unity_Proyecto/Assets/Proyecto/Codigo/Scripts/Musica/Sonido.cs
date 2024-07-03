using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Sonido
{
    [SerializeField]
    private string nombreInstrumento;
    public Instrumento instrumento;
    public List<AudioClip> clips;
}
