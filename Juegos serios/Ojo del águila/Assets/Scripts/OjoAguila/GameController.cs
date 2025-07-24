using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;


[System.Serializable]
public struct ItemDupla
{
    
    public string nombre;
    public bool aire;
}
public class GameController : MonoBehaviour


{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject ControllerMapIni;
    public GameObject ControllerInput;
     
    public bool introGender = false;
    private bool introMap = false;
    private bool introInp = false;
    public bool mision0 = false;
    public bool mision1 = false;
    public bool mision2 = false;
    public bool mision3 = false;
    public bool mision4 = false;
    public string gender = "";
    [SerializeField] List<ItemDupla> namesAnimals = new List<ItemDupla>();
    [SerializeField] public List<Animal> ltsAnimals = new List<Animal>();
    public List<Mision> ltsMision = new List<Mision>();

    [SerializeField] float tiempoInicio;
    [SerializeField] float reduccionTiempo;
    [SerializeField] int CantidadAparicion;
    [SerializeField] int CantidadMisiones;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject TextCanvaPlayer;
    [SerializeField] private Avatar avatarnino;
    [SerializeField] GameObject cineMachineBrain;
    [SerializeField] List<GameObject> Npcs = new List<GameObject>();
    [SerializeField] GameObject ControllerMision0;
    [SerializeField] GameObject ControllerMision1;
    [SerializeField] GameObject ControllerMision2;
    [SerializeField] GameObject ControllerMision3;
    [SerializeField] GameObject ControllerMision4;


    [SerializeField] GameObject mission0;
    [SerializeField] GameObject mission1;
    [SerializeField] GameObject mission2;
    [SerializeField] GameObject mission3;
    [SerializeField] GameObject mission4;

    bool aux1 = true;
    bool aux2 = true;
    bool aux3 = true;
    bool aux4 = true;
    DiccionarioEventos events = new DiccionarioEventos();

    void Start()
    {
        renderBarRiver();
        ControllerMapIni.SetActive(false);
        Player.GetComponent<PlayerInput>().enabled = false;
        CreateListAnimals();
        CreateListMision();
        MoneLibrary.InitializePlugin("com.beepro.monelib.PluginInstance");
    }


    // Update is called once per frame
    void Update()
    {
        if (introGender)
        {
                //Destroy(ControllerMapIni);
            ControllerMapIni.SetActive(true);
            introGender = false;


            if (gender == "m")
            {
                Player.GetComponent<Animator>().avatar = avatarnino;
                Destroy(Npcs[1]);

            }
            if (gender == "f")
            {
                Destroy(Npcs[0]);

            }

        }



        if (!introMap)
        {
            if (ControllerMapIni.GetComponent<ControllerVision>().Inicio == true)
            {
                //Destroy(ControllerMapIni);
                ControllerMapIni.SetActive(false);
                introMap = true;
                ControllerInput.SetActive(true);
                Destroy(Npcs[1]);
                Destroy(Npcs[0]);
                cineMachineBrain.GetComponent<CinemachineBrain>().DefaultBlend = new(CinemachineBlendDefinition.Styles.EaseInOut, 2f);
                //Destroy(ControllerMapIni);
            }
        }

        if (!introInp)
        {
            if (ControllerInput.GetComponent<ControllerInputs>().Inicio == true)
            {
                
                ControllerInput.SetActive(false);
                introInp = true;
                //Destroy(ControllerInput);
            }
        }

        if (ltsMision[0].finalizo && aux1)
        {
            ControllerMision0.SetActive(false);
            ControllerMision1.SetActive(true);
            mision0 = false;
            mision1 = true;
            aux1 = false;
        }

        if ( ltsMision[1].finalizo && aux2)
        {
            ControllerMision1.SetActive(false);
            ControllerMision2.SetActive(true);
            mision2 = true;
            mision1 = false;
            aux2 = false;
        }

        if (ltsMision[2].finalizo && aux3)
        {
            ControllerMision2.SetActive(false);
            ControllerMision3.SetActive(true);
            mision2 = false;
            mision3 = true;
            aux3 = false;
        }

        if (ltsMision[3].finalizo && aux4)
        {
            ControllerMision3.SetActive(false);
            ControllerMision4.SetActive(true);
            mision3 = false;
            mision4 = true;
            aux4 = false;
        }

        if (mision4 && ltsMision[4].finalizo)
        {
            ControllerMision4.SetActive(false);
            mision4 = false;
            Player.GetComponent<PlayerInput>().enabled = false;
            TextCanvaPlayer.GetComponent<TMP_Text>().text = "JUEGO FINALIZADO";
        }
    }


    private void CreateListAnimals()
    {
        foreach (var tupla in namesAnimals)
        {
            ltsAnimals.Add(UpdateListAnimals(tupla.nombre, tupla.aire));
        }
    }
    private Animal UpdateListAnimals(string name, bool aire)
    {
        
        GameObject obj3d = Resources.Load<GameObject>("obj3d"+name);
        Image render = Resources.Load<Image>("render"+name);
        Animal animal1 = new Animal(name,obj3d,render,false, asigMision(name),aire);
        return animal1;
    
    }

    public int asigMision(string name)
    {
        int mision = 0;

        if (name == "Oso" || name == "Danta")
        {
            mision = 1;
        }

        if (name == "Tucan" || name == "Mono")
        {
            mision = 2;
        }
        if (name == "Rana" || name == "Colibri")
        {
            mision = 3;
        }


        return mision;
    }

    public void CreateListMision()
    {
        for (int i = 0; i < CantidadMisiones; i++)
        {
            if (i == 0)
            {

                var result = ListAnimalMision(i);
                List<Animal> ltsAnimalsTarget = result.Item1;
                List<Animal> ltsAnimalsNOTarget = result.Item2;
                Mision mision = new Mision(i, tiempoInicio*3, CantidadAparicion, ltsAnimalsTarget, ltsAnimalsNOTarget, false);
                ltsMision.Add(mision);
            }

            if (i == 1)
            {
                var result = ListAnimalMision(i);
                List<Animal> ltsAnimalsTarget = result.Item1;
                List<Animal> ltsAnimalsNOTarget = result.Item2;
                Mision mision = new Mision(i, tiempoInicio -(reduccionTiempo*0), CantidadAparicion, ltsAnimalsTarget, ltsAnimalsNOTarget, false);
                ltsMision.Add(mision);
            }
            if (i == 2)
            {
                var result = ListAnimalMision(i);
                List<Animal> ltsAnimalsTarget = result.Item1;
                List<Animal> ltsAnimalsNOTarget = result.Item2;
                Mision mision = new Mision(i, tiempoInicio - (reduccionTiempo * 1), CantidadAparicion, ltsAnimalsTarget, ltsAnimalsNOTarget, false);
                ltsMision.Add(mision);
            }
            if (i == 3)
            {
                var result = ListAnimalMision(i);
                List<Animal> ltsAnimalsTarget = result.Item1;
                List<Animal> ltsAnimalsNOTarget = result.Item2;
                Mision mision = new Mision(i, tiempoInicio - (reduccionTiempo * 2), CantidadAparicion, ltsAnimalsTarget, ltsAnimalsNOTarget, false);
                ltsMision.Add(mision);
            }
            if (i == 4)
            {
                var result = ListAnimalMision(i);
                List<Animal> ltsAnimalsTarget = result.Item1;
                List<Animal> ltsAnimalsNOTarget = result.Item2;
                Mision mision = new Mision(i, tiempoInicio - (reduccionTiempo * 3), CantidadAparicion, ltsAnimalsTarget, ltsAnimalsNOTarget, false);
                ltsMision.Add(mision);
            }

        }
    }


    public (List<Animal>, List<Animal>) ListAnimalMision(int mision)
    {
        List<Animal> ltsAnimalsTarget = new List<Animal>();
        List<Animal> ltsAnimalsNOTarget = new List<Animal>(ltsAnimals);

        if (mision == 0)
        {
            ltsAnimalsTarget.Add(ltsAnimals[0]);
            ltsAnimalsNOTarget.Remove(ltsAnimals[0]);
        }
        if (mision == 1)
        {
            ltsAnimalsTarget.Add(ltsAnimals[0]);
            ltsAnimalsTarget.Add(ltsAnimals[1]);
            ltsAnimalsNOTarget.Remove(ltsAnimals[0]);
            ltsAnimalsNOTarget.Remove(ltsAnimals[1]);
        }
        if (mision == 2)
        {
            ltsAnimalsTarget.Add(ltsAnimals[2]);
            ltsAnimalsTarget.Add(ltsAnimals[3]);
            ltsAnimalsNOTarget.Remove(ltsAnimals[3]);
            ltsAnimalsNOTarget.Remove(ltsAnimals[2]);
        }
        if (mision == 3)
        {
            ltsAnimalsTarget.Add(ltsAnimals[4]);
            ltsAnimalsTarget.Add(ltsAnimals[5]);
            ltsAnimalsNOTarget.Remove(ltsAnimals[5]);
            ltsAnimalsNOTarget.Remove(ltsAnimals[4]);
        }
        if (mision == 4)
        {
            int aleatorio1 = UnityEngine.Random.Range(0, 4);
            int aleatorio2 = UnityEngine.Random.Range(0, 4);
            while (aleatorio1 == aleatorio2)
            {
                aleatorio2 = UnityEngine.Random.Range(0, 4);
            }
            ltsAnimalsTarget.Add(ltsAnimals[aleatorio1]);
            ltsAnimalsTarget.Add(ltsAnimals[aleatorio2]);
            ltsAnimalsNOTarget.Remove(ltsAnimals[aleatorio2]);
            ltsAnimalsNOTarget.Remove(ltsAnimals[aleatorio1]);
        }

        return (ltsAnimalsTarget,ltsAnimalsNOTarget);
    }

    // M�todo para activar el teclado en un InputField espec�fico
    public void ActivateKeyboard(InputField inputField)
    {
        if (inputField != null)
        {
            inputField.ActivateInputField();
            inputField.Select();
        }
    }

    public Mision getCurrentMission()
    {
        Mision currentMission = null;
        if (mision0)
        {
            currentMission = ltsMision[0];
        }
        if (mision1)
        {
            currentMission = ltsMision[1];
        }
        if (mision2)
        {
            currentMission = ltsMision[2];
        }
        if (mision3)
        {
            currentMission = ltsMision[3];
        }
        if (mision4)
        {
            currentMission = ltsMision[4];
        }

        return currentMission;
    }

    
    public void ChangeTimeCurrentMission(float time)
    {
        if (mision0)
        {
            ltsMision[0].tiempoAnimal = time;
        }
        if (mision1)
        {
             ltsMision[1].tiempoAnimal = time;
        }
        if (mision2)
        {
            ltsMision[2].tiempoAnimal = time;
        }
        if (mision3)
        {
             ltsMision[3].tiempoAnimal = time;
        }
        if (mision4)
        {
            ltsMision[4].tiempoAnimal = time;
        }

    }

    public void ChangeCantCurrentMission(int cant)
    {
        if (mision0)
        {
            ltsMision[0].CantidadAparicion = cant;

        }
        if (mision1)
        {
            ltsMision[1].CantidadAparicion = cant;
        }
        if (mision2)
        {
             ltsMision[2].CantidadAparicion = cant; 
        }
        if (mision3)
        {
            ltsMision[3].CantidadAparicion = cant; 
        }
        if (mision4)
        {
             ltsMision[4].CantidadAparicion = cant; 
        }
    }

    public void renderBarRiver()
    {
        GameObject barriver = GameObject.Find("Barreras");

        Renderer[] allRenderers = barriver.GetComponentsInChildren<Renderer>(true);
        foreach (Renderer r in allRenderers)
        {
            r.enabled = false;
        }
    }

    public void finalizarMision()
    {
        if (mision0)
        {
            mission0.GetComponent<ControllerMision>().finalizarMision();
            mission0.SetActive(false);
        }
        if (mision1)
        {
            mission1.GetComponent<ControllerMision>().finalizarMision();
            mission1.SetActive(false);
        }
        if (mision2)
        {
            mission2.GetComponent<ControllerMision>().finalizarMision();
            mission2.SetActive(false);
        }
        if (mision3)
        {
            mission3.GetComponent<ControllerMision>().finalizarMision();
            mission3.SetActive(false);
        }
        if (mision4)
        {
            mission4.GetComponent<ControllerMision>().finalizarMision();
            mission4.SetActive(false);
        }
    }

}
