using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectPlayer : MonoBehaviour
{
    public Transform selfCollider;
    public LayerMask m_WhatIsPlayer;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject text4;
    public GameObject player;
    private bool text2Showing = false;
    private bool text3Showing = false;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<PlayerMovement>().canAttack = false;
    }

    // Update is called once per frame
    void Update()
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(selfCollider.position, 0.1f, m_WhatIsPlayer);

        for (int i = 0; i < colliders.Length; i++)
        {

            if (colliders[i].gameObject != gameObject && text2Showing == false)
            {
                text1.SetActive(false);
                text2.SetActive(true);
                text2Showing = true;
                player.GetComponent<PlayerMovement>().canAttack = true;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        if (Input.GetKeyDown("l") && text2Showing == true && text3Showing == false)
        {
            text2.SetActive(false);
            text3.SetActive(true);
            text3Showing = true;
        }

        if (Input.GetKeyDown("p") && text3Showing == true)
        {
            text3.SetActive(false);
            text4.SetActive(true);
            Invoke("NextLevel", 5);
        }

    }


    void NextLevel()
    {
        int index = Random.Range(1, 5);
        SceneManager.LoadScene(index);
    }
}
