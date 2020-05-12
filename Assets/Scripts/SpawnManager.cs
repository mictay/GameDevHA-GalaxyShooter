using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject[] _powerUps;

    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private float _spawnRate = 3.0f;

    [SerializeField]
    private float _powerUpRate = 11.0f;

    private bool _stopSpawning = false;

    [SerializeField]
    private AudioSource _explosionSound;

    [SerializeField]
    private int _playerLives = 3;

    private int _help = 0;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        _stopSpawning = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartSpawning()
    {
        StartCoroutine(this.SpawnEnemyRoutine());
        StartCoroutine(this.SpawnPowerUps());
    }

    IEnumerator SpawnEnemyRoutine()
    {

        yield return new WaitForSeconds(3.0f);

        while(!_stopSpawning)
        {
            Vector3 positionToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, positionToSpawn, Quaternion.identity);

            //put the enemy in a different container
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(_spawnRate);
        }

    }

    IEnumerator SpawnPowerUps()
    {
        while (!_stopSpawning)
        {
            Vector3 positionToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);

            int start = 0;

            //Help 3 times...
            if (_playerLives == 1 && _help++ < 3)
            {
                Debug.Log("Helping with a Shield or Health PowerUp:" + _help);
                start = 2;
            }

            int rnd = Random.Range(start, _powerUps.Length);

            //Help 3 times...
            if (_playerLives == 1 && _help++ < 3)
            {
                Debug.Log("Helping with a Health PowerUp:" + _help);
                rnd = 3;
            }

            //rnd = 2; //Shield Only for testing
            //rnd = 3; //Health Only for testing

            GameObject powerup = _powerUps[rnd];

            Instantiate(powerup, positionToSpawn, Quaternion.identity);

            float seconds = Random.Range(_powerUpRate, _powerUpRate * 1.5f);
            Debug.Log("SpawnPowerUps waiting for " + seconds + " seconds");

            yield return new WaitForSeconds(seconds);
        }

    }

    /***************************************
     * If the player is near death, we'll
     * throw a few health powerups to help
     */
    public void SetLives(int lives)
    {
        _playerLives = lives;

        if(_playerLives > 1)
            _help = 0;

    }

    public void PlayerDied()
    {
        _explosionSound.Play();
        _stopSpawning = true;
    }

    public void Restart()
    {
        Start();
    }

}
