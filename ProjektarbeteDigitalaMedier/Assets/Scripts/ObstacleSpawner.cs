using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private float spawnIntervall = 3;
    [SerializeField] private GameObject _obstacle;
    private float timer;
    [SerializeField] private GameManager gM;
    private float laneWidth;
    private int numberOfLanes;

    private void Start()
    {
        laneWidth = gM.laneWidth;
        numberOfLanes = gM.numberOfLanes;
        timer = spawnIntervall - 1;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnIntervall)
        {
            SpawnObstacles();
            gM.increaseScore(1);
            timer = 0;
        }
    }
    void SpawnObstacles()
    {
        //numb = number of obstacles to spawn per row
        int numb = Random.Range(0, numberOfLanes);// will give numbers in intervall 0 to (numberOfLanes - 1)
        Vector3 spawnPosition;

        int?[] used = new int?[numb];//? means its initialized to null by default

        for (int i = 0; i < numb; i++)//for each obstacle to spawn
        {
            int lane = (int) Mathf.Round(Random.Range((float)-numberOfLanes/2, numberOfLanes/2));//generate random lane

            int a = 0;
            while (a < used.Length)
            {
                if (lane == used[a])//check if that lane already has been used
                {  //if so then generate a new lane and start loop over (check again)         
                    lane = (int)Mathf.Round(Random.Range((float)-numberOfLanes / 2, numberOfLanes / 2));
                    a = 0;
                }
                else a++;
            }
            //if lane has not yet been used
            spawnPosition = transform.position + new Vector3(lane * laneWidth, 0, 0);
            GameObject obstacle = Instantiate(_obstacle, spawnPosition, Quaternion.identity);
            used[i] = lane;
            Destroy(obstacle, 10f);
            Debug.Log("lane: " + lane);
        }
        Debug.Log("numb: " + numb);
    }
}
