using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//����(����) Ȱ��, ��Ȱ�� ���¸� �����ϴ� ��ũ��Ʈ
public class WeaponActive : MonoBehaviour
{
    public GameObject slingshot; // ������ ������Ʈ�� ������ ����
    public GameObject hammer;    // �ظ� ������Ʈ�� ������ ����
    public GameObject flashlight; //������ ������Ʈ�� ������ ����

    void Start()
    {
        // ������ �� ��� ���⸦ ��Ȱ��ȭ
        slingshot.SetActive(false);
        hammer.SetActive(false);
        flashlight.SetActive(false);
    }

    void Update()
    {
        // 1�� Ű�� ������ ������ Ȱ��ȭ, �ظ� ��Ȱ��ȭ, ������ ��Ȱ��ȭ 
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            slingshot.SetActive(true);
            hammer.SetActive(false);
            flashlight.SetActive(false);
        }
        // 2�� Ű�� ������ �ظ� Ȱ��ȭ, ������ ��Ȱ��ȭ, ������ ��Ȱ��ȭ 
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            slingshot.SetActive(false);
            hammer.SetActive(true);
            flashlight.SetActive(false);
        }

        //3�� Ű�� ������ ������ Ȱ��ȭ, ������ ��Ȱ��ȭ, �ظ� ��Ȱ��ȭ
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            slingshot.SetActive(false);
            hammer.SetActive(false);
            flashlight.SetActive(true);
        }
    }
}
