using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [System.Serializable]
    //몬스터 스폰 정보를 담은 변수
    public class SpawnInfo
    {
        public GameObject monsterPrefab;
        public Transform spawnPoint;
    }

    public List<SpawnInfo> spawnInfos = new List<SpawnInfo>();

    private int currentIndex = 0;

    private void Start()
    {
        SpawnMonster(currentIndex);
    }

    public void MonsterDied()
    {
        currentIndex++;

        if(currentIndex < spawnInfos.Count)
        {
            SpawnMonster(currentIndex);
        }
    }
    public void SpawnMonster(int index)
    {
        SpawnInfo spawnInfo = spawnInfos[index];
        GameObject monster = Instantiate(spawnInfo.monsterPrefab, spawnInfo.spawnPoint.position, Quaternion.identity);
        monster.GetComponent<Character>().spawner = this;
    }
}
