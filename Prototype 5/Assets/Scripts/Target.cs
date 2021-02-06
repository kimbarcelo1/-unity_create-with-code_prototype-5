using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody rb;
    private float minSpeed = 12.0f;
    private float maxSpeed = 16.0f;
    private float torque = 10.0f;
    private float xSpawnPosition = 4.0f;
    private float ySpawnPosition = 2.0f;
    private GameManager gameManager;

    public int pointValue;
    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();

        rb.AddForce(RandomForce(), ForceMode.Impulse);
        rb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPosition();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            gameManager.IncreaseScore(pointValue);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-torque, torque);
    }

    Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-xSpawnPosition, xSpawnPosition), -ySpawnPosition);
    }
}
