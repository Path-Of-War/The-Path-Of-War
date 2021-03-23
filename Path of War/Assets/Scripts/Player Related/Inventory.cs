using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //Properties
    bool isEnable;

    public GameObject inventory;

    public static GameObject slotHovered;

    public Material selectedMat;
    public Material unselectedMat;

    //Functions
    private void Start()
    {
        
    }

    private void Update()
    {
        //Slots detection
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.tag == "InvSlot")
            {
                if(slotHovered)
                    slotHovered.GetComponent<Renderer>().material = unselectedMat;
                slotHovered = hit.transform.gameObject;
                slotHovered.GetComponent<Renderer>().material = selectedMat;
                print("Hovering inventory slot " + hit.transform.name);
            }
        }

        //enable Inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            isEnable = !isEnable;
            Player.instance.canMove = !isEnable;
        }

        if(Input.GetMouseButtonDown(0) && isEnable && slotHovered)
        {
            slotHovered.GetComponent<Slots>().UseItem();
        }

        if (!isEnable)
        {
            if (slotHovered)
                slotHovered.GetComponent<Renderer>().material = unselectedMat;
            slotHovered = null;
        }

        inventory.SetActive(isEnable);
    }
}
