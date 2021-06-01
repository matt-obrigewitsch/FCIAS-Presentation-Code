using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public BattleMenu currentmenu; //Used in switch below.
    private GameObject enemyPoke; //legacy value, previously cloned old enemy pokemon, kept in case of revert.

    //initialises values with fancy headers below
    [Header("Selection")]
    public GameObject SelectionMenu;
    public GameObject SelectionInfo;
    public Text SelectionInfoText;
    public Text fight;
    private string fightT;
    public Text bag;
    private string bagT;
    public Text pokemon;
    private string pokemonT;
    public Text run;
    private string runT;

    [Header("Moves")]
    public GameObject movesMenu;
    public GameObject MovesDetails;
    public Text PP;
    private string PPT;
    public Text PowerT;
    public Text pType;
    public Text moveO;
    private string moveOT;
    public Text moveT;
    private string moveTT;
    public Text moveTH;
    private string moveTHT;
    public Text moveF;
    private string moveFT;

    [Header("Info")]
    public GameObject InfoMenu;
    public Text InfoText;
    public Text ename;
    public Text pname;
    public Text epokehealthmin;
    public Text epokehealthmax;
    public Text ppokehealthmin;
    public Text ppokehealthmax;
    public Text ppokeexpmin;
    public Text ppokeexpmax;
    public Text elevel;
    public Text plevel;

    [Header("Pokemon")]
    public GameObject PokemonList;
    public GameObject PokemonInfo;
    public Text PokeSwitchNameInfo;
    public Text PokeSwitchHP;
    public Text PokeSwitchType;
    public Text pokeNameText1;
    private string pokeNameText1T;
    public Text pokeNameText2;
    private string pokeNameText2T;
    public Text pokeNameText3;
    private string pokeNameText3T;
    public Text pokeNameText4;
    private string pokeNameText4T;
    public Text pokeNameText5;
    private string pokeNameText5T;
    public Text pokeNameText6;
    private string pokeNameText6T;
    private int switchPokeCurHP;

    [Header("Misc")]
    public int currentSelection;
    public int currentSelectionPoke;
    public int currentSelectionOptions;
    public GameObject gamemanage;
    public GameObject player;
    public bool runused;
    public GameManager gm;
    public int damageDealt;
    public int damage1;
    public float damage2;
    private float timeStamp;
    private float timeStamp2;
    public int emCounter;
    public int activeMoveCode;
    public bool stepCheck;
    public int failSafe;
    public bool misact;
    public bool misactForcedPokemon;
    public bool spaceUp;
    public bool attackStep1;
    public bool attackStep2;
    public bool attackStep3;
    public bool attackStep4;
    public bool attackStep5;
    public bool EPokeDeathStep1;
    public bool PPokeDeathStep1;
    public bool PPokeDeathStep2;
    public bool PPokeDeathStep3;
    public bool PPokeDeathAllPokeDeadStep1;
    public int pMoveChoice;
    public int eMoveChoice;
    public int switchedPoke;
    public int xpRecieved;

    // Use this for initialization
    void Start()
    {
        //sets up all values
        misact = false;
        runused = false;
        //all text below is legacy code, learned .text can be immediatly converted
        fightT = fight.text;
        bagT = bag.text;
        pokemonT = pokemon.text;
        runT = run.text;
        moveOT = moveO.text;
        moveTT = moveT.text;
        moveTHT = moveTH.text;
        moveFT = moveF.text;
        PPT = PP.text;
        pokeNameText1T = pokeNameText1.text;
        pokeNameText2T = pokeNameText2.text;
        pokeNameText3T = pokeNameText3.text;
        pokeNameText4T = pokeNameText4.text;
        pokeNameText5T = pokeNameText5.text;
        pokeNameText6T = pokeNameText6.text;
        currentSelectionOptions = 4;
        //sets gm to already existing object derived from GameManager script
        gm = gamemanage.GetComponent<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        //below are switch controls.
        if (Input.GetKeyUp(KeyCode.Space))
        {
            spaceUp = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentSelection < currentSelectionOptions)
            {
                currentSelection++;
            }
            //if (currentSelectionPoke < 6)
            //{
            //    currentSelectionPoke++;
            //}
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (currentSelection > 0)
            {
                currentSelection--;
            }
            if (currentSelection == 0)
            {
                currentSelection = 1;
            }
            //if (currentSelectionPoke > 0)
            //{
            //    currentSelectionPoke--;
            //}
            //if (currentSelection == 0)
            //{
            //    currentSelectionPoke = 1;
            //}
        }
        switch (currentmenu)
            //uses a switch to control the menu
        {

            case BattleMenu.Fight:
                //case one is if the fight menu is open. Allows the player to choose a move, and then uses 
                switch (currentSelection)
                {
                    //all cases describe which move is active. They all have the available keys space and 
                    //escape, which either use the move, if its empty say its empty, or exit fight with escape
                    case 1:
                        moveO.text = "> " + moveOT;
                        moveT.text = moveTT;
                        moveTH.text = moveTHT;
                        moveF.text = moveFT;

                        PP.text = gm.activePoke.Move1PP.ToString();
                        PowerT.text = gm.allMoves[gm.activePoke.Move1C].power.ToString();
                        pType.text = gm.allMoves[gm.activePoke.Move1C].moveType.ToString();
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            ChangeMenu(BattleMenu.Selection);
                        }
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            if (gm.activePoke.Move1PP != 0)
                            {
                                pMoveChoice = gm.activePoke.Move1C;
                                PokeAttack();
                            }
                            else if (gm.activePoke.Move1PP == 0)
                            {
                                misact = true;
                                ChangeMenu(BattleMenu.Info);
                                InfoText.text = "You have no PP for that move!";
                            }
                        }
                        break;
                    case 2:
                        moveO.text = moveOT;
                        moveT.text = "> " + moveTT;
                        moveTH.text = moveTHT;
                        moveF.text = moveFT;
                        if (gm.activePoke.Move2C != 0)
                        {
                            PP.text = gm.activePoke.Move2PP.ToString();
                            PowerT.text = gm.allMoves[gm.activePoke.Move2C].power.ToString();
                            pType.text = gm.allMoves[gm.activePoke.Move2C].moveType.ToString();
                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                if (gm.activePoke.Move2PP != 0)
                                {
                                    pMoveChoice = gm.activePoke.Move2C;
                                    PokeAttack();
                                }
                                else if (gm.activePoke.Move2PP == 0)
                                {
                                    misact = true;
                                    ChangeMenu(BattleMenu.Info);
                                    InfoText.text = "You have no PP for that move!";
                                }
                            }
                        }
                        else if (gm.activePoke.Move2C == 0)
                        {
                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                misact = true;
                                ChangeMenu(BattleMenu.Info);
                                InfoText.text = "There is no move here!";
                            }
                        }
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            ChangeMenu(BattleMenu.Selection);
                        }
                        break;
                    case 3:
                        moveO.text = moveOT;
                        moveT.text = moveTT;
                        moveTH.text = "> " + moveTHT;
                        moveF.text = moveFT;
                        if (gm.activePoke.Move3C != 0)
                        {
                            PP.text = gm.activePoke.Move3PP.ToString();
                            PowerT.text = gm.allMoves[gm.activePoke.Move3C].power.ToString();
                            pType.text = gm.allMoves[gm.activePoke.Move3C].moveType.ToString();
                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                if (gm.activePoke.Move3PP != 0)
                                {
                                    pMoveChoice = gm.activePoke.Move3C;
                                    PokeAttack();
                                }
                                else if (gm.activePoke.Move3PP == 0)
                                {
                                    misact = true;
                                    ChangeMenu(BattleMenu.Info);
                                    InfoText.text = "You have no PP for that move!";
                                }
                            }
                        }
                        else if (gm.activePoke.Move3C == 0)
                        {
                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                misact = true;
                                ChangeMenu(BattleMenu.Info);
                                InfoText.text = "There is no move here!";
                            }
                        }
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            ChangeMenu(BattleMenu.Selection);
                        }
                        break;
                    case 4:
                        moveO.text = moveOT;
                        moveT.text = moveTT;
                        moveTH.text = moveTHT;
                        moveF.text = "> " + moveFT;
                        if (gm.activePoke.Move4C != 0)
                        {
                            PP.text = gm.activePoke.Move4PP.ToString();
                            PowerT.text = gm.allMoves[gm.activePoke.Move4C].power.ToString();
                            pType.text = gm.allMoves[gm.activePoke.Move4C].moveType.ToString();
                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                if (gm.activePoke.Move2PP != 0)
                                {
                                    pMoveChoice = gm.activePoke.Move4C;
                                    PokeAttack();
                                }
                                else if (gm.activePoke.Move4PP == 0)
                                {
                                    misact = true;
                                    ChangeMenu(BattleMenu.Info);
                                    InfoText.text = "You have no PP for that move!";
                                }
                            }
                        }
                        else if (gm.activePoke.Move4C == 0)
                        {
                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                misact = true;
                                ChangeMenu(BattleMenu.Info);
                                InfoText.text = "There is no move here!";
                            }
                        }
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            ChangeMenu(BattleMenu.Selection);
                        }
                        break;
                }

                break;
            case BattleMenu.Selection:
                //selection is the hub menu which lets the player decide to either switch pokemon, use an item,
                //or run away.
                switch (currentSelection)
                {
                    case 1:
                        fight.text = "> " + fightT;
                        bag.text = bagT;
                        pokemon.text = pokemonT;
                        run.text = runT;
                        SelectionInfoText.text = "Use a move with the current Pokemon";
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            ChangeMenu(BattleMenu.Fight);
                        }
                        break;
                    case 2:
                        fight.text = fightT;
                        bag.text = "> " + bagT;
                        pokemon.text = pokemonT;
                        run.text = runT;
                        SelectionInfoText.text = "Select an item from your bag";
                        break;
                    case 3:
                        fight.text = fightT;
                        bag.text = bagT;
                        pokemon.text = "> " + pokemonT;
                        run.text = runT;
                        SelectionInfoText.text = "Change out your pokemon";
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            ChangeMenu(BattleMenu.Pokemon);
                        }
                        break;
                    case 4:
                        fight.text = fightT;
                        bag.text = bagT;
                        pokemon.text = pokemonT;
                        run.text = "> " + runT;
                        SelectionInfoText.text = "Run away!";
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            InfoText.text = "Got away safely!";
                            runused = true;
                            ChangeMenu(BattleMenu.Info);
                        }
                        break;
                }
                break;

            case BattleMenu.Info:
                //info describes the last action. As it turns into a sort of "final destination" for all actions,
                //and all actions use space bar, it also acts as the change to allow all actions to progress without
                //skipping steps.
                {
                    if (Input.GetKeyDown(KeyCode.Space) & runused)
                    {
                        //destroys both pokemon if run is used.
                        gamemanage.GetComponent<GameManager>().battleCamera.SetActive(false);
                        gamemanage.GetComponent<GameManager>().playerCamera.SetActive(true);
                        player.GetComponent<PlayerMovement2>().isAllowedToMove = true;
                        Destroy(GameObject.FindWithTag("epoke"));
                        Destroy(GameObject.FindWithTag("ppoke"));
                    }
                    //phases attacks.
                    if (Input.GetKeyDown(KeyCode.Space) & attackStep1)
                    {
                        PokeAttack1();
                    }
                    if (Input.GetKeyDown(KeyCode.Space) & attackStep2 & spaceUp)
                    {
                        PokeAttack2();
                    }
                    if (Input.GetKeyDown(KeyCode.Space) & attackStep3 & spaceUp)
                    {
                        PokeAttack3();
                    }
                    if (Input.GetKeyDown(KeyCode.Space) & attackStep4 & spaceUp)
                    {
                        PokeAttack4();
                    }
                    if (Input.GetKeyDown(KeyCode.Space) & misact)
                    {
                        misact = false;
                        ChangeMenu(BattleMenu.Selection);
                    }
                    if (Input.GetKeyDown(KeyCode.Space) & misactForcedPokemon)
                    {
                        misactForcedPokemon = false;
                        ChangeMenu(BattleMenu.ForcedPokemon);
                    }
                    if (Input.GetKeyDown(KeyCode.Space) & EPokeDeathStep1 & spaceUp)
                    {
                        EPokeDeath1();
                    }
                    if (Input.GetKeyDown(KeyCode.Space) & PPokeDeathStep1 & spaceUp)
                    {
                        PPokeDeath1();
                    }
                    if (Input.GetKeyDown(KeyCode.Space) & PPokeDeathStep2 & spaceUp)
                    {
                        PPokeDeath2();
                    }
                    if (Input.GetKeyDown(KeyCode.Space) & PPokeDeathStep3 & spaceUp)
                    {
                        PPokeDeath3();
                    }
                    if (Input.GetKeyDown(KeyCode.Space) & PPokeDeathAllPokeDeadStep1 & spaceUp)
                    {
                        PPokeAllDead1();
                    }
                    break;
                }
            case BattleMenu.Pokemon:
                {
                    switch (currentSelection)
                    {
                        case 1:
                            pokeNameText1.text = "> " + pokeNameText1T;
                            pokeNameText2.text = pokeNameText2T;
                            pokeNameText3.text = pokeNameText3T;
                            pokeNameText4.text = pokeNameText4T;
                            pokeNameText5.text = pokeNameText5T;
                            pokeNameText6.text = pokeNameText6T;
                            if (player.GetComponent<Player>().ownedPokemon[0].pokemon != null)
                            {
                                PokeSwitchNameInfo.text = pokeNameText1T;
                                PokeSwitchHP.text = player.GetComponent<Player>().ownedPokemon[0].pokemon.HP.ToString();
                                PokeSwitchType.text = player.GetComponent<Player>().ownedPokemon[0].pokemon.type.ToString();
                                if (Input.GetKeyDown(KeyCode.Space) && switchedPoke != 0)
                                {
                                    switchedPoke = 0;
                                    PokeSwitch();
                                }
                                else if (Input.GetKeyDown(KeyCode.Space) && switchedPoke == 0)
                                {
                                    misact = true;
                                    InfoText.text = "This pokemon is already battling!";
                                    ChangeMenu(BattleMenu.Info);
                                }
                            }
                            else if (Input.GetKeyDown(KeyCode.Space))
                            {
                                misact = true;
                                InfoText.text = "There is no pokemon here!";
                                ChangeMenu(BattleMenu.Info);
                            }
                            if (Input.GetKeyDown(KeyCode.Escape))
                            {
                                ChangeMenu(BattleMenu.Selection);
                            }
                            break;
                        case 2:
                            pokeNameText1.text = pokeNameText1T;
                            pokeNameText2.text = "> " + pokeNameText2T;
                            pokeNameText3.text = pokeNameText3T;
                            pokeNameText4.text = pokeNameText4T;
                            pokeNameText5.text = pokeNameText5T;
                            pokeNameText6.text = pokeNameText6T;
                            if (player.GetComponent<Player>().ownedPokemon[1].pokemon != null)
                            {
                                PokeSwitchNameInfo.text = pokeNameText2T;
                                PokeSwitchHP.text = player.GetComponent<Player>().ownedPokemon[1].pokemon.HP.ToString();
                                PokeSwitchType.text = player.GetComponent<Player>().ownedPokemon[1].pokemon.type.ToString();
                                if (Input.GetKeyDown(KeyCode.Space) && switchedPoke != 1)
                                {
                                    if (player.GetComponent<Player>().ownedPokemon[1].pokemon.HP > 0)
                                    {
                                        switchedPoke = 1;
                                        PokeSwitch();
                                    }
                                    else if(player.GetComponent<Player>().ownedPokemon[1].pokemon.HP <= 0)
                                    {
                                        misact = true;
                                        InfoText.text = "This pokemon is fainted! They cannot fight";
                                        ChangeMenu(BattleMenu.Info);
                                    }
                                }
                                else if (Input.GetKeyDown(KeyCode.Space) && switchedPoke == 1)
                                {
                                    misact = true;
                                    InfoText.text = "This pokemon is already battling!";
                                    ChangeMenu(BattleMenu.Info);
                                }
                            }
                            else if (Input.GetKeyDown(KeyCode.Space))
                            {
                                misact = true;
                                InfoText.text = "There is no pokemon here!";
                                ChangeMenu(BattleMenu.Info);
                            }
                            if (Input.GetKeyDown(KeyCode.Escape))
                            {
                                ChangeMenu(BattleMenu.Selection);
                            }
                            break;
                        case 3:
                            pokeNameText1.text = pokeNameText1T;
                            pokeNameText2.text = pokeNameText2T;
                            pokeNameText3.text = "> " + pokeNameText3T;
                            pokeNameText4.text = pokeNameText4T;
                            pokeNameText5.text = pokeNameText5T;
                            pokeNameText6.text = pokeNameText6T;
                            if (player.GetComponent<Player>().ownedPokemon[2].pokemon != null)
                            {
                                PokeSwitchNameInfo.text = pokeNameText3T;
                                PokeSwitchHP.text = player.GetComponent<Player>().ownedPokemon[2].pokemon.HP.ToString();
                                PokeSwitchType.text = player.GetComponent<Player>().ownedPokemon[2].pokemon.type.ToString();
                                if (Input.GetKeyDown(KeyCode.Space))
                                {
                                    switchedPoke = 2;
                                    PokeSwitch();
                                }
                            }
                            else if (Input.GetKeyDown(KeyCode.Space))
                            {
                                Debug.Log("There is no pokemon here!");
                            }
                            if (Input.GetKeyDown(KeyCode.Escape))
                            {
                                ChangeMenu(BattleMenu.Selection);
                            }
                            break;
                        case 4:
                            pokeNameText1.text = pokeNameText1T;
                            pokeNameText2.text = pokeNameText2T;
                            pokeNameText3.text = pokeNameText3T;
                            pokeNameText4.text = "> " + pokeNameText4T;
                            pokeNameText5.text = pokeNameText5T;
                            pokeNameText6.text = pokeNameText6T;
                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                switchedPoke = 3;
                                PokeSwitch();
                            }
                            if (Input.GetKeyDown(KeyCode.Escape))
                            {
                                ChangeMenu(BattleMenu.Selection);
                            }
                            break;
                        case 5:
                            pokeNameText1.text = pokeNameText1T;
                            pokeNameText2.text = pokeNameText2T;
                            pokeNameText3.text = pokeNameText3T;
                            pokeNameText4.text = pokeNameText4T;
                            pokeNameText5.text = "> " + pokeNameText5T;
                            pokeNameText6.text = pokeNameText6T;
                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                switchedPoke = 4;
                                PokeSwitch();
                            }
                            if (Input.GetKeyDown(KeyCode.Escape))
                            {
                                ChangeMenu(BattleMenu.Selection);
                            }
                            break;
                        case 6:
                            pokeNameText1.text = pokeNameText1T;
                            pokeNameText2.text = pokeNameText2T;
                            pokeNameText3.text = pokeNameText3T;
                            pokeNameText4.text = pokeNameText4T;
                            pokeNameText5.text = pokeNameText5T;
                            pokeNameText6.text = "> " + pokeNameText6T;
                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                switchedPoke = 5;
                                PokeSwitch();
                            }
                            if (Input.GetKeyDown(KeyCode.Escape))
                            {
                                ChangeMenu(BattleMenu.Selection);
                            }
                            break;
                    }
                }
                break;
            case BattleMenu.ForcedPokemon:
                {
                    switch (currentSelection)
                    {
                        case 1:
                            pokeNameText1.text = "> " + pokeNameText1T;
                            pokeNameText2.text = pokeNameText2T;
                            pokeNameText3.text = pokeNameText3T;
                            pokeNameText4.text = pokeNameText4T;
                            pokeNameText5.text = pokeNameText5T;
                            pokeNameText6.text = pokeNameText6T;
                            if (player.GetComponent<Player>().ownedPokemon[0].pokemon != null)
                            {
                                PokeSwitchNameInfo.text = pokeNameText1T;
                                PokeSwitchHP.text = player.GetComponent<Player>().ownedPokemon[0].pokemon.HP.ToString();
                                PokeSwitchType.text = player.GetComponent<Player>().ownedPokemon[0].pokemon.type.ToString();
                                if (Input.GetKeyDown(KeyCode.Space) && switchedPoke != 0)
                                {
                                    switchedPoke = 0;
                                    PokeSwitch();
                                }
                                else if (Input.GetKeyDown(KeyCode.Space) && switchedPoke == 0)
                                {
                                    misactForcedPokemon = true;
                                    InfoText.text = "This pokemon is already battling!";
                                    ChangeMenu(BattleMenu.Info);
                                }
                            }
                            else if (Input.GetKeyDown(KeyCode.Space))
                            {
                                misactForcedPokemon = true;
                                InfoText.text = "There is no pokemon here!";
                                ChangeMenu(BattleMenu.Info);
                            }
                            break;
                        case 2:
                            pokeNameText1.text = pokeNameText1T;
                            pokeNameText2.text = "> " + pokeNameText2T;
                            pokeNameText3.text = pokeNameText3T;
                            pokeNameText4.text = pokeNameText4T;
                            pokeNameText5.text = pokeNameText5T;
                            pokeNameText6.text = pokeNameText6T;
                            if (player.GetComponent<Player>().ownedPokemon[1].pokemon != null)
                            {
                                PokeSwitchNameInfo.text = pokeNameText2T;
                                PokeSwitchHP.text = player.GetComponent<Player>().ownedPokemon[1].pokemon.HP.ToString();
                                PokeSwitchType.text = player.GetComponent<Player>().ownedPokemon[1].pokemon.type.ToString();
                                if (Input.GetKeyDown(KeyCode.Space) && switchedPoke != 1)
                                {
                                    switchedPoke = 1;
                                    PokeSwitch();
                                }
                                else if (Input.GetKeyDown(KeyCode.Space) && switchedPoke == 1)
                                {
                                    misactForcedPokemon = true;
                                    InfoText.text = "This pokemon is already battling!";
                                    ChangeMenu(BattleMenu.Info);
                                }
                            }
                            else if (Input.GetKeyDown(KeyCode.Space))
                            {
                                misactForcedPokemon = true;
                                InfoText.text = "There is no pokemon here!";
                                ChangeMenu(BattleMenu.Info);
                            }
                            break;
                        case 3:
                            pokeNameText1.text = pokeNameText1T;
                            pokeNameText2.text = pokeNameText2T;
                            pokeNameText3.text = "> " + pokeNameText3T;
                            pokeNameText4.text = pokeNameText4T;
                            pokeNameText5.text = pokeNameText5T;
                            pokeNameText6.text = pokeNameText6T;
                            if (player.GetComponent<Player>().ownedPokemon[2].pokemon != null)
                            {
                                PokeSwitchNameInfo.text = pokeNameText3T;
                                PokeSwitchHP.text = player.GetComponent<Player>().ownedPokemon[2].pokemon.HP.ToString();
                                PokeSwitchType.text = player.GetComponent<Player>().ownedPokemon[2].pokemon.type.ToString();
                                if (Input.GetKeyDown(KeyCode.Space) && switchedPoke != 2)
                                {
                                    switchedPoke = 2;
                                    PokeSwitch();
                                }
                                else if (Input.GetKeyDown(KeyCode.Space) && switchedPoke == 2)
                                {
                                    misactForcedPokemon = true;
                                    InfoText.text = "This pokemon is already battling!";
                                    ChangeMenu(BattleMenu.Info);
                                }
                            }
                            else if (Input.GetKeyDown(KeyCode.Space))
                            {
                                misactForcedPokemon = true;
                                InfoText.text = "There is no pokemon here!";
                                ChangeMenu(BattleMenu.Info);
                            }
                            break;
                        case 4:
                            pokeNameText1.text = pokeNameText1T;
                            pokeNameText2.text = pokeNameText2T;
                            pokeNameText3.text = pokeNameText3T;
                            pokeNameText4.text = "> " + pokeNameText4T;
                            pokeNameText5.text = pokeNameText5T;
                            pokeNameText6.text = pokeNameText6T;
                            if (player.GetComponent<Player>().ownedPokemon[3].pokemon != null)
                            {
                                PokeSwitchNameInfo.text = pokeNameText4T;
                                PokeSwitchHP.text = player.GetComponent<Player>().ownedPokemon[3].pokemon.HP.ToString();
                                PokeSwitchType.text = player.GetComponent<Player>().ownedPokemon[3].pokemon.type.ToString();
                                if (Input.GetKeyDown(KeyCode.Space) && switchedPoke != 3)
                                {
                                    switchedPoke = 3;
                                    PokeSwitch();
                                }
                                else if (Input.GetKeyDown(KeyCode.Space) && switchedPoke == 3)
                                {
                                    misactForcedPokemon = true;
                                    InfoText.text = "This pokemon is already battling!";
                                    ChangeMenu(BattleMenu.Info);
                                }
                            }
                            else if (Input.GetKeyDown(KeyCode.Space))
                            {
                                misactForcedPokemon = true;
                                InfoText.text = "There is no pokemon here!";
                                ChangeMenu(BattleMenu.Info);
                            }
                            break;
                        case 5:
                            pokeNameText1.text = pokeNameText1T;
                            pokeNameText2.text = pokeNameText2T;
                            pokeNameText3.text = pokeNameText3T;
                            pokeNameText4.text = pokeNameText4T;
                            pokeNameText5.text = "> " + pokeNameText5T;
                            pokeNameText6.text = pokeNameText6T;
                            if (player.GetComponent<Player>().ownedPokemon[4].pokemon != null)
                            {
                                PokeSwitchNameInfo.text = pokeNameText5T;
                                PokeSwitchHP.text = player.GetComponent<Player>().ownedPokemon[4].pokemon.HP.ToString();
                                PokeSwitchType.text = player.GetComponent<Player>().ownedPokemon[4].pokemon.type.ToString();
                                if (Input.GetKeyDown(KeyCode.Space) && switchedPoke != 4)
                                {
                                    switchedPoke = 4;
                                    PokeSwitch();
                                }
                                else if (Input.GetKeyDown(KeyCode.Space) && switchedPoke == 4)
                                {
                                    misactForcedPokemon = true;
                                    InfoText.text = "This pokemon is already battling!";
                                    ChangeMenu(BattleMenu.Info);
                                }
                            }
                            else if (Input.GetKeyDown(KeyCode.Space))
                            {
                                misactForcedPokemon = true;
                                InfoText.text = "There is no pokemon here!";
                                ChangeMenu(BattleMenu.Info);
                            }
                            break;
                        case 6:
                            pokeNameText1.text = pokeNameText1T;
                            pokeNameText2.text = pokeNameText2T;
                            pokeNameText3.text = pokeNameText3T;
                            pokeNameText4.text = pokeNameText4T;
                            pokeNameText5.text = pokeNameText5T;
                            pokeNameText6.text = "> " + pokeNameText6T;
                            if (player.GetComponent<Player>().ownedPokemon[5].pokemon != null)
                            {
                                PokeSwitchNameInfo.text = pokeNameText6T;
                                PokeSwitchHP.text = player.GetComponent<Player>().ownedPokemon[5].pokemon.HP.ToString();
                                PokeSwitchType.text = player.GetComponent<Player>().ownedPokemon[5].pokemon.type.ToString();
                                if (Input.GetKeyDown(KeyCode.Space) && switchedPoke != 5)
                                {
                                    switchedPoke = 5;
                                    PokeSwitch();
                                }
                                else if (Input.GetKeyDown(KeyCode.Space) && switchedPoke == 5)
                                {
                                    misactForcedPokemon = true;
                                    InfoText.text = "This pokemon is already battling!";
                                    ChangeMenu(BattleMenu.Info);
                                }
                            }
                            else if (Input.GetKeyDown(KeyCode.Space))
                            {
                                misactForcedPokemon = true;
                                InfoText.text = "There is no pokemon here!";
                                ChangeMenu(BattleMenu.Info);
                            }
                            break;
                    }
                }
                break;
        }


    }

    public void ChangeMenu(BattleMenu m)
    {
        //switches the menu depending on the current choice in the UI. Actives objects accordingly.
        currentmenu = m;
        currentSelection = 1;
        switch (m)
        {
            case BattleMenu.Selection:
                SelectionMenu.gameObject.SetActive(true);
                SelectionInfo.gameObject.SetActive(true);
                movesMenu.gameObject.SetActive(false);
                MovesDetails.gameObject.SetActive(false);
                InfoMenu.gameObject.SetActive(false);
                PokemonInfo.gameObject.SetActive(false);
                PokemonList.gameObject.SetActive(false);
                currentSelectionOptions = 4;
                break;
            case BattleMenu.Fight:
                SelectionMenu.gameObject.SetActive(false);
                SelectionInfo.gameObject.SetActive(false);
                movesMenu.gameObject.SetActive(true);
                MovesDetails.gameObject.SetActive(true);
                InfoMenu.gameObject.SetActive(false);
                PokemonInfo.gameObject.SetActive(false);
                PokemonList.gameObject.SetActive(false);
                currentSelectionOptions = 4;
                break;
            case BattleMenu.Info:
                SelectionMenu.gameObject.SetActive(false);
                SelectionInfo.gameObject.SetActive(false);
                movesMenu.gameObject.SetActive(false);
                MovesDetails.gameObject.SetActive(false);
                InfoMenu.gameObject.SetActive(true);
                PokemonInfo.gameObject.SetActive(false);
                PokemonList.gameObject.SetActive(false);
                currentSelectionOptions = 4;
                break;
            case BattleMenu.Pokemon:
                SelectionMenu.gameObject.SetActive(false);
                SelectionInfo.gameObject.SetActive(false);
                movesMenu.gameObject.SetActive(false);
                MovesDetails.gameObject.SetActive(false);
                InfoMenu.gameObject.SetActive(false);
                PokemonInfo.gameObject.SetActive(true);
                PokemonList.gameObject.SetActive(true);
                currentSelectionOptions = 6;
                break;
            case BattleMenu.ForcedPokemon:
                SelectionMenu.gameObject.SetActive(false);
                SelectionInfo.gameObject.SetActive(false);
                movesMenu.gameObject.SetActive(false);
                MovesDetails.gameObject.SetActive(false);
                InfoMenu.gameObject.SetActive(false);
                PokemonInfo.gameObject.SetActive(true);
                PokemonList.gameObject.SetActive(true);
                currentSelectionOptions = 6;
                break;
        }

    }

    public void EnemyMoveCounter()
    {
        //used to set up enemy moves, deciding which moves exist or not.
        if (gm.activeEPoke.Move1C != 0)
        {
            emCounter = emCounter + 1;
        }
        if (gm.activeEPoke.Move2C != 0)
        {
            emCounter = emCounter + 1;
        }
        if (gm.activeEPoke.Move3C != 0)
        {
            emCounter = emCounter + 1;
        }
        if (gm.activeEPoke.Move4C != 0)
        {
            emCounter = emCounter + 1;
        }
        Debug.Log("emcounter = " + emCounter);
    }
    public void ConvCounterToMoveCode()
    {
        //converts the counter into a move code.
        if (eMoveChoice == 0)
        {
            activeMoveCode = gm.activeEPoke.Move1C;
        }
        if (eMoveChoice == 1)
        {
            activeMoveCode = gm.activeEPoke.Move2C;
        }
        if (eMoveChoice == 2)
        {
            activeMoveCode = gm.activeEPoke.Move3C;
        }
        if (eMoveChoice == 3)
        {
            activeMoveCode = gm.activeEPoke.Move4C;
        }
        Debug.Log("activeMoveCode = " + activeMoveCode);
    }

    public void PokeAttack()
    {
        //phase one of attacking. calculates damage with the formula:
        //Damage = ((((2*level)/5)+2)*movepower*(attack/defence))/50
        spaceUp = false;
        ChangeMenu(BattleMenu.Info);
        InfoText.text = gm.activePoke.PName + " used " + gm.allMoves[pMoveChoice].MoveName + "!";
        damage1 = gm.activePoke.AttackStat / gm.activeEPoke.DefenceStat;
        damage2 = 2 * gm.activePoke.level / 5 + 2 * gm.allMoves[pMoveChoice].power * damage1 / 50;
        damageDealt = (int)damage2;
        attackStep1 = true;
    }
    public void PokeAttack1()
    {
        //displays damage in phase 2.
        attackStep1 = false;
        spaceUp = false;
        ChangeMenu(BattleMenu.Info);
        InfoText.text = gm.activeEPoke.PName + " recieved " + damageDealt + " damage!";
        gm.activeEPoke.HP = gm.activeEPoke.HP - damageDealt;
        gm.activePoke.Move1PP = gm.activePoke.Move1PP - 1;
        attackStep2 = true;
    }
    public void PokeAttack2()
    {
        //calculates enemy attack, and possible enemy death in phase 3.
        spaceUp = false;
        attackStep2 = false;
        if (gm.activeEPoke.HP > 0)
        {
            epokehealthmin.text = gm.activeEPoke.HP.ToString();
            EnemyMoveCounter();
            if (emCounter == 0)
            {
                Debug.Log("no enemy moves");
            }
            int eMoveChoice = Random.Range(0, emCounter - 1);
            Debug.Log("eMoveChoice = " + eMoveChoice);
            ConvCounterToMoveCode();
            InfoText.text = gm.activeEPoke.PName + " used " + gm.allMoves[activeMoveCode].MoveName + "!";
            Debug.Log(gm.allMoves[activeMoveCode].MoveName);
            damage1 = gm.activeEPoke.AttackStat / gm.activePoke.DefenceStat;
            damage2 = 2 * gm.activeEPoke.level / 5 + 2 * gm.allMoves[activeMoveCode].power * damage1 / 50;
            damageDealt = (int)damage2;
            attackStep3 = true;
        }
        else if (gm.activeEPoke.HP <= 0)
        {
            epokehealthmin.text = "0";
            InfoText.text = gm.activeEPoke.PName + " fainted!";
            EPokeDeathStep1 = true;
        }
    }
    public void PokeAttack3()
    {
        spaceUp = false;
        attackStep3 = false;
        //displays damage in phase 4.
        attackStep1 = false;
        spaceUp = false;
        ChangeMenu(BattleMenu.Info);
        InfoText.text = gm.activePoke.PName + " recieved " + damageDealt + " damage!";
        gm.activePoke.HP = gm.activePoke.HP - damageDealt;
        if (gm.activePoke.HP <= 0)
        {
            gm.activePoke.HP = 0;
            ppokehealthmin.text = gm.activePoke.HP.ToString();
            PPokeDeathStep1 = true;
        }
        else if(gm.activePoke.HP > 0) 
        {
            attackStep4 = true;
        }

    }
    public void PokeAttack4()
    {
        if (attackStep4)
        {
            //resets in phase 5.
            ppokehealthmin.text = gm.activePoke.HP.ToString();
            spaceUp = false;
            attackStep4 = false;
            ChangeMenu(BattleMenu.Selection);
        }
    }
    public void EPokeDeath1()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //temporary
            //replace with xp recieved, then a split case to either furthering battle or leave to player.

            int rarityCheck;
            EPokeDeathStep1 = false;
            spaceUp = false;
            rarityCheck = (int)gm.activeEPoke.rarity;
            rarityCheck = rarityCheck + 1;
            xpRecieved = (gm.activeEPoke.level * 10) * rarityCheck;
            gm.activePoke.XP = gm.activePoke.XP + xpRecieved;
            Debug.Log("XP: " + gm.activePoke.XP);
            Debug.Log("XP Recieved: " + xpRecieved);
            Debug.Log("XP to next level: " + gm.activePoke.XPtoLvl);
            while (gm.activePoke.XP >= gm.activePoke.XPtoLvl)
            {
                float ToLvlCalcVal;

                gm.activePoke.XP = gm.activePoke.XP - gm.activePoke.XPtoLvl;
                gm.activePoke.level = gm.activePoke.level + 1;
                ToLvlCalcVal = Mathf.Pow(1.1f, gm.activePoke.level);
                gm.activePoke.XPtoLvl = (int)(100 * ToLvlCalcVal);
                gm.activePoke.maxHP = gm.activePoke.HPGain + gm.activePoke.maxHP;
                gm.activePoke.AttackStat = gm.activePoke.AttackGain + gm.activePoke.AttackStat;
                gm.activePoke.DefenceStat = gm.activePoke.DefenceGain + gm.activePoke.DefenceStat;
                gm.activePoke.SpeedStat = gm.activePoke.SpeedGain + gm.activePoke.SpeedStat;
                gm.activePoke.SpAttackStat = gm.activePoke.SpAttackGain + gm.activePoke.SpAttackStat;
                gm.activePoke.SpDefenceStat = gm.activePoke.SpDefenceGain + gm.activePoke.SpDefenceStat;
            }
            gamemanage.GetComponent<GameManager>().battleCamera.SetActive(false);
            gamemanage.GetComponent<GameManager>().playerCamera.SetActive(true);
            player.GetComponent<PlayerMovement2>().isAllowedToMove = true;
            Destroy(GameObject.FindWithTag("epoke"));
            Destroy(GameObject.FindWithTag("ppoke"));
        }
    }
    public void PPokeDeath1()
    {
        PPokeDeathStep1 = false;
        spaceUp = false;
        ChangeMenu(BattleMenu.Info);
        InfoText.text = gm.activePoke.PName + " fainted!";
        PPokeDeathStep2 = true;
    }
    public void PPokeDeath2()
    {
        PPokeDeathStep2 = false;
        spaceUp = false;
        ChangeMenu(BattleMenu.Info);
        int spt0 = 0;
        int spt1 = 0;
        int spt2 = 0;
        int spt3 = 0;
        int spt4 = 0;
        int spt5 = 0;
        if (player.GetComponent<Player>().ownedPokemon[0].pokemon != null)
        {
            spt0 = player.GetComponent<Player>().ownedPokemon[0].pokemon.HP;
        }
        if (player.GetComponent<Player>().ownedPokemon[1].pokemon != null)
        {
            spt1 = player.GetComponent<Player>().ownedPokemon[1].pokemon.HP;
        }
        if (player.GetComponent<Player>().ownedPokemon[2].pokemon != null)
        {
            spt2 = player.GetComponent<Player>().ownedPokemon[2].pokemon.HP;
        }
        if (player.GetComponent<Player>().ownedPokemon[3].pokemon != null)
        {
            spt3 = player.GetComponent<Player>().ownedPokemon[3].pokemon.HP;
        }
        if (player.GetComponent<Player>().ownedPokemon[4].pokemon != null)
        {
            spt4 = player.GetComponent<Player>().ownedPokemon[4].pokemon.HP;
        }
        if (player.GetComponent<Player>().ownedPokemon[5].pokemon != null)
        {
            spt5 = player.GetComponent<Player>().ownedPokemon[5].pokemon.HP;
        }
        Debug.Log(spt0);
        Debug.Log(spt1);
        if (spt0 != 0 || spt1 != 0 || spt2 != 0 || spt3 != 0 || spt4 != 0 || spt5 != 0)
        {
            InfoText.text = "Please select next pokemon.";
            PPokeDeathStep3 = true;
        }
        else
        {
            InfoText.text = "All your Pokemon are fainted!";
            PPokeDeathAllPokeDeadStep1 = true;
        }
    }
    public void PPokeDeath3()
    {
        PPokeDeathStep3 = false;
        spaceUp = false;
        ChangeMenu(BattleMenu.ForcedPokemon);
    }
    public void PPokeAllDead1()
    {
        PPokeDeathAllPokeDeadStep1 = false;
        ChangeMenu(BattleMenu.Info);
    }
    public void PokeSwitch()
    {
        spaceUp = false;
        attackStep2 = false;
        gm.activePoke = player.GetComponent<Player>().ownedPokemon[switchedPoke].pokemon;
        GameObject.FindWithTag("ppoke").GetComponent<SpriteRenderer>().sprite = gm.activePoke.image;
        //draws the health stat in the UI
        ppokehealthmin.text = gm.activePoke.HP.ToString();
        ppokehealthmax.text = gm.activePoke.maxHP.ToString();
        plevel.text = gm.activePoke.level.ToString();
        pname.text = gm.activePoke.PName;

        //derives player moves
        if (player.GetComponent<Player>().ownedPokemon[switchedPoke].pokemon.Move1C != 0)
        {
            moveOT = gm.allMoves[gm.activePoke.Move1C].MoveName;
            moveO.text = gm.allMoves[gm.activePoke.Move1C].MoveName;
            //Debug.Log("Pogchamp");
        }
        else
        {
            moveOT = "Empty";
            moveO.text = "Empty";
        }
        if (player.GetComponent<Player>().ownedPokemon[switchedPoke].pokemon.Move2C != 0)
        {
            moveTT = gm.allMoves[gm.activePoke.Move2C].MoveName;
            moveT.text = gm.allMoves[gm.activePoke.Move2C].MoveName;
        }
        else
        {
            moveTT = "Empty";
            moveT.text = "Empty";
        }
        if (player.GetComponent<Player>().ownedPokemon[switchedPoke].pokemon.Move3C != 0)
        {
            moveTHT = gm.allMoves[gm.activePoke.Move3C].MoveName;
            moveTH.text = gm.allMoves[gm.activePoke.Move3C].MoveName;
        }
        else
        {
            moveTHT = "Empty";
            moveTH.text = "Empty";
        }
        if (player.GetComponent<Player>().ownedPokemon[switchedPoke].pokemon.Move4C != 0)
        {
            moveFT = gm.allMoves[gm.activePoke.Move4C].MoveName;
            moveF.text = gm.allMoves[gm.activePoke.Move4C].MoveName;
        }
        else
        {
            moveFT = "Empty";
            moveF.text = "Empty";
        }
        ChangeMenu(BattleMenu.Info);
        InfoText.text = "You switched in " + gm.activePoke.PName;
        attackStep2 = true;
    }
}

public enum BattleMenu
{
    //enum used to derive the switch
    Selection,
    Pokemon,
    Bag,
    Fight,
    Info,
    ForcedPokemon
}
