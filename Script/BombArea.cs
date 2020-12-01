using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombArea : MonoBehaviour
{
    public Vector3 bottomLeftCoords, upperRightCoords;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetRandomCoords()
    {
        float randomX =(Random.Range(bottomLeftCoords.x, upperRightCoords.x));
        float randomY = (Random.Range(bottomLeftCoords.y, upperRightCoords.y));
        Vector3 randomPos = new Vector3(randomX, randomY);

        return randomPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(bottomLeftCoords, new Vector2(upperRightCoords.x, bottomLeftCoords.y));
        Gizmos.DrawLine(new Vector2(upperRightCoords.x, bottomLeftCoords.y),upperRightCoords);
        Gizmos.DrawLine(upperRightCoords, new Vector2(bottomLeftCoords.x,upperRightCoords.y));
        Gizmos.DrawLine(new Vector2(bottomLeftCoords.x, upperRightCoords.y), bottomLeftCoords);
    }
}
