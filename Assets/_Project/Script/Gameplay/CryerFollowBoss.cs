using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryerFollowBoss : OneSpriteCharacter
{
    public override int Score => -1;
    private void Start()
    {
        _health = 1;
        onDie += GameplayController.Instance.UpdateScore;    
    }

    public override void Setup(CharacterData characterData, CharacterGroupSetting characterGroup)
    {
        
    }

    protected override void Update()
    {
        
    }

    protected override void LateUpdate()
    {
       
    }
}
