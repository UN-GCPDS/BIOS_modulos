using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControllerZone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private GameObject UITextDialog;


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UITextDialog.SetActive(true);
            UITextDialog.GetComponent<TMP_Text>().text = "Estas Cerca a una zona de avistamiento, busca un trípode para activar la cámara.";

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UITextDialog.SetActive(true);
            UITextDialog.GetComponent<TMP_Text>().text = "Te estas alejando...";

        }
    }


}
