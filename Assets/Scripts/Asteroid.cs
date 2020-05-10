using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField]
    float _rotationSpeed = 3.0f;

    [SerializeField]
    GameObject _prefabExplosion;

    [SerializeField]
    SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1), _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {

            Player player = other.transform.GetComponent<Player>();

            if (player != null)
                player.Damage();

            this.gameObject.GetComponent<Collider2D>().enabled = false;

            Instantiate(_prefabExplosion, this.transform.position, Quaternion.identity);
            _spawnManager.StartSpawning();

            Destroy(this.gameObject, 2.8f);

        }
        else if (other.tag == "Laser")
        {
            this.gameObject.GetComponent<Collider2D>().enabled = false;

            Instantiate(_prefabExplosion, this.transform.position , Quaternion.identity);
            _spawnManager.StartSpawning();

            Destroy(other.gameObject);
            Destroy(this.gameObject, .05f);
        }

    }



}
