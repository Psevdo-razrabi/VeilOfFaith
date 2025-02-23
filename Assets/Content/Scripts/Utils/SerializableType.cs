using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Content.Scripts.Utils
{
    [Serializable]
    public class SerializableType : ISerializationCallbackReceiver
    {
        [SerializeField] string assemblyQualifiedName = string.Empty;

        public Type Type { get; private set; }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            assemblyQualifiedName = Type?.AssemblyQualifiedName ?? assemblyQualifiedName;
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (!TryGetType(assemblyQualifiedName, out var type))
            {
                //Debug.LogError($"Type {assemblyQualifiedName} not found");
                return;
            }

            Type = type;
        }

        static bool TryGetType(string typeString, out Type type)
        {
            type = Type.GetType(typeString);
            return type != null || !string.IsNullOrEmpty(typeString);
        }

        public static implicit operator Type(SerializableType sType) => sType.Type;

        public static implicit operator SerializableType(Type type) => new() {Type = type};
    }

    #if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SerializableType))]
    public class SerializableTypeDrawer : PropertyDrawer
    {
        TypeFilterAttribute typeFilter;
        string[] typeNames, typeFullNames;

        void Initialize()
        {
            if (typeFullNames != null) return;

            typeFilter = (TypeFilterAttribute) Attribute.GetCustomAttribute(fieldInfo, typeof(TypeFilterAttribute));

            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .First(x => x.FullName == "Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
            var filteredTypes = assembly.GetTypes()
                .Where(t => typeFilter == null ? DefaultFilter(t) : typeFilter.Filter(t))
                .OrderBy(x => x.Name)
                .ToList();

            switch (typeFilter.Scope)
            {
                case Scope.Scene:
                    filteredTypes = filteredTypes.Where(t => IsTypeOnScene(t)).ToList();
                    break;
                case Scope.Assets:
                    filteredTypes = filteredTypes.Where(t => !IsTypeOnScene(t)).ToList();
                    break;
            }

            typeNames = filteredTypes.Select(t => t.ReflectedType == null ? t.Name : $"None")
                .ToArray();
            typeFullNames = filteredTypes.Select(t => t.AssemblyQualifiedName).ToArray();
        }
        
        static bool IsTypeOnScene(Type type)
        {
            return Object.FindObjectsOfType(type).Length > 0;
        }

        static bool DefaultFilter(Type type)
        {
            return !type.IsAbstract && !type.IsInterface && !type.IsGenericType;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Initialize();
            var typeIdProperty = property.FindPropertyRelative("assemblyQualifiedName");

            if (string.IsNullOrEmpty(typeIdProperty.stringValue))
            {
                typeIdProperty.stringValue = typeFullNames.First();
                property.serializedObject.ApplyModifiedProperties();
            }

            var currentIndex = Array.IndexOf(typeFullNames, typeIdProperty.stringValue);
            var selectedIndex = EditorGUI.Popup(position, label.text, currentIndex, typeNames);

            if (selectedIndex >= 0 && selectedIndex != currentIndex)
            {
                typeIdProperty.stringValue = typeFullNames[selectedIndex];
                property.serializedObject.ApplyModifiedProperties();
            }
        }
    }
    
    #endif

    public class TypeFilterAttribute : PropertyAttribute
    {
        public Func<Type, bool> Filter { get; }
        public Scope Scope { get; }

        public TypeFilterAttribute(Type filterType, Scope scope = Scope.All)
        {
            Filter = type => !type.IsAbstract &&
                             !type.IsInterface &&
                             !type.IsGenericType &&
                             type.InheritsOrImplements(filterType);
            Scope = scope;
        }
    }

    public enum Scope
    {
        All,
        Assets,
        Scene,
    }

    public static class TypeExtensions
    {
        public static bool InheritsOrImplements(this Type type, Type baseType)
        {
            type = ResolveGenericType(type);
            baseType = ResolveGenericType(baseType);

            while (type != typeof(object))
            {
                if (baseType == type || HasAnyInterfaces(type, baseType)) return true;

                type = ResolveGenericType(type.BaseType);
                if (type == null) return false;
            }

            return false;
        }

        static Type ResolveGenericType(Type type)
        {
            if (type is not {IsGenericType: true}) return type;

            var genericType = type.GetGenericTypeDefinition();
            return genericType != type ? genericType : type;
        }

        static bool HasAnyInterfaces(Type type, Type interfaceType)
        {
            return type.GetInterfaces().Any(i => ResolveGenericType(i) == interfaceType);
        }
    }
}
