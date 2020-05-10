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
    private float _powerUpRate = 8.0f;

    private bool _stopSpawning = false;

    [SerializeField]
    private AudioSource _explosionSound;

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

            int rnd = Random.Range(0, _powerUps.Length);
            Debug.Log("Powerup Spawn = " + rnd);

            GameObject powerup = _powerUps[rnd];

            Instantiate(powerup, positionToSpawn, Quaternion.identity);

            float seconds = Random.Range(_powerUpRate, _powerUpRate * 2);
            Debug.Log("SpawnPowerUps waiting for " + seconds + " seconds");

            yield return new WaitForSeconds(seconds);
        }

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
