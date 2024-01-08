﻿using System.Text.Json;

namespace DotNetCardsServer.Utils
{
    public class ObjectHelper
    {
        public static T DeepCopy<T>(T obj)
        {
            var json = JsonSerializer.Serialize(obj);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
