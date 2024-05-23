using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public AudioClip attackSound;

    private AudioSource audioSource;

    public MonsterSpawner spawner;
    //ĳ���� ��Ʈ�ѷ�, �ִϸ�����, �÷��̾� �Է��� ������ ����
    private CharacterController _cc;
    private Animator _animator;
    private PlayerInput _playerInput;

    //�̵��ӵ�, ȸ�� �ӵ�, �߷�, ���������� ������ ����
    public float MoveSpeed = 5f;
    public float RotationSpeed = 180.0f;
    public float Gravity = -9.8f;
    public float JumpForce = 5.0f;

    //ĳ������ �������� �����ϴ� ����
    private Vector3 _movementVelocity;
    private float _verticalVelocity;
    //���� ���¸� ������ ���� 
    private bool _isJumping = false;

    private int _jumpCount = 0;
    public int MaxJumpCount = 2;


    //���� ������ ���� ���� �߰�
    //��ü�� ��������, �÷��̾������� �Ǵ�
    public bool IsPlayer = true;
    private UnityEngine.AI.NavMeshAgent _navMeshAgent;

    private Transform TargetPlayer;

    //ü��
    public Health _health;

    //damagecaster script
    private DamageCaster _damageCaster;


    //������ ����� ���� ����
    public GameObject ItemToDrop;

    //�÷��̾� ���� ������ ���� enum ���� ����
    public enum CharacterState
    {
        Normal, Attacking, Dead
    }
    public CharacterState CurrentState;

   

    private void Awake()
    {
        //�� ������Ʈ�� �����ͼ� ������ �Ҵ�
        _cc = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        _damageCaster = GetComponentInChildren<DamageCaster>();

        if (!IsPlayer)
        {
            _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            TargetPlayer = GameObject.FindWithTag("Player").transform;
            _navMeshAgent.speed = MoveSpeed;
        }
        else
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        // AudioSource ������Ʈ�� �����ͼ� ������ �Ҵ�
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) // AudioSource ������Ʈ�� ���ٸ�
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource ������Ʈ �߰�
        }

    }

    private void CalculateEnemyMovement()
    {
        if (Vector3.Distance(TargetPlayer.position, transform.position) >= _navMeshAgent.stoppingDistance)
        {
            _navMeshAgent.SetDestination(TargetPlayer.position);
            _animator.SetFloat("Speed", 0.2f);
        }
        else
        {
            _navMeshAgent.SetDestination(transform.position);
            _animator.SetFloat("Speed", 0f);

            SwitchStateTo(CharacterState.Attacking);
        }

    }
    private void CalculatePlayerMovement()
    {
        //ĳ������ �����ӿ� ���� ���͸� ���
        _movementVelocity.Set(0f, _verticalVelocity, _playerInput.VerticalInput);
        _movementVelocity = transform.TransformDirection(_movementVelocity);
        _movementVelocity *= MoveSpeed * Time.deltaTime;

        //�ִϸ����Ϳ��� �ӵ� �� �����Ͽ� ������ �ִϸ��̼��� ����
        //0.01 �̻��� ��� RUN, 0.01������ ��� Idle�� Ʈ������
        _animator.SetFloat("Speed", _movementVelocity.magnitude);

        //�÷��̾��� �¿� ȸ�� ����
        if (_playerInput.HorizontalInput != 0)
        {
            float rotationAmount = _playerInput.HorizontalInput * RotationSpeed * Time.deltaTime;
            transform.Rotate(0f, rotationAmount, 0f);
        }

        //���� �Է��� �ְ�, ���� ���� ���¶�� ���� ����

        if (_playerInput.JumpInput && (_cc.isGrounded || _jumpCount < MaxJumpCount))
        {
            _isJumping = true;
            _verticalVelocity = JumpForce;
            _jumpCount++;
            _animator.SetBool("IsJump", true);
        }

        else if (_cc.isGrounded && !_isJumping)
        {
            _verticalVelocity = -0.1f;
            _jumpCount = 0;
            _animator.SetBool("IsJump", false);
        }

        //���� ���� ���� ���¶�� �߷��� ����
        if (!_cc.isGrounded)
        {
            _isJumping = false;
            _verticalVelocity += Gravity * Time.deltaTime;
        }

        //�÷��̾� �̵� ����
        _movementVelocity.y = _verticalVelocity * Time.deltaTime;
        _cc.Move(_movementVelocity);

        //���콺 ���� ��ư�� Ŭ���ϰ�, ���� ���� ���¶�� ���� ����
        if (_playerInput.MouseButtonDown && _cc.isGrounded)
        {
            SwitchStateTo(CharacterState.Attacking);
            return;
        }
    }

    private void FixedUpdate()
    {
        switch (CurrentState)
        {
            case CharacterState.Normal:
                if (IsPlayer)
                {
                    CalculatePlayerMovement();
                }
                else
                {
                    CalculateEnemyMovement();
                }
                break;
            case CharacterState.Attacking:
                break;
            case CharacterState.Dead:
                return;
        }


    }

    //�÷��̾� ���¸� ��ȯ�ϴ� �Լ�
    public void SwitchStateTo(CharacterState newState)
    {
        if (IsPlayer)
        {
            _playerInput.MouseButtonDown = false;
        }
        //currentState -> newState / newState->currentState���� ��ȯ�� ���� 
        //��� ��쿡 ���� switch case �ۼ�
        switch (CurrentState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attacking:
                break;
            case CharacterState.Dead:
                if (newState != CharacterState.Normal)
                    return;
                break;
        }

        switch (newState)
        {
            case CharacterState.Normal:
                // Normal ���·� ��ȯ�� �� Idle �ִϸ��̼��� ����ϵ��� �����մϴ�.
                _animator.SetFloat("Speed", 0f);
                break;
            case CharacterState.Attacking:
                
                if (!IsPlayer)
                {
                    Quaternion newRotation = Quaternion.LookRotation(TargetPlayer.position - transform.position);
                    transform.rotation = newRotation;
                }
                _animator.SetTrigger("Attack");
                break;
            case CharacterState.Dead:
                _cc.enabled = false;
                _animator.SetTrigger("Dead");

                if (!IsPlayer)
                    GetComponent<EnemyVFXManager>().PlayDeathVFX();
                else
                    Invoke("ReviveCharacter", 5.0f);
                break;
        }

        CurrentState = newState;

        //�Լ� ���� Ȯ���� ���� �ܼ� ����� �޽��� ���
        UnityEngine.Debug.Log("Switched to " + CurrentState);
    }
    public void AttackAnimationEnds()
    {
        SwitchStateTo(CharacterState.Normal);
    }

    //�������� �����ϴ� �Լ�
    public void ApplyDamage(int damage)
    {

        if (_health != null)
        {
            _health.ApplyDamage(damage);
        }
        PlayAttackSound();
        if (!IsPlayer)
        {
            GetComponent<EnemyVFXManager>().PlayBeingHitVFX();
        }
        else
            GetComponent<PlayerVFXManager>().PlayBeingHitVFX();
    }

    public void EnableDamageCaster()
    {
        _damageCaster.EnableDamageCaster();
    }
    public void DisableDamageCaster()
    {
        _damageCaster.DisableDamageCaster();
    }

    //ü���� 0�̸� ������Ʈ ��Ȱ��ȭ
    public void DisableCharacter()
    {
        gameObject.SetActive(false);
        DropItem();

        if(spawner != null)
        {
            spawner.MonsterDied();
        }
    }

    //������ ����� ���� �Լ�
    public void DropItem()
    {
        if(ItemToDrop != null)
        {
            Instantiate(ItemToDrop, transform.position, Quaternion.Euler(0f, 180f, 0f));
        }
    }

    public void ReviveCharacter()
    {

        // ĳ���͸� Ȱ��ȭ�ϰ� ü���� ����
        gameObject.SetActive(true);

        // CharacterController�� �ٽ� Ȱ��ȭ
        _cc.enabled = true;
        
        if (_health != null)
        {
            _health.ResetHealth();
        }
        
        // ĳ������ ���¸� Normal
        SwitchStateTo(CharacterState.Normal);
    }

    private void PlayAttackSound()
    {
        if (attackSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(attackSound);  // ���� �Ҹ� ���
        }
    }


}