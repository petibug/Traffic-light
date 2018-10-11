using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameManage : MonoBehaviour {

    public GameObject FeuRouge;
    public GameObject FeuOrange;
    public GameObject FeuVert;

    public GameObject FeuRougeOff;
    public GameObject FeuOrangeOff;
    public GameObject FeuVertOff;

    public float FeuVertDurationMin;
    public float FeuVertDurationMax;
    public float FeuOrangeDuration;
    public float FeuRougeDurationMin;
    public float FeuRougeDurationMax;

    public bool FreePlay;
    public bool ShowTimer;

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

        }
        else
        {
            SwitchLight();
        }


	}

    float setDuration (float min, float max)
    {
        float newDuration = Random.Range(min, max);
        return FreePlay ? 360000 : newDuration;
    }

    public void setGreenActive() {
        FeuRouge.SetActive(false);
        FeuRougeOff.SetActive(true);

        FeuOrange.SetActive(false);
        FeuOrangeOff.SetActive(true);

        FeuVert.SetActive(true);
        FeuVertOff.SetActive(false);

        activeFeu = (int)Feu.vert;
        timer = setDuration(FeuVertDurationMin, FeuVertDurationMax);
        Debug.Log("Feu vert actif : " + timer + " secondes");
    }

    public void SetOrangeActive() {
        FeuVert.SetActive(false);
        FeuVertOff.SetActive(true);

        FeuOrange.SetActive(true);
        FeuOrangeOff.SetActive(false);

        FeuRouge.SetActive(false);
        FeuRougeOff.SetActive(true);

        activeFeu = (int)Feu.orange;
        timer = FeuOrangeDuration;
        Debug.Log("Feu Orange actif : " + timer + " secondes");
    }

    public void SetRedActive()
    {
        FeuVert.SetActive(false);
        FeuVertOff.SetActive(true);

        FeuOrange.SetActive(false);
        FeuOrangeOff.SetActive(true);

        FeuRouge.SetActive(true);
        FeuRougeOff.SetActive(false);

        activeFeu = (int)Feu.rouge;
        timer = setDuration(FeuRougeDurationMin, FeuRougeDurationMax);
        Debug.Log("Feu rouge actif : " + timer + " secondes");
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
}
