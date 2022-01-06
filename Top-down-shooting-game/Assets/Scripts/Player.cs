using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private GameManager GameManager;
    private InputMaster Controls;
    private Animator Animator;
    private LineRenderer Line;
    private bool IsShooting;
    private bool ClickChack = true;
    private bool CanRoll = true;
    private bool CanThrowGrenade = true;
    private bool HoldingThrow;
    private bool IsStun;
    [SerializeField] private Image ShootingPeriodImage;
    [SerializeField] private float ShootingPeriod;
    [SerializeField] private Image RollCoolDownImage;
    [SerializeField] private float RollCoolDownTime;
    [SerializeField] private GameObject Grenade;
    [SerializeField] private Image ThrowGrenadeCoolDownImage;
    [SerializeField] private float ThrowGrenadeTime;
    [SerializeField] private float Speed;
    [SerializeField] private bool MouseRotating;
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private VisualEffect ShootingEffect;
    [SerializeField] private VisualEffect RollEffect;
    [SerializeField] private int NumPoints;
    [SerializeField] private LayerMask CollidableLayer;
    
    public bool isStun
    {
        get { return IsStun; }
        set { IsStun = value; }
    }
    private void Awake()
    {
        GameManager = FindObjectOfType<GameManager>();
        Line = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
        Controls = new InputMaster();
        if (MouseRotating)
        {
            Controls.Player.MouseDirection.performed += _ => MouseRotation();
        }
        else
        {
            Controls.Player.JoystickDirection.performed += _ => JoystickRotation();
        }
        Controls.Player.Shooting.performed += _ => Shooting();
        Controls.Player.Roll.performed += _ => Roll(1);
        Controls.Player.Throw.performed += _ => ThrowGrenade();
    }
    private void FixedUpdate()
    {
        MoveMent();
        if(rb.velocity.x == 0 || rb.velocity.y == 0)
        {
            RollEffect.SendEvent("Stop");
        }
        if (HoldingThrow)
        {
            DrawProjection();
        }
        else
        {
            Line.positionCount = 0;
        }
    }
    private void MoveMent()
    {
        if (!IsStun)
        {
            Vector2 InputVector = Controls.Player.Movement.ReadValue<Vector2>();
            rb.MovePosition(new Vector3(InputVector.x, 0, InputVector.y) * Speed * Time.deltaTime + rb.position);
            MoveAnimation();
        }
    }
    private void MoveAnimation()
    {
        if(Controls.Player.Movement.ReadValue<Vector2>().x != 0 || Controls.Player.Movement.ReadValue<Vector2>().y != 0)
        {
            Animator.SetBool("IsWalking", true);
        }
        else
        {
            Animator.SetBool("IsWalking", false);
        }
    }
    private void JoystickRotation()
    {
        Vector2 InputVector = Controls.Player.JoystickDirection.ReadValue<Vector2>();
        float TurnAngle = Mathf.Atan2(InputVector.y, InputVector.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0f, -TurnAngle, 0f));
    }
    private void MouseRotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Controls.Player.MouseDirection.ReadValue<Vector2>());
        if (Physics.Raycast(ray, out RaycastHit hitinfo, 300f))
        {
            Vector3 Target = hitinfo.point;
            Vector3 LookDirection = Target - transform.position;
            float TurnAngle = Mathf.Atan2(LookDirection.z, LookDirection.x) * Mathf.Rad2Deg + 90f;
            transform.rotation = Quaternion.Euler(new Vector3(0f, -TurnAngle, 0f));
        }
    }
    private void Shooting()
    {
        if (!IsShooting && ClickChack)
        {
            ClickChack = false;
            IsShooting = true;
            StartCoroutine(HoldShooting(ShootingPeriod));
        }
        else
        {
            IsShooting = false;
            ShootingPeriodImage.fillAmount = 1;
            return;
        }
    }
    IEnumerator HoldShooting(float Cycle)
    {
        float Count = Cycle;
        while (IsShooting && !IsStun)
        {
            Count -= Time.deltaTime;
            ShootingPeriodImage.fillAmount = Count / ShootingPeriod;
            if(Count <= 0)
            {
                Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
                ShootingEffect.SendEvent("Shoot");
                ShootAnimation(ShootingPeriod);
                Count = Cycle;
            }
            yield return null;
        }
        ClickChack = true;
    }
    private void ShootAnimation(float Speed)
    {
        Animator.SetFloat("AnimSpeed", Speed);
        Animator.SetTrigger("Shoot");
    }
    private void Roll(float Distance)
    {
        Vector2 InputVector = Controls.Player.Movement.ReadValue<Vector2>();
        if((InputVector.x != 0 || InputVector.y != 0) && CanRoll && !IsStun)
        {
            CanRoll = false;
            Vector3 Target = new Vector3(InputVector.x, 0, InputVector.y);
            rb.AddForce(Target * 1000 * Distance * Time.deltaTime, ForceMode.VelocityChange);
            StartCoroutine(RollCoolDownCount(RollCoolDownTime));
            RollEffect.SendEvent("Roll");
        }
    }
    private IEnumerator RollCoolDownCount(float time)
    {
        while(time >= 0)
        {
            time -= Time.deltaTime;
            RollCoolDownImage.fillAmount = time / RollCoolDownTime;
            yield return null;
        }
        CanRoll = true;
    }
    private void ThrowGrenade()
    {
        Debug.Log("Click");
        if(CanThrowGrenade && HoldingThrow && !IsStun)
        {
            HoldingThrow = false;
            CanThrowGrenade = false;
            GameObject grenade = Instantiate(Grenade, FirePoint.position, transform.rotation);
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            rb.AddForce((FirePoint.transform.up - transform.forward) * GameManager.GetThrowDistance(), ForceMode.VelocityChange) ;
            StartCoroutine(ThrowGrenadeCoolDownCount(ThrowGrenadeTime));
        }
        else if(CanThrowGrenade)
        {
            HoldingThrow = true;
        }
    }
    private IEnumerator ThrowGrenadeCoolDownCount(float time)
    {
        while(time >= 0)
        {
            time -= Time.deltaTime;
            ThrowGrenadeCoolDownImage.fillAmount = time / ThrowGrenadeTime;
            yield return null;
        }
        CanThrowGrenade = true;
    }
    private void DrawProjection()
    {
        Line.positionCount = NumPoints;
        List<Vector3> points = new List<Vector3>();
        Vector3 StartingPosition = FirePoint.position;
        Vector3 StartingVelosity = (FirePoint.up - transform.forward) * GameManager.GetThrowDistance();
        for (float i = 0; i < NumPoints; i += 0.1f)
        {
            Vector3 NewPoint = StartingPosition + i * StartingVelosity;
            NewPoint.y = StartingPosition.y + i * StartingVelosity.y + Physics.gravity.y / 2f * i * i;
            points.Add(NewPoint);
            if(Physics.OverlapSphere(NewPoint, 0.1f, CollidableLayer).Length > 0)
            {
                Line.positionCount = points.Count;
                break;
            }
        }
        Line.SetPositions(points.ToArray());
    }

    private void OnEnable()
    {
        Controls.Enable();
    }
    private void OnDisable()
    {
        Controls.Disable();
    }
    
}
