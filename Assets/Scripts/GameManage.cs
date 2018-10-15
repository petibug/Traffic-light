using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class GameManage : MonoBehaviour {

    public Lights GreenLight;
    public Lights OrangeLight;
    public Lights RedLight;

    public Color ColorGreenText;
    public Color ColorRedText;

    public float FeuVertDurationMin;
    public float FeuVertDurationMax;
    public float FeuOrangeDuration;
    public float FeuRougeDurationMin;
    public float FeuRougeDurationMax;

    public bool FreePlay;
    public bool ShowTimer;

    public GameObject StandObject;
    public GameObject TimerObject;
    public Text countDownText;
    public float WithTimerPosition = 43.4f;
    public float WithoutTimerPosition = 51.4f;

    private float timer;

    private enum Feu {vert,orange,rouge};
    private int activeFeu;

    public enum Pref {VertMin,VertMax,RougeMin,RougeMax,FreePlay,ShowTimer};
    private UserPref PrefFunction;

    public static GameManage instance = null;

    // Use this for initialization
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {

        // Disable screen dimming
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //get preferences
        PrefFunction = gameObject.GetComponent<UserPref>();
        PrefFunction.LoadPref();

        setGreenActive();
    }
	
	// Update is called once per frame
	void Update () {


        
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            //display timer

            TimerText();

        }
        else
        {
             SwitchLight();
        }


	}

    float setDuration (float min, float max)
    {
        float newDuration = Random.Range(min, max);
        return FreePlay ? 360000 : Mathf.Round(newDuration);
    }

    public void setGreenActive() {
        RedLight.TurnOff();
        OrangeLight.TurnOff();
        GreenLight.TurnOn();

        activeFeu = (int)Feu.vert;
        timer = setDuration(FeuVertDurationMin, FeuVertDurationMax);
        Debug.Log("Feu vert actif : " + timer + " secondes");
    }

    public void SetOrangeActive() {
        RedLight.TurnOff();
        OrangeLight.TurnOn();
        GreenLight.TurnOff();

        activeFeu = (int)Feu.orange;
        timer = FeuOrangeDuration;
        Debug.Log("Feu Orange actif : " + timer + " secondes");
    }

    public void SetRedActive()
    {
        RedLight.TurnOn();
        OrangeLight.TurnOff();
        GreenLight.TurnOff();

        activeFeu = (int)Feu.rouge;
        timer = setDuration(FeuRougeDurationMin, FeuRougeDurationMax);
        Debug.Log("Feu rouge actif : " + timer + " secondes");
    }

    private void SetShowTimer()
    {
        StandObject.transform.position = new Vector3(StandObject.transform.position.x, WithTimerPosition, StandObject.transform.position.z);
        TimerObject.SetActive(true);
    }

    private void SetHideTimer()
    {
        StandObject.transform.position = new Vector3(StandObject.transform.position.x,WithoutTimerPosition, StandObject.transform.position.z);
        TimerObject.SetActive(false);
    }

    public void SwitchLight() {
            switch (activeFeu)
            {
                case (int)Feu.vert:
                    SetOrangeActive();
                    return;

                case (int)Feu.orange:
                    SetRedActive();
                    return;

            case (int)Feu.rouge:
                    setGreenActive();
                    return;
            }
    }

    private void TimerText()
    {
        countDownText.text = Mathf.Floor(timer) > 0 ? Mathf.Floor(timer).ToString() : "";

        switch (activeFeu)
        {
            case (int)Feu.vert:
                countDownText.color = ColorGreenText;
                return;

            case (int)Feu.orange:
                countDownText.text = "";
                return;

            case (int)Feu.rouge:
                countDownText.color = ColorRedText;
                return;
        }
    }

    public void ChangeFreePlay(bool check)
    {
        bool dif = check != FreePlay;
        FreePlay = check;

        if (dif == true)
        {
            switch (activeFeu)
            {
                case (int)Feu.vert:
                    setGreenActive();
                    return;

                case (int)Feu.rouge:
                    SetRedActive();
                    return;
            }
        }

    }

    public void ChangeShowTimer(bool check)
    {
        bool dif = check != ShowTimer;
        ShowTimer = check;

        if (ShowTimer == true)
        {
            if (FreePlay == true)
            {
                SetHideTimer();
            }
            else {
                SetShowTimer();
            }
        }
        else
        {
            SetHideTimer();
        }

    }
}
