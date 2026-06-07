using Il2CppInterop.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace liblib.Core
{
    internal class Instance:MonoWrap
    {
        internal static Instance instance;

        internal void Awake()
        {
            instance = this;
            Logger.Log(@$"liblib {Constants.GUID} {Constants.Version} by xX_L4RPL0RDM45T3R_Xx");

            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsClass && t.GetCustomAttribute<AddOnAwake>() != null);
            foreach (var type in types)
                gameObject.AddComponent(Il2CppType.From(type));
        }

        [AttributeUsage(AttributeTargets.Class)]
        public class AddOnAwake : Attribute
        {
        }

        public interface IPlugin
        {
            string Name { get; }
            string Version { get; }

            void Initialize();
        }
    }
}
