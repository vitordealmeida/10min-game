using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float forwardSpeed;
    public float playerMovement;
    public float acceleration;
    public Text score;
    public GameObject obstaclePrefab;

    private bool _isAlive;
    private float _initialPosition;
    private readonly List<float> _obstaclePositions = new List<float>();
    
    private void Start()
    {
        _isAlive = true;
        _initialPosition = 0;
        
        SpawnObstacle(5);
        SpawnObstacle();
    }

    private void Update()
    {
        if (_isAlive)
        {
            transform.position += new Vector3(Input.GetAxis("Horizontal") * playerMovement, Input.GetAxis("Vertical") * playerMovement, forwardSpeed) *
                                  Time.deltaTime;
            score.text = transform.position.z.ToString("0");
            forwardSpeed += acceleration * Time.deltaTime;
            if (transform.position.z >= _obstaclePositions[0])
            {
                _obstaclePositions.RemoveAt(0);
                SpawnObstacle();
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            _isAlive = false;
            GetComponent<Rigidbody>().useGravity = true;
            Invoke(nameof(RestartGame), 3);            
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SpawnObstacle()
    {
        SpawnObstacle(_obstaclePositions[0] + 3 + forwardSpeed);
    }

    private void SpawnObstacle(float distance)
    {
        var obstacle = Instantiate(obstaclePrefab);
        obstacle.transform.position = new Vector3(0, 0, distance);
        _obstaclePositions.Add(distance);
    }
}