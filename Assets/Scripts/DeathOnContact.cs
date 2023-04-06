using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathOnContact : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("dead");
            Destroy(gameObject);
           // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            int index = Random.Range(1, 5);
            SceneManager.LoadScene(index);
        }
        else if (!other.gameObject.CompareTag("Enemy")) { Destroy(gameObject); }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("dead");
            Destroy(gameObject);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            int index = Random.Range(1, 5);
            SceneManager.LoadScene(index);
        }
        else if (!other.gameObject.CompareTag("Enemy")){ Destroy(gameObject);}
    }

}

