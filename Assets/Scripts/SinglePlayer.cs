using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class SinglePlayer : Unit
{
    [SerializeField] private GameObject TempGO;
    [SerializeField] private bool Select;
    [SerializeField] private LayerMask Layer;
    [SerializeField] private Animator _damageSprite;
    [SerializeField] private Animator _healSprite;
    [SerializeField] private GameObject _healthKit;
    [SerializeField] private GameObject _mine;
    [SerializeField] private Rigidbody _hRB;
    [SerializeField] private Rigidbody _mRB;

    private Transform Mcam;
    private RaycastHit Hit;

    protected Text _hp;
    protected int _tempHP;
    private ISaveData _data;
    private ISaveStuff _stuff;
    [SerializeField] Transform _stufParent;

    protected override void Awake()
    {
        _stuff = new XMLStuff();
        PlayerData SinglePlayerData = new PlayerData
        {
            PlayerDead = Dead,
            PlayerHealth = Health,
            PlayerName = name,
        };
        _healthKit = Resources.Load<GameObject>("Prefabs/HealthKit");
        _mine = Resources.Load<GameObject>("Prefabs/Mine.prefab");
        // _data.Save(SinglePlayerData);
        // PlayerData NEWPlayer = _data.Load();
    }


    void Start()
    {
        _tempHP = Health;
        Mcam = Camera.main.transform;
        _hp = GameObject.FindWithTag("HP").GetComponent<Text>();
        _damageSprite = GameObject.Find("DamageSprite").GetComponent<Animator>();
        _healSprite = GameObject.Find("HealSprite").GetComponent<Animator>();
        _stufParent = GameObject.Find("StuffParent").transform;
        _healthKit = Resources.Load<GameObject>("Prefabs/Stuff/HealthKit");
        _mine = Resources.Load<GameObject>("Prefabs/Stuff/Mine");
        _hRB = _healthKit.GetComponent<Rigidbody>();
        _mRB = _mine.GetComponent<Rigidbody>();
        Debug.Log(_mine.name);
        Debug.Log(_healthKit.name);
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
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject temp = Instantiate(_mine, gameObject.transform.position+Vector3.one, gameObject.transform.rotation);
            temp.transform.parent = _stufParent;
            temp.GetComponent<Rigidbody>().AddForce(0, 0, -1, ForceMode.Impulse);
            temp.name = "Mine";
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameObject temp = Instantiate(_healthKit, gameObject.transform.position+Vector3.one, gameObject.transform.rotation);
            temp.transform.parent = _stufParent;
            temp.name = "HealthKit";
            temp.GetComponent<Rigidbody>().AddForce(0, 0, -1, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            Load();
        }
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
                int ID = TempGO.GetComponent<PickupGun>().weaponID;
                PickupItem(GetComponentInChildren<IInventory>(), ID);
                Destroy(TempGO);

            }
        }
        else
        {
            if (TempGO)
            {
                TempGO.GetComponentInChildren<SpriteRenderer>().enabled = false;
            }
        }
        _hp.text = "+" + Health.ToString();

        if (_tempHP > Health)
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

    private void Save()
    {

        DroppedStuff[] tempStuff = new DroppedStuff[_stufParent.childCount];
        for (int i = 0; i < _stufParent.childCount; i++)
        {
            Transform temp = _stufParent.GetChild(i);
            {
                tempStuff[i].PrefName = temp.name;
                tempStuff[i].PrefPos = temp.position;
                tempStuff[i].PrefRotation = temp.rotation;
                tempStuff[i].PrefScale = temp.localScale;
                tempStuff[i].PrefTag = tag;
            }
            _stuff.Save(tempStuff, tempStuff.Length);

        }
    }
    private void Load()
    {
        DroppedStuff[] tempStuff = _stuff.Load();
        Debug.Log("StuffLength" +   tempStuff.Length);
        foreach (DroppedStuff tempDS in tempStuff)
        {
            
            GameObject _temp = Resources.Load<GameObject>($"Prefabs/Stuff/{tempDS.PrefName}");
            GameObject _tempGO = Instantiate(_temp, tempDS.PrefPos, tempDS.PrefRotation);
            _tempGO.transform.localScale = tempDS.PrefScale;
            _tempGO.name = tempDS.PrefName;
            _tempGO.transform.parent = _stufParent;
        }
    }
   

}
