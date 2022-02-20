using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float maxRotation = 0.4f;
    public float rotateAmount = 0.00000f;
    public float rotateSpeed = 20.0f;
    public float forwardSpeed = 1.0f;
    public float upDownRotation = 0.1f;
    public float maxAltitude = 20.0f;

    public float maxforwardSpeed = 15.0f;
    public float speedChange = 0.1f;
    public GameObject GameManager;


    public Image hitImage; // red image to show when collision

    public AudioClip audioTurn;

    public bool accelerate = false;


    private Transform _origTransform;

    private float startX;
    private float startY;
    private float startZ;
    private float endX;

    private float minZ;
    private float maxZ;

    private float xStartRotation;
    private float yStartRotation;
    private float zStartRotation;
    private float _standardSpeed;

    private float hitScreenPercentage = 0.0f;

    private bool _accelerateStayOn = false;

    private int numCracks = 5;
    private int numHits = 0;


    // Start is called before the first frame update
    void Start()
    {
      Rigidbody r = gameObject.GetComponent<Rigidbody>();
        HideCracks();

      startX = GameObject.Find("Start").transform.position.x;
      startY = transform.position.y;
      startZ = transform.position.z;

      endX = GameObject.Find("End").transform.position.x;

      minZ = GameObject.Find("Start").transform.position.z;
      maxZ = GameObject.Find("End").transform.position.z;

      // save the initial rotation for reset
      xStartRotation = transform.rotation.eulerAngles.x;
      yStartRotation = transform.rotation.eulerAngles.y;
      zStartRotation = transform.rotation.eulerAngles.z;

      _origTransform = transform;

      _standardSpeed = forwardSpeed;

        numHits = 0;

    }

    // Update is called once per frame
    void Update()
    {

      if (accelerate) {
        if (forwardSpeed < maxforwardSpeed) {
          forwardSpeed += speedChange;
        }

        Debug.Log("accelerate");
      }
      else {
        if (forwardSpeed > _standardSpeed) {
          forwardSpeed -= speedChange;

          Debug.Log("normal");
        }
      }

      transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);

      // adjust left righ position based on rotation angle - try this
      transform.position = transform.position + new Vector3(0, 0,rotateSpeed * Time.deltaTime * transform.rotation.z);

      // restart if past end gameobject
      if (transform.position.x < endX) {
        //  transform.position = new Vector3(startX, 3f, -1.7f);
        GameManager.GetComponent<GameManager>().ReachedEnd();
        restartPosition();
      }

      // left boundary
      if (transform.position.z < minZ) {
          transform.position = new Vector3(transform.position.x, transform.position.y, minZ);
      }

      // right boundary
      if (transform.position.z > maxZ) {
          transform.position = new Vector3(transform.position.x, transform.position.y, maxZ);
      }

      if (transform.position.y > maxAltitude) {
          transform.position = new Vector3(transform.position.x, maxAltitude, transform.position.z);
        //  Debug.Log(transform.position);
          transform.Rotate(0.3f,0,0);
        //  transform.rotation = Quaternion.Euler(0, 0, 0);
      }

      if (transform.position.y < 1.0f) {
          transform.position = new Vector3(transform.position.x, 1.1f, transform.position.z);
        //  Debug.Log(transform.position);
          transform.Rotate(-0.3f,0,0);
        //  transform.rotation = Quaternion.Euler(0, 0, 0);
      }

      if (hitScreenPercentage > 0.0) // if there was a hit collision
      {
          hitScreenPercentage -= 0.005f; // reduce by 10%
          ShowHitScreen(hitScreenPercentage);
      }


    }

    void FixedUpdate()
    {

      float rotate = Time.deltaTime * rotateAmount;
    //  Debug.Log(Mathf.Abs(transform.rotation.z - rotate) + " > " + maxRotation);

      if (Input.GetKey(KeyCode.RightArrow) && transform.rotation.z < maxRotation)
      {
      //  transform.Rotate(0,0, -Time.deltaTime * rotateAmount);
        transform.Rotate(0,0, -0.5f );
    //    float movementSpeed = 1.0f;
      //  AudioSource.PlayClipAtPoint(audioTurn, transform.position);
        Debug.Log("Rotation: " + transform.rotation.z);
      }
      else if (Input.GetKey(KeyCode.LeftArrow) && transform.rotation.z > -maxRotation)
      {

        transform.Rotate(0,0, 0.5f);
        Debug.Log("Rotation: " + transform.rotation.z);
      //  AudioSource.PlayClipAtPoint(audioTurn, transform.position, 2.0f);
      }
      else if (Input.GetKey(KeyCode.UpArrow))
      {
        transform.Rotate(upDownRotation,0,0 );
      //  AudioSource.PlayClipAtPoint(audioTurn, transform.position, 2.0f);
      }
      else if (Input.GetKey(KeyCode.DownArrow))
      {
        transform.Rotate(-upDownRotation,0,0);
      //  AudioSource.PlayClipAtPoint(audioTurn, transform.position, 2.0f);
      }
      else
      {
        transform.Rotate(0,0,0);  // TODO: smoother correction

      }

      // space bar accelerates
      if (_accelerateStayOn || Input.GetKey(KeyCode.Space))
      {
        accelerate = true;
      }
      else {
        accelerate = false;
      }

      // shift toggles full speed on/off
      if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
      {
        _accelerateStayOn = !_accelerateStayOn;
      }

    }

    private void OnCollisionEnter(Collision col)
    {

        ++numHits;

        if (numHits < 6)
        {
            Transform childTrans = transform.Find("crack_0" + numHits.ToString());
            childTrans.gameObject.SetActive(true);
        }
        else
        {
           Transform childTrans = transform.Find("crack_final");
            childTrans.gameObject.SetActive(true);
        }

        if (numHits > 30)
        {
            restartPosition();
        }


        hitScreenPercentage = 0.8f; // set hit to 80% red and let Update phase out

        ShowHitScreen(hitScreenPercentage);
        Debug.LogFormat("{0} collision enter: {1}", this, col.gameObject);

        // check if wall then stop
        if (col.gameObject.tag == "Wall") {
          Debug.Log("*** WALL ***");
        //  forwardSpeed = 0.0f; // stop
        // transform.position = new Vector3(startX, 3f, -1.7f);
      //  transform.position = new Vector3(startX, startY, startZ);
      //  transform.rotation = Quaternion.Euler(xStartRotation, yStartRotation, zStartRotation);
      /*  transform.position = _origTransform.position;
        transform.rotation = _origTransform.rotation; */
          restartPosition();
        }

        hitScreenPercentage = 0.8f; // set hit to 80% red and let Update phase out

        ShowHitScreen(hitScreenPercentage);


    }

    private void ShowHitScreen(float alpha)
    {
        var tempColor = hitImage.color;
        tempColor.a = alpha;
        hitImage.color = tempColor;

    }

    private void restartPosition()
    {
        transform.position = new Vector3(startX, startY, startZ);
        transform.rotation = Quaternion.Euler(xStartRotation, yStartRotation, zStartRotation);
        numHits = 0;
        HideCracks();
        GameManager.GetComponent<GameManager>().ResetScore();
    }

    public bool isSpeedBoost()
    {
        return accelerate;
    }


    private void HideCracks()
    {

        numHits = 0;

        Transform childTrans = transform.Find("crack_01");
        childTrans.gameObject.SetActive(false);

        childTrans = transform.Find("crack_02");
        childTrans.gameObject.SetActive(false);

        childTrans = transform.Find("crack_03");
        childTrans.gameObject.SetActive(false);

        childTrans = transform.Find("crack_04");
        childTrans.gameObject.SetActive(false);

        childTrans = transform.Find("crack_05");
        childTrans.gameObject.SetActive(false);

        childTrans = transform.Find("crack_06");
        childTrans.gameObject.SetActive(false);

        childTrans = transform.Find("crack_final");
        childTrans.gameObject.SetActive(false);


    }





}
