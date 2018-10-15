using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserPref : MonoBehaviour {

    public InputField vertMin;
    public InputField vertMax;
    public InputField rougeMin;
    public InputField rougeMax;

    public Toggle FreePlayToggle;
    public Toggle ShowTimerToggle;

    private void Start()
    {
        LoadPref();
    }

    public void LoadPref() {

        float LoadVertMin = PlayerPrefs.GetFloat(GameManage.Pref.VertMin.ToString(), GameManage.instance.FeuVertDurationMin);
        float LoadVertMax = PlayerPrefs.GetFloat(GameManage.Pref.VertMax.ToString(), GameManage.instance.FeuVertDurationMax);
        float LoadRougeMin = PlayerPrefs.GetFloat(GameManage.Pref.RougeMin.ToString(), GameManage.instance.FeuRougeDurationMin);
        float LoadRougeMax = PlayerPrefs.GetFloat(GameManage.Pref.RougeMax.ToString(), GameManage.instance.FeuRougeDurationMax);
        bool LoadFreePlay = GetBool(GameManage.Pref.FreePlay.ToString(), GameManage.instance.FreePlay);
        bool LoadShowTimer = GetBool(GameManage.Pref.ShowTimer.ToString(), GameManage.instance.ShowTimer);


        //set UI panel
        vertMin.text = LoadVertMin.ToString();
        vertMax.text = LoadVertMax.ToString();
        rougeMin.text = LoadRougeMin.ToString();
        rougeMax.text = LoadRougeMax.ToString();

        FreePlayToggle.isOn = LoadFreePlay;
        SwitchFreePlay(LoadFreePlay);

        ShowTimerToggle.isOn = LoadShowTimer;
        SwitchTimer(LoadShowTimer);

        //send prefrences to game manager
        GameManage.instance.FeuRougeDurationMin = LoadVertMin;
        GameManage.instance.FeuRougeDurationMax = LoadVertMax;
        GameManage.instance.FeuVertDurationMin = LoadRougeMin;
        GameManage.instance.FeuVertDurationMax = LoadRougeMax;

        GameManage.instance.ChangeFreePlay(LoadFreePlay);
        GameManage.instance.ChangeShowTimer (LoadShowTimer);

    }

    public void SavePref() {
  
        //gamemanager
        GameManage.instance.FeuVertDurationMin = float.Parse(vertMin.text);
        GameManage.instance.FeuVertDurationMax = float.Parse(vertMax.text);
        GameManage.instance.FeuRougeDurationMin = float.Parse(rougeMin.text);
        GameManage.instance.FeuRougeDurationMax = float.Parse(rougeMax.text);

        GameManage.instance.ChangeFreePlay(FreePlayToggle.isOn);
        GameManage.instance.ChangeShowTimer(ShowTimerToggle.isOn);
  

        //preferences
        PlayerPrefs.SetFloat(GameManage.Pref.VertMin.ToString(), float.Parse(vertMin.text));
        PlayerPrefs.SetFloat(GameManage.Pref.VertMax.ToString(), float.Parse(vertMax.text));
        PlayerPrefs.SetFloat(GameManage.Pref.RougeMin.ToString(), float.Parse(rougeMin.text));
        PlayerPrefs.SetFloat(GameManage.Pref.RougeMax.ToString(), float.Parse(rougeMax.text));

        SetBool(GameManage.Pref.FreePlay.ToString(), FreePlayToggle.isOn);
        SetBool(GameManage.Pref.ShowTimer.ToString(), ShowTimerToggle.isOn);


        PlayerPrefs.Save();
        gameObject.GetComponent<UI>().CloseUI();

        Debug.Log("Preferences saved");

    }

    public static void SetBool(string key, bool state)
    {
        PlayerPrefs.SetInt(key, state ? 1 : 0);
    }

    public static bool GetBool(string key, bool defaultBool = false)
    {
        int intbool = defaultBool ? 1 : 0;
        
        int value = PlayerPrefs.GetInt(key, intbool);

        if (value == 1)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public void SwitchFreePlay(bool active)
    {
        if(active == true)
        {
            gameObject.GetComponent<UI>().DeactivateMinMax();
        }
        else
        {
            gameObject.GetComponent<UI>().ActivateMinMax();
        }        
    }

    public void SwitchTimer(bool active)
    {
        if (active == true)
        {

        }
        else
        {

        }
    }

    public void CheckMinGreen(string toCheck)
    {
        if (float.Parse(toCheck) > float.Parse(vertMax.text))
        {
            vertMin.text = vertMax.text;
            gameObject.GetComponent<UI>().Error();
        }
        
    }

    public void CheckMaxGreen(string toCheck)
    {
        if (float.Parse(toCheck) < float.Parse(vertMin.text))
        {
            vertMax.text = vertMin.text;
            gameObject.GetComponent<UI>().Error();
        }
        
    }

    public void CheckMinRed(string toCheck)
    {
        if (float.Parse(toCheck) > float.Parse(rougeMax.text))
        {
            rougeMin.text = rougeMax.text;
            gameObject.GetComponent<UI>().Error();
        }
        
    }

    public void CheckMaxRed(string toCheck)
    {
        if (float.Parse(toCheck) < float.Parse(rougeMin.text))
        {
            rougeMax.text = rougeMin.text;
            gameObject.GetComponent<UI>().Error();
        }        
    }
}
