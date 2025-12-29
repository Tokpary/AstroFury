using System;
using System.Linq;
using Components;
using Patterns.ObjectPool.Components;
using Scripts.Patterns.State.Components;
using Scripts.Patterns.State.States;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Vector3 = System.Numerics.Vector3;

namespace Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        private Player _player;
        
        private Rigidbody2D _rb;
        private Vector2 _movement;
        private PlayerInput _playerInput;
        private Camera mainCamera;
        private Vector2 _mousePosition;

        [SerializeField] private float _maxForce;

        public static UnityEvent OnShoot;
        public static UnityEvent<Player> OnCollide;
        public bool CanShoot {  get; private set; } 
        
        private float _shootTimer;

        void Start()
        {
            _player = GetComponentInParent<Player>();
            _rb = GetComponentInParent<Rigidbody2D>();
            _playerInput = GetComponentInParent<PlayerInput>();
            mainCamera = Camera.main;
            
            InputActionManager.OnFiredStarted += StartShooting;
            InputActionManager.OnFiredEnded += EndShooting;
            InputActionManager.OnAimed += RotateTowardsMouse;
        }

        private void OnDestroy()
        {
            InputActionManager.OnFiredStarted -= StartShooting;
            InputActionManager.OnFiredEnded -= EndShooting;
            InputActionManager.OnAimed -= RotateTowardsMouse;
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if(GameManager.Instance.GetCurrentState() is not PlayingState)
                return;
            
            AEntity enemy = other.GetComponent<AEntity>();
            
            if(enemy != null) 
            {
                _player.TakeDamage(enemy.GetEntitySO().Damage);
                enemy.TakeDamage(_player.Damage);
            }
            
            
        }

        private void StartShooting()
        {
            _shootTimer = 0;
            CanShoot = true;
        }
        
        private void EndShooting()
        {
            CanShoot = false;
        }



        // PLAYER MOVEMENT
        
        private void Update()
        {
            if(GameManager.Instance.GetCurrentState() is not PlayingState)
                return;
            
            _movement = _playerInput.actions["Move"].ReadValue<Vector2>();
            if (CanShoot)
            {
                _shootTimer += Time.deltaTime;
                if (_shootTimer >= _player.FireRate)
                {
                    _shootTimer = 0;
                    ShootProjectile();
                }
            }
        }
        
        private void ShootProjectile()
        {
            OnShoot?.Invoke();
        }
        
        private void FixedUpdate()
        {
            if(GameManager.Instance.GetCurrentState() is not PlayingState)
                return;
            
            Vector2 newPosition = _rb.position + new Vector2(_movement.x, _movement.y) * _player.Speed * Time.fixedDeltaTime;
            newPosition = ClampPositionToScreen(newPosition);
            _rb.MovePosition(newPosition);
/*
                  _rb.AddForce(new Vector2(_movement.x, _movement.y) * _player.Speed);
                  if(_rb.velocity.magnitude > _player.Speed)
                  {
                      _rb.velocity = _rb.velocity.normalized * _player.Speed;
                  }

            */
        }
         
        
        Vector2 ClampPositionToScreen(Vector2 position)
        {
            
            Vector2 minScreenBounds = mainCamera.ScreenToWorldPoint(new Vector2(0, 0));
            Vector2 maxScreenBounds = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            position.x = Mathf.Clamp(position.x, minScreenBounds.x, maxScreenBounds.x);
            position.y = Mathf.Clamp(position.y, minScreenBounds.y, maxScreenBounds.y);

            return position;
        }
        
        
        // PLAYER ORIENTATION
        
        void RotateTowardsMouse(InputAction.CallbackContext context)
        {
           
                
            
            
            if(context.action.bindings[0].groups.Contains("Keyboard&Mouse"))
            {
                _mousePosition = mainCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
            
                Vector2 lookDirection = _mousePosition - _rb.position;
                float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
                _rb.MoveRotation(angle);
            }
            else
            {
                // LOGICA CON DELTA DE JOYSTICK
            }
        }
    }
}