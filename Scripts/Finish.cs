using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishAudio; // victory audio
    private bool finishLevel = false; //
    private void Start()
    {
        finishAudio = GetComponent<AudioSource>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !finishLevel)
        {
            finishAudio.Play();
            finishLevel = true; // Audio will not keep playing since level is already finished
            Invoke("CompleteLevel", 1.5f);//Helps the switch from each level not be so abrupt
        }
    }

    private void CompleteLevel()
    {
        //Go to next level when finish line is reached
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
