using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections;

public class Clocks : MonoBehaviour 
{
    // public GameObject Праздник, Непраздник;
    //public Text txt;
    public bool Activated;
    public Text TimeLable;
    public DateTime Time, FinalTime, LastTime;
    public int
        CurYear,
        CurMonth,
        CurDay = 0,
        CurHour,
        CurMinunte,
        CurMSec,
        LastSec,
        LastMin,
        LastHour,
        LastDay,
        LastMonth,
        LastYear,
        NeedYear,
        NeedMonth,
        NeedDay,
        NeedHour,
        NeedMinute,
        NeedSecond;

    private const int
        SECOND = 60,
        MINUTE = 60,
        HOUR = 23,
        DAY = 30,
        MONTH = 12;

    public GameLevel Scripts;


    // Use this for initialization
    void Start()
    {
       // ClocksStart();
        DisplayTime();
    }


    public void ClocksStart()
    {
        Time = DateTime.Now;
        // FinalTime = new DateTime(NeedYear, NeedMonth, NeedDay, NeedHour, NeedMinute, NeedSecond);
        TimeLable = GameObject.Find("Time_Energy_Left").GetComponent<Text>();

        CurYear = Time.Year;
        CurMonth = Time.Month;
        CurDay = Time.Day;
        CurHour = Time.Hour;
        CurMinunte = Time.Minute;
        CurMSec = Time.Second;

        InvokeRepeating("ClocksUpdate", 0, 60);

        Activated = true;     
    }

    // Update is called once per frame
    void ClocksUpdate()
    {
        Time = DateTime.Now;

        CurYear = Time.Year;
        CurMonth = Time.Month;
        CurDay = Time.Day;
        CurHour = Time.Hour;
        CurMinunte = Time.Minute;
        CurMSec = Time.Second;

        if (CurYear == NeedYear && CurMonth == NeedMonth && CurDay >= NeedDay && CurHour >= NeedHour && CurMinunte >= NeedMinute)
            DONE();


        if (NeedYear > CurYear)
            LastYear = NeedYear - CurYear;


        else if (NeedYear <= CurYear)
            LastYear = 0;

        if (NeedMonth > CurMonth)
            LastMonth = NeedMonth - CurMonth;

        else if (NeedMonth <= CurMonth)
            LastMonth = 0;

        else if (NeedMonth == CurMonth)
            LastMonth = 0;


        if (NeedDay > CurDay)
        {
            LastDay = NeedDay - CurDay;
            if (NeedDay > CurDay && CurDay > 0)
                LastDay--;
        }


        else if (NeedDay < CurDay || CurDay == 0 && NeedMonth > CurMonth && CurDay != NeedDay)
        {
            LastDay = DAY - CurDay + NeedDay;
            if (NeedDay > CurDay && CurDay > 0)
                LastDay--;
        }


        else if (NeedDay == CurDay)
        {
            LastDay = 0;
        }

        if (NeedHour > CurHour)
        {
            LastHour = NeedHour - CurHour;
            if (NeedHour > CurHour && CurHour > 0)
                LastHour--;
        }

        else if (NeedHour < CurHour || CurHour == 0 && NeedHour > CurHour && CurHour != NeedHour)
        {
            LastHour = HOUR - CurHour + NeedHour;
            if (NeedHour > CurHour && CurHour > 0)
                LastHour--;
        }

        else if (NeedHour == CurHour)
            LastHour = 0;

        if (NeedMinute > CurMinunte)
            LastMin = NeedMinute - CurMinunte;

        else if (NeedMinute < CurMinunte || CurMinunte == 0 && NeedMinute > CurMinunte && CurMinunte != NeedMinute)
            LastMin = MINUTE - CurMinunte + NeedMinute;

        else if (NeedMinute == CurMinunte)
            LastMin = 0;
        
        DisplayTime();
    }


    public void DONE()
    {
        //TimeLable.text = "Full Energy";
        Activated = false;
        CancelInvoke();
        Variables.AddEnergy();
        Scripts.Display_Values();
    }

    public void DisplayTime()
    {
        String txt = Convert.ToString(CreateTimeSpan(LastDay, LastHour, LastMin));
        TimeLable.text = txt;
    }

    /// <summary>
    /// Устанавливаем часы на восстановление энерегии в минутах
    /// </summary>
    /// <param name="SetMinute"></param>
    public void SetCoulDown(int SetMinute)
    {

        Time = DateTime.Now;
        int SetHours = 0;

        if (SetMinute > MINUTE)
        {
            SetHours = SetMinute / MINUTE;
            SetMinute = SetMinute - SetHours * 60;
        }

        if (SetHours > HOUR)
            Debug.LogError("Глупый геймдизайнер, значение SetMinute не может превышать 23 часов! Ты всё сломаешь своими кривыми руками!");

        NeedYear = Time.Year;
        NeedMonth = Time.Month;
        NeedDay = Time.Day;

        NeedHour = Time.Hour + SetHours;
        if (NeedHour > HOUR)
        {
            NeedHour -= HOUR;
            NeedDay++;
        }

        NeedMinute = Time.Minute + SetMinute;

        if (NeedMinute > MINUTE)
        {
            NeedMinute -= MINUTE;
            NeedHour++;
        }
        NeedSecond = 0;

        ClocksStart();
    }

    TimeSpan CreateTimeSpan(int days, int hours, int minutes)
    {
        TimeSpan elapsedTime =
            new TimeSpan(days, hours, minutes);

        // Format the constructor for display.
        string ctor =
            String.Format("TimeSpan( {0}, {1}, {2} )",
                days, hours, minutes);

        return elapsedTime;
    }
}
