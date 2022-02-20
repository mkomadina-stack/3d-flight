using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collide : MonoBehaviour
{


    private float startX;
    private float endX;
    private GameObject p;
    private GameObject cone;
    private GameObject cylinder; // TODO this is workaround to group object hierarchy


    // Start is called before the first frame update
    void Start()
    {
      startX = GameObject.Find("Start").transform.position.x;
      endX = GameObject.Find("End").transform.position.x;
      p = GameObject.Find("plane");

      cone = GameObject.Find("Cone");
      cylinder = GameObject.Find("Cylinder");

      //cone.transform.SetParent(gameObject.transform);
      //cylinder.transform.SetParent(gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

      //LogCollsiionEnter.text = "On Collision Enter: " + collision.collider.name;
      p.transform.position = new Vector3(startX, 3f, -1.7f);
      Debug.Log("collision");

    }
}
