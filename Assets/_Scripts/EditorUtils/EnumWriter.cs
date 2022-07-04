using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Misc;
using UnityEditor;
using UnityEngine;

namespace EditorUtils
{
    public static class EnumWriter
    {
        public static void GenerateCode(string path, string sourceCode)
        {
            // a bit of a hack but we need to convert the Unity asset path into a valid system path by erasing one duplicate "Assets"
            var pathParts = path.Split('/').ToArray();

            // overwrite the "Assets" with the full path to Assets
            pathParts[0] = Application.dataPath;

            // re-combine all path parts but this time use the according system file path separator char
            var targetSystemPath = Path.Combine(pathParts);

            // Write the content into the file via the normal file IO
            File.WriteAllText(targetSystemPath, sourceCode);

            // trigger a refresh so unity re-loads and re-compiles
            AssetDatabase.Refresh();
        }
        

        public static string CreateEnumSubTypeScript(Type parentClass)
        {
            return CreateEnumerationScript(parentClass.Name + "Type", GetEnumSubTypeMembers(parentClass), parentClass.Namespace);
        }
        
        public static string[] GetEnumSubTypeMembers(Type parentClass)
        {
            var subTypes = ReflectiveEnumerator.GetEnumerableOfType(parentClass);
            List<string> subTypeNames = new List<string>();
            foreach(var subType in subTypes)
                subTypeNames.Add(subType.Name);
            return subTypeNames.ToArray();
        }

        public static string CreateEnumerationScript(string enumName, string[] enumMembers, string namespaceName = null)
        {
            StringBuilder script = new StringBuilder();
            if (namespaceName != null)
            {
                script.AppendLine($"namespace {namespaceName}");
                script.AppendLine("{");
            }

            script.AppendLine($"\t[System.Serializable]");
            script.AppendLine($"\tpublic class {enumName} : Enumeration");
            
            script.AppendLine("\t{");

            script.AppendLine("\t\t// Members");
            int i = 0;
            foreach (string enumMember in enumMembers)
            {
                script.AppendLine($"\t\tpublic static readonly {enumName} {enumMember}" +
                                  $" = new {enumName}({i},\"{enumMember}\");");
                i++;
            }

            script.AppendLine("\n\t\t// Constructors");
            script.AppendLine($"\t\tprivate {enumName}() {{ }}");
            script.AppendLine($"\t\tprivate {enumName}(int value, string displayName) : base(value, displayName) {{ }}");
            script.AppendLine("\t}");

            if (namespaceName != null)
                script.Append("}");
            
            return script.ToString();
        }
        
        public static string CreateEnumScript(string enumName, string[] enumMembers, string namespaceName = null)
        {
            StringBuilder script = new StringBuilder();
            
            if (namespaceName != null)
            {
                script.AppendLine($"namespace {namespaceName}");
                script.AppendLine("{");
            }

            script.AppendLine($"\tpublic enum {enumName}");
            
            script.AppendLine("\t{");

            foreach (string enumMember in enumMembers)
            {
                script.AppendLine($"\t\t{enumMember},");
            }
            script.AppendLine("\t}");

            if (namespaceName != null)
                script.Append("}");
            
            return script.ToString();
        }
    }
}