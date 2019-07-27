using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Seeker))]
public class EnemyFunctions : MonoBehaviour
{
    public Transform target;
    public float UpdateRate = 2f;
    private Seeker seeker;
    private Rigidbody2D rigidbody;
    public Path mypath;
    public float speed = 200f;
    public ForceMode2D fmode;
    [HideInInspector]
    public bool EndPath = false;
    public float nextWayPointDistance = 3;
    private int currentWayPoint = 0;
    private bool OnTarget = false;

    void Start() {
        seeker = GetComponent<Seeker>();
        rigidbody = GetComponent<Rigidbody2D>();
        if (target == null){
            if (! OnTarget){
                OnTarget = true;
                StartCoroutine(SearchPlayer());
            }
            return;
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        StartCoroutine(UpdatePath());
    }
    IEnumerator SearchPlayer(){
        GameObject res = GameObject.FindGameObjectWithTag("Player");
        if (res == null){
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(SearchPlayer());
        } else {
            target = res.transform;
            OnTarget = false;
            StartCoroutine(UpdatePath());
            yield return false;
        }
    }

    IEnumerator UpdatePath(){
        if (target == null){
            if (! OnTarget){
                OnTarget = true;
                StartCoroutine(SearchPlayer());
            }
            yield return false;
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        yield return new WaitForSeconds (1f/UpdateRate);
        StartCoroutine(UpdatePath());
    }

    void OnPathComplete(Path p){
        if (! p.error){
            mypath = p;
            currentWayPoint = 0;
        }
    }

    void FixedUpdate() {
        if (target == null){
            if (! OnTarget){
                OnTarget = true;
                StartCoroutine(SearchPlayer());
            }
            return;
        }
        if (mypath == null) {
            return;
        }
        if (currentWayPoint >= mypath.vectorPath.Count){
            if (EndPath){
                return;
            }
            EndPath = true;
            return;
        }
        EndPath = false;
        Vector3 dir = (mypath.vectorPath[currentWayPoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;
        rigidbody.AddForce(dir, fmode);

        float distance = Vector3.Distance(transform.position, mypath.vectorPath[currentWayPoint]);
        if (distance < nextWayPointDistance){
            currentWayPoint++;
            return;
        }
    }
}
