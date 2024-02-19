using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> CurrentEnemies = new List<GameObject>();

    [SerializeField] List<GameObject> EnemyKinds = new List<GameObject>();

    [SerializeField] int EnemySpawnNumEveryTurn = 5;
    [SerializeField] float EnemySpawnInterval = 5;
    [SerializeField] float PossibilityOfEnemy2 = 0.3f;

    private GameObject[] enemies;
    private float EnemyInitialDistanceToPoints;



    void Start()
    {
        enemies = EnemyKinds.ToArray();
        EnemyInitialDistanceToPoints = DataManager.datas.EnemyInitialDistanceToPoints;

        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        int NumOfEnemy02 = Mathf.FloorToInt(EnemySpawnNumEveryTurn * PossibilityOfEnemy2);
        int NumOfEnemy01 = EnemySpawnNumEveryTurn - NumOfEnemy02;

        for (int i = 0; i < NumOfEnemy01; i++)
        {
            Vector3 pos = GetRandomPosition();
            if (pos != Vector3.zero)
            {
                GameObject temp = Instantiate(enemies[0], pos, Quaternion.identity);
                CurrentEnemies.Add(temp);
            }
        }

        for (int i = 0; i < NumOfEnemy02; i++)
        {
            Vector3 pos = GetRandomPosition();
            if (pos != Vector3.zero)
            {
                GameObject temp = Instantiate(enemies[1], pos, Quaternion.identity);
                CurrentEnemies.Add(temp);
            }
        }

        yield return new WaitForSecondsRealtime(EnemySpawnInterval);
        StartCoroutine(Spawn());
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 Pos = Vector3.zero;
        DashPoint[] points = FindObjectsOfType<DashPoint>();

        for (int j = 0; j < 100; j++)
        {
            float x = Random.Range(-10f, 10f);
            float y = Random.Range(-7f, 7f);

            foreach (var point in points)
            {
                if (Vector2.Distance(point.transform.position, new Vector2(x, y)) >= EnemyInitialDistanceToPoints)
                {
                    Pos = new Vector3(x, y, 0);
                    break;
                }
                else Pos = Vector3.zero;
            }
        }

        return Pos;
    }
}