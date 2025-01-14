using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject projectilePrefab;
    public float projectileSpeed = 100f;
    public AudioClip spellSFX;

    public Image reticleImage;
    public Color reticleDementorColor;
    private Color originalReticleColor;
    void Start()
    {
        originalReticleColor = reticleImage.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject projectile = 
                Instantiate(projectilePrefab, transform.position + transform.forward, transform.rotation) as GameObject;

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            
            rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);
            
            projectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);
            
            AudioSource.PlayClipAtPoint(spellSFX, transform.position);
        }
    }

    private void FixedUpdate()
    {
        ReticleEffect();
    }

    void ReticleEffect()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                reticleImage.color = Color.Lerp(reticleImage.color, reticleDementorColor, Time.deltaTime * 2);

                reticleImage.transform.localScale =
                    Vector3.Lerp(reticleImage.transform.localScale, new Vector3(.7f, .7f, 1), Time.deltaTime * 2);
            }
            else
            {
                reticleImage.color = Color.Lerp(reticleImage.color, originalReticleColor, Time.deltaTime * 2);

                reticleImage.transform.localScale =
                    Vector3.Lerp(reticleImage.transform.localScale, Vector3.one, Time.deltaTime * 2);
            }
        }
        
    }
}
