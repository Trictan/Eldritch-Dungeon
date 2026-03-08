using UnityEngine;

public class Flower1Shooting : MonoBehaviour
{
    public GameObject seedBullet;
    public GameObject specialBullet;
    public Transform seedBulletPos;
    public int bulletsShoot = 1;
    public float fireDelay;
    private float timer = 0.73f;
    private GameObject player;
    private GameObject projectileFolder;
    public bool specialAttack=false;

    private float timerAlive = 11;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        projectileFolder = GameObject.FindGameObjectWithTag("projectileParent");
    }

    // Update is called once per frame
    void Update()
    {
        timerAlive -= Time.deltaTime;
        if(timerAlive <= 0)
        {
            ChangeDelay();
            timerAlive = 10;
        }
        
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if(distance < 11) //Hardcoding should be changed maybe.
        {
           timer -= Time.deltaTime;

           if(timer <= 0)
            {
                timer = fireDelay;
                shoot();
            }
        }
    }

    void shoot()
    {
        
        Vector2 baseDir = (player.transform.position - seedBulletPos.position).normalized;
        if(bulletsShoot == 1)
        {
            SpawnBullet(baseDir);
        }

        if(bulletsShoot == 3)
        {
            float spreadAngle = 15; //OK?

            SpawnBullet(RotateVector(baseDir, -spreadAngle));
            SpawnBullet(baseDir);
            SpawnBullet(RotateVector(baseDir, spreadAngle));
        }
        if(bulletsShoot == 24) //väldigt fin kod här
        {
            float spreadAngle = 15; 
            SpawnBullet(RotateVector(baseDir, 11*-spreadAngle));
            SpawnBullet(RotateVector(baseDir, 10*-spreadAngle));
            SpawnBullet(RotateVector(baseDir, 9*-spreadAngle));
            SpawnBullet(RotateVector(baseDir, 8*-spreadAngle));
            SpawnBullet(RotateVector(baseDir, 7*-spreadAngle));
            SpawnBullet(RotateVector(baseDir, 6*-spreadAngle));
            SpawnBullet(RotateVector(baseDir, 5*-spreadAngle));
            SpawnBullet(RotateVector(baseDir, 4*-spreadAngle));
            SpawnBullet(RotateVector(baseDir, 3*-spreadAngle));
            SpawnBullet(RotateVector(baseDir, 2*-spreadAngle));
            SpawnBullet(RotateVector(baseDir, -spreadAngle));
            SpawnBullet(baseDir);
            SpawnBullet(RotateVector(baseDir, spreadAngle));
            SpawnBullet(RotateVector(baseDir, 2*spreadAngle));
            SpawnBullet(RotateVector(baseDir, 3*spreadAngle));
            SpawnBullet(RotateVector(baseDir, 4*spreadAngle));
            SpawnBullet(RotateVector(baseDir, 5*spreadAngle));
            SpawnBullet(RotateVector(baseDir, 6*spreadAngle));
            SpawnBullet(RotateVector(baseDir, 7*spreadAngle));
            SpawnBullet(RotateVector(baseDir, 8*spreadAngle));
            SpawnBullet(RotateVector(baseDir, 9*spreadAngle));
            SpawnBullet(RotateVector(baseDir, 10*spreadAngle));
            SpawnBullet(RotateVector(baseDir, 11*spreadAngle));
            SpawnBullet(RotateVector(baseDir, 12*spreadAngle));
        }
    }

    void SpawnBullet(Vector2 dir)
    {   
        if (specialAttack && specialBullet != null)
        {
            GameObject Specialbullet = Instantiate(specialBullet, seedBulletPos.position, Quaternion.identity, projectileFolder.transform);
            Specialbullet.GetComponent<FlowerBulletScript>().SetDirection(dir);
            
        }
        else
        {
            GameObject bullet = Instantiate(seedBullet, seedBulletPos.position, Quaternion.identity, projectileFolder.transform);
            bullet.GetComponent<FlowerBulletScript>().SetDirection(dir);
        }
        
    }

    Vector2 RotateVector(Vector2 basedir, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        return new Vector2(basedir.x * cos - basedir.y * sin, basedir.x * sin + basedir.y * cos);
    }

    void ChangeDelay()
    {
        fireDelay -= 0.1f;
    }
}
