
using TMPro;
using UnityEngine;

public class overlymanager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText,ScoreText2,TimerText,TimerText2;
    private PlayerManager pm;

    private float Min, Sec;
    private bool TimerOn = false;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        /*ScoreText = GetComponentInChildren<TextMeshProUGUI>();
        TimerText = GetComponentInChildren<TextMeshProUGUI>();
        ScoreText2 = GetComponentInChildren<TextMeshProUGUI>();
        TimerText2 = GetComponentInChildren<TextMeshProUGUI>();
        foreach(var TextMeshProUGUI in gm.GetComponentInChildren<TextMeshProUGUI>())
        {
            if(TextMeshProUGUI.name == "ScoreText") ScoreText = GetComponentInChildren<TextMeshProUGUI>();
            else if(TextMeshProUGUI.name == "TimerText") TimerText = GetComponentInChildren<TextMeshProUGUI>();
            else if(TextMeshProUGUI.name == "ScoreText2") ScoreText2 = GetComponentInChildren<TextMeshProUGUI>();
            else if(TextMeshProUGUI.name == "TimerText2") TimerText2 = GetComponentInChildren<TextMeshProUGUI>();
        }*/
        pm = FindObjectOfType<PlayerManager>();
        Min =Sec= 0;
    }

    // Update is called once per frame
    void Update()
    {
        score = pm.CurrentScore;
        ScoreText2.text = ScoreText.text = ("Score : " + score);
        TimerText2.text=TimerText.text = "Time: " + (int)Min + ":" + (int)Sec;
        if(TimerOn) Sec += Time.deltaTime;
        if (Sec >= 60)
        {
            Sec -= 60;
            Min++;
        }
    }

    public void TimerReset()
    {
        TimerOn = true;
        Sec = Min = 0;
    }

    public void TimerStop()
    {
        TimerOn = false;
    }
    
}
