using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private Animator _animator;

    private Vector2 lookDirection = new Vector2(1, 0);

    public float speed = 10.0f;
    public int maxHealth = 5;
    public int startHP = 1;

    public int health { get { return currentHealth; } }

    [SerializeField] private int currentHealth;

    private float horizontal;
    private float vertical;


    public float timeInvincible = 2.0f;

    bool isInvincible;
    float invincibleTimer;

    private AudioSource _audioSource;


    #region RefactorVar
    public float fireRate = 0.5f;
    private float _nextFire = 0.0f;
    #endregion

    #region MonoBehaviour
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        currentHealth = Mathf.Clamp(startHP, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);

        Physics2D.IgnoreLayerCollision(0, 7, true);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        SetLookDirection();
        SetMoveAnimation();

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (Input.GetKeyDown(KeyCode.C) && Time.time > _nextFire)
        {
            _nextFire = Time.time + fireRate;
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            TryInteraction();
        }
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.fixedDeltaTime;
        position.y = position.y + speed * vertical * Time.fixedDeltaTime;

        rigidbody2d.MovePosition(position);
    }

    #endregion

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            _animator.SetTrigger(Globals.RubyAnimation.Hit);

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    void Launch()
    {
        Projectile projectile = CogPool.instance.GetCog();
        projectile.transform.position = rigidbody2d.position + Vector2.up * 0.5f;

        projectile.Launch(lookDirection, 300);

        _animator.SetTrigger(Globals.RubyAnimation.Launch);
    }

    public void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    #region RefactorFunc
    private void SetLookDirection()
    {
        if (!Mathf.Approximately(horizontal, 0.0f) || !Mathf.Approximately(vertical, 0.0f))
        {
            lookDirection.Set(horizontal, vertical);
            lookDirection.Normalize();
        }
    }

    private void SetMoveAnimation()
    {
        _animator.SetFloat(Globals.RubyAnimation.LookX, lookDirection.x);
        _animator.SetFloat(Globals.RubyAnimation.LookY, lookDirection.y);
        _animator.SetFloat(Globals.RubyAnimation.Speed, new Vector2(horizontal, vertical).magnitude);
    }

    private void TryInteraction()
    {
        RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
        if (hit.collider != null)
        {
            NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
            if (character != null)
            {
                character.DisplayDialog();
            }
        }
    }
    #endregion
}
