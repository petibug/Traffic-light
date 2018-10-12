using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {

    public GameObject UIPanel;
    public GameObject UIMinMaxPanel;

    // Use this for initialization
    void Start () {
		
	}
	
    public void OpenUI()
    {
        GetComponent<UserPref>().LoadPref();
        UIPanel.SetActive(true);
    }

    public void CloseUI()
    {
        UIPanel.GetComponent<Animator>().SetTrigger("out");
    }

    
    public void ActivateMinMax()
    {
        CanvasGroup minmaxplanel = UIMinMaxPanel.GetComponent<CanvasGroup>();
        minmaxplanel.interactable = true;
        minmaxplanel.alpha = 1;
    }

    public void DeactivateMinMax()
    {
        CanvasGroup minmaxplanel = UIMinMaxPanel.GetComponent<CanvasGroup>();
        minmaxplanel.interactable = false;
        minmaxplanel.alpha = .5f;
    }

    public void Error()
    {
        UIPanel.GetComponent<Animator>().SetTrigger("error");
    }
}
