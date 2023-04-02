using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardGen : MonoBehaviour
{
    public GameObject hazardAnt;

    private float timer = 0;
    private float timerLimit = 3;
    private float timerDelta = 0.001f;
    private bool enableStart = false;

    void Start()
    {
        Invoke(nameof(Enable), 2f);
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (Time.timeScale > 0 && timer > timerLimit && enableStart)
        {
            timer = 0;
            timerLimit -= timerDelta;
            createAnt();

            if (timerLimit > 0.1f)
            {

            }

            if (timerLimit < 0.9f)
            {
                createAnt();
            }

            if (timerLimit < 0.8f)
            {
                createAnt();
            }
        }
    }

    private void Enable()
    {
        enableStart = true;
    }

    public void createAnt()
    {
        float x = Random.Range(-15f, 15f);
        Instantiate(hazardAnt, new Vector3(x, 5, 0), Quaternion.identity);
    }
}
