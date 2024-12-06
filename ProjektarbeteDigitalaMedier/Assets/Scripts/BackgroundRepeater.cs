using UnityEngine;

public class BackgroundRepeater : MonoBehaviour
{
    [SerializeField] private float backgroundLength;
    [SerializeField] private Transform otherBackground;
    [SerializeField] private float speedModifier = 0.5f; // �ndra variablen f�r att sakta ner eller speed upp spelet

    private void Update()
    {
        // Flytta p� bakgrunden l�ngsammare
        transform.position += Vector3.back * (GameManager.worldSpeed * speedModifier) * Time.deltaTime;

        // Flytta p� bakgrunden n�r den g�r ut u bild
        if (transform.position.z <= -backgroundLength)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                otherBackground.position.z + backgroundLength - 0.25f
            );
        }
    }
}
