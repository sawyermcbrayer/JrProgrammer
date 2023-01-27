using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRb;
    public float forwardInput;
    public float speed = 5.0f;
    public float jumpForce = 200f;
    public float gravityModifier;

    private GameObject focalPoint;

    // Start is called before the first frame update
    void Start()
    {
        focalPoint = GameObject.Find("Focal Point");

        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }



    public GameObject powerUpIndicator;
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -5)
        {
            PlayerReset();
        }
        powerUpIndicator.transform.position = transform.position + new Vector3(0, 1f, 0);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
    }

    public bool hasPowerUp = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            powerUpIndicator.gameObject.SetActive(true);
            hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDownRoutine());
        }

    }
    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        powerUpIndicator.gameObject.SetActive(false);
    }
    private float powerUpStrength = 15f;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log("Collided with " + collision.gameObject.name + " with power-up set to: " + hasPowerUp);
            enemyRb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
        }
    }
    public void PlayerReset()
    {
        transform.position = new Vector3(0, 1, 0);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

    }
}
