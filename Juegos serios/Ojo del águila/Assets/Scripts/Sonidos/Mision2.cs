using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Mision2 
{


    public int nombre;
    public Animal2 animalPrincipal;
    public List<Animal2> NoAnimalPrincipal;
    public int secuenciatama;
    public List<Animal2> secuencia = new List<Animal2>();
    public float tiempoRepro;
    public bool finalizo;


    public Mision2()
    {

    }
    public Mision2(int nombre, Animal2 animalPrincipal, List<Animal2> NoAnimalPrincipal, int secuenciatama,List<Animal2> secuencia,float tiempoRepro, bool finalizo)
    {
        this.nombre = nombre;
        this.animalPrincipal = animalPrincipal;
        this.finalizo = finalizo;
        this.NoAnimalPrincipal = NoAnimalPrincipal;
        this.secuenciatama = secuenciatama;
        this.secuencia = secuencia;
        this.tiempoRepro=tiempoRepro;
    }

}
