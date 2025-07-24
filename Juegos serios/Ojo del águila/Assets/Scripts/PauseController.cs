using TMPro;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject buttonpause;
    [SerializeField] GameObject textpausa;
    bool estado;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        estado = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void pausar()
    {
        if (estado)
        {
            Debug.Log("aca");
            estado = false;
            textpausa.SetActive(true);
            buttonpause.GetComponent<TMP_Text>().text = "Reanudar ";
            Time.timeScale = 0f; // Pausa el juego (física, animaciones, etc.)
        }
        else
        {
            Debug.Log("aca2");
            Time.timeScale = 1f; // Pausa el juego (física, animaciones, etc.)
            estado = true;
            buttonpause.GetComponent<TMP_Text>().text = "Pausar";
            textpausa.SetActive(false);
        }
    }

}

