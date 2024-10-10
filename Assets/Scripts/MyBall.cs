using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBall : Ball
{
    public Vector3 myVelocity;
    bool isDragging;
    public LayerMask myBallLayer;
    public LayerMask groundLayer;
    public Vector3 dragStartPos;
    public Vector3 dragEndPos;


    protected override void Start()
    {
        base.Start();
        Managers.Game.Init();
        isDragging = false;
    }

    protected override void Update()
    {
        base.Update();
        Managers.Game.Timer -= Time.deltaTime;
        myVelocity = rb.velocity;

        // ���콺 Ŭ�� �� ���̸� �߻��Ͽ� �� �� ����
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //�� ������ Ȯ��
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, myBallLayer))
            {
                dragStartPos = hit.point;
                dragStartPos.y = 1f;

                isDragging = true;
            }
        }

        // ���콺 �巡�� ��
        if (isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                dragEndPos = hit.point;
                dragEndPos.y = 1f;
            }
        }

        // ���콺 ��ư�� ������ ��
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;

            if (rb != null)
            {
                // dragStartPos�κ��� dragEndPos������ ���� ���
                Vector3 dragDirection = dragEndPos - dragStartPos;

                // �ݴ� �������� ���� ���ϱ� ���� ���͸� ����
                Vector3 forceDirection = -dragDirection.normalized;

                // �Ÿ� ����Ͽ� �� ũ�� ���� (�Ÿ� * �� ���)
                float forceMagnitude = Mathf.Clamp(dragDirection.magnitude * 10f, 0.1f, 50);

                // myBall�� ���� ����
                rb.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        string targetLayer = LayerMask.LayerToName(collision.gameObject.layer);

        switch (targetLayer)
        {
            case "RedBall":
                Managers.Game.HitRedBall();
                break;
            case "YellowBall":
                Managers.Game.HitYellowBall();
                break;
        }
    }
}
