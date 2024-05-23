using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPopup : MonoBehaviour
{
    public Image itemImage; // Inspector���� �Ҵ��� ������ �̹���
    public GameObject sphere;
    public void Start()
    {
        itemImage.gameObject.SetActive(false);
    }
    // �÷��̾�� �浹 �� ȣ��� �Լ�
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // "Player" �±װ� �޸� ������Ʈ�� �浹 ��
        {
            itemImage.gameObject.SetActive(true); // �̹����� Ȱ��ȭ

            UnityEngine.Debug.Log("�̹��� Ȱ��ȭ");

            if (sphere != null) Destroy(sphere);
        }
    }

    public void CloseImage()
    {
        UnityEngine.Debug.Log("�ݱ�");
        Destroy(itemImage.gameObject);
    }
}
