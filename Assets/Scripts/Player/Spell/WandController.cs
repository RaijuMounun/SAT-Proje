using UnityEngine;

public class WandController : MonoBehaviour
{
    public static WandController Instance;
    
    Camera _camera;
    Transform _playerTransform;
    GameObject _spellParent;
    CharacterController _playerSc;
    int _counter;
    float _timer;

    public float spellSpeed, knockBackPower, spellLifeTime;
    public Vector2 spellDamageRange;
    [SerializeField] float cDTime;
    [SerializeField] int spellPoolSize;
    [SerializeField] GameObject spellPrefab;

    private void Awake()
    {
        if(Instance == null) Instance = this;
        _camera = Camera.main;
        _spellParent = GameObject.Find("SpellParent");
        _timer = cDTime - 0.01f;
    }

    private void Start()
    {
        _playerSc = CharacterController.Instance;
        _playerTransform = _playerSc.transform;
        SpellPool(spellPoolSize, spellPrefab);
    }

    private void Update()
    {
        Rotation();
        
        _timer += Time.deltaTime;
        if(Input.GetMouseButtonDown(0)) OnClick();
    }


    void Rotation()
    {
        //Get mouse position
        var mousePos = Input.mousePosition;  //Get mouse position
        mousePos.z = -_camera.transform.position.z;  //Set z to be the distance from the camera
        var worldPos = _camera.ScreenToWorldPoint(mousePos); //Convert mouse position to world position
        //Turn to mouse position
        var transform1 = transform;
        var spriteDirection = worldPos - transform1.position;  //Get direction to mouse position
        transform1.up = spriteDirection; //Turn to mouse position
        transform1.position = _playerTransform.position; //Set position to player position
    }

    void SpellPool(int poolSize, GameObject prefab)
    {
        for (var i = 0; i < poolSize; i++)
        {
            var spell = Instantiate(prefab, transform.position + new Vector3(0, 0.7f,0), Quaternion.identity);
            spell.SetActive(false);
            spell.name= "Spell " + i;
            spell.transform.parent = _spellParent.transform;
        }
    }

    void OnClick()
    {
        if(_timer < cDTime) return; //Cooldown
        _timer = 0f; //Reset timer
        _counter++; //Increase counter
        if (_counter >= spellPoolSize) _counter = 0; //Reset counter if it's bigger than pool size
        CastSpell();
    }

    void CastSpell()
    {
        var spell = _spellParent.transform.GetChild(_counter).gameObject; //Get spell from pool
        spell.transform.up = transform.up; //Turn spell to mouse position
        spell.SetActive(true); //Activate spell
    }
    
}
