using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRay : MonoBehaviour
{
    private Camera myCamera;
    private Purse myPurse;
    public GameObject tower;
    public Transform towerParent;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = GetComponent<Camera>();
        myPurse = GameObject.FindGameObjectWithTag("Purse").GetComponent<Purse>();
    }

  // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseClick = Input.mousePosition;

            Ray ray = myCamera.ScreenPointToRay(mouseClick);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.GetComponent<Enemy>().Damage(20);
                }
            }

            if (hit.transform.tag == "TowerPosition")
            {
                // Calls PlaceTower() to add defense when TowerPosition tag is clicked on
                if (myPurse.PlaceTower(500))
                {
                    Instantiate(tower, hit.transform.position, Quaternion.identity, towerParent);
                    Destroy(hit.transform.gameObject);
                }
            }        
        }
    }   
}
