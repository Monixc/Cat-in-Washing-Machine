using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPopup : MonoBehaviour
{
    public Image itemImage; // Inspector에서 할당할 아이템 이미지
    public GameObject sphere;
    public void Start()
    {
        itemImage.gameObject.SetActive(false);
    }
    // 플레이어와 충돌 시 호출될 함수
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // "Player" 태그가 달린 오브젝트와 충돌 시
        {
            itemImage.gameObject.SetActive(true); // 이미지를 활성화

            UnityEngine.Debug.Log("이미지 활성화");

            if (sphere != null) Destroy(sphere);
        }
    }

    public void CloseImage()
    {
        UnityEngine.Debug.Log("닫기");
        Destroy(itemImage.gameObject);
    }
}
