using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Stealer
{
    public class Spy
    {
        public string StealFieldInfo(string investigatedClass, params string[] input)
        {
            Type hacker = Type.GetType(investigatedClass);


            FieldInfo[] fields = hacker.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

            StringBuilder sb = new StringBuilder();

            Object classInstance = Activator.CreateInstance(hacker, new object[] { });

            sb.AppendLine($"Class under investigation: {investigatedClass}");

            foreach (FieldInfo field in fields.Where(f => input.Contains(f.Name)))
            {
                sb.AppendLine($"{field.Name} = {field.GetValue(classInstance)}");
            }

            return sb.ToString().Trim();
        }

        public string AnalyzeAccessModifiers(string className)
        {
            StringBuilder sb = new StringBuilder();

            Type classType = Type.GetType(className);

            FieldInfo[] fields = classType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            MethodInfo[] publicMethods = classType.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            MethodInfo[] nonPublicMethods = classType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                sb.AppendLine($"{field.Name} must be private!");
            }

            foreach (var method in nonPublicMethods.Where(f => f.Name.StartsWith("get")))
            {
                sb.AppendLine($"{method.Name} have to be public!");
            }

            foreach (var method in publicMethods.Where(f => f.Name.StartsWith("set")))
            {
                sb.AppendLine($"{method.Name} have to be private!");
            }

            return sb.ToString().Trim();
        }

        public string RevealPrivateMethods(string className)
        {
            StringBuilder sb = new StringBuilder();

            Type classType = Type.GetType(className);

            sb.AppendLine($"All private Methods of Class: {classType.Name}");
            sb.AppendLine($"Base Class: {classType.BaseType.Name}");

            MethodInfo[] methods = classType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);


            foreach (var method in methods)
            {
                sb.AppendLine(method.Name);
            }

            return sb.ToString().Trim();
        }

        public string CollectGettersAndSetters(string className)
        {
            StringBuilder sb = new StringBuilder();

            Type classType = Type.GetType(className);

            MethodInfo[] methods = classType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var method in methods.Where(m => m.Name.StartsWith("get")))
            {
                sb.AppendLine($"{method.Name} will set field of {method.ReturnType.FullName}");
            }

            foreach (var method in methods.Where(m => m.Name.StartsWith("set")))
            {
                sb.AppendLine($"{method.Name} will return {method.GetParameters().First().ParameterType}");
            }

            

            return sb.ToString().Trim();
        }
    }
}
