using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 4;
    private float laneWidth;
    private int numberOfLanes;
    private int currentLane;

    private float current, target;
    private Vector3 startPosition = new Vector3(0, 0, 0);
    private Vector3 goalPosition = new Vector3(0, 0, 0);
    [SerializeField] private GameManager gM;

    private void Start()
    {
        laneWidth = gM.laneWidth;
        numberOfLanes = gM.numberOfLanes;
        currentLane = gM.startLane;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (transform.position.x == goalPosition.x && currentLane > 1)// only move again if player has reached new lane, and theres space to move
            {
                newLane(-1);// -1 = left
            }
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (transform.position.x == goalPosition.x && currentLane < numberOfLanes)// only move again if player has reached new lane, and theres space to move
            {
                newLane(1);// 1 = right
            }
        }

        //move player position to new lane using lerping
        current = Mathf.MoveTowards(current, target, speed * Time.deltaTime);
        transform.position = Vector3.Lerp(startPosition, goalPosition, current);
    }

    void newLane(int direction)//-1 = left, 1 = right
    {
        target = 1;
        current = 0;
        startPosition.x = goalPosition.x;
        goalPosition.x += (direction * laneWidth);
        currentLane += direction;
    }
}
