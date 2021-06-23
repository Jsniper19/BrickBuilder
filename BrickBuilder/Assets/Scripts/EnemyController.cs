using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
#pragma warning disable 0649
    //[SerializeField] float BB_searchRange = 0;
    [SerializeField] float BB_speed = 5;
    [SerializeField] float BB_lookSpeed = 10;
    [SerializeField] float BB_isLeftMaxValue = 30;
    [SerializeField] float BB_searchRange;
    float BB_isLeftValue;
    [SerializeField] float BB_patrolTime;

    bool isLeft = true;
    public bool isHome;
    bool crtnSearch;

    [SerializeField] Rigidbody rb = null;
    [SerializeField] Transform Home = null;
    [SerializeField] Transform Target;
    GameObject player;
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject eyes;




    [SerializeField]int state = 0;
    //0 = idle, 1 = search, 2 = move to target, 3 = move to home
#pragma warning restore 0649

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        BB_isLeftValue = BB_isLeftMaxValue / 180;
        //player = GameObject.Find("BrickBuilder");
        //Target = player.transform;
    }
    private void Update()
    {
        {
            if (state == 1)
            {
                if (!crtnSearch) { StartCoroutine("SearchCancel"); }
                Search();
            }
            else if (state == 2)
            {
                Move(Target);
            }
            else if (state == 3)
            {
                if (isHome)
                {
                    state = 0;
                }
                else
                {
                    Move(Home);
                }
            }
            if (state != 1)
            {
                StopCoroutine("SearchCancel");
                crtnSearch = false;
            }
        }//state Machine

        RaycastHit hit;//check if can see
        if (Physics.Raycast(eyes.transform.position, eyes.transform.TransformDirection(Vector3.forward), out hit, BB_searchRange, layerMask))
        {
            state = 2;
            Target = hit.transform;
        }
        else
        {
            
        }

        var xPos = transform.position.x - Target.position.x;
        var zPos = transform.position.z - Target.position.z;
        if (xPos < 0) { xPos *= -1; }
        if (zPos < 0) { zPos *= -1; }

        if (xPos <= 1 && zPos  <= 1 )
        {

        }
    }

    void Move(Transform targetTransform)//moves enemy to target transform
    {
        //make object face target
        var lookPos = targetTransform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * BB_lookSpeed);

        //move object to target
        Vector3 tempVect = new Vector3(0, 0, 1);
        tempVect = tempVect.normalized * BB_speed * Time.deltaTime;
        tempVect = transform.TransformDirection(tempVect);
        rb.MovePosition(transform.position + tempVect);
    }

    IEnumerator SearchCancel()//Cancels search after set time
    {
        crtnSearch = true;
        yield return new WaitForSeconds(BB_patrolTime);
        state = 3;
        crtnSearch = false;
    }
    void Search()//makes the character rotate to "search" for player
    {
        {
            if (isLeft)
            {
                eyes.transform.Rotate(new Vector3(0, -BB_lookSpeed * Time.deltaTime * 90, 0));
                BB_isLeftValue -= Time.deltaTime * BB_lookSpeed;
            }
            else
            {
                eyes.transform.Rotate(new Vector3(0, BB_lookSpeed * Time.deltaTime * 90, 0));
                BB_isLeftValue += Time.deltaTime * BB_lookSpeed;
            }

            if (isLeft)
            {
                if (BB_isLeftValue <= 0)
                {
                    isLeft = false;
                }
            }
            else
            {
                if (BB_isLeftValue >= BB_isLeftMaxValue / 90)
                {
                    isLeft = true;
                }
            }

            //clamping x+z rotation
            if (eyes.transform.rotation.x != 0)
            {
                eyes.transform.Rotate(new Vector3(-transform.rotation.x, 0, 0));
            }
            if (eyes.transform.rotation.z != 0)
            {
                eyes.transform.Rotate(new Vector3(0, 0, -transform.rotation.z));
            }
        }//look around script
    }


    void OnDrawGizmos()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = eyes.transform.TransformDirection(Vector3.forward) * BB_searchRange;
        Gizmos.DrawRay(eyes.transform.position, direction);
    }
}
