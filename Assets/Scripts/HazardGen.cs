using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardGen : MonoBehaviour
{
    public GameObject hazardAnt;

    private float timer = 0;
    private float timerLimit = 1;
    private float timerDelta = 0.01f;
    private float mass = 1;
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
            createSword();

            if (timerLimit > 0.1f)
            {

            }

            if (timerLimit < 0.9f)
            {
                createSword();
                mass += 0.1f;
            }

            if (timerLimit < 0.8f)
            {
                createSword();
            }
        }
    }

    private void Enable()
    {
        enableStart = true;
    }

    public void createSword()
    {
        float x = Random.Range(-6.5f, 6.5f);
        //hazardAnt sword = new hazardAnt(new Vector3(x, 5, 0), hazardAnt, gameObject, mass);
    }
}
