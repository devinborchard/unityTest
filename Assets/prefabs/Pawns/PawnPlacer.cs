using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PawnPlacer : MonoBehaviour
{
    public int UnitPoints = 10;
    public GameObject Scout;
    public GameObject Soldier;
    public GameObject Archer;
    public GameObject Cleric;
    public GameObject King;

    public GameObject UIPrefab;
    private GameObject ui;
    private TMP_Text unitPointsText;

    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;

    private GameObject selectedUnit;
    private GameObject selectedTile;
    private bool kingPlaced = false;
    private GameObject ReadyButton;

    void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        // create pawn placing ui
        ui = Instantiate(UIPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        // find ready button element
        Transform canvasTransformReadyButton = ui.transform.Find("Canvas - Ready Button");
        Transform buttonTransform = canvasTransformReadyButton.Find("Button");
        ReadyButton = buttonTransform.gameObject;
        ReadyButton.SetActive(false);

        // find ui element for unit points available
        Transform canvasTransformUnitPoints = ui.transform.Find("Canvas - Unit Points");
        Transform textTransform = canvasTransformUnitPoints.Find("Text (TMP)");
        unitPointsText = textTransform.GetComponent<TMP_Text>();

        // get raycaster for unit bar
        Transform canvasTransformUnitBar = ui.transform.Find("Canvas - Unit Bar");
        raycaster = canvasTransformUnitBar.gameObject.GetComponent<GraphicRaycaster>();
                  
        
    }

    void DetectUnitSelect(){
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            PointerEventData pointerData = new PointerEventData(eventSystem)
            {
                position = Input.mousePosition // Set pointer position to the mouse position
            };

            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerData, results); // Perform the raycast

            if (results.Count > 0)
            {
                if(selectedUnit){
                    Destroy(selectedUnit);
                }

                // Debug.Log("Hit UI Element: " + results[0].gameObject.name);
                string canvasName = results[0].gameObject.name;
                if(canvasName.Contains("king")){
                    selectedUnit = Instantiate(King, new Vector3(0, 0, 0), Quaternion.identity);
                }
                else if(canvasName.Contains("soldier")){
                    selectedUnit = Instantiate(Soldier, new Vector3(0, 0, 0), Quaternion.identity);
                }
                else if(canvasName.Contains("archer")){
                    selectedUnit = Instantiate(Archer, new Vector3(0, 0, 0), Quaternion.identity);
                }
                else if(canvasName.Contains("scout")){
                    selectedUnit = Instantiate(Scout, new Vector3(0, 0, 0), Quaternion.identity);
                }
                else if(canvasName.Contains("cleric")){
                    selectedUnit = Instantiate(Cleric, new Vector3(0, 0, 0), Quaternion.identity);
                }
            }
        }
    }

    void DetectUnitDeselect(){
        
        if (Input.GetMouseButtonDown(1)) // Right mouse button click
        {
            if(selectedUnit){
                Destroy(selectedUnit);
                selectedUnit = null;     
            }
            else{

                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Convert screen point to world point
                Collider2D[] colliders = Physics2D.OverlapPointAll(mousePosition);

                if(colliders.Length>0){
                    foreach (Collider2D collider in colliders)
                    {
                        if(collider.gameObject.tag == "pawn"){
                            int unitCost = collider.gameObject.GetComponent<Pawn>().cost;
                            UnitPoints = UnitPoints + unitCost;

                            if(collider.gameObject.name.Contains("king") || collider.gameObject.name.Contains("King")){
                                kingPlaced = false;
                                ReadyButton.SetActive(false);
                            }

                            Destroy(collider.gameObject);
                            GameObject unitsTile = collider.gameObject.GetComponent<Pawn>().tileOccupying;
                            unitsTile.GetComponent<Tile>().Occupied = false;
                        }
                    }
                }
            }
        }
    }

    void TrackSelectedUnitToMouse(){
        // Get mouse position in screen space
        Vector3 mousePos = Input.mousePosition;
        // Convert to world space
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        if(selectedTile){
            float yOffset = 0.2f;
            // Set sprite position to selected tile
            selectedUnit.transform.position = selectedTile.transform.position + new Vector3(0,yOffset,0);
        }
        else{
            float yOffset = -0.2f;
            // Set the sprite's position to the converted mouse position
            selectedUnit.transform.position = new Vector3(worldPos.x, worldPos.y + yOffset, transform.position.z);
        }
        
    }

    void FindAndSelectTiles(){
        // Debug.Log("Searching");
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Convert screen point to world point
        Collider2D[] colliders = Physics2D.OverlapPointAll(mousePosition);

        if(colliders.Length>0){
            bool groundSelected = false;
            foreach (Collider2D collider in colliders)
            {
            // Debug.Log($"COLLIDED: {hit.collider.gameObject.name}");
                if(collider.gameObject.name.Contains("ground")){
                    selectedTile = collider.gameObject;
                    groundSelected = true;
                }
            }
            if(!groundSelected){
                selectedTile = null;
            }
        }
        else{
            selectedTile = null;
        }
    }

    void PlaceUnit(){
        if (Input.GetMouseButtonDown(0) && selectedTile) // Left mouse button click
        {
            if(!selectedTile.GetComponent<Tile>().Occupied){
                int unitCost = selectedUnit.GetComponent<Pawn>().cost;
                string unitName = selectedUnit.name;

                if(UnitPoints >= unitCost){

                    if(!unitName.Contains("king") && !unitName.Contains("King")){
                        GameObject newUnit = Instantiate(selectedUnit, selectedUnit.transform.position, Quaternion.identity);
                        UnitPoints = UnitPoints - unitCost;
                        selectedTile.GetComponent<Tile>().Occupied = true;
                        newUnit.GetComponent<Pawn>().setTile(selectedTile);
                    }
                    else{
                        if(!kingPlaced){
                            GameObject newUnit = Instantiate(selectedUnit, selectedUnit.transform.position, Quaternion.identity);
                            selectedTile.GetComponent<Tile>().Occupied = true;
                            newUnit.GetComponent<Pawn>().setTile(selectedTile);
                            kingPlaced = true;
                            ReadyButton.SetActive(true);
                        }
                    }
                }

            }
        }
    }

    void UnitSelected(){
        if(selectedUnit){
            TrackSelectedUnitToMouse();
            PlaceUnit();
        }
    }

    void Update()
    {
        unitPointsText.text = $"Unit points available: {UnitPoints}";
        DetectUnitSelect();
        DetectUnitDeselect();
        UnitSelected();
        FindAndSelectTiles();

    }
}
