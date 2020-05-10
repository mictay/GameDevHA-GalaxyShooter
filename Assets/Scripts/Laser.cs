using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField]
    private float _speed = 20f;
    private Vector3 _direction;

    private bool _enemyLaser = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_direction != null) {
            transform.Translate(_direction * _speed * Time.deltaTime);

            if (transform.position.y > 10f || transform.position.y < -10f)
            {

                if (transform.parent != null)
                    Destroy(transform.parent.gameObject);
                else
                    Destroy(this.gameObject);
            }

        };

    }

    public void setDirectionUp(bool up)
    {
        if(up)
        {
            _direction = new Vector3(0, 1, 0);
        } else
        {
            _enemyLaser = true;
            _direction = new Vector3(0, -1, 0);
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && other.GetComponent<Player>() != null && _enemyLaser)
        {
            other.GetComponent<Player>().Damage();
        }
    }

}
