using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class Player : MonoBehaviour
{
    private HealthSystem HealthSystem;
    private Rigidbody rb;
    private InputMaster Controls;
    private Animator Animator;
    private bool IsShooting;
    private bool ClickChack = true;
    private bool RollIsCoolDown = true;
    [SerializeField]
    private float RollCoolDownTime;
    [SerializeField]
    private float Speed;
    [SerializeField]
    private bool MouseRotating;
    [SerializeField]
    private GameObject BulletPrefab;
    [SerializeField]
    private Transform FirePoint;
    [SerializeField]
    private VisualEffect ShootingEffect;
    [SerializeField]
    private VisualEffect RollEffect;
    
    private void Awake()
    {
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
    }
    private void FixedUpdate()
    {
        MoveMent();
        if(rb.velocity.x == 0 || rb.velocity.y == 0)
        {
            RollEffect.SendEvent("Stop");
        }
    }
    private void MoveMent()
    {
        Vector2 InputVector = Controls.Player.Movement.ReadValue<Vector2>();
        rb.MovePosition(new Vector3(InputVector.x, 0, InputVector.y) * Speed * Time.deltaTime + rb.position);
        MoveAnimation();
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
        if (!IsShooting)
        {
            if (ClickChack)
            {
                ClickChack = false;
                IsShooting = true;
                StartCoroutine(HoldShooting(1f));
            }
        }
        else
        {
            IsShooting = false;
            return;
        }
        
    }
    IEnumerator HoldShooting(float Cycle)
    {
        while (IsShooting)
        {
            Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
            ShootingEffect.SendEvent("Shoot");
            ShootAnimation(1f);
            yield return new WaitForSeconds(Cycle);
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
        if((InputVector.x != 0 || InputVector.y != 0) && RollIsCoolDown)
        {
            RollIsCoolDown = false;
            Vector3 Target = new Vector3(InputVector.x, 0, InputVector.y);
            rb.AddForce(Target * 1000 * Distance * Time.deltaTime, ForceMode.VelocityChange);
            StartCoroutine(RollCoolDownCount(RollCoolDownTime));
            RollEffect.SendEvent("Roll");
        }
    }
    private IEnumerator RollCoolDownCount(float time)
    {
        yield return new WaitForSeconds(time);
        RollIsCoolDown = true;
    }
    private void OnEnable()
    {
        Controls.Enable();
    }
    private void OnDisable()
    {
        Controls.Disable();
    }

    public void SetHealthSystem(HealthSystem HealthSystem)
    {
        this.HealthSystem = HealthSystem;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            HealthSystem.Damage(10);
        }
    }
}
