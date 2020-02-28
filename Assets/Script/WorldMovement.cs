using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMovement : MonoBehaviour
{
    [SerializeField] GameObject sectionsHost;
    [SerializeField] List<GameObject> sections;
    [SerializeField] List<GameObject> treadmillObjects;
    [SerializeField] Transform hidePos;


    [SerializeField] float moveSpeed;
    [SerializeField] float killThreshold;

    private int numOfActiveSections =1;

    GameManager gm;

    Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState((int)(Time.time * 10));
        gm = GameManager.Get;


        foreach (Transform t in sectionsHost.transform)
        {

            sections.Add(t.gameObject);
        }

        SpawnSection();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnSection();
        foreach (GameObject t in treadmillObjects)
        {
            t.transform.position = new Vector3(t.transform.position.x, t.transform.position.y, t.transform.position.z - gm.MoveSpeed * Time.deltaTime);
        }
        PositionOfSection();

        }


    void SpawnSection()
    {
        if(numOfActiveSections<=15)
        {
            int rand = Random.Range(0, sections.Count);
            Vector3 pos = treadmillObjects[treadmillObjects.Count-1].transform.position + new Vector3(0,0, treadmillObjects[treadmillObjects.Count-1].transform.localScale.z*2f);
            sections[rand].transform.position = pos;
            numOfActiveSections += 1;
            treadmillObjects.Add(sections[rand]);
            sections.RemoveAt(rand);
        }
    }

    void PositionOfSection()
    {
        for(int i = 0; i<treadmillObjects.Count;i++)
        {
            Vector3 pos = treadmillObjects[i].transform.position;
            pos = new Vector3(pos.x, pos.y, pos.z - gm.MoveSpeed * Time.deltaTime);

            if (pos.z <= killThreshold - (pos.z +100f))
            {

                treadmillObjects[i].transform.position = hidePos.position;
                numOfActiveSections -= 1;
                sections.Add(treadmillObjects[i]);
                Debug.Log("relocated");
                treadmillObjects.RemoveAt(i);
            }
           
        }
    }
}
