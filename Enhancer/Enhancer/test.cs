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
        
        public void test2<T>(T obj, String method, String name, params object[] values)
        {
            T ldarg0 = obj;
            string methodName = method;
            string memberName = name;
            object[] array = values;

            MemberInfo member = this.GetType().BaseType.GetMember(memberName)[0];
            EnhancerAttribute a = (EnhancerAttribute) member.GetCustomAttribute(typeof(EnhancerAttribute));
            a.Check(array);
            MethodInfo baseMethod = this.GetType().BaseType.GetMethod(methodName);
            baseMethod.Invoke(this, array);

        }

        public void test1<T>(T obj, String method, String name, params object[] values)
        {
            T ldarg0 = obj;
            string methodName = method;
            string memberName = name;
            object[] array = values;

            MemberInfo member = ldarg0.GetType().BaseType.GetMember(memberName)[0];
            EnhancerAttribute a = (EnhancerAttribute)member.GetCustomAttribute(typeof(EnhancerAttribute));
            a.Check(array);
            /*Type t = ldarg0.GetType().BaseType;
            MethodInfo baseMethod = t.GetMethod(methodName);
            baseMethod.Invoke(ldarg0, array);
            return;*/

        }

    }
}
