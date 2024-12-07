using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject sten;
    [SerializeField] private GameObject stock;
    [SerializeField] private GameObject stock2Lanes;

    [SerializeField] private GameManager gM;
    [Header("StenSprites")]
    [SerializeField] private Material[] stenar;
    [SerializeField] private Material[] stockar;
    private float timer;
    public float spawnIntervall = 3;
    private float laneWidth;
    private int numberOfLanes;

    private int?[] used;
    private Vector3 spawnPosition;
    int currentObstacle = 0;

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
            gM.IncreaseScore(1);
            timer = 0;
        }
    }
    void SpawnObstacles()
    {
        //numb = number of obstacles to spawn per row
        int numb = Random.Range(1, numberOfLanes + 1);// ska kunna fylla alla lanes

        used = new int?[numb];//list of the used lanes. ? means its initialized to null by default
        currentObstacle = 0;

        if (numb == numberOfLanes) //fyller alla lanes, alltså måste vi ha minst en stock
        {

            //SPAWNA EN STOCK!
            int lane = FindLane();
            int longOrShort = Random.Range(0, 2); // 1 ger kort stock, 0 ger lång stock
            //Debug.Log("long or short: " + longOrShort);
            SpawnStock(longOrShort, lane);
        }

        while (currentObstacle < numb)
        {
            //SPAWNA RESTEN!
            int lane = FindLane();
            if (numb - used.Length >= 2)
            {
                if (CheckLane(0))
                {
                    // en stock på två lanes får plats
                    ChooseAndSpawnObstacle(true, lane);
                }
            }
            ChooseAndSpawnObstacle(false, lane);
        }
    }
    private int FindLane()
    {
        int lane = (int)Mathf.Round(Random.Range((float)-numberOfLanes / 2, numberOfLanes / 2));//generate random lane

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
        return lane;
    }
    private bool CheckLane(int index)// true om lane är tom, false om upptagen
    {
        int a = 0;
        while (a < used.Length)
        {
            if (used[a] == index)
            {
                return false;
            }
            else a++;
        }
        return true;
    }
    private void ChooseAndSpawnObstacle(bool twoLanesFree, int lane)
    {
        int obstacle;

        if (twoLanesFree){obstacle = Random.Range(0, 4);}
        else{obstacle = Random.Range(0, 3);}

        if (obstacle == 0 || obstacle == 1)
        {
            SpawnSten(lane);
        }
        else if (obstacle == 2)
        {
            SpawnStock(1, lane);
        }
        else
        {
            //lång stock
            //långa stocken blir mer rare ändå eftersom den inte alltid får plats
            SpawnStock(0, lane);
        } 
    }
    private void SpawnSten(int lane)
    {
        spawnPosition = transform.position + new Vector3(lane * laneWidth, 0, 0);
        GameObject obstacle = Instantiate(sten, spawnPosition, Quaternion.identity);
        obstacle.GetComponentInChildren<MeshRenderer>().material = stenar[Random.Range(0, stenar.Length)];
        Destroy(obstacle, 7f);
        used[currentObstacle] = lane;
        currentObstacle++;
    }
    private void SpawnStock(int twoLanesStock, int lane)
    {
        // twoLanesStock == 1 ger kort stock, twoLanesStock == 0 ger lång stock
        GameObject obstacle;
        if (twoLanesStock == 0)
        {
            int n;

            if (Random.Range(0, 2) == 0) { n = -1; } else n = 1;// randomly choose left or right lane

            if (CheckLane(n))
            {
                spawnPosition = transform.position + new Vector3((n * 0.5f * laneWidth), 0, 0);
                used[currentObstacle] = 0;
                currentObstacle++;
                used[currentObstacle] = n;
                currentObstacle++;
            }
            else
            {
                spawnPosition = transform.position + new Vector3((n * -0.5f * laneWidth), 0, 0);
                used[currentObstacle] = 0;
                currentObstacle++;
                used[currentObstacle] = (-1 * n);
                currentObstacle++;
            }
            obstacle = Instantiate(stock2Lanes, spawnPosition, Quaternion.identity);
        }
        else
        {
            spawnPosition = transform.position + new Vector3(lane * laneWidth, 0, 0);
            obstacle = Instantiate(stock, spawnPosition, Quaternion.identity);
            obstacle.GetComponentInChildren<MeshRenderer>().material = stockar[Random.Range(0, stockar.Length)];
            used[currentObstacle] = lane;
            currentObstacle++;
        }
        Destroy(obstacle, 7f);
    }

}
