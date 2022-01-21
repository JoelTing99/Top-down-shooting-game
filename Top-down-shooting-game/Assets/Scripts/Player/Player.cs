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
    private Transform FirePoint;
    private Vector2 LaseVector;
    private bool IsShooting;
    private bool ClickChack = true;
    private bool CanRoll = true;
    private bool CanThrowGrenade = true;
    private bool HoldingThrow;
    private bool IsStun;
    private bool RollCoolDownImageActive;
    private bool GrenadeCoolDownImageActive;
    private float VelocityX = 0;
    private float VelocityZ = 0;
    private float RotateAngle;
    private float Acceleration = 2f;
    private float Deceleration = 2f;
    private float ThrowGrenadeTime;
    private float RollCoolDownTime;
    private float RollDistance;
    private float ShootingPeriod;
    private float Speed;
    private float ReloadImagefillAmount;
    private float RollCoolDownImagefillAmount;
    private float GrenadeCoolDownImagefillAmount;
    [SerializeField] private int NumPoints;
    [SerializeField] private bool MouseRotating;
    [SerializeField] private GameObject Grenade;
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private VisualEffect ShootingEffect;
    [SerializeField] private VisualEffect RollEffect;
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
        //ThrowGrenadeTime = GameManager.GetGrendaeCoolDownTime();
        //RollCoolDownTime = GameManager.GetRollCoolDownTime();
        //RollDistance = GameManager.GetRollDistance();
        //ShootingPeriod = GameManager.GetAttackSpeed();
        //Speed = GameManager.GetPlayerSpeed();
        FirePoint = transform.Find("FirePoint");
        if (MouseRotating)
        {
            Controls.Player.MouseDirection.performed += _ => MouseRotation();
        }
        else
        {
            Controls.Player.JoystickDirection.performed += _ => JoystickRotation();
        }
        Controls.Player.Shooting.performed += _ => Shooting();
        Controls.Player.Roll.performed += _ => Roll(GameManager.GetRollDistance());
        Controls.Player.Throw.performed += _ => ThrowGrenade();
    }
    private void FixedUpdate()
    {
        changeVelocity();
        MoveMent();
    }
    private void Update()
    {
        
        if (rb.velocity.x == 0 || rb.velocity.y == 0)
        {
            RollEffect.SendEvent("Stop");
        }
        if (HoldingThrow)
        {
            DrawGrenadeProjection();
        }
        else
        {
            DrawProjection();
        }
    }
    private void changeVelocity()
    {
        Vector2 InputVector = Controls.Player.Movement.ReadValue<Vector2>();
        if (InputVector.y > 0 && VelocityZ < 0.5f)
        {
            VelocityZ += Time.deltaTime * Acceleration;
        }
        if (InputVector.y == 0 && VelocityZ > 0)
        {
            VelocityZ -= Time.deltaTime * Deceleration;
        }
        if (InputVector.y < 0 && VelocityZ > -0.5f)
        {
            VelocityZ -= Time.deltaTime * Acceleration;
        }
        if (InputVector.y == 0 && VelocityZ < 0)
        {
            VelocityZ += Time.deltaTime * Deceleration;
        }
        if (InputVector.y == 0 && VelocityZ != 0 && VelocityZ > -0.05 && VelocityZ < 0.05)
        {
            VelocityZ = 0;
        }
        if (InputVector.x > 0 && VelocityX > -0.5f)
        {
            VelocityX -= Time.deltaTime * Acceleration;
        }
        if (InputVector.x == 0 && VelocityX < 0)
        {
            VelocityX += Time.deltaTime * Deceleration;
        }
        if (InputVector.x < 0 && VelocityX < 0.5f)
        {
            VelocityX += Time.deltaTime * Acceleration;
        }
        if (InputVector.x == 0 && VelocityX > 0)
        {
            VelocityX -= Time.deltaTime * Deceleration;
        }
        if (InputVector.x == 0 && VelocityX != 0 && VelocityX > -0.05 && VelocityX < 0.05)
        {
            VelocityX = 0;
        }
    }
    private void MoveMent()
    {
        if (!IsStun)
        {
            Vector2 InputVector = Controls.Player.Movement.ReadValue<Vector2>();
            transform.position += new Vector3(InputVector.x, 0, InputVector.y) * GameManager.GetPlayerSpeed() * Time.deltaTime;
            MoveAnimation();
        }
    }
    private void MoveAnimation()
    {
        if (Controls.Player.Movement.ReadValue<Vector2>().x != 0 || Controls.Player.Movement.ReadValue<Vector2>().y != 0)
        {
            Animator.SetBool("IsWalking", true);
        }
        else
        {
            Animator.SetBool("IsWalking", false);
        }
        Animator.SetFloat("Velocity X", VelocityX);
        Animator.SetFloat("Velocity Z", VelocityZ);
        if(RotateAngle < 0)
        {
            Animator.SetFloat("Angle", -RotateAngle / 360);
        }
        else
        {
            Animator.SetFloat("Angle", RotateAngle / 360);
        }
    }
    private void JoystickRotation()
    {
        Vector2 InputVector = Controls.Player.JoystickDirection.ReadValue<Vector2>();
        float TurnAngle = Mathf.Atan2(InputVector.y, InputVector.x) * Mathf.Rad2Deg - 90;
        RotateAngle = TurnAngle;
        transform.rotation = Quaternion.Euler(new Vector3(0f, -TurnAngle, 0f));
    }
    private void MouseRotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Controls.Player.MouseDirection.ReadValue<Vector2>());
        if (Physics.Raycast(ray, out RaycastHit hitinfo, 300f))
        {
            Vector3 Target = hitinfo.point;
            Vector3 LookDirection = Target - transform.position;
            float TurnAngle = Mathf.Atan2(LookDirection.z, LookDirection.x) * Mathf.Rad2Deg - 90;
            RotateAngle = TurnAngle;
            Debug.Log(TurnAngle);
            transform.rotation = Quaternion.Euler(new Vector3(0f, -TurnAngle, 0f));
        }
    }
    private void Shooting()
    {
        if (!IsShooting && ClickChack && !HoldingThrow)
        {
            ClickChack = false;
            IsShooting = true;
            StartCoroutine(HoldShooting(GameManager.GetAttackSpeed()));
        }
        else
        {
            IsShooting = false;
            return;
        }
    }
    IEnumerator HoldShooting(float Cycle)
    {
        float Count = Cycle;
        while (IsShooting && !IsStun)
        {
            Count -= Time.deltaTime;
            ReloadImagefillAmount = Count / GameManager.GetAttackSpeed();
            if(Count <= 0)
            {
                Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
                ShootingEffect.SendEvent("Shoot");
                ShootAnimation(GameManager.GetAttackSpeed());
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
            rb.AddForce(Target * Distance, ForceMode.Impulse);
            StartCoroutine(RollCoolDownCount(GameManager.GetRollCoolDownTime()));
            RollEffect.SendEvent("Roll");
        }
    }
    private IEnumerator RollCoolDownCount(float time)
    {
        RollCoolDownImageActive = true;
        while(time >= 0)
        {
            time -= Time.deltaTime;
            RollCoolDownImagefillAmount = time / GameManager.GetRollCoolDownTime();
            yield return null;
        }
        CanRoll = true;
        RollCoolDownImageActive = false;
    }
    private void ThrowGrenade()
    {
        if(CanThrowGrenade && HoldingThrow && !IsStun)
        {
            HoldingThrow = false;
            CanThrowGrenade = false;
            GameObject grenade = Instantiate(Grenade, FirePoint.position, transform.rotation);
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            rb.AddForce((FirePoint.transform.up - transform.forward) * GameManager.GetThrowGrenadeDistance(), ForceMode.VelocityChange) ;
            StartCoroutine(ThrowGrenadeCoolDownCount(GameManager.GetGrendaeCoolDownTime()));
        }
        else if(CanThrowGrenade)
        {
            HoldingThrow = true;
        }
    }
    private IEnumerator ThrowGrenadeCoolDownCount(float time)
    {
        GrenadeCoolDownImageActive = true;
        while(time >= 0)
        {
            time -= Time.deltaTime;
            GrenadeCoolDownImagefillAmount = time / GameManager.GetGrendaeCoolDownTime();
            yield return null;
        }
        CanThrowGrenade = true;
        GrenadeCoolDownImageActive = false;
    }
    private void DrawGrenadeProjection()
    {
        List<Vector3> points = new List<Vector3>();
        Vector3 StartingPosition = FirePoint.position;
        Vector3 StartingVelosity = (FirePoint.up - transform.forward) * GameManager.GetThrowGrenadeDistance();
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
    private void DrawProjection()
    {
        List<Vector3> points = new List<Vector3>();
        Vector3 StartingPosition = FirePoint.position;
        Vector3 StartingVelosity = -transform.forward;
        for (float i = 0; i < 15; i++)
        {
            Vector3 NewPoint = StartingPosition + i * StartingVelosity;
            points.Add(NewPoint);
            if (Physics.OverlapSphere(NewPoint, 0.3f, CollidableLayer).Length > 0)
            {
                Line.positionCount = points.Count;
                break;
            }
        }
        Line.SetPositions(points.ToArray());
    }
    public float GetReloadImagefillAmount()
    {
        return ReloadImagefillAmount;
    }
    public float GetRollCoolDownImagefillAmount()
    {
        return RollCoolDownImagefillAmount;
    }
    public bool GetRollCoolDownImageActive()
    {
        return RollCoolDownImageActive;
    }
    public float GetGrenadeCoolDownImagefillAmount()
    {
        return GrenadeCoolDownImagefillAmount;
    }
    public bool GetGrenadeCoolDownImageActive()
    {
        return GrenadeCoolDownImageActive;
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
