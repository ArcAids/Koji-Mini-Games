//using UnityEngine;


//public class WeaponController : MonoBehaviour, IElemental
//{ 
//    [SerializeField] Transform gunPivot;
//    [SerializeField] Transform gunHolder;
//    [SerializeField] IElementalWeapon weapon;
//    [SerializeField] ElementalEventController elementController;
//    [SerializeField] WeaponsEventController weaponEvent;
//    [SerializeField] WeaponHUDManager hudManager;
//    [SerializeField] GunPointer pointer;
    

//    Elements currentElement;
//    IWeaponInput input;
//    SpriteRenderer sprite;

//    Vector3 aimDirection;
//    float aimAngle;
//    public Elements Element
//    {
//        get => currentElement; private set
//        {
//            currentElement = value;
//        }
//    }

//    private void Start()
//    {
//        input = GetComponent<IWeaponInput>();
        
//    }

//    private void OnEnable()
//    {
//        elementController.RegisterElmentSwitch(this);
//    }

//    private void OnDisable()
//    {
//        elementController.DeregisterElmentSwitch(this);
//    }

//    private void Update()
//    {
//        input.SetWeaponInputs();
//        Aim();
//        if(input.Shooting)
//            PullTrigger();
//    }

//    void Equip(IElementalWeapon weapon)
//    {
//        this.weapon = weapon;
//        weapon.Equip();
//        weapon.SwitchElement(Element);
//        pointer.Deactivate();
//        gunHolder.localScale = new Vector2(1, 1);
//        weapon.gunTransform.parent = gunHolder;
//        weapon.gunTransform.localPosition = Vector2.zero;
//        weapon.gunTransform.localRotation= Quaternion.identity;
//        sprite = weapon.gunTransform.GetComponent<SpriteRenderer>();
//        hudManager.SetWeaponData(weapon.gunTransform.name,weapon.AmmoLeft,weapon.AmmoLeft,sprite.sprite);
//    }


//    void Aim()
//    {
//        aimDirection.x= input.MouseXDirection;
//        aimDirection.y= input.MouseYDirection;
//        aimAngle = Vector2.SignedAngle(Vector2.right,aimDirection);
//        if (sprite != null)
//        {
//            weapon?.FlipSpriteY(aimDirection.x < 0);
//        }
//        else
//        if (aimDirection.x <0)
//            gunHolder.localScale = new Vector2(1,-1);
//        else
//            gunHolder.localScale = new Vector2(1,1);

//        gunPivot.rotation =Quaternion.Euler(0,0,aimAngle);
//    }

//    void PullTrigger()
//    {
//        if (weapon == null)
//            return;

//        if (weapon.Shoot())
//        {
//            weaponEvent.OnWeaponShot();
//            hudManager.UpdateAmmoCount(weapon.AmmoLeft);
//        }
//        if (weapon.AmmoLeft<=0)
//        {
//            weapon.Dequip();
//            weapon = null;
//            hudManager.SetDefaultState();
//        }
        
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (this.weapon != null)
//            return;
//        IElementalWeapon weapon=collision.GetComponent<IElementalWeapon>();
//        if (weapon!=null)
//        {
//            Equip(weapon);
//        }
//    }

//    public void SwitchElement(Elements element)
//    {
//        Element = element;
//        weapon?.SwitchElement(element);
//    }




//}
