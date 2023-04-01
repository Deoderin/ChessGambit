
using System;

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
  }
}
