using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControllerZone2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private GameObject UITextDialog;


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UITextDialog.SetActive(true);
            UITextDialog.GetComponent<TMP_Text>().text = "Estas Cerca a una zona de avistamiento, afina tu oido...";

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UITextDialog.SetActive(true);
            UITextDialog.GetComponent<TMP_Text>().text = "Te estas alejando, escucha con atención";

        }
    }


}
