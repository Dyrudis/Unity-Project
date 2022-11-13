using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject timer;

    private bool isFinished = false;

    // Update is called once per frame
    void Update()
    {
        if (!isFinished)
        {
            // Update the timer as "MM:SS"
            timer.GetComponent<TextMeshProUGUI>().text = string.Format("Time : {0:00}:{1:00}", Mathf.Floor(Time.timeSinceLevelLoad / 60), Mathf.Floor(Time.timeSinceLevelLoad % 60));
        }
    }

    public void FinishGame()
    {
        // Change the color of the timer
        timer.GetComponent<TextMeshProUGUI>().color = Color.green;

        // Change the text of the timer
        timer.GetComponent<TextMeshProUGUI>().text = "FINISHED !\nYour time : " + string.Format("{0:00}:{1:00}", Mathf.Floor(Time.timeSinceLevelLoad / 60), Mathf.Floor(Time.timeSinceLevelLoad % 60));

        isFinished = true;
    }
}
