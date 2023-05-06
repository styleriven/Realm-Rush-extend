using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockColor = Color.gray;
    TextMeshPro label;
    Vector2Int coordinates ;

    WayPoint wayPoint;

    void Awake() {
        label = GetComponent<TextMeshPro>();
        wayPoint = GetComponentInParent<WayPoint>();
        label.enabled =true;
        DisplayCoordinates();
        displayParent();
    }
    // Update is called once per frame
    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            displayParent();
        }

        SetLabelColor();
        ToggleLabels();
    }
    void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }
    void SetLabelColor()
    {
        if(wayPoint.IsPlaceable)
        {
            label.color = defaultColor;
        }
        else
        {
            label.color = blockColor;
        }
    }
    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x)/10;
        coordinates.y = Mathf.RoundToInt(transform.parent.transform.localPosition.z)/10;
        
        label.text = coordinates.ToString();
        
    }
    void displayParent()
    {
        transform.parent.name = coordinates.ToString();
    }
}
