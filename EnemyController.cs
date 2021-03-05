using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    public Transform player;
    NavMeshAgent agent;

    bool trackingPlayer;

    //patrol code
    bool patrolSet;
    public float maxPatrolDist;
    public float maxPatrolRad;
    public int numPatrolPoints;
    public Vector3[] patrolPoints;
    int point = 1;


    NavMeshPath path;
    public float waitTime;

    public Animator anime;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        patrolPoints = new Vector3[numPatrolPoints];

        path = new NavMeshPath();
        anime.SetBool("Walk Forward", true);
    }

    // Update is called once per frame
    void Update()
    {
        #region move towards player
        float distance = Vector3.Distance(player.position, transform.position);

        if(distance<=lookRadius)
        {
            anime.SetBool("Walk Forward", false);
            anime.SetBool("Run Forward", true);
            trackingPlayer = true;
            agent.SetDestination(player.position);
            patrolSet = false;
        }
        else
        {
            anime.SetBool("Run Forward", false); 
            anime.SetBool("Walk Forward", true);
            trackingPlayer = false;
        }
        #endregion

        #region set enemy patrol zone
        if (!patrolSet && !trackingPlayer)
        {
            patrolPoints[0] = transform.position;
            patrolSet = true;
            StartCoroutine(AddPatrolPath());
            
        }
        #endregion

        #region using patrol path
        else if (patrolSet && !trackingPlayer)
        {
            agent.SetDestination(patrolPoints[point]);

            if(Vector3.Distance(patrolPoints[point], transform.position)<=agent.stoppingDistance)
            {
                point = (point + 1) % numPatrolPoints;
            }    
        }
        #endregion
    }

    public float TotalDistance(Vector3[] points)
    {
        if (points.Length < 2) return 0;
        float totalDist = 0;
        for (int i = 0; i < points.Length - 1; i++)
            totalDist += Vector3.Distance(points[i], points[i + 1]);
        return totalDist;
    }

    public IEnumerator AddPatrolPath()
    {
        bool inRange;
        for (int i = 1; i<numPatrolPoints; i++)
        {
            inRange = false;

            while(!inRange)
            {
                #region make point to check
                float randZ = Random.Range(-maxPatrolRad, maxPatrolRad);
                float randX = Random.Range(-maxPatrolRad, maxPatrolRad);

                Vector3 walkPoint = new Vector3(transform.position.x + randX,
                    transform.position.y, transform.position.z + randZ);
                #endregion

                #region check if point valid
                bool pathAvailable = NavMesh.CalculatePath(transform.position, walkPoint, NavMesh.AllAreas, path);
                if(pathAvailable)
                {
                    Debug.DrawLine(walkPoint + transform.up * 10, walkPoint - transform.up * 10, Color.green, 10f);
                }
                else
                {
                    Debug.DrawLine(walkPoint + transform.up * 10, walkPoint - transform.up * 10, Color.red, 10f);
                }
                #endregion

                if (pathAvailable)
                {
                    agent.SetDestination(walkPoint);
                    yield return new WaitForSeconds(waitTime);
                    float pathDist = TotalDistance(agent.path.corners);
                    yield return new WaitForSeconds(waitTime);

                    if(pathDist<=maxPatrolDist)
                    {
                        patrolPoints[i] = walkPoint;
                        inRange = true;
                    }
                }
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
