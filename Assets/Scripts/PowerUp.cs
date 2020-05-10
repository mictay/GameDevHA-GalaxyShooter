using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = .5f;

    [SerializeField]
    private int powerupId;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.tag == "Player")
        {

            Debug.Log("Collider powerupId =" + powerupId);

            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {

                switch (powerupId)
                {
                    case 0:
                        player.TripleShotPowerUp();
                        break;
                    case 1:
                        player.SpeedPowerUp();
                        break;
                    case 2:
                        player.ShieldPowerUp();
                        break;
                    case 3:
                        player.HealthPowerUp();
                        break;
                    default:
                        Debug.Log("default value switch");
                        break;
                }

            }

            // only dissappear if we get hit by the player
            Destroy(this.gameObject);
        }
        

    }

}
