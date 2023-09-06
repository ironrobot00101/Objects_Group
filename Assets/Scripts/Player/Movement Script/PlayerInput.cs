using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Transform currentMouseRotation;
    private bool isMoving;
    AudioSource audioSource;
    [SerializeField] private float _fadeInterval = 0.004f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        
        RotateTowardDirection();
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Debug.Log(vertical);

        Vector2 mouseInput = Input.mousePosition;
        mouseInput = Camera.main.ScreenToWorldPoint(mouseInput);

        Vector2 direction = new Vector2(mouseInput.x - transform.position.x, mouseInput.y - transform.position.y);
        transform.right = direction; 


            
        moveInput = new Vector2(horizontal, vertical).normalized;
        if (isMoving)
        {
            if (horizontal == 0 && vertical == 0)
            {
                isMoving = false;
                StopCoroutine(FadeInVolume());
                StartCoroutine(FadeOutVolume());

            }
        }
        if (isMoving == false)
        {
            if (horizontal != 0 || vertical != 0)
            {
                isMoving = true;
                StopCoroutine(FadeOutVolume());
                StartCoroutine(FadeInVolume());
            }
        }
            GetComponent<Rigidbody2D>().velocity = moveInput * moveSpeed;

        
    }

    private void RotateTowardDirection()
    {
        if(moveInput != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0,0,angle);
        }
    }

    private IEnumerator FadeOutVolume()
    {
        while(audioSource.volume > 0f && !isMoving)
        {
            yield return new WaitForSeconds(_fadeInterval);
            audioSource.volume -= _fadeInterval;
        }
        yield break;
    }

    private IEnumerator FadeInVolume()
    {
        
        while (audioSource.volume < 0.45f && isMoving)
        {
            yield return new WaitForSeconds(_fadeInterval);
            audioSource.volume += _fadeInterval;
        }
        Debug.Log("Im done fading");
        yield break;
    }

}
