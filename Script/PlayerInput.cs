using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float HorizontalInput;
    public float VerticalInput;
    public bool JumpInput;

    //플레이어 상태 관리를 위한 변수 선언
    public bool MouseButtonDown;

    private Character _character; // Character 클래스의 인스턴스

    private void Awake()
    {
        _character = GetComponent<Character>(); // Character 클래스의 인스턴스를 가져옵니다.
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
        JumpInput = Input.GetKeyDown(KeyCode.Space);

        if(!MouseButtonDown && Time.timeScale != 0)
        {
            MouseButtonDown = Input.GetMouseButtonDown(0);
        }
    }

    private void OnDisable()
    {
        HorizontalInput = 0;
        VerticalInput = 0;
        JumpInput = false;
        MouseButtonDown = false;
    }
}
