using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameManager _gameManager;

    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Text _gameOverText;

    [SerializeField]
    private Text _restartText;

    [SerializeField]
    private Text _powerUpText;

    [SerializeField]
    private Image _livesDisplayImage;

    [SerializeField]
    private Sprite[] _livesSprites;

    // Start is called before the first frame update
    void Start()
    {

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _scoreText.text = "Score: 0";
        _livesDisplayImage.sprite = _livesSprites[3];
        _gameOverText.gameObject.SetActive(false);
        _gameOverText.text = "GAME OVER";
        _restartText.gameObject.SetActive(false);
        _restartText.text = "Press R to restart game";

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }

    public void UpdatePowerUpLevel(string title, int level)
    {
        if(level == 0)
        {
            _powerUpText.gameObject.SetActive(false);
        } else
        {
            _powerUpText.text = title + ": " + level;
            _powerUpText.gameObject.SetActive(true);
        }

    }

    public void SetLives(int lives)
    {

        if(lives < _livesSprites.Length)
            _livesDisplayImage.sprite = _livesSprites[lives];

        if(lives == 0)
        {
            _gameManager.GaveOver();
            _gameOverText.gameObject.SetActive(true);
            _restartText.gameObject.SetActive(true);
            StartCoroutine(GameOverFlickerRoutine(.1f, .9f, 1.0f));
        }

    }

    IEnumerator GameOverFlickerRoutine(float aValue1, float aValue2, float aTime)
    {
        float alpha = _gameOverText.color.a;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(_gameOverText.color.r, _gameOverText.color.g, _gameOverText.color.b, Mathf.Lerp(alpha, aValue1, t));
            _gameOverText.color = newColor;
            yield return null;
        }

        //Do it again, flip the values
        if (_gameOverText.gameObject.activeSelf)
        {
            StartCoroutine(GameOverFlickerRoutine(aValue2, aValue1, aTime));
        }

    }

}
