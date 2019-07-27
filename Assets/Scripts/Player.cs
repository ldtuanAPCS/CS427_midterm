using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats {
       public int maxHP = 10000000;
       private int _currentHP;
       public int currentHP{
           get { return _currentHP; }
           set { _currentHP = Mathf.Clamp(value, 0 , maxHP);}
       }
       public void Init(){
           currentHP = maxHP;
       }
    }
    public int fallBoundary = -10;
    public PlayerStats mplayerStats = new PlayerStats();

    void Update (){
        if (transform.position.y <= fallBoundary){
            DamageToPlayer(200);
        }
    }
    public void DamageToPlayer (int value){
       mplayerStats.currentHP -= value;
        if (mplayerStats.currentHP <= 0){
            GameMasterAdmin.KillPlayer(this);
        }
    }
}
