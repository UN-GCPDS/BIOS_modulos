using UnityEngine;
using UnityEngine.UI;


public class Animal2 
{


    public string nombre;
    public GameObject obj3d;
    public Image render;
    public bool aire;
    public GameObject sound;

    public Animal2()
    {

    }
    public Animal2(string nombre, GameObject obj3d,  bool aire, GameObject sound, Image render)
    {
        this.nombre = nombre;
        this.obj3d = obj3d;
        this.aire = aire;
        this.sound = sound;
        this.render = render;
    }



}
