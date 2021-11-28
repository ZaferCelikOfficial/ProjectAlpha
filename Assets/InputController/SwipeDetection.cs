using System.Collections;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private float minimumDistance = .2f;
    [SerializeField] private float maximumTime = 1f;
    [SerializeField] [Range(0,1)] private float directionThreshold = .9f;
    private InputManager PlayerInputManager;
    private Vector2 StartingPosition;
    private Vector2 EndingPosition;
    private float StartingTime;
    private float EndingTime;
    [SerializeField] private GameObject trail;
    private Coroutine coroutine;
    public GameObject Food;
    private void Awake()
    {
        PlayerInputManager = InputManager.Instance;
    }
    private void OnEnable()
    {
        PlayerInputManager.OnStartTouch += SwipeStart;
        PlayerInputManager.OnEndTouch += SwipeEnd;

    }
    private void OnDisable()
    {
        PlayerInputManager.OnStartTouch -= SwipeStart;
        PlayerInputManager.OnEndTouch -= SwipeEnd;
    }
    private void SwipeStart(Vector2 position, float time)
    {
        StartingPosition = position;
        StartingTime = time;
        if (!GameManager.isGameStarted || GameManager.isGameEnded) { return; }
        trail.SetActive(true);
        trail.transform.position = position;
        coroutine=StartCoroutine("Trail");
    }
    private IEnumerator Trail()
    {
        while (GameManager.isGameStarted && !GameManager.isGameEnded)
        {
            trail.transform.position = PlayerInputManager.PrimaryPosition();
            yield return null;
        }
    }
    private void SwipeEnd(Vector2 position, float time)
    {
        EndingPosition = position;
        EndingTime = time;
        if (!GameManager.isGameStarted || GameManager.isGameEnded) { return; }
        trail.SetActive(false);
        StopCoroutine(coroutine);
        DetectSwipe();
    }
    private void DetectSwipe()
    {
        if(Vector3.Distance(StartingPosition,EndingPosition)>=minimumDistance&&(EndingTime-StartingTime)<=maximumTime)
        {
            Debug.Log("SwipeDetected");
            Debug.DrawLine(StartingPosition, EndingPosition, Color.red, 5f);
            Vector3 direction= EndingPosition - StartingPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
            
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        Food = other.gameObject;
    }
    private void SwipeDirection(Vector2 direction)
    {
        /*if(Vector2.Dot(Vector2.up,direction)>directionThreshold)
        {
            Debug.Log("SwipeUp");
        }*/
        /*else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            Debug.Log("SwipeDown");
        }*/
        
        if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            Debug.Log("SwipeLeft");
            Rigidbody FoodRigidbody = Food.GetComponent<Rigidbody>();
            if (FoodRigidbody == null) { return; }
            Food.GetComponent<Rigidbody>().AddForce(new Vector3(-400f, 0),ForceMode.Force);

            //{ ApplyForce.ForceInstance.ForceLeft(Food); }
        }
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            Debug.Log("SwipeRight");
            Rigidbody FoodRigidbody = Food.GetComponent<Rigidbody>();
            if (FoodRigidbody == null) { return; }
            Food.GetComponent<Rigidbody>().AddForce(new Vector3(400f, 0), ForceMode.Force);
            //ApplyForce.ApplyForceInstance.ForceRight(Food);
        }

    }
}
