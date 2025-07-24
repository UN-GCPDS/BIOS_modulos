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
public struct ItemDuplaAnimal
{
    
    public string nombre;
    public bool aire;
}
public class GameControllerSonidos : MonoBehaviour


{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject ControllerMapIni;
    public GameObject ControllerGuia;
    public GameObject ControllerInput;
    List<Animal2> ocup = new List<Animal2>();

    public bool introGender = false;
    private bool introMap = false;
    private bool introInp = false;
    public bool mision0 = false;
    public bool mision1 = false;
    public bool mision2 = false;
    public bool mision3 = false;
    public bool mision4 = false;
    public string gender = "";
    [SerializeField] List<ItemDuplaAnimal> namesAnimals = new List<ItemDuplaAnimal>();
    [SerializeField] public List<Animal2> ltsAnimals = new List<Animal2>();
    public List<Mision2> ltsMision = new List<Mision2>();

    [SerializeField] int CantidadAvesInicio;
    [SerializeField] int CantidadMisiones;
    [SerializeField] float TiempoReproduccion;
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
    //DiccionarioEventos events = new DiccionarioEventos();

    void Start()
    {
        renderBarRiver();
        ControllerMapIni.SetActive(false);
        Player.GetComponent<PlayerInput>().enabled = false;
        CreateListAnimals();
        CreateListMision();
        //MoneLibrary.InitializePlugin("com.beepro.monelib.PluginInstance");
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
            if (ControllerGuia.GetComponent<ControllerGuia>().Inicio == true)
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
            if (ControllerInput.GetComponent<ControllerInputs2>().Inicio == true)
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
        Debug.Log(" adsdasd"+ltsAnimals.Count);
    }
    private Animal2 UpdateListAnimals(string name, bool aire)
    {
        
        GameObject obj3d = Resources.Load<GameObject>("obj3d"+name);
        GameObject sound = Resources.Load<GameObject>("sound"+name);
        Image render = Resources.Load<Image>("render"+name);
        Animal2 animal1 = new Animal2(name,obj3d,aire, sound,render);
        return animal1;
    
    }


    public void CreateListMision()
    {
        for (int i = 0; i < CantidadMisiones; i++)
        {            
            Animal2 AnimalsTarget = AnimalMision();
            List<Animal2> ltsAnimalsNOTarget = AnimalsNoTarget(AnimalsTarget);
            List<Animal2> secuencia = listAnimalSecuencia(i);
            //List<Animal2> secuencia = new List<Animal2>();
            Mision2 mision = new Mision2(i, AnimalsTarget, ltsAnimalsNOTarget, CantidadAvesInicio, secuencia, TiempoReproduccion-(float)0.5, false);
            ltsMision.Add(mision);
        }
    }


    public Animal2 AnimalMision()
    {
        Animal2 animal2Inicio = new Animal2();
        int pos = UnityEngine.Random.Range(0, ltsAnimals.Count - 1);
        if (ocup.Count==0)
        {
            ocup.Add(ltsAnimals[pos]);
            animal2Inicio= ltsAnimals[pos];
        }
        else
        {
            while (ocup.Contains(ltsAnimals[pos]))
            {
                pos = UnityEngine.Random.Range(0, ltsAnimals.Count - 1);
            }
            ocup.Add(ltsAnimals[pos]);
            animal2Inicio = ltsAnimals[pos];
        }

            return animal2Inicio;
    }

    public List<Animal2> AnimalsNoTarget(Animal2 target)
    {
        List<Animal2> noTarget = new List<Animal2>(ltsAnimals);
        noTarget.Remove(target);        
        while (noTarget.Count != 2)
        {
            int pos = UnityEngine.Random.Range(0, noTarget.Count - 1);
            noTarget.RemoveAt(pos);

        }
        return noTarget;
    }

    public List<Animal2> listAnimalSecuencia(int mision)
    {
        Debug.Log("tama" + ltsAnimals.Count);
        List<Animal2> secuencia = new List<Animal2>();
        Debug.Log("sec" + secuencia.Count);
        Debug.Log("cant aves" + CantidadAvesInicio);
        if (mision == 0)
        {
            while (secuencia.Count != 2)
            {
                int pos = UnityEngine.Random.Range(0, ltsAnimals.Count - 1);
                secuencia.Add(ltsAnimals[pos]);
            }
        }
        if (mision == 1)
        {
            while (secuencia.Count != 2)
            {
                int pos = UnityEngine.Random.Range(0, ltsAnimals.Count - 1);
                secuencia.Add(ltsAnimals[pos]);
            }
        }
        if (mision == 2) //3
        {
            while (secuencia.Count != 3)
            {
                int pos = UnityEngine.Random.Range(0, ltsAnimals.Count - 1);
                secuencia.Add(ltsAnimals[pos]);
            }
        }
        if (mision == 3) // 4
        {
            while (secuencia.Count != 4)
            {
                int pos = UnityEngine.Random.Range(0, ltsAnimals.Count - 1);
                secuencia.Add(ltsAnimals[pos]);
            }
        }
        if (mision == 4) // 5
        {
            while (secuencia.Count != 5)
            {
                int pos = UnityEngine.Random.Range(0, ltsAnimals.Count - 1);
                secuencia.Add(ltsAnimals[pos]);
            }
        }
        return secuencia;
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

    public Mision2 getCurrentMission()
    {
        Mision2 currentMission = null;
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
            ltsMision[0].tiempoRepro = time;
        }
        if (mision1)
        {
             ltsMision[1].tiempoRepro = time;
        }
        if (mision2)
        {
            ltsMision[2].tiempoRepro = time;
        }
        if (mision3)
        {
             ltsMision[3].tiempoRepro = time;
        }
        if (mision4)
        {
            ltsMision[4].tiempoRepro = time;
        }

    }

    public void ChangeCantCurrentMission(int cant)
    {
        if (mision0)
        {
            ltsMision[0].secuenciatama = cant;

        }
        if (mision1)
        {
            ltsMision[1].secuenciatama = cant;
        }
        if (mision2)
        {
             ltsMision[2].secuenciatama = cant; 
        }
        if (mision3)
        {
            ltsMision[3].secuenciatama = cant; 
        }
        if (mision4)
        {
             ltsMision[4].secuenciatama = cant; 
        }
    }

    public void renderBarRiver()
    {
        GameObject barriver = GameObject.Find("barrerasWaterIsla1");

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
