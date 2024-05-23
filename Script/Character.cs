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
    //캐릭터 컨트롤러, 애니메이터, 플레이어 입력을 저장할 변수
    private CharacterController _cc;
    private Animator _animator;
    private PlayerInput _playerInput;

    //이동속도, 회전 속도, 중력, 점프강도를 설정할 변수
    public float MoveSpeed = 5f;
    public float RotationSpeed = 180.0f;
    public float Gravity = -9.8f;
    public float JumpForce = 5.0f;

    //캐릭터의 움직임을 저장하는 변수
    private Vector3 _movementVelocity;
    private float _verticalVelocity;
    //점프 상태를 관리할 변수 
    private bool _isJumping = false;

    private int _jumpCount = 0;
    public int MaxJumpCount = 2;


    //몬스터 동작을 위한 변수 추가
    //객체가 몬스터인지, 플레이어인지를 판단
    public bool IsPlayer = true;
    private UnityEngine.AI.NavMeshAgent _navMeshAgent;

    private Transform TargetPlayer;

    //체력
    public Health _health;

    //damagecaster script
    private DamageCaster _damageCaster;


    //아이템 드롭을 위한 변수
    public GameObject ItemToDrop;

    //플레이어 상태 관리를 위한 enum 변수 선언
    public enum CharacterState
    {
        Normal, Attacking, Dead
    }
    public CharacterState CurrentState;

   

    private void Awake()
    {
        //각 컴포넌트를 가져와서 변수에 할당
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

        // AudioSource 컴포넌트를 가져와서 변수에 할당
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) // AudioSource 컴포넌트가 없다면
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource 컴포넌트 추가
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
        //캐릭터의 움직임에 대한 벡터를 계산
        _movementVelocity.Set(0f, _verticalVelocity, _playerInput.VerticalInput);
        _movementVelocity = transform.TransformDirection(_movementVelocity);
        _movementVelocity *= MoveSpeed * Time.deltaTime;

        //애니메이터에서 속도 값 설정하여 움직임 애니메이션을 제어
        //0.01 이상일 경우 RUN, 0.01이하일 경우 Idle로 트랜지션
        _animator.SetFloat("Speed", _movementVelocity.magnitude);

        //플레이어의 좌우 회전 수행
        if (_playerInput.HorizontalInput != 0)
        {
            float rotationAmount = _playerInput.HorizontalInput * RotationSpeed * Time.deltaTime;
            transform.Rotate(0f, rotationAmount, 0f);
        }

        //점프 입력이 있고, 땅에 닿은 상태라면 점프 수행

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

        //땅에 닿지 않은 상태라면 중력을 적용
        if (!_cc.isGrounded)
        {
            _isJumping = false;
            _verticalVelocity += Gravity * Time.deltaTime;
        }

        //플레이어 이동 수행
        _movementVelocity.y = _verticalVelocity * Time.deltaTime;
        _cc.Move(_movementVelocity);

        //마우스 왼쪽 버튼을 클릭하고, 땅에 닿은 상태라면 공격 수행
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

    //플레이어 상태를 변환하는 함수
    public void SwitchStateTo(CharacterState newState)
    {
        if (IsPlayer)
        {
            _playerInput.MouseButtonDown = false;
        }
        //currentState -> newState / newState->currentState로의 전환에 대한 
        //모든 경우에 대해 switch case 작성
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
                // Normal 상태로 전환될 때 Idle 애니메이션을 재생하도록 설정합니다.
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

        //함수 동작 확인을 위한 콘솔 디버그 메시지 출력
        UnityEngine.Debug.Log("Switched to " + CurrentState);
    }
    public void AttackAnimationEnds()
    {
        SwitchStateTo(CharacterState.Normal);
    }

    //데미지를 적용하는 함수
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

    //체력이 0이면 오브젝트 비활성화
    public void DisableCharacter()
    {
        gameObject.SetActive(false);
        DropItem();

        if(spawner != null)
        {
            spawner.MonsterDied();
        }
    }

    //아이템 드롭을 위한 함수
    public void DropItem()
    {
        if(ItemToDrop != null)
        {
            Instantiate(ItemToDrop, transform.position, Quaternion.Euler(0f, 180f, 0f));
        }
    }

    public void ReviveCharacter()
    {

        // 캐릭터를 활성화하고 체력을 복구
        gameObject.SetActive(true);

        // CharacterController를 다시 활성화
        _cc.enabled = true;
        
        if (_health != null)
        {
            _health.ResetHealth();
        }
        
        // 캐릭터의 상태를 Normal
        SwitchStateTo(CharacterState.Normal);
    }

    private void PlayAttackSound()
    {
        if (attackSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(attackSound);  // 공격 소리 재생
        }
    }


}