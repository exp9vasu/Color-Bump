using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private Image fillBarProgress;
    private float lastValue;

    // Update is called once per frame
    void Update()
    {
        /*if (!GameManager.instance.GameStarted)
            return;*/

        float traveledDistance = GameManager.instance.EntireDistance - GameManager.instance.DistanceLeft;
        float value = traveledDistance / GameManager.instance.EntireDistance;

        /*if (GameManager.instance.GameEnded && value < lastValue)
            return;*/

        fillBarProgress.fillAmount = Mathf.Lerp(fillBarProgress.fillAmount, value, 5 * Time.deltaTime);

        lastValue = value; 
    }
}
