using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastaFeatures : MonoBehaviour
{
    public static List<GameObject> Kinds;
    public static List<GameObject> Shapes;
    public static List<GameObject> FlourTypes;
    public static List<GameObject> Extras;

    public static GameObject kind;
    public static GameObject shape;
    public static GameObject flour;
    public static List<GameObject> pickedExtras;

    public static float juicy = 0;
    public static float crunchs = 0;
    public static float smooth = 0;
    public static float sticky = 0;

    public static float sweet = 0;
    public static float sour = 0;
    public static float bitter = 0;
    public static float salty = 0;

    public static float quantative = 0;                 
    public static float effort = 0;                     
    public static float quality = 0;                    
                                                         
    public static float oldiesBoost = 0;                //boostlar ile eklenicek
    public static float studentBoost = 0;               //boostlar ile eklenicek
    public static float whiteCBoost = 0;                //boostlar ile eklenicek
    public static float familiyBoost = 0;               //boostlar ile eklenicek
    public static float richiesBoost = 0;               //boostlar ile eklenicek
                                                        //boostlar ile eklenicek
    public static float juicyBoost = 0;                 //boostlar ile eklenicek
    public static float crunchyBoost = 0;               //boostlar ile eklenicek
    public static float smoothBoost = 0;                //boostlar ile eklenicek
    public static float stickyBoost = 0;                //boostlar ile eklenicek
                                                        //boostlar ile eklenicek
    public static float sweetBoost = 0;                 //boostlar ile eklenicek
    public static float sourBoost = 0;                  //boostlar ile eklenicek
    public static float bitterBoost = 0;                //boostlar ile eklenicek
    public static float saltyBoost = 0;                 //boostlar ile eklenicek
                                                        //boostlar ile eklenicek
    public static float quantativeBoost = 0;            //boostlar ile eklenicek
    public static float effortBoost = 0;                //boostlar ile eklenicek
    public static float qualityBoost = 0;               //boostlar ile eklenicek

    public static Dictionary<Station.StationType, int> StationTimeReducer = new Dictionary<Station.StationType, int>()
    { {Station.StationType.Boiler ,0},{Station.StationType.DoughCutter ,0},{Station.StationType.DoughKneader ,0} };
    

    public static void AddExtra(Extra extra)
    {

    }

    /*public static List<GameObject> KindsClosed;
    public static List<GameObject> ShapesClosed;
    public static List<GameObject> FlourTypesClosed;
    public static List<GameObject> ExtrasClosed;

    [SerializeField] List<GameObject> KindsLoc;
    [SerializeField] List<GameObject> ShapesLoc;
    [SerializeField] List<GameObject> FlourTypesLoc;
    [SerializeField] List<GameObject> ExtrasLoc;

    [SerializeField] List<GameObject> KindsClosedLoc;
    [SerializeField] List<GameObject> ShapesClosedLoc;
    [SerializeField] List<GameObject> FlourTypesClosedLoc;
    [SerializeField] List<GameObject> ExtrasClosedLoc;

    private void Awake()
    {
        Kinds = KindsLoc;
        Shapes = ShapesLoc;
        FlourTypes = FlourTypesLoc;
        Extras = ExtrasLoc;

        KindsClosed = KindsClosedLoc;
        ShapesClosed = ShapesClosedLoc;
        FlourTypesClosed = FlourTypesClosedLoc;
        ExtrasClosed = ExtrasClosedLoc;
    }*/
}
