using UnityEngine;

public class BackgroundRepeater : MonoBehaviour
{
    [SerializeField] private float backgroundLength;
    [SerializeField] private Transform otherBackground;
    [SerializeField] private float speedModifier = 0.5f; // Ändra variablen för att sakta ner eller speed upp spelet

    private void Update()
    {
        // Flytta på bakgrunden långsammare
        transform.position += Vector3.back * (GameManager.worldSpeed * speedModifier) * Time.deltaTime;

        // Flytta på bakgrunden när den går ut u bild
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
