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

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState((int)(Time.time * 10));
        gm = GameManager.Get;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform t in treadmillObjects)
        {
            t.position = new Vector3(t.position.x, t.position.y, t.position.z -gm.MoveSpeed * Time.deltaTime);

            if (t.position.z <= killThreshold - (t.lossyScale.z * 2))
            {
                t.position = GameManager.Get.Lanes[1].position;
            }
        }

        foreach (Transform t in dynamicallyPlacedObjects)
        {
            t.position = new Vector3(t.position.x, t.position.y, t.position.z - gm.MoveSpeed * Time.deltaTime);

            if (t.position.z <= killThreshold)
            {
                int startingLane = Random.Range((int)0, (int)3);

                t.position = GameManager.Get.Lanes[startingLane].position;
            }
        }
    }
}
