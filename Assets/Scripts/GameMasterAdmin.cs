using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterAdmin : MonoBehaviour
{
    public static GameMasterAdmin gm;
    void Start(){
        if (gm == null){
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMasterAdmin>();
        }
    }
    public Transform playerPrefab;
    public Transform spawnLocation;
    public GameObject spawnPrefab;
    public float SpawnDelay = 2f;
    public IEnumerator RespawnPlayer(){
        yield return new WaitForSeconds(SpawnDelay);
        GetComponent<AudioSource>().Play();
        Instantiate (playerPrefab, spawnLocation.position, spawnLocation.rotation);
        GameObject clone = Instantiate(spawnPrefab, spawnLocation.position, spawnLocation.rotation) as GameObject;
        Destroy(clone, 3f);
    }
    
    public static void KillPlayer (Player mPlayer){
        Destroy(mPlayer.gameObject);
        gm.StartCoroutine(gm.RespawnPlayer());
    }
    public static void KillEnemy (EnemySpaceship enemy){
        gm._KillEnemy(enemy);
    }
    public void _KillEnemy (EnemySpaceship _enemy){
        Destroy(_enemy.gameObject);
    }
}
