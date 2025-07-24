using UnityEngine;
using UnityEngine.UI;


public class Animal 
{


    public string nombre;
    public GameObject obj3d;
    public Image renderImage;
    public bool coleccionado;
    public int mision;
    public bool aire;

    public Animal()
    {

    }
    public Animal(string nombre, GameObject obj3d, Image renderImage, bool coleccionado, int mision, bool aire)
    {
        this.nombre = nombre;
        this.obj3d = obj3d;
        this.renderImage = renderImage;
        this.coleccionado = coleccionado;
        this.mision = mision;
        this.mision = mision;
        this.aire = aire;
    }



}
