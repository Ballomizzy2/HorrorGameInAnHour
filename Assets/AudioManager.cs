using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Found()
    {
        GetComponent<AudioSource>().Play();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
