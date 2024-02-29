using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private Transform obstacleParent;

    public float obstacleSpawnTime = 3f;
    [Range(0, 1)] public float obstacleSpawnTimeFactor = 0.1f;
    public float obstacleSpeed = 4f;
    [Range(0, 1)] public float obstacleSpeedFactor = 0.2f;

    private float _obstacleSpawnTime;
    private float _obstacleSpeed;

    private float timeAlive;
    private float timeUntilObstacleSpawn;

    private void Start()
    {
        GameManager.Instance.onGameOver.AddListener(ClearObstacle);
        GameManager.Instance.onPlay.AddListener(ResetFactor);
      
    }
    private void Update()
    {
        if(GameManager.Instance.isPlaying)
        {

            timeAlive += Time.deltaTime;

            CalculateFactor();

            Spawnloop();

        }
    }

    private void Spawnloop()
    {
        timeUntilObstacleSpawn += Time.deltaTime;

        if( timeUntilObstacleSpawn > obstacleSpawnTime )
        {
            Spawn();
            timeUntilObstacleSpawn = 0;         
        }
    }

    private void ClearObstacle()
    {
        foreach( Transform child in obstacleParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void CalculateFactor()
    {
        _obstacleSpawnTime = obstacleSpawnTime / Mathf.Pow(timeAlive, obstacleSpawnTimeFactor);
        _obstacleSpeed = obstacleSpeed * Mathf.Pow(timeAlive, obstacleSpeedFactor);
    }

    private void ResetFactor()
    {
        timeAlive = 1f;
        _obstacleSpawnTime = obstacleSpawnTime;
        _obstacleSpeed = obstacleSpeed;
    }
    private void Spawn()
    {
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0,obstaclePrefabs.Length)];

        GameObject spawnObstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);
        spawnObstacle.transform.parent = obstacleParent;    

        Rigidbody2D obstacleRB = spawnObstacle.GetComponent<Rigidbody2D>();

        obstacleRB.velocity = Vector2.left * obstacleSpeed;


    }
}
