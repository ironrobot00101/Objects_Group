using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Powerup : MonoBehaviour
{
    [SerializeField] protected float activePowerupTime;
    [SerializeField] protected bool powerupIsActive;
    [SerializeField] private AudioManager audioManager;
    private float timeRemaining;
    private bool timerIsPlaying;
    public virtual void Awake()
    {
        timeRemaining = activePowerupTime;
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        if (powerupIsActive && !timerIsPlaying)
        {
            timerIsPlaying = true;
            StartCoroutine(PlayTimerSound());
        }
        if (timeRemaining > 0 && powerupIsActive)
        {
            timeRemaining -= Time.deltaTime;
            ScoreManager.quickFireTimer = Mathf.RoundToInt(timeRemaining).ToString();
            
        }
        else
        {
            ScoreManager.quickFireTimer = "";
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ActivatePowerup());
            GetComponent<Renderer>().enabled = false;
            GetComponent<PolygonCollider2D>().enabled = false;
        }
    }

    public virtual IEnumerator ActivatePowerup()
    {
        powerupIsActive = true;
        Player player = FindObjectOfType<Player>().GetComponent<Player>();
        player.SetPowerupFeatures();
        yield return new WaitForSeconds(activePowerupTime);
        powerupIsActive = false;
        player.ResetPowerupsToNull();

    }

    public IEnumerator PlayTimerSound()
    {
        yield return new WaitForSeconds(0.4f);
        while (powerupIsActive)
        {
            audioManager.PlaySFXAudio("quickfire_timer");
            yield return new WaitForSeconds(1);
        }
    }
}
