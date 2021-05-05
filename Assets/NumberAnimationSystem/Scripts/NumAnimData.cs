using UnityEngine;

[CreateAssetMenu(fileName = "NumAnimData", menuName = "Data Tools/Create Number Animation Data", order = 1)]
public class NumAnimData : ScriptableObject
{


  public int _total;
  public float _animationTime;

    public NumAnimData(int total, float animationTime)
    {
        _total = total;
        _animationTime = animationTime;
    }
}
