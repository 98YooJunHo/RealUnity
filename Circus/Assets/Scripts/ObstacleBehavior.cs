using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{
    private float speed;
    void Start()
    {
        speed = 6f;
        // 10�ۼ�Ʈ�� ���� Ȱ��ȭ
        if (Random.Range(0, 10) == 0)
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
