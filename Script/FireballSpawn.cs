using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawn : MonoBehaviour
{
    public GameObject[] lasers;
    public GameObject fireball;
    private Transform firePoint;
    public int numOfProj;
    public float startTimeBtwSpawn;
    private float timeBtwSpawn;
    bool attacking;
    StageRequirement stageReq;
    Animator animator;
    private GameObject cutScene;
    private bool finishingUpGame= false;

    private List<GameObject> spawnedObj;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        cutScene = GameObject.Find("CutScene");
        cutScene.SetActive(false);
        firePoint = gameObject.transform.GetChild(0);
        timeBtwSpawn = startTimeBtwSpawn;
        stageReq = GameObject.FindObjectOfType<StageRequirement>().GetComponent<StageRequirement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacking && !stageReq.GameStatus())
        {
            if (timeBtwSpawn <= 0)
            {
                //Attack
                int random = Random.Range(0, 2);
                Debug.Log(random);
                if (random == 1)
                {
                    StartCoroutine(SpawnFireBalls());
                }
                else
                {
                    StartCoroutine(ShootLaser());
                }

                timeBtwSpawn = startTimeBtwSpawn;
            }
            else
            {
                timeBtwSpawn -= Time.deltaTime;
            }
        }
        else if (stageReq.GameStatus() && !finishingUpGame)
        {
            finishingUpGame = true;
            StartCoroutine(EndTransition());
        }
    }

    private IEnumerator SpawnFireBalls()
    {
        spawnedObj = new List<GameObject>();
        attacking = true;
        for (int i = 0; i < numOfProj; i++)
        {
            yield return new WaitForSeconds(1f);
            GameObject obj = Instantiate(fireball, firePoint.position, Quaternion.identity);
            spawnedObj.Add(obj);

        }
        attacking = false;
    }

    private IEnumerator ShootLaser()
    {
        attacking = true;
        List<int> usedIndexes;
        usedIndexes = new List<int>();
        int numOfLaser = 0;
        while (numOfLaser < 2)
        {
            int index = Random.Range(0, lasers.Length);
            if (!usedIndexes.Contains(index))
            {
                usedIndexes.Add(index);
                lasers[index].GetComponent<LaserBeam>().SetWarning();
                numOfLaser++;
            }
        }

        yield return new WaitForSeconds(5);
            attacking = false;
    }

    public IEnumerator EndTransition()
    {
        cutScene.SetActive(true);
        yield return new WaitForSeconds(1f);
        DeleteFireballs();
        GameObject.FindGameObjectWithTag("Player").SetActive(false);
        yield return new WaitForSeconds(2f);
        animator.SetTrigger("FlyUp");
    }

    void DeleteFireballs()
    {
        foreach (GameObject fireball in spawnedObj)
        {
            if (fireball != null)
            {
                Destroy(fireball);
            }
        }
    }

    private void LoadCredits()
    {
        StartCoroutine(GameManager.instance.LoadNextScene());
    }
}
