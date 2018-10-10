using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {

    public GameObject UIPanel;

	// Use this for initialization
	void Start () {
		
	}
	
    public void OpenUI()
    {
        UIPanel.SetActive(true);
    }

    public void CloseUI()
    {
        UIPanel.SetActive(false);
    }
}
