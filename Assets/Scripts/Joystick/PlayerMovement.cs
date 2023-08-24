using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PhotonView _photonView;
    [SerializeField] private Weapon _weapon;
    [SerializeField] protected float Speed;

    private FixedJoystick _joystick;
    private Rigidbody2D _rb;
    private Vector2 _move;

    public static bool PointerDown = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _joystick = FindObjectOfType<FixedJoystick>();
    }

    private void Update()
    {
        if (!_photonView.IsMine)
            return;

        if (PointerDown == true)
        {
            _move.x = _joystick.Horizontal;
            _move.y = _joystick.Vertical;

            float hAxis = _move.x;
            float vAxis = _move.y;
            float zAxis = Mathf.Atan2(hAxis, vAxis) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0f, 0f, -zAxis);

            _weapon.TryShoot(_move);
        }
    }

    private void FixedUpdate()
    {
        if (PointerDown == false)
            _rb.velocity = Vector3.zero;
        else
            _rb.MovePosition(_rb.position + _move * Speed * Time.fixedDeltaTime);
    }
}
