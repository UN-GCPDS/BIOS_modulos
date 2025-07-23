using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanvaConfigController : MonoBehaviour
{

    [SerializeField] private Slider sliderTime;
    [SerializeField] private Slider sliderCant;
    [SerializeField] private GameObject UITextTime;
    [SerializeField] private GameObject UITextCant;
    [SerializeField] public GameController controllergame;
    private Mision currentMission;
    private float newtime;
    private int newcant;


    void OnEnable()
    {

        currentMission = controllergame.getCurrentMission();
        UITextTime.GetComponent<TMP_Text>().text = currentMission.tiempoAnimal.ToString("0.00");
        UITextCant.GetComponent<TMP_Text>().text = currentMission.CantidadAparicion.ToString("0");
        sliderTime.value = currentMission.tiempoAnimal;
        sliderCant.value = currentMission.CantidadAparicion;
        sliderTime.onValueChanged.AddListener(OnSliderValueChangedTime);
        sliderCant.onValueChanged.AddListener(OnSliderValueChangedCant);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnSliderValueChangedTime(float value)
    {
        newtime = (float)((int)(value * 100f)) / 100f;
        UITextTime.GetComponent<TMP_Text>().text = newtime.ToString("0.00");
    }

    private void OnSliderValueChangedCant(float value)
    {
        // Aquí puedes agregar la lógica que responde al cambio
        newcant=(int)value;
        UITextCant.GetComponent<TMP_Text>().text = newcant.ToString("0");

    }

    public float getNewTime()
    {
        if (newtime==0)
        {
            return currentMission.tiempoAnimal;
        }
        else
        {
            Debug.Log("nuevo tiempo " + newtime);
            return newtime;
        }
           
        
    }

    public int getNewCant()
    {
        if (newcant == 0)
        {
            return currentMission.CantidadAparicion;
        }
        else
        {
            Debug.Log("nuevo cantidad " + newcant);
            return newcant;
        }
            
        
    }
}
