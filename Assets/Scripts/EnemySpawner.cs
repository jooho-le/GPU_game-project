using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]  
    private GameObject[] enemies; 
    private float[] arrPosY = { -4f, -2f, 2f, 4f };
    [SerializeField]
    private float spawnInterval = 1.5f;

    [SerializeField] public int monstersindex;
    [SerializeField] public int monstercount;

    // 22개의 생성 가능한 위치
    private Vector3[] spawnPositions = new Vector3[] {
        new Vector3(-5f, 3f, 0f),
        new Vector3(-3.5f, 3f, 0f),
        new Vector3(-1.5f, 3f, 0f),
        new Vector3(0f, 3f, 0f),
        new Vector3(1.5f, 3f, 0f),
        new Vector3(3.5f, 3f, 0f),
        new Vector3(5f, 3f, 0f),

        new Vector3(-5f, -3f, 0f),
        new Vector3(-3.5f, -3f, 0f),
        new Vector3(-2f, -3f, 0f),
        new Vector3(0f, -3f, 0f),
        new Vector3(1.5f, -3f, 0f),
        new Vector3(3.5f, -3f, 0f),
        new Vector3(5f, -3f, 0f),

        new Vector3(-5f, 6f, 0f),
        new Vector3(-3f, 6f, 0f),
        new Vector3(0f, 6f, 0f),
        new Vector3(3f, 6f, 0f),
        new Vector3(-3f, -6f, 0f),
        new Vector3(0f, -6f, 0f),
        new Vector3(3f, -6f, 0f),
        new Vector3(5f, 6f, 0f),
    };

    void Start()
    {
        StartEnemyRoutine();
    }

    void StartEnemyRoutine() 
    {
        StartCoroutine(EnemyRoutine());
    }

    IEnumerator EnemyRoutine() 
    {
        yield return new WaitForSeconds(2f);

        int enemyIndex = 0;
        int spawnCount = 0;

        while (true) 
        {
            // 12개의 위치 중 랜덤으로 4개 선택해서 적 생성
            List<Vector3> selectedPositions = new List<Vector3>();
            List<int> selectedIndices = new List<int>(); // 선택된 위치의 인덱스를 추적

            // 랜덤 위치를 선택
            while (selectedPositions.Count < monstercount) 
            {
                int randomIndex = Random.Range(0, spawnPositions.Length);
                if (!selectedIndices.Contains(randomIndex)) // 이미 선택된 위치는 제외
                {
                    selectedPositions.Add(spawnPositions[randomIndex]);
                    selectedIndices.Add(randomIndex);
                }
            }

            // 선택된 위치에 적을 스폰
            foreach (Vector3 spawnPos in selectedPositions)
            {
                SpawnEnemy(spawnPos, enemyIndex); 
            }

            spawnCount += 1;
            

            yield return new WaitForSeconds(spawnInterval);
        }
    }


    void SpawnEnemy(Vector3 spawnPos, int index)
    {
        int randomindex = 0;

        randomindex = Random.Range(0, 10);
        switch (monstersindex)
        {
            case 0:
                if (randomindex < 2)
                    index = 1;
                else
                    index = 0;
                break;

            case 1:
                if (randomindex < 3)
                    index = 0;
                else if (randomindex >= 3 && randomindex < 8)
                    index = 1;
                else
                    index = 2;
                break;

            case 2:
                if (randomindex < 1)
                    index = 0;
                else if (randomindex >= 1 && randomindex < 3)
                    index = 1;
                else if (randomindex >= 3 && randomindex < 8)
                    index = 2;
                else
                    index = 3;
                break;

            case 3:
                if (randomindex < 2)
                    index = 1;
                else if (randomindex >= 2 && randomindex < 7)
                    index = 2;
                else
                    index = 3;
                break;

            case 4:
                if (randomindex < 2)
                    index = 1;
                else if (randomindex >= 2 && randomindex < 4)
                    index = 2;
                else if (randomindex >= 4 && randomindex < 9)
                    index = 3;
                else
                    index = 4;
                break;
        }



        Instantiate(enemies[index], spawnPos, Quaternion.identity);

    }



}
