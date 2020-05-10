using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private bool _isGameOver = false;

    void Update()
    {
        if ( (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.Mouse0)) && _isGameOver == true)
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKey(KeyCode.Escape) && _isGameOver == false)
        {
            SceneManager.LoadScene("MainMenu");
        }

        if ( Input.GetKey(KeyCode.P) && _isGameOver == false)
        {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;            
        }

        if (Input.GetKey(KeyCode.Escape) && _isGameOver == true)
        {
            Application.Quit();
        }

    }

    public void GaveOver()
    {
        _isGameOver = true;
        GameObject.Find("SpawnManager").GetComponent<SpawnManager>().PlayerDied();
    }

}
