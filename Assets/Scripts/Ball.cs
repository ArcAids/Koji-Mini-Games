using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    GameObject scoreBoard;
    [SerializeField]
    int balls = 3;
    [SerializeField]
    float speed = 5f;
    [SerializeField]
    float maxSpeedForGoal = 0.1f;
    [SerializeField] CourseManager courseManager;

    AudioSource audioSource;

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
        TryGetComponent(out audioSource);
    }

    public void StartGame()
    {
        canPlay=true;

    }

    private void Update()
    {
        if (!canPlay) return;
        if (rigid.velocity.magnitude <= 0.1f && inHole == false)
        {
            rigid.velocity = Vector2.zero;
            ready = true;
            scoreBoard.SetActive(true);
        } else
        {
            ready = false;
        }

        //if(ready && wasFinalMove)
        //{
        //    balls--;
        //    StopAllCoroutines();
        //    canPlay = false;
        //    if (balls == 0)
        //    {
        //        GameManager.Instance.GameOver();
        //    }
        //    else
        //    {
        //        courseManager.ResetCourse();
        //        wasFinalMove = false;
        //        canPlay = true;
        //    }
        //    return;
        //}

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
        scoreBoard.SetActive(true);
        line.enabled = false;
        if (Vector2.Distance(startPos, endPos) > maxDistance)
        {

        }
        Vector2 direction = startPos - endPos;
        rigid.AddForce(direction * speed, ForceMode2D.Impulse);
        if (courseManager.ShotMade())
        {
            wasFinalMove = true;
        }
        //@TODO: Play shooting sound here
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Hole" && rigid.velocity.magnitude <= maxSpeedForGoal)
        {
            Debug.Log("Entered At :" + rigid.velocity.magnitude);
            StartCoroutine(Goal(collision.gameObject));
            transform.position = collision.transform.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.PlayOneShot(audioSource.clip,rigid.velocity.magnitude*0.1f);
    }

    IEnumerator Goal(GameObject target)
    {
        rigid.velocity = Vector2.zero;
        ready = false;
        inHole = true;
        wasFinalMove = false;
        canPlay = false;
        targetGoal = target;

        //@TODO: Play Goal Sound Here
        courseManager.OnHole();
        yield return null;

    }

    public void NextLevel()
    {
        courseManager.ActivateNextCourse();
        ready = true;
        inHole = false;
        canPlay = true;

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
