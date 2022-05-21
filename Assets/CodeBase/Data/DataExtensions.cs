using UnityEngine;

namespace CodeBase.Data
{
  public static class DataExtensions
  {

    public static Vector3 AddY(this Vector3 vector, float y)
    {
      vector.y = y;
      return vector;
    }
    
    public static Vector2 ToVector2(this Vector3 vector3) => 
      new Vector2(vector3.x, vector3.y);
    
    public static Vector3 FromScreenToWorld(this Vector2 screenPosition)
    {
      Vector3 mousePos = screenPosition;
      mousePos.z = -Camera.main.transform.position.z;
      return Camera.main.ScreenToWorldPoint(mousePos);
    }

    public static string ToJson(this object obj) => 
      JsonUtility.ToJson(obj);

    public static T ToDeserialized<T>(this string json) =>
      JsonUtility.FromJson<T>(json);
  }
}