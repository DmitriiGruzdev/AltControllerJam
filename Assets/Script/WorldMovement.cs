using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMovement : MonoBehaviour
{
    [SerializeField] List<Transform> treadmillObjects;
    [SerializeField] List<Transform> dynamicallyPlacedObjects;
    [SerializeField] float moveSpeed;
    [SerializeField] float killThreshold;
    GameManager gm;

    Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState((int)(Time.time * 10));
        gm = GameManager.Get;

        foreach (Transform t in treadmillObjects)
        {
            originalPos = new Vector3(t.position.x, t.position.y, t.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform t in treadmillObjects)
        {
            t.position = new Vector3(t.position.x, t.position.y, t.position.z -gm.MoveSpeed* Time.deltaTime);

            if (t.position.z <= killThreshold - (t.lossyScale.z + 0.5f))
            {

                t.position = originalPos;
                //t.position = GameManager.Get.Lanes[1].position;
            }
        }

        foreach (Transform t in dynamicallyPlacedObjects)
        {
            t.position = new Vector3(t.position.x, t.position.y, t.position.z -gm.MoveSpeed * Time.deltaTime);

            if (t.position.z <= killThreshold)
            {
                int startingLane = Random.Range((int)0, (int)3);
                Vector3 distanceFromPlayer = new Vector3(GameManager.Get.Lanes[startingLane].position.x, GameManager.Get.Lanes[startingLane].position.y, GameManager.Get.Lanes[startingLane].position.z + Random.Range(-5f, 300f));

                t.position = distanceFromPlayer;
            }
        }
    }
}
