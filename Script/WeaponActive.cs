using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//무기(도구) 활성, 비활성 상태를 설정하는 스크립트
public class WeaponActive : MonoBehaviour
{
    public GameObject slingshot; // 슬링샷 오브젝트를 연결할 변수
    public GameObject hammer;    // 해머 오브젝트를 연결할 변수
    public GameObject flashlight; //손전등 오브젝트를 연결할 변수

    void Start()
    {
        // 시작할 때 모든 무기를 비활성화
        slingshot.SetActive(false);
        hammer.SetActive(false);
        flashlight.SetActive(false);
    }

    void Update()
    {
        // 1번 키를 누르면 슬링샷 활성화, 해머 비활성화, 손전등 비활성화 
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            slingshot.SetActive(true);
            hammer.SetActive(false);
            flashlight.SetActive(false);
        }
        // 2번 키를 누르면 해머 활성화, 슬링샷 비활성화, 손전등 비활성화 
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            slingshot.SetActive(false);
            hammer.SetActive(true);
            flashlight.SetActive(false);
        }

        //3번 키를 누르면 손전등 활성화, 슬링샷 비활성화, 해머 비활성화
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            slingshot.SetActive(false);
            hammer.SetActive(false);
            flashlight.SetActive(true);
        }
    }
}
