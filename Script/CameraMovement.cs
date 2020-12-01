using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject target;
    private Vector2 bottomScreen, topScreen;
    public float moveLength;
    public Sprite[] backgrounds;
    private Sprite bgSprite;
    // Start is called before the first frame update
    void Start()
    {
       bgSprite = GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite;
 
    }

    // Update is called once per frame
    void Update()
    {
        bottomScreen = Camera.main.ViewportToWorldPoint(Vector2.zero);
        topScreen = Camera.main.ViewportToWorldPoint(Vector2.one);
        Vector3 cameraPos = transform.position;

        if (target.transform.position.y > topScreen.y) // checks if target is above the camera screen
        {
            cameraPos.y += moveLength;
            transform.position = cameraPos;
            ChangeBackground();
        }
        if (target.transform.position.y < bottomScreen.y) // checks if target is below the camera screen
        {
            cameraPos.y -= moveLength;
            transform.position = cameraPos;
            ChangeBackground();
        }
       
    }

    private void ChangeBackground()
    {

        Vector2 currentPos = transform.position;
        if (currentPos.y >= 0 && currentPos.y <= 40)
        {
            //Change Land Background
            GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite = backgrounds[0];
            Debug.Log(bgSprite.name);
        }
        else if (currentPos.y > 40 && currentPos.y <= 80)
        {
            //Change it to City Background
            GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite = backgrounds[1];
            Debug.Log(bgSprite.name);
        }
    }
}
