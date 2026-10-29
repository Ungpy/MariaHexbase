using UnityEngine;

public class TileManager : MonoBehaviour
{
    private float xOffset = 0.9f;
    private float yOffset = 0.78f;
    [SerializeField] private float maxCol;
    [SerializeField] private float maxRow;
    [SerializeField] GameObject basetile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int row = 0; row < maxRow; row++)
        {
            //when row increased the start of first tile of row move a little (1/2 of offset)
            float rowStartOffset = (row % 2) * (xOffset / 2);
            float yPos = row * yOffset;

            for (int col = 0; col < maxCol; col++)
            {
                float xPos = rowStartOffset + (col * xOffset);
                Instantiate(basetile, new Vector3(xPos, yPos, 0f), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
