using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBall : Ball
{
    [SerializeField] bool isDragging;
    [SerializeField] bool isMoving;

    Vector3 dragStartPos;
    Vector3 dragEndPos;

    public LayerMask myBallLayer;
    public LayerMask groundLayer;
    
    public Vector3 velocity;

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

        if (isMoving)
            return;

        // ���콺 Ŭ�� �� ���̸� �߻��Ͽ� �� �� ����
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);

            RaycastHit hit;

            // �� ������ Ȯ��
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, myBallLayer))
            {
                dragStartPos = transform.position;
                isDragging = true;
            }
        }

        // ���콺 �巡�� ��
        if (isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.green, 2f);

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
                Vector3 dragDirection = dragEndPos - dragStartPos;

                //�߻� ���� ����(�巡�� �ݴ� ����)
                Vector3 forceDirection = -dragDirection.normalized;

                //�߻� �� ����(�巡�� ���� ���)
                float forceMagnitude = Mathf.Clamp(dragDirection.magnitude * 5f, 0.1f, 50);

                rb.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
            }
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        //���� �ſ� ������ �ٷ� ���߱�
        if (rb.velocity.magnitude < 0.05f)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            isMoving = false;
            Managers.Game.EndTurn();
        }
        else
        {
            isMoving = true;
        }

        // ������ velocity üũ
        if (velocity != rb.velocity)
        {
            velocity = rb.velocity;
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
