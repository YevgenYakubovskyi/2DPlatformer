using Player;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExternalDevicesInputReader : IEntityInputSource
{
    public float HorizontalDirection => Input.GetAxisRaw("Horizontal");
    public bool Jump { get; private set;  }
    public bool Attack { get; private set;  }
    public void OnUpdate()
    {
        if(Input.GetButtonDown("Jump"))
            Jump = true;
        if (IsPointerOverUi() && Input.GetButtonDown("Fire1"))
            Attack = true;
        if (Input.GetButtonUp("Fire1"))
            Attack = false;
    }
    
    private bool IsPointerOverUi() => !EventSystem.current.IsPointerOverGameObject();

    
    public void ResetOneTimeActions()
    {
        Jump = false;
    }
}