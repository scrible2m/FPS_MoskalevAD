using JetBrains.Annotations;
using System.CodeDom.Compiler;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlayer : Unit
{
    [SerializeField] private GameObject TempGO;
    [SerializeField] private bool Select;
    [SerializeField] private LayerMask Layer;
    [SerializeField] private Animator _damageSprite;
    [SerializeField] private Animator _healSprite;

    private Transform Mcam;
    private RaycastHit Hit;

    protected Text _hp;
    protected int _tempHP;


    void Start()
    {
        _tempHP = Health;
        Mcam = Camera.main.transform;
        _hp = GameObject.FindWithTag("HP").GetComponent<Text>();
        _damageSprite = GameObject.Find("DamageSprite").GetComponent<Animator>();
        _healSprite = GameObject.Find("HealSprite").GetComponent<Animator>();
       
    }
    public void PickupItem(IInventory obj, int ID)
    {
        if (obj != null)
        {
            obj.PickupItem(ID);
            
        }

    }
    void Update()
    {
        if (Physics.Raycast(Mcam.position, Mcam.forward, out Hit, 7f, Layer))
        {
            Collider target = Hit.collider;
            if (target.tag == "Pickup")
            {
                if (!TempGO)
                {
                    TempGO = target.gameObject;
                }
                Select = true;

                if (TempGO.GetInstanceID() != target.gameObject.GetInstanceID())
                {
                    Select = false;
                    TempGO = null;
                }
            }
            else
            {
                Select = false;
            }
        }
        else
        {
            Select = false;
        }
        if (Select)
        {
            TempGO.GetComponentInChildren<SpriteRenderer>().enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
               int ID =  TempGO.GetComponent<PickupGun>().weaponID;
                PickupItem(GetComponentInChildren<IInventory>(), ID);
                Destroy(TempGO);

            }
        }
        else
        {
            if (TempGO)
            {
                TempGO.GetComponentInChildren<SpriteRenderer>().enabled = false ;
            }
        }
        _hp.text = "+" + Health.ToString();

        if (_tempHP>Health)
        {
            _damageSprite.SetTrigger("Fade");
            _tempHP = Health;

        }
        if (_tempHP < Health)
        {
           _healSprite.SetTrigger("Fade");
            _tempHP = Health;
        }

    }
   

}
