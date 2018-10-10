using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserPref : MonoBehaviour {

    public InputField vertMin;
    public InputField vertMax;
    public InputField rougeMin;
    public InputField rougeMax;

    private void Start()
    {
        LoadPref();
    }

    void LoadPref() {

        vertMin.text = PlayerPrefs.GetFloat(GameManage.Pref.VertMin.ToString(), GameManage.instance.FeuVertDurationMin).ToString();
        vertMax.text = PlayerPrefs.GetFloat(GameManage.Pref.VertMax.ToString(), GameManage.instance.FeuVertDurationMax).ToString();
        rougeMin.text = PlayerPrefs.GetFloat(GameManage.Pref.RougeMin.ToString(), GameManage.instance.FeuRougeDurationMin).ToString();
        rougeMax.text = PlayerPrefs.GetFloat(GameManage.Pref.RougeMax.ToString(), GameManage.instance.FeuRougeDurationMax).ToString();

    }

    public void SavePref() {
 
        PlayerPrefs.SetFloat(GameManage.Pref.VertMin.ToString(), float.Parse(vertMin.text));
        PlayerPrefs.SetFloat(GameManage.Pref.VertMax.ToString(), float.Parse(vertMax.text));
        PlayerPrefs.SetFloat(GameManage.Pref.RougeMin.ToString(), float.Parse(rougeMin.text));
        PlayerPrefs.SetFloat(GameManage.Pref.RougeMax.ToString(), float.Parse(rougeMax.text));

        GameManage.instance.FeuVertDurationMin = float.Parse(vertMin.text);
        GameManage.instance.FeuVertDurationMax = float.Parse(vertMax.text);
        GameManage.instance.FeuRougeDurationMin = float.Parse(rougeMin.text);
        GameManage.instance.FeuRougeDurationMax = float.Parse(rougeMax.text);


        PlayerPrefs.Save();
        gameObject.GetComponent<UI>().CloseUI();

    }
}
