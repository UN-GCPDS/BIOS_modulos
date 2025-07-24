using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Mision 
{


    public int nombre;
    public float tiempoAnimal;
    public int CantidadAparicion;
    public List<Animal> ltsAnimalsTarget = new List<Animal>();
    public List<Animal> ltsAnimalsNOTarget = new List<Animal>();
    public bool finalizo;


    public Mision()
    {

    }
    public Mision(int nombre, float tiempoAnimal, int CantidadAparicion, List<Animal> ltsAnimalsTarget, List<Animal> ltsAnimalsNOTarget, bool finalizo)
    {
        this.nombre = nombre;
        this.tiempoAnimal = tiempoAnimal;
        this.CantidadAparicion = CantidadAparicion;
        this.ltsAnimalsTarget = ltsAnimalsTarget;
        this.finalizo = finalizo;
        this.ltsAnimalsNOTarget=ltsAnimalsNOTarget;
    }



}
