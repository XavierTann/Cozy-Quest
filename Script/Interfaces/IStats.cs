using UnityEngine;

interface IStats {

    float MaxHealth {get;}
    
    float Health {get; set;}

    float AttackDamage {get;}

    float AttackRange {get;}

    float MovementSpeed {get;}

    float DetectRange {get;}
    
}