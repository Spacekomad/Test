using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ParticleSystem smokeEffect;

    public float speed = 1.0f;
    public bool vertical = false;

    public float changeTime = 3.0f;
    public int direction = 1;

    private bool _broken = true;

    private Rigidbody2D _rigidbody2D;

    private Animator _animator;

    [SerializeField]
    private float _timer;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _timer = changeTime;

        _animator = GetComponent<Animator>();

        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(!_broken)
        {
            return;
        }

        _timer -= Time.deltaTime;

        if (_timer < 0)
        {
            direction = -direction;
            _timer = changeTime;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_broken)
        {
            return;
        }

        Vector2 move = _rigidbody2D.position;

        if (vertical)
        {
            move.y += speed * Time.deltaTime * direction;

            _animator.SetFloat(Globals.EnemyAnimation.MoveX, 0);
            _animator.SetFloat(Globals.EnemyAnimation.MoveY, direction);
        }
        else
        {
            move.x += speed * Time.deltaTime * direction;
            _animator.SetFloat(Globals.EnemyAnimation.MoveX, direction);
            _animator.SetFloat(Globals.EnemyAnimation.MoveY, 0);
        }


        _rigidbody2D.MovePosition(move);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Tag
        

        RubyController controller = collision.gameObject.GetComponent<RubyController>();

        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        _broken = false;
        _rigidbody2D.simulated = false;

        smokeEffect.Stop();
        _animator.SetTrigger(Globals.EnemyAnimation.Fixed);

        _audioSource.Stop();


    }
}