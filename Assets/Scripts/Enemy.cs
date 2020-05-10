using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    [SerializeField]
    public AudioSource _explosionSound;

    Player _player;

    Animator _anim;

    [SerializeField]
    private float _laserOffset = -1.0f;

    [SerializeField]
    private GameObject _laserPrefab;

    private bool _canFire = true;

    [SerializeField]
    private float AMPLITUDE = 2.0f;

    private float zigzagX = 0f;
    private Vector3 zigzagPosition;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        _anim = this.GetComponent<Animator>();

        _explosionSound = GameObject.Find("ExplosionSound").GetComponent<AudioSource>();

        //StartCoroutine(StartRandomFireRoutine());
        //transform.position = new Vector3(0, 0, 0);
    }

    void Awake()
    {
        StartCoroutine(StartRandomFireRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        zigzagX += AMPLITUDE * Time.deltaTime;

        zigzagPosition = new Vector3(Mathf.Cos(zigzagX), -1, 0);

        //zigzagPosition = Vector3.down * _speed * Time.deltaTime;

        //zipzagPosition.x = AMPLITUDE * Mathf.Cos(zipzagX += Time.deltaTime);

        //transform.Translate(Vector3.down * _speed * Time.deltaTime);
        transform.Translate(zigzagPosition * _speed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            transform.position = new Vector3(Random.Range(-9.0f, 9.0f) , 7, 0);
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {        

        if(other.tag == "Player")
        {
            _canFire = false;
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
                player.Damage();

            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _explosionSound.Play();
            Destroy(this.gameObject, 2.8f);

        } else if (other.tag == "Laser")
        {
            _canFire = false;
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            _player.AddScore(Random.Range(5, 9));
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;            
            _explosionSound.Play();
            Destroy(other.gameObject);
            Destroy(this.gameObject, 2.8f);
        }

    }

    /******************************************************************************************
     * 
     */
    void DoFire()
    {

        Vector3 offset2 = new Vector3(transform.position.x, transform.position.y + _laserOffset, 0);

        if(_laserPrefab != null)
        {
            if(_canFire)
                Instantiate(_laserPrefab, offset2, Quaternion.identity).GetComponent<Laser>().setDirectionUp(false);
        } else
        {
            Debug.Log("Enemy Laser Prefab is NULL.");
        }

    }

    /******************************************************************************************
     * 
     */
    IEnumerator StartRandomFireRoutine()
    {
        Debug.Log("StartRandomFireRoutine called");

        while (_canFire)
        {
            yield return new WaitForSeconds(Random.Range(.25f, 2));

            if (_canFire)
                DoFire();
        }

    }

}
