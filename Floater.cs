using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{

    public float speed = 5.0f;

    private float startZ;
    private float endZ;

    private float width;



    // Start is called before the first frame update
    void Start()
    {
        startZ = GameObject.Find("Start").transform.position.z;
        endZ = GameObject.Find("End").transform.position.z;
        RectTransform r = gameObject.GetComponent<RectTransform>();
        width = r.rect.height;
    }

    // Update is called once per frame
    void Update()
    {
      if (transform.position.z > endZ + 50.0f) {  // TODO fix rectangle size
          transform.position = new Vector3(transform.position.x, transform.position.y, startZ - 50.0f);
      }

        transform.position = transform.position + new Vector3(0, 0, speed * Time.deltaTime);

    }
}
