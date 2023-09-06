using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public bool isActiveInventory = true;
    private bool isFirstTime;
    public ParticleSystem explosion;
    public bool thrown;
    private float bombSpeed = 0.1f;
    // Start is called before the first frame update
    public void Awake()
    {
        explosion = GetComponentInChildren<ParticleSystem>();
        explosion.Stop();
    }

    private void Update()
    {
    }

    public Bomb SetBomb(bool _isActiveInventory)
    {
        isActiveInventory = _isActiveInventory;
        
        return this;
    }

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isActiveInventory)
        {
            Player player = collision.GetComponent<Player>();
            player._hasBomb = true;
            ScoreManager.bombsInventory++;
            Destroy(gameObject);

        }

    }



}
