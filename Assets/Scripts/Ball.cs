using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    int balls = 3;
    [SerializeField]
    float speed = 5f;
    [SerializeField]
    float maxSpeedForGoal = 0.1f;
    [SerializeField] CourseManager courseManager;
    [SerializeField] TMPro.TMP_Text goalsText;

    int goals;
    bool inHole = false;
    bool aiming;
    bool ready;
    bool canPlay = false;
    Rigidbody2D rigid;
    [SerializeField]
    LineRenderer line;
    Vector3 startPos, endPos;
    float maxDistance = 2f;
    GameObject targetGoal;
    bool wasFinalMove=false;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        canPlay=true;
    }

    private void Update()
    {
        if (!canPlay) return;
        if (rigid.velocity.magnitude <= 0.1f && inHole == false)
        {
            ready = true;
        } else
        {
            ready = false;
        }

        if(ready && wasFinalMove)
        {
            balls--;
            StopAllCoroutines();
            canPlay = false;
            if (balls == 0)
            {
                GameManager.Instance.GameOver();
            }
            else
            {
                courseManager.ResetCourse();
                wasFinalMove = false;
                canPlay = true;
            }
            return;
        }

        if (Input.GetMouseButtonDown(0) && !aiming && ready)
        {
            aiming = true;
        }

        if (aiming)
        {
            line.enabled = true;
            startPos = this.transform.position;
            line.SetPosition(0, startPos);
            Vector3 shootPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            shootPos.z = 0;
            shootPos = this.transform.position + (this.transform.position - shootPos);
            endPos = shootPos;

            if (Vector3.Distance(startPos, shootPos) > maxDistance)
            {
                Vector3 dir = endPos - startPos;
                endPos = this.transform.position + (dir.normalized * maxDistance);
            }
            line.SetPosition(1, endPos);
        }
        else
        {
            line.enabled = false;
        }

        if (Input.GetMouseButtonUp(0) && aiming && ready)
        {
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Shoot();
        }

        if (targetGoal)
        {

        }
    }

    void Shoot()
    {
        aiming = false;
        canPlay = false;
        StartCoroutine(ShotDelay());
        line.enabled = false;
        if (Vector2.Distance(startPos, endPos) > maxDistance)
        {

        }
        Vector2 direction = startPos - endPos;
        rigid.AddForce(direction * speed);
        if (courseManager.ShotMade())
        {
            wasFinalMove = true;
        }
        //@TODO: Play shooting sound here
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered At :" + rigid.velocity.magnitude);
        if (collision.transform.tag == "Hole" && rigid.velocity.magnitude <= maxSpeedForGoal)
        {
            StartCoroutine(Goal(collision.gameObject));
            transform.position = collision.transform.position;
        }
    }

    IEnumerator Goal(GameObject target)
    {
        rigid.velocity = Vector2.zero;
        ready = false;
        inHole = true;
        wasFinalMove = false;
        canPlay = false;
        targetGoal = target;
        goals++;
        goalsText.text = "Holes: "+goals;
        //@TODO: Play Goal Sound Here
        yield return new WaitForSeconds(2.0f);

        Debug.Log("Goal Works!");
        courseManager.ActivateRandomCourse();

        ready = true;
        inHole = false;
        canPlay = true;
        // ELSE
        // StartCoroutine(EndGame());
        Debug.Log("Game Ended");
    }

    IEnumerator ShotDelay()
    {
        yield return new WaitForSeconds(2);
        canPlay = true;
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(4.0f);

    }
}
