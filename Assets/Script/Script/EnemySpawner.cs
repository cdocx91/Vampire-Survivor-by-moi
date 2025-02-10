using UnityEngine;

public class EnemySpawner : MonoBehaviour 
{
    public GameManager gameManager;
    public GameObject EnemyPrefab; // Le prefab de l'ennemi à spawn
    public GameObject StrongEnemyPrefab;
    public Transform[] spawnPoints; // Liste des points de spawn
    public float spawnInterval = 3f; // Intervalle entre chaque spawn en secondes
    public Transform player;
    private float spawnTimer;
    public bool strongEnemySpawned = false; // Vérifie si l'ennemi a été spawn

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            SpawnEnemy(EnemyPrefab);
            spawnTimer = spawnInterval; // Réinitialiser le timer
        }
        if (gameManager.currentTime >= 150f && !strongEnemySpawned) // 2:30 = 150 secondes
        {
            SpawnEnemy(StrongEnemyPrefab);
            strongEnemySpawned = true;
        }
    }

    void SpawnEnemy(GameObject prefab)
    {
        // Choisir un point de spawn aléatoire
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        // Instancier l'ennemi au point choisi
        var enemy = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        enemy.GetComponent<EnemyBehavior>().player = player;
       

        Debug.Log("Enemy Spawned at " + spawnPoint.name);
    }
}
