using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{
    public GameObject reticle;
    public RectTransform reticleArea;
    public Camera cam;

    public Transform temp;
    public GameObject tempGO;
    public LayerMask maskMask;

    public float minSize;
    public float maxSize;
    public float retSpeed;
    public float recoil;
    float currentSize;
    float reticleChangeCutoff = 0.4f;
    float screenX;
    float screenY;

    // Start is called before the first frame update
    void Start()
    {
        reticleArea = reticle.GetComponent<RectTransform>();
        screenX = Screen.width;
        screenY = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical);
        if (reticle.activeSelf)
        {
            if(direction.magnitude > reticleChangeCutoff)
            {
                currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime * retSpeed);
            }//enlarge reticle
            else
            {
                currentSize = Mathf.Lerp(currentSize, minSize, Time.deltaTime * retSpeed);
            }//shrink reticle
            //Debug.Log(direction.magnitude);

            reticleArea.sizeDelta = new Vector2(currentSize, currentSize);
        }
    }

    public void ToggleReticle(bool on)
    {
        //likely part of the issue, probably called in the wrong spot
        currentSize = maxSize;
        reticle.SetActive(on);
    }

    public void Shoot()
    {
        #region shot direction
        float horzOffset = Random.Range(reticleArea.rect.xMin, reticleArea.rect.xMax);
        float vertOffset = Random.Range(reticleArea.rect.yMin, reticleArea.rect.yMax);
        horzOffset = (screenX / 2 + horzOffset)/screenX;
        vertOffset = (screenY / 2 + vertOffset) / screenY;
        #endregion

        #region fire shot
        Ray ray = cam.ViewportPointToRay(new Vector2(horzOffset, vertOffset));
        RaycastHit hit;

        if(Physics.Raycast(temp.position, ray.direction, out hit, 10f, maskMask))
        {
            if(hit.transform.GetComponent<EnemyHitStun>())
            {
                hit.transform.GetComponent<EnemyHitStun>().StartHitstun();
            }
        }
        //Debug.DrawRay(temp.position, ray.direction * 15f, Color.red, 3f);
        #endregion

        #region recoil info
        currentSize += recoil;
        if(currentSize>maxSize)
        {
            currentSize = maxSize;
        }
        #endregion
    }
}