using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public Transform selfCollider;
    public LayerMask m_WhatIsPlayer;
    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(selfCollider.position, 0.1f, m_WhatIsPlayer);

        for (int i = 0; i < colliders.Length; i++)
        {

            if (colliders[i].gameObject != gameObject)
            {
                text.SetActive(true);
            }
        }
    }
}
