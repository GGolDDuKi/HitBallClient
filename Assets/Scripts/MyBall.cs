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

    [SerializeField] GameObject direction;

    protected override void Start()
    {
        base.Start();
        isDragging = false;
        direction.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();

        if(Managers.Game.isStart)
        {
            Managers.Game.Timer -= Time.deltaTime;
        }

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
                if (hit.collider.gameObject.tag != "MainBall")
                    return;

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

                // ȭ��ǥ ���� ����
                Vector3 dragDirection = dragEndPos - dragStartPos;
                direction.SetActive(true); // �巡�� ������ �� ȭ��ǥ ���̱�
                direction.transform.position = transform.position; // ȭ��ǥ�� �� ��ġ��
                direction.transform.rotation = Quaternion.LookRotation(-dragDirection); // �巡�� �ݴ� �������� ȸ��

                // ȭ��ǥ ����(ũ��)�� �巡�� ���̿� ����ϰ� ����
                float arrowScale = Mathf.Clamp(dragDirection.magnitude, 0.1f, 10f);
                direction.transform.localScale = new Vector3(1, 1, arrowScale); // Z�� �������θ� ũ�� ����
            }
        }

        // ���콺 ��ư�� ������ ��
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            direction.SetActive(false); // �巡�� ������ ȭ��ǥ �����

            if (rb != null)
            {
                Vector3 dragDirection = dragEndPos - dragStartPos;

                // �߻� ���� ����(�巡�� �ݴ� ����)
                Vector3 forceDirection = -dragDirection.normalized;

                // �߻� �� ����(�巡�� ���� ���)
                float forceMagnitude = Mathf.Clamp(dragDirection.magnitude * 5f, 0.1f, 50);

                rb.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
            }
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        // ���� �ſ� ������ �ٷ� ���߱�
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

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (!Managers.Game.isStart)
            return;

        string targetTag = collision.gameObject.tag;

        switch (targetTag)
        {
            case "RedBall":
                Managers.Game.HitRedBall();
                break;
            case "YellowBall":
                Managers.Game.HitYellowBall();
                break;
            default:
                return;
        }
    }
}
