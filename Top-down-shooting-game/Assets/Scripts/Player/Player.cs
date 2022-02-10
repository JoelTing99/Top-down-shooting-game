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
    private Vector2 LaseVector;
    private bool IsShooting;
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
    [SerializeField] private Transform FirePoint;
    [SerializeField] private Transform ThrowGrenadePoint;
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
        Animator.SetFloat("AttackSpeed", GameManager.GetAttackSpeed());
        //ThrowGrenadeTime = GameManager.GetGrendaeCoolDownTime();
        //RollCoolDownTime = GameManager.GetRollCoolDownTime();
        //RollDistance = GameManager.GetRollDistance();
        //ShootingPeriod = GameManager.GetAttackSpeed();
        //Speed = GameManager.GetPlayerSpeed();
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
        Controls.Player.Throw.performed += _ => ThrowGrenadeAnimation();
    }
    private void FixedUpdate()
    {
        changeVelocity();
        MoveMent();
        MoveAnimation();
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
        if (IsStun)
        {
            SetLayerWeight("GetHit", 1);
        }
        else
        {
            SetLayerWeight("GetHit", 0);
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
        if (InputVector.x > 0 && VelocityX < 0.5f)
        {
            VelocityX += Time.deltaTime * Acceleration;
        }
        if (InputVector.x == 0 && VelocityX > 0)
        {
            VelocityX -= Time.deltaTime * Deceleration;
        }
        if (InputVector.x < 0 && VelocityX > -0.5f)
        {
            VelocityX -= Time.deltaTime * Acceleration;
        }
        if (InputVector.x == 0 && VelocityX < 0)
        {
            VelocityX += Time.deltaTime * Deceleration;
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
            //rb.AddForce(new Vector3(InputVector.x, 0, InputVector.y) * GameManager.GetPlayerSpeed() * Time.deltaTime);
            Vector3 velocity = rb.velocity;
            velocity.x = VelocityX * Time.deltaTime * GameManager.GetPlayerSpeed();
            velocity.z = VelocityZ * Time.deltaTime * GameManager.GetPlayerSpeed();
            rb.velocity = velocity;
        }
    }
    private void MoveAnimation()
    {
        Animator.SetFloat("Velocity X", VelocityX);
        Animator.SetFloat("Velocity Z", VelocityZ);
        Animator.SetFloat("Angle", RotateAngle / 180);
    }
    private void JoystickRotation()
    {
        Vector2 InputVector = Controls.Player.JoystickDirection.ReadValue<Vector2>();
        float TurnAngle = Mathf.Atan2(InputVector.y, InputVector.x) * Mathf.Rad2Deg;
        RotateAngle = TurnAngle;
        transform.rotation = Quaternion.Euler(new Vector3(0f, -TurnAngle + 90, 0f));
    }
    private void MouseRotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Controls.Player.MouseDirection.ReadValue<Vector2>());
        if (Physics.Raycast(ray, out RaycastHit hitinfo, 300f))
        {
            Vector3 Target = hitinfo.point;
            Vector3 LookDirection = Target - transform.position;
            float TurnAngle = Mathf.Atan2(LookDirection.z, LookDirection.x) * Mathf.Rad2Deg;
            RotateAngle = TurnAngle;
            transform.rotation = Quaternion.Euler(new Vector3(0f, -TurnAngle + 90, 0f));
        }
    }
    private void Shooting()
    {
        if (!IsShooting && !HoldingThrow)
        {
            Animator.SetLayerWeight(Animator.GetLayerIndex("Shoot"), 1);
            IsShooting = true;
        }
        else
        {
            Animator.SetLayerWeight(Animator.GetLayerIndex("Shoot"), 0);
            IsShooting = false;
        }
    }
    private void Fire()
    {
        ShootingEffect.SendEvent("Shoot");
        Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
    }
    private void Roll(float Distance)
    {
        if(CanRoll && !IsStun)
        {
            Animator.SetTrigger("Roll");
            CanRoll = false;
            StartCoroutine(RollCoolDownCount(GameManager.GetRollCoolDownTime()));
            RollEffect.SendEvent("Roll");
        }
    }
    private void Rollingg()
    {
        VelocityX = 0;
        VelocityZ = 0;
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
    private void ThrowGrenadeAnimation()
    {
        if(CanThrowGrenade && HoldingThrow && !IsStun)
        {
            HoldingThrow = false;
            CanThrowGrenade = false;
            Animator.SetLayerWeight(Animator.GetLayerIndex("Throw"), 1);
        }
        else if(CanThrowGrenade)
        {
            HoldingThrow = true;
        }
    }
    private void ThrowGrenade()
    {
        GameObject grenade = Instantiate(Grenade, ThrowGrenadePoint.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce((ThrowGrenadePoint.up + transform.forward) * GameManager.GetThrowGrenadeDistance(), ForceMode.VelocityChange);
        StartCoroutine(ThrowGrenadeCoolDownCount(GameManager.GetGrendaeCoolDownTime()));
        StartCoroutine(SetLayerWeight("Throw", -1));
    }
    private IEnumerator SetLayerWeight(string Name, int Constant)
    {
        float weight = Animator.GetLayerWeight(Animator.GetLayerIndex(Name));

        while (weight >= 0)
        {
            weight += Constant * Time.deltaTime;
            Animator.SetLayerWeight(Animator.GetLayerIndex(Name), weight);
            if(weight == 0 || weight == 1)
            {
                break;
            }
            yield return null;
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
        Vector3 StartingPosition = ThrowGrenadePoint.position;
        Vector3 StartingVelosity = (ThrowGrenadePoint.up + transform.forward) * GameManager.GetThrowGrenadeDistance();
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
        Vector3 StartingVelosity = transform.forward;
        for (float i = 0; i < 15; i++)
        {
            Vector3 NewPoint = StartingPosition + i * StartingVelosity;
            points.Add(NewPoint);
            if (Physics.OverlapSphere(NewPoint, 0.1f, CollidableLayer).Length > 0)
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
