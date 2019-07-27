using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemySpaceship : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats {
        public int damageValue = 0;
       public int maxHP = 100;
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
    public EnemyStats stats = new EnemyStats();
    public void DamageToEnemy (int value){
       stats.currentHP -= value;
        if (stats.currentHP <= 0){
            GameMasterAdmin.KillEnemy(this);
        }
    }

    void OnCollisionEnter2D(Collision2D info){
        Player mPlayer = info.collider.GetComponent<Player>();
        if (mPlayer != null){
            mPlayer.DamageToPlayer(stats.damageValue);
            //DamageToEnemy(9999);
        }
    }
}
