using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    Animator transitionAnimator;
    public float loadDelay;
    private void Awake()
    {
        if (instance == null) //Checks if there is another GameManager instance
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // if there is, this gameObject gets destroyed
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play("BG Music");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LoadNextScene()
    {
        transitionAnimator = GameObject.Find("ScreenTransition").GetComponent<Animator>();
        transitionAnimator.SetTrigger("LevelFinished");
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("BaseLevel", LoadSceneMode.Additive);
        
    }

    public IEnumerator RestartLevel()
    {
        transitionAnimator = GameObject.Find("ScreenTransition").GetComponent<Animator>();
        transitionAnimator.SetTrigger("LevelFinished");
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("BaseLevel", LoadSceneMode.Additive);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("BaseLevel", LoadSceneMode.Additive);
        Debug.Log("Play");
    }
}
