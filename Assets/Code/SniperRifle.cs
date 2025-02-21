using UnityEngine;
using TMPro;

public class SniperRifle : MonoBehaviour
{
    public float range = 300f;
    public Camera fpsCamera;
    public float fireRate = 1f;
    private float nextTimeToFire = 0f;
    
    public int enemiesKilled = 0;
    public TextMeshProUGUI scoretext;

    public GameObject impactEffect;

    // Sniper scope settings
    public float scopeFOV = 5f;
    public float normalFOV = 60f;
    public GameObject Scope;

    private bool isScoped = false;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            Shoot();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            ToggleScope();
        }

        if(enemiesKilled == 17)
        {
            scoretext.text = "GOOD WORK AGENT";
        }
    }

    void Shoot()
    {
        nextTimeToFire = Time.time + 1f / fireRate;

        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            if(hit.transform.CompareTag("enemy"))
            {
                Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hit.transform.gameObject);

                enemiesKilled++;
                scoretext.text = enemiesKilled+"/17";
            }
        }
    }

    void ToggleScope()
    {
        isScoped = !isScoped;
        
        if (isScoped)
        {
            fpsCamera.fieldOfView = scopeFOV;
            Scope.SetActive(true);
            CameraControls.mouseSensitivity = 0.1f;
            PlayerControls.Speed = 2f;
        }
        else
        {
            fpsCamera.fieldOfView = normalFOV;
            Scope.SetActive(false);
            CameraControls.mouseSensitivity = 2f;
            PlayerControls.Speed = 5f;
        }
    }
}
