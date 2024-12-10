using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 4;
    [SerializeField] private float diveSpeed = 5;
    private float laneWidth;
    private int numberOfLanes;
    private int currentLane;

    private float current, target;
    private float currentY, targetY;
    private Vector3 startPosition = new Vector3(0, 0, 0);
    private Vector3 goalPosition = new Vector3(0, 0, 0);

    [SerializeField] private GameManager gM;
    [SerializeField] private GameObject particlesNer;

    private float djup = 3;
    public bool diving = false;

    private float timer = 0;
    private float timeLimit = 0.8f;

    private AudioSource audioSource;

    private void Start()
    {
        laneWidth = gM.laneWidth;
        numberOfLanes = gM.numberOfLanes;
        currentLane = gM.startLane;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (transform.position.x == goalPosition.x && currentLane > 1)// only move again if player has reached new lane, and theres space to move
            {
                NewLane(-1);// -1 = left
            }
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (transform.position.x == goalPosition.x && currentLane < numberOfLanes)// only move again if player has reached new lane, and theres space to move
            {
                NewLane(1);// 1 = right
            }
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!diving) { 
                Dive(); 
            }
        }

        if (diving)// only move again if player has reached new lane, and theres space to move
        {
            timer += Time.deltaTime;
            if (transform.position.y == goalPosition.y && timer >= timeLimit)
            {
                UP();
            }
        }

        //move player position to new lane using lerping
        current = Mathf.MoveTowards(current, target, speed * Time.deltaTime);
        currentY = Mathf.MoveTowards(currentY, targetY, diveSpeed * Time.deltaTime);

        transform.position = new Vector3(Mathf.Lerp(startPosition.x, goalPosition.x, current), Mathf.Lerp(startPosition.y, goalPosition.y, currentY), transform.position.z);
    }
    void Dive()
    {

        targetY = 1;
        currentY = 0;
        startPosition = goalPosition;
        goalPosition.y += (-1 * djup);
        timer = 0;
        diving = true;
        Particles();
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
    void UP()
    {
        targetY = 1;
        currentY = 0;
        startPosition = goalPosition;
        goalPosition.y += (1 * djup);
        diving = false;
    }

    void NewLane(int direction)//-1 = left, 1 = right
    {
        target = 1;
        current = 0;
        startPosition = goalPosition;
        goalPosition.x += (direction * laneWidth);
        currentLane += direction;
         if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    void Particles()
    {
        if(particlesNer.activeSelf == true)
        {
            particlesNer.SetActive(false);
            particlesNer.SetActive(true);
        }
        else
        {
            particlesNer.SetActive(true);
        }
        
    }
}
