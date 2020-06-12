using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] PathManager pathManager;
    [SerializeField] MaterialOffsetScroller scroller;

    Rigidbody rigid;
    bool goingRight = true;
    bool canPlay = false;

    Vector3 velocity;
    Vector3 angularVelocity;
    private void Awake()
    {
        TryGetComponent(out rigid);
    }
    protected List<Handle> Handles { get; private set; }
    private void Start()
    {
        Handles = new List<Handle>();
        Handles.Add(
            KojiBridge.ObservableNumberOfKey("game.ball_speed").DidChange.Subscribe(UpdateSpeed, true)
        );
        
    }

    public void StartGame()
    {
        canPlay = true;
        scroller.SetSpeed(speed*0.1f);
    }

    void UpdateSpeed(float newSpeed)
    {
#if !UNITY_EDITOR
        speed=Mathf.Clamp(newSpeed, 1, 20);
        scroller.SetSpeed(speed);
#endif
    }

    private void Update()
    {
        if (!canPlay)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            scroller.canPlay = true;
            scroller.ChangeDirection();
            if(goingRight)
            {
                goingRight = false;
                velocity = -Vector3.right *speed;
                angularVelocity = Vector3.forward *speed;
            }
            else
            {
                goingRight = true;
                velocity = Vector3.forward * speed;
                angularVelocity = Vector3.right *speed;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Platform platform;
        if (other.gameObject.TryGetComponent(out platform))
        {
            platform.Fall();
            pathManager.PlaceNextPlatform();

        }
    }
    //private void OnCollisionExit(Collision collision)
    //{
    //    Platform platform;
    //    if (collision.gameObject.TryGetComponent(out platform))
    //    {
    //        platform.Fall();
    //        pathManager.PlaceNextPlatform();
            
    //    }
    //}
        
    private void FixedUpdate()
    {
        velocity.y = rigid.velocity.y;
        rigid.velocity = velocity;
        rigid.angularVelocity = angularVelocity;
        if (transform.position.y <= -1)
        {
            canPlay = false;
            scroller.canPlay = false;
            GetComponent<Disabler>().DisableWithDelay();
            GameManager.Instance.GameOver();
        }
    }
}
