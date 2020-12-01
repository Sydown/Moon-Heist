using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageRequirement : MonoBehaviour
{
    public int buttonRequirements;
    private int numOfButtonsPressed;
    public GameObject button;
    public Vector3 bottomLeftCoords, upperRightCoords;
    public float distance;
    private bool isGameFinish = false;
    public float spawnDelay;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public IEnumerator SpawnButton(Vector3 currentPos)
    {
        numOfButtonsPressed++;
        if (numOfButtonsPressed == buttonRequirements)
        {
            //finish stage
            Debug.Log("Game is finish");
            isGameFinish = true;
        }
        if (numOfButtonsPressed < buttonRequirements)
        {
            yield return new WaitForSeconds(spawnDelay);
            Instantiate(button, GetRandomCoords(currentPos), Quaternion.identity);
        }
    }

    public Vector3 GetRandomCoords(Vector3 currentPos)
    {
        Vector3 randomPos = currentPos;
        while (Vector3.Distance(currentPos, randomPos) <= distance)
        {
            float randomX = (Random.Range(bottomLeftCoords.x, upperRightCoords.x));
            float randomY = (Random.Range(bottomLeftCoords.y, upperRightCoords.y));
            randomPos = new Vector3(randomX, randomY);
        }
        return randomPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(bottomLeftCoords, new Vector2(upperRightCoords.x, bottomLeftCoords.y));
        Gizmos.DrawLine(new Vector2(upperRightCoords.x, bottomLeftCoords.y), upperRightCoords);
        Gizmos.DrawLine(upperRightCoords, new Vector2(bottomLeftCoords.x, upperRightCoords.y));
        Gizmos.DrawLine(new Vector2(bottomLeftCoords.x, upperRightCoords.y), bottomLeftCoords);
    }

    public bool GameStatus()
    {
        return isGameFinish;
    }

}
