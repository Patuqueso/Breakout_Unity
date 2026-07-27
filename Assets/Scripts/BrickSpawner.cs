using UnityEngine;
public class BrickSpawner : MonoBehaviour
{

    public GameObject BrickPrefab;
    public int Rows = 5;
    public int Columns = 10;

    public float spacingX = 1.0f;
    public float spacingY = 0.5f;
    public GameObject startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                Vector2 position = new Vector2(startPosition.transform.position.x + col * spacingX, startPosition.transform.position.y - row * spacingY);
                Instantiate(BrickPrefab, position, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
