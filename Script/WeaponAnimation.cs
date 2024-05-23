using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour
{
    public GameObject slingshot; // 슬링샷 오브젝트를 연결할 변수
    public GameObject hammer;    // 해머 오브젝트를 연결할 변수
    private Animator animator;   // Girl 애니메이터 컴포넌트를 연결할 변수

    private bool isSlingshotActive = false; // 슬링샷 활성화 여부를 나타내는 변수
    private bool isHammerActive = false;    // 해머 활성화 여부를 나타내는 변수

    void Start()
    {
        // Girl 캐릭터의 Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>();

        // 시작할 때 모든 무기를 비활성화
        slingshot.SetActive(false);
        hammer.SetActive(false);
    }

    void Update()
    {
        // 1번 키를 누르면 슬링샷 활성화, 해머 비활성화
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isSlingshotActive = true;
            isHammerActive = false;
            slingshot.SetActive(true);
            hammer.SetActive(false);
        }
        // 2번 키를 누르면 해머 활성화, 슬링샷 비활성화
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isSlingshotActive = false;
            isHammerActive = true;
            slingshot.SetActive(false);
            hammer.SetActive(true);
        }

        // F 키를 누르면 해당 무기에 맞는 공격 애니메이션 실행
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isSlingshotActive)
            {
                // 슬링샷 공격 애니메이션 실행
                animator.Play("Girl_action_slingshot");
            }
            else if (isHammerActive)
            {
                // 해머 공격 애니메이션 실행
                animator.Play("Girl_action_hammer");
            }
        }
    }
}
