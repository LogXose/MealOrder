using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechItem : MonoBehaviour {

    public TechItem[] preconditions;
    public enum TypeOfTech
    {
        Machine,
        Shape,
        Kind,
        Flour,
        Boost,
        Extra
    }
    public TypeOfTech _type;
    public enum BoostType
    {
        None,
        Texture,
        Segmentation,
        Profile
    }
    public BoostType _boostType;
    public enum TextureType
    {
        None,
        All,
        Smoothness,
        Crunchy,
        Juicy,
        Sticky
    }
    public TextureType _textureType;
    public enum ProfileType
    {
        None,
        All,
        Sweet,
        Sour,
        Bitter,
        Salty
    }
    public ProfileType _profileType;
    public enum SegmentationType
    {
        None,
        All,
        Quantaty,
        Effort,
        Quality
    }
    public SegmentationType _segmentationType;
    public Kind kind;
    public Shape shape;
    public Station[] stations;
    public FlourType flour;
    public Extra extra;
    public float boostQuant = 0;
    public int price = 0;
    bool preconsBool = false;

    private void Start()
    {
        Text priceText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        priceText.text = "$ " + price.ToString();
    }

    private void Update()
    {
        if (donePreconditions())
        {
            GetComponent<BasicButton>().donePrecondition = true;
            preconsBool = true;
            if (!gameObject.GetComponent<BasicButton>().hasActivated)
            {
                if (price > InventoryOfPlayer.Money) changeState(3);
                else changeState(2);
            }
        }
        else
        {
            changeState(0);
        }
    }

    bool donePreconditions()
    {
        if (preconditions.Length == 0) return true;
        foreach (var item in preconditions)
        {
            if (!item.GetComponent<BasicButton>().hasActivated) return false;
        }
        return true;
    }

    public void Activate()
    {
        if (!gameObject.GetComponent<BasicButton>().hasActivated)
        {
            if (InventoryOfPlayer.Money - price < 0)
            {
                return;
            }
            InventoryOfPlayer.Money -= price;
            switch (_type)
            {
                case TypeOfTech.Machine:
                    foreach (var item in stations)
                    {
                        BuyingPanelController.openedStationList.Add(item.gameObject);
                    }
                    break;
                case TypeOfTech.Shape:
                    PastaFeatures._shapes.Add(shape.gameObject);
                    break;
                case TypeOfTech.Kind:
                    PastaFeatures._kinds.Add(kind.gameObject);
                    break;
                case TypeOfTech.Flour:
                    PastaFeatures._flours.Add(flour.gameObject);
                    break;
                case TypeOfTech.Boost:
                    switch (_boostType)
                    {
                        case BoostType.None:
                            break;
                        case BoostType.Texture:
                            switch (_textureType)
                            {
                                case TextureType.None:
                                    break;
                                case TextureType.All:
                                    PastaFeatures.juicyBoost += boostQuant;
                                    PastaFeatures.crunchyBoost += boostQuant;
                                    PastaFeatures.smoothBoost += boostQuant;
                                    PastaFeatures.stickyBoost += boostQuant;
                                    break;
                                case TextureType.Smoothness:
                                    PastaFeatures.smoothBoost += boostQuant;
                                    break;
                                case TextureType.Crunchy:
                                    PastaFeatures.crunchyBoost += boostQuant;
                                    break;
                                case TextureType.Juicy:
                                    PastaFeatures.juicyBoost += boostQuant;
                                    break;
                                case TextureType.Sticky:
                                    PastaFeatures.stickyBoost += boostQuant;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case BoostType.Segmentation:
                            switch (_segmentationType)
                            {
                                case SegmentationType.None:
                                    break;
                                case SegmentationType.All:
                                    PastaFeatures.quantativeBoost += boostQuant;
                                    PastaFeatures.effortBoost += boostQuant;
                                    PastaFeatures.qualityBoost += boostQuant;
                                    break;
                                case SegmentationType.Quantaty:
                                    PastaFeatures.quantativeBoost += boostQuant;
                                    break;
                                case SegmentationType.Effort:
                                    PastaFeatures.effortBoost += boostQuant;
                                    break;
                                case SegmentationType.Quality:
                                    PastaFeatures.qualityBoost += boostQuant;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case BoostType.Profile:
                            switch (_profileType)
                            {
                                case ProfileType.None:
                                    break;
                                case ProfileType.All:
                                    PastaFeatures.sweetBoost += boostQuant;
                                    PastaFeatures.sourBoost += boostQuant;
                                    PastaFeatures.bitterBoost += boostQuant;
                                    PastaFeatures.saltyBoost += boostQuant;
                                    break;
                                case ProfileType.Sweet:
                                    PastaFeatures.sweetBoost += boostQuant;
                                    break;
                                case ProfileType.Sour:
                                    PastaFeatures.sourBoost += boostQuant;
                                    break;
                                case ProfileType.Bitter:
                                    PastaFeatures.bitterBoost += boostQuant;
                                    break;
                                case ProfileType.Salty:
                                    PastaFeatures.saltyBoost += boostQuant;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case TypeOfTech.Extra:
                    PastaFeatures._extras.Add(extra.gameObject);
                    break;
                default:
                    break;
            }
            gameObject.GetComponent<BasicButton>().hasActivated = true;
            changeState(1);
            TechnologuTree.activateds.Add(gameObject.name);
        }
    }

    public void changeState(int state)
    {
        for (int i = 0; i < 4; i++)
        {
            transform.GetChild(6).GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(6).GetChild(state).gameObject.SetActive(true);

        if (state == 1) GetComponent<Image>().color = TechnologuTree.activatedColor;
        else { GetComponent<Image>().color = Color.white; }
    }
}
