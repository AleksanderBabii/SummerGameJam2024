using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
        void LoadData(GameData data);

        // In C#, non-primitive types are automatically passed by reference.
        void SaveData(GameData data);
  
}
