
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Extensions{
  public static class Extensions
  {
    public static T With<T>(this T _self, Action<T> _set){
      _set.Invoke(_self);
      return _self;
    }

    public static T With<T>(this T _self, Action<T> _apply, Func<bool> _when){
      if(_when())
        _apply?.Invoke(_self);

      return _self;
    }    
    
    public static T With<T>(this T _self, Action<T> _apply, bool _when){
      if(_when)
        _apply?.Invoke(_self);

      return _self;
    }

    public static int[][] ConvertToIntMass(this List<Vector2Int> _self){
      int[][] mass = new int[_self.Count][];

      for(var i = 0; i < _self.Count; i++){
        mass[i] = new[]{_self[i].x, _self[i].y};
      }

      return mass;
    }
  }
}
