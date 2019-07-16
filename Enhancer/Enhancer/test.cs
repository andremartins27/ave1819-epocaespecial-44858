using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Enhancer
{
    public class test
    {
        public void test1(MethodBuilder metBuilder, MethodInfo m, string name)
        {
            object[] array = { 1, 2 };//apenas para teste
            MemberInfo member = GetType().GetMember(name)[0];
            Attribute a = member.GetCustomAttribute(typeof(EnhancerAttribute));
            a.GetType().GetMethod("Check").Invoke(a, array);
            GetType().BaseType.GetMethod(name).Invoke(this, array);


        }

        public void test2(MethodBuilder metBuilder, MethodInfo m, string name)
        {
            object[] array = { };//apenas para teste
            MemberInfo member = GetType().GetMember(name)[0];
            Attribute a = member.GetCustomAttribute(typeof(EnhancerAttribute));
            a.GetType().GetMethod("Check").Invoke(a, array);
            GetType().BaseType.GetMethod(name).Invoke(this, array);


        }

        public static void test3<T>(T obj, String method, String name, params object[] values)
        {
            T ldarg0 = obj;
            string methodName = method;
            string memberName = name;
            object[] array = values;

            MemberInfo member = ldarg0.GetType().BaseType.GetMember(memberName)[0];
            Attribute a = member.GetCustomAttribute(typeof(EnhancerAttribute));
            MethodInfo check = a.GetType().GetMethod("Check");
            check.Invoke(a, array);
            MethodInfo baseMethod = ldarg0.GetType().BaseType.GetMethod(methodName);
            baseMethod.Invoke(ldarg0, array);

        }

    }
}
