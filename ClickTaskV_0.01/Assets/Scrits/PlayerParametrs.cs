﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerParametrs : MonoBehaviour {
    private float _monsterArmor=10;//10 percent
    private float _monsterMagicresist = 10;
    [SerializeField]
    private Text _strengthCount, _sleightCount, _intellectCount, _staminaCount;

    [SerializeField]
    private float _strength, _agility, _intellect, _stamina,_SAICoef; //Main states 
    
    [SerializeField]
    private float _basicClickDamage=1, _basicCritChance=10,_basicCritPowerCoef=1.5f,_basicSkillsPowerCoef=1,_basicColdounCoef=1,_basicResistCoef=0;

    [SerializeField]
    private float LvL_Score;
    //Game params
    [SerializeField]
    private float _clotheStrength=0, _clotheAgility=0, _clotheIntellect=0, _clotheStamina=0,_clotheMultiplyCriticalPowerChance=1.5f,_clotheMultiplyColdounCoef=1;//Clothes prams

    [SerializeField]
    private float _passiveModStrength=1, _passiveModAgility=1, _passiveModintellect=1, _passiveModStamina=1, _passiveMultiplierCriticalChanceCoef=1 , _passiveMultiplierRollbackCoolDown=1;

    //Passive modification without 2 coefs;
    [SerializeField]
    private float _passiveLvlExecutioner, _passiveLvlrush, _passiveLvlmagicArmor;
    [HideInInspector]
    public float timeDecreaseCoeficient = 0.1f;

    [HideInInspector]
    public float HitDecreaseCoefForSpell = 1f;

    private const float BASE_HEALTH_DECREESE_COEFICIENT = 0.01f;
    private float _clickStrength;
  
    private float critChanse = 1.05f;

    private float correctiveHitStrangth = 1.0f;

    [SerializeField]
    private float newPassiveSpending=0;
    // calculated params
    public void CalulateParams()
        {
        //params
        _strength = System.Convert.ToSingle(_strengthCount);
        _agility = System.Convert.ToSingle(_sleightCount);
        _intellect = System.Convert.ToSingle(_intellectCount);
        _stamina = System.Convert.ToSingle(_staminaCount);
        float _spendingStregth  = 2 * _passiveLvlExecutioner;
        float _spendingAgility = 2 * _passiveLvlrush;
        float _spendingIntellect = 2 * _passiveLvlmagicArmor;
        float _charapterParams = _spendingStregth + _spendingAgility + _spendingIntellect + newPassiveSpending;
        float _koefB15 = (_strength + _agility + _intellect + _stamina + _charapterParams)/4;
        float _heroLevel = 1 + ((_strength + _agility + _intellect + _stamina) - 40) / LvL_Score;
        float _expForLevel = 100 * (_heroLevel * _heroLevel);
        float _finalStateStrength = (_strength + _clotheStrength) * _passiveModStrength;
        float _finalStateAgility = (_agility + _clotheAgility) * _passiveModAgility;
        float _finalStateIntellect = (_intellect + _clotheIntellect) * _passiveModintellect;
        float _finalStateStaina = (_stamina + _clotheStamina) * _passiveModStamina;
        float _finalMultiCritPowerCoef = (_clotheMultiplyCriticalPowerChance * _passiveMultiplierCriticalChanceCoef);
        float _finalMultiplierRollbackCoolDown = (_passiveMultiplierRollbackCoolDown*_clotheMultiplyColdounCoef);
        float _clickCritcoef=1+(0.2f*_passiveLvlExecutioner);
        float _clickDamage = (_basicClickDamage + _finalStateStrength) * _clickCritcoef;
        float _effectOnCritChance = 3 * _basicCritChance * _finalStateAgility;
        float _critChance = (_basicCritChance + (_finalStateAgility / (_koefB15 * _SAICoef))) + _effectOnCritChance;
        float _critStrengthCoef = 1.5f;
        float _skillPowercoef = _basicSkillsPowerCoef+(_finalStateIntellect/(_koefB15/(_SAICoef)));
        float _coldounSkillCoef = 1;
        float _resistCoef = _basicResistCoef + (_koefB15 / _finalStateStaina);
        //Monsters
        float _monsterDamageEffect = 1- (_passiveLvlmagicArmor*0.05f);
        float _monsterDamage = ((1 + _expForLevel / 50) / 2) * _monsterDamageEffect;
        float _monsterHealth =_koefB15*_heroLevel*1.5f;
        float _monsterExp = (_monsterHealth + _expForLevel / (_koefB15 * _heroLevel))/15;
        float _monsterGold = ((_monsterHealth + _expForLevel / (_heroLevel * _koefB15)) / 15) * _heroLevel;
        float _monsterDropCoef = (_monsterHealth + _expForLevel / (_koefB15 * _heroLevel)) / 15;
    }   
    public void setNormalTimeDecrease()
    {
        timeDecreaseCoeficient = 0.1f;
    }

    public float CalculateTimeDecrease()
    {

        return timeDecreaseCoeficient;
    }

    public float CalculateHit(MonstersBasicClass monster)
    {
        _clickStrength = ((BASE_HEALTH_DECREESE_COEFICIENT + Random.Range(0.01f, 0.50f)) * CalculateCritChanse()) / Random.Range(0.5f + (BigMom.ENC._scoreCounter + 1f) / 5f, 1.4f + (BigMom.ENC._scoreCounter + 1f) / 5f);
        _clickStrength = _clickStrength * monster.ClickStrengthCorrectiveVector * HitDecreaseCoefForSpell;
        Debug.Log(monster.ClickStrengthCorrectiveVector.ToString());
        return _clickStrength;
        
    }

    private float CalculateCritChanse()
    {

        if (Random.Range(1f, 100f) * critChanse > 99)
            return 4.0f;
        else if (Random.Range(1f, 100f) * critChanse > 98)
            return 3.0f;
        else if (Random.Range(1f, 100f) * critChanse > 97)
            return 2.0f;
        else
            return 1.0f;
    }
}
