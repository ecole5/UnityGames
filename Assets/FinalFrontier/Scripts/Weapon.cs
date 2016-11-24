using UnityEngine;


//This is an enum of the various possible weapon types, including shield type for power up

public enum WeaponType
{
    none, //default / no weapon
    blaster, //simple blaster
    spread, //Two shots simultaneously
    phaser, //Shots that move in waves (for extension)
    missile, //Homing missiles  (for extension later
    laser, //Damage over time
    shield
}
// The WeaponDefinition class allows you to set the properties
[System.Serializable]
public class WeaponDefinition
{
    public WeaponType type = WeaponType.none;
    public string letter; //The letter to show on the power-up
    public Color color = Color.white; //Color of Collar & power-up
    public GameObject projectilePrefab; // Prefab for projectiles
    public Color projectileColor = Color.white;
    public float damageOnHit = 0; // Amount of damage caused
    public float continuousDamage = 0; //Damage per second (Laser)
    public float delayBetweenShots = 0;
    public float velocity = 20; // Speed of projectiles
}


public class Weapon : MonoBehaviour
{
    static public Transform PROJECTILE_ANCHOR;
    public bool ____________________;
    [SerializeField]
    private WeaponType _type = WeaponType.none;
    public WeaponDefinition def;
    public GameObject collar;
    public float lastShot; // Time last shot was fired

    void Awake()
    {
        collar = transform.Find("Collar").gameObject;
    }

    void Start()
    {

        // Call SetType() properly for the default _type
        SetType(_type);
        if (PROJECTILE_ANCHOR == null)
        {
            GameObject go = new GameObject("_Projectile_Anchor");
            PROJECTILE_ANCHOR = go.transform;
        }
        // Find the fireDelegate of the parent
        GameObject parentGO = transform.parent.gameObject;
        if (parentGO.tag == "Player")
        {
            Player.S.fireDelegate += Fire;
        }
    }

    public WeaponType type
    {
        get { return (_type); }
        set { SetType(value); }
    }

    public void SetType(WeaponType wt)
    {
        _type = wt;

        if (type == WeaponType.none)
        {
            this.gameObject.SetActive(false);
            return;

        }
        else
        {
            this.gameObject.SetActive(true);
        }

        def = Main.GetWeaponDefinition(_type);
        collar.GetComponent<Renderer>().material.color = def.color;
        lastShot = 0;
    }
    public void Fire()
    {
        // If this.gameObject is inactive
        if (!gameObject.activeInHierarchy) return;

        //Note enough time between shots 
        if (Time.time - lastShot < def.delayBetweenShots)
        {
            return;
        }
        Projectile p;
        switch (type)
        {

            case WeaponType.blaster:
                p = MakeProjectile();
                p.GetComponent<Rigidbody>().velocity = Vector3.up * def.velocity;
                break;

            case WeaponType.spread:
                p = MakeProjectile();
                p.GetComponent<Rigidbody>().velocity = Vector3.up * def.velocity;
                p = MakeProjectile();
                p.GetComponent<Rigidbody>().velocity = new Vector3(-.2f, 0.9f, 0) * def.velocity;
                p = MakeProjectile();
                p.GetComponent<Rigidbody>().velocity = new Vector3(.2f, 0.9f, 0) * def.velocity;
                break;
        }
    }

    public Projectile MakeProjectile()
    { //make the projectile

        //NEW!!: Add fire sounds 
        Main.S.shootBox.clip = MissionControl.huston.shootSounds[GameData.Prefs.space.shootChoice];
        Main.S.shootBox.Play();



        GameObject go = Instantiate(def.projectilePrefab) as GameObject;
        if (transform.parent.gameObject.tag == "Player")
        {
            go.tag = "ProjectilePlayer";
            go.layer = LayerMask.NameToLayer("ProjectilePlayer");
        }
        else
        {
            go.tag = "ProjectileEnemy";
            go.layer = LayerMask.NameToLayer("ProjectileEnemy");
        }

        go.transform.position = collar.transform.position;
        go.transform.parent = PROJECTILE_ANCHOR;
        Projectile p = go.GetComponent<Projectile>();
        p.type = type;
        lastShot = Time.time;
        return (p);
    }
}