using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject timer;

    // Update is called once per frame
    void Update()
    {
        // Update the timer as "MM:SS"
        timer.GetComponent<TextMeshProUGUI>().text = string.Format("{0:00}:{1:00}", Mathf.Floor(Time.timeSinceLevelLoad / 60), Mathf.Floor(Time.timeSinceLevelLoad % 60));
    }

    public void FinishGame()
    {
        // Stop the timer
        Time.timeScale = 0;

        // Change the color of the timer
        timer.GetComponent<TextMeshProUGUI>().color = Color.green;
    }
}
