using UnityEngine;
using System.Collections;

public class Lights : MonoBehaviour
{

    public GameObject lightOn;
    public GameObject lightOff;

    // Use this for initialization
    void Start()
    {

    }


    public void TurnOn()
    {
        lightOn.SetActive(true);
        lightOff.SetActive(false);
    }

    public void TurnOff()
    {
        lightOn.SetActive(false);
        lightOff.SetActive(true);
    }

    public void Switch()
    {
        if (lightOn.activeSelf == true)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }
}
