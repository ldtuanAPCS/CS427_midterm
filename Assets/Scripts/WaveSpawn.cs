using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawn : MonoBehaviour
{
    public enum SpawnState
    {
        SPAWNING, WAITING, TIMING        
    };
    [System.Serializable]
    public class Wave{
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }
    public Transform[] EnemySpawnLocation;
    public Wave[] mywaves;
    private int nextWave = 0;
    public float timeToPrepare = 2f;
    public float countdown;
    private float searchTime = 1f;
    private SpawnState state = SpawnState.TIMING;
    void Start (){
        countdown = timeToPrepare;
    }

    void Update() {
        if (state == SpawnState.WAITING){
            if (! CheckEnemyAlive()){
                WaveComplete();
            } else {
                return;
            }
        }
        if (countdown <= 0){
            if (state != SpawnState.SPAWNING){
                StartCoroutine(SpawnWave(mywaves[nextWave]));
            }
        } else {
            countdown -= Time.deltaTime;
        }
    }
    void WaveComplete(){
        state = SpawnState.TIMING;
        countdown = timeToPrepare;
        if (nextWave + 1 >= mywaves.Length - 1){
            nextWave = 0;
        } else {
            nextWave++;
        }
    }
    bool CheckEnemyAlive(){
        searchTime -= Time.deltaTime;
        if (searchTime <= 0){
            searchTime = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null){
                return false;
            }
        }
        return true;
    }
    IEnumerator SpawnWave (Wave mWave){
        state = SpawnState.SPAWNING;
        for (int i = 0; i < mWave.count; i++){
            SpawnEnemy(mWave.enemy);
            yield return new WaitForSeconds(1f/mWave.rate);
        }
        state = SpawnState.WAITING;
        yield break;
    }
    void SpawnEnemy(Transform enemy){        
        Transform sp = EnemySpawnLocation[Random.Range(0, EnemySpawnLocation.Length)];
        Instantiate (enemy, sp.position, sp.rotation);
    }
}
