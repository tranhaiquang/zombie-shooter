using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletPoint;
    [SerializeField] private float bulletSpeed = 2f;
    [SerializeField] private float fireRate = 3f;
    [SerializeField] private GameObject gun;
    [SerializeField] private Animator gunAnimator;
    public Camera cam;
    void Start()
    {
        gun = GameObject.Find("Ak-47");
        Animator gunAnimator = gun.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot()
    {
        setTrigger();
        StartCoroutine(ShootWithFireRate(fireRate));
    }
    IEnumerator ShootWithFireRate(float fireRate)
    {
        yield return new WaitForSeconds(fireRate);
        
        FindObjectOfType<AudioManager>().Play("Gun Shoot");
        Vector3 targetPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = cam.ScreenPointToRay(targetPosition);
        Vector3 direction = ray.direction;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            direction = hit.point - bulletPoint.transform.position;
        }

        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, Quaternion.Euler(90f - bulletPoint.transform.rotation.eulerAngles.x,  bulletPoint.transform.rotation.eulerAngles.y,  bulletPoint.transform.rotation.eulerAngles.z));
        bullet.GetComponent<Rigidbody>().AddForce(direction.normalized * bulletSpeed, ForceMode.Impulse);

    }

    public void setTrigger()
    {
        gunAnimator.SetTrigger("shoot");
    }

    public void resetTrigger()
    {
        gunAnimator.ResetTrigger("shoot");
    }
}
