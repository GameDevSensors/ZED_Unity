using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Escala de sonidos", menuName = "Escala de tonos", order = 0)]
public class Escalas : ScriptableObject
{
    public List<Sonido> sonidos = new();
}