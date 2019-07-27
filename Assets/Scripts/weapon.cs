using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public float effectSpawnRate = 10;
    public float FireRate = 0;
    public int Damage = 10;
    public LayerMask allowToHit;
    public Transform Bullet_trail_prefab;
    public Transform GunMuzzlePrefab;
    float timeToSpawnEffect = 0;
    float timetofire = 0;
    Transform firePoint;
    // Start is called before the first frame update
    void Awake()
    {
        firePoint = transform.Find("Firepoint");
    }

    // Update is called once per frame
    void Update()
    {        
        if (FireRate == 0){
            if (Input.GetButtonDown("Fire1")){
                Shoot();
            }
        } else {
            if (Input.GetButton("Fire1") && Time.time > timetofire){
                timetofire = Time.time + 1/FireRate;
                Shoot();
            }
        }
    }

    void Shoot(){
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointpos = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointpos, mousePosition - firePointpos, 100, allowToHit);
              
        Debug.DrawLine(firePointpos, (mousePosition - firePointpos)*100, Color.cyan); 
        if (hit.collider != null){
            Debug.DrawLine(firePointpos, hit.point, Color.red);
            EnemySpaceship enemy = hit.collider.GetComponent<EnemySpaceship>();
            if (enemy != null){
                enemy.DamageToEnemy(Damage);
            }
        }
        if (Time.time >= timeToSpawnEffect){
            Vector3 hit_pos;
            if (hit.collider == null){
                hit_pos = (mousePosition - firePointpos)*30;
            } else {
                hit_pos = hit.point;
            }
            

            Effect(hit_pos);
            timeToSpawnEffect = Time.time + 1/effectSpawnRate;
        }  
    }

    void Effect(Vector3 hit_pos){
        Transform trail = Instantiate (Bullet_trail_prefab, firePoint.position, firePoint.rotation) as Transform;
        LineRenderer lr = trail.GetComponent<LineRenderer>();
        
        if (lr != null){
            lr.SetPosition(0, firePoint.position);
            lr.SetPosition(1, hit_pos);
        }
        Destroy(trail.gameObject, 0.04f);

        Transform clone = Instantiate (GunMuzzlePrefab, firePoint.position, firePoint.rotation) as Transform;
        clone.parent = firePoint;
        float size = Random.Range(0.2f, 0.3f);
        clone.localScale = new Vector3 (size,size,size);
        Destroy(clone.gameObject, 0.02f);
    }
}
