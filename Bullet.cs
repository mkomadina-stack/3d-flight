using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    Rigidbody m_Rigidbody;

    public float speed = 0.1f;

    private Vector3 direction;
    private float startX;

    //TODO add a color, range, height

    // Start is called before the first frame update
    void Start()
    {
      //player = GameObject.Find("player");
      startX = GameObject.Find("Start").transform.position.x;

      // remove after 10 s to account for bounces
      Destroy(gameObject, 10.0f);
    //  Fire();

        //m_Rigidbody.AddForce(transform.forward * 20f, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > startX)
        {
            Destroy(gameObject);
            //Debug.Log("*destroy bullet*");
        }
    }

/*    void Fire()
    {
      m_Rigidbody = GetComponent<Rigidbody>();

      direction = player.transform.position - transform.position;
      //Debug.Log(direction);
      m_Rigidbody.AddForce(direction * speed, ForceMode.Impulse);
    }*/
}
