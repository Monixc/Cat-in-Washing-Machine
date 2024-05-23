using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour
{
    public GameObject slingshot; // ������ ������Ʈ�� ������ ����
    public GameObject hammer;    // �ظ� ������Ʈ�� ������ ����
    private Animator animator;   // Girl �ִϸ����� ������Ʈ�� ������ ����

    private bool isSlingshotActive = false; // ������ Ȱ��ȭ ���θ� ��Ÿ���� ����
    private bool isHammerActive = false;    // �ظ� Ȱ��ȭ ���θ� ��Ÿ���� ����

    void Start()
    {
        // Girl ĳ������ Animator ������Ʈ ��������
        animator = GetComponent<Animator>();

        // ������ �� ��� ���⸦ ��Ȱ��ȭ
        slingshot.SetActive(false);
        hammer.SetActive(false);
    }

    void Update()
    {
        // 1�� Ű�� ������ ������ Ȱ��ȭ, �ظ� ��Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isSlingshotActive = true;
            isHammerActive = false;
            slingshot.SetActive(true);
            hammer.SetActive(false);
        }
        // 2�� Ű�� ������ �ظ� Ȱ��ȭ, ������ ��Ȱ��ȭ
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isSlingshotActive = false;
            isHammerActive = true;
            slingshot.SetActive(false);
            hammer.SetActive(true);
        }

        // F Ű�� ������ �ش� ���⿡ �´� ���� �ִϸ��̼� ����
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isSlingshotActive)
            {
                // ������ ���� �ִϸ��̼� ����
                animator.Play("Girl_action_slingshot");
            }
            else if (isHammerActive)
            {
                // �ظ� ���� �ִϸ��̼� ����
                animator.Play("Girl_action_hammer");
            }
        }
    }
}
