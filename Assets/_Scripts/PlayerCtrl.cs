using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCtrl : MonoBehaviour
{
    Plane vPlane;


    public float xSpeed = 0.1f;
    public float ySpeed = 0.1f;




    ///[ For ClapmToScreenWidth() ]
    private float xmin, xmax;
    public float awayFromEdges = 0.25f;
    float zOffsetFromCam;
    float yOffsetFromCam;


    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        ClapmToScreenWidth();
    }

    void ClapmToScreenWidth()
    {
        yOffsetFromCam = this.transform.position.y - Camera.main.transform.position.y;
        zOffsetFromCam = this.transform.position.z - Camera.main.transform.position.z;

        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, zOffsetFromCam));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0, zOffsetFromCam));

        xmin = leftMost.x + awayFromEdges;
        xmax = rightMost.x - awayFromEdges;
    }

    void Update()
    {
        xMovement();

        yMovement();
    }

    void xMovement()
    {
        vPlane = new Plane(Camera.main.transform.forward * -1, transform.position);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter;
        vPlane.Raycast(ray, out enter);

        Vector3 rayPoint = ray.GetPoint(enter);
        rayPoint.x = Mathf.Clamp(rayPoint.x, xmin, xmax);

        Vector3 O = new Vector3(rayPoint.x, transform.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, O, xSpeed);

        if (transform.position.x > O.x)         print("Left");
        else if (transform.position.x < O.x)    print("Right");
        else                                    print("straight");
    }

    void yMovement()
    {
        transform.Translate(Vector3.up * ySpeed);

        float yPos = transform.position.y;

        Camera.main.transform.position = new Vector3(0, yPos - yOffsetFromCam, -zOffsetFromCam);
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.tag == "danger")
        {
            StartCoroutine("TimeDelayLoadLevel");
            
            //particle
            //audio
            //score view
            //gameover panel
            //
        }

        if(trig.tag == "coin")
        {
            gameManager.coin++;
            //particle
            //sound
            Destroy(trig.gameObject);
        }
    }

    IEnumerator TimeDelayLoadLevel()
    {
        xSpeed = 0f;
        ySpeed = 0f;

        yield return new WaitForSeconds(0.5f);

        Application.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
