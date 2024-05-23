using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float HorizontalInput;
    public float VerticalInput;
    public bool JumpInput;

    //�÷��̾� ���� ������ ���� ���� ����
    public bool MouseButtonDown;

    private Character _character; // Character Ŭ������ �ν��Ͻ�

    private void Awake()
    {
        _character = GetComponent<Character>(); // Character Ŭ������ �ν��Ͻ��� �����ɴϴ�.
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
