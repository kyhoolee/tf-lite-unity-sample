using UnityEngine;
[CreateAssetMenu]
public class PowerSO : ScriptableObject
{
    [SerializeField] private int _value = 3;
    public int Value
    {
        set { _value = value; }
        get => _value;
    }

    public void PowerFull(){
        _value = 3;
    }

    public bool CanDecreament(){
        if(_value > 0){
            return true;
        }
        return false;
    }
}
