using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{

    public Transform Bullet;
    public GameObject Player;
    public float speed = 0.7f;
    public float range = 200.0f;
    public float maxAltitude;
    public float minAltitude;


    // TODO - add color

    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
      Fire();
      InvokeRepeating("Fire", Random.Range(1.0f, 5.0f), Random.Range(1.0f, 3.0f));  //1s delay, repeat every 1s
    //    obj = Instantiate(prefab, transform.position, transform.rotation);
    //    obj.GetComponent<BulletRed>().player = player;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Fire()
    {
      GameObject bulletInstance;

    //  m_Rigidbody = obj.GetComponent<Rigidbody>();

    //  direction = (Player.transform.position - transform.position).normalized;
      direction = (Player.transform.position + (Player.transform.forward.normalized * 10.0f) - transform.position);
      Debug.Log(direction.magnitude);
      if (direction.magnitude < range) {
        bulletInstance = GameObject.Instantiate(Bullet, transform.position, transform.rotation).gameObject;
        bulletInstance.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
      }
    }
}
