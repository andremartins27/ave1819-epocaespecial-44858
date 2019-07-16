using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;


namespace Enhancer
{

    public class Enhancer
    {
        public static T Build<T>(params object[] args)
        {
            Type klass = typeof(T);
            string NAME = klass.Name + "Enhanced";

            AssemblyBuilder asmBuilder = AssemblyBuilder.DefineDynamicAssembly(
                new AssemblyName(NAME),
                AssemblyBuilderAccess.RunAndSave);

            ModuleBuilder modBuilder = asmBuilder.DefineDynamicModule(NAME, NAME + ".dll");

            TypeBuilder tb = modBuilder.DefineType(NAME);

            tb.SetParent(klass);

            //build constructors
            ConstructorInfo[] ci = klass.GetConstructors();

            foreach (ConstructorInfo c in ci)
            {
                ParameterInfo[] parameters = c.GetParameters();
                Type[] argTypes = new Type[parameters.Length];
                int i = 0;
                foreach (ParameterInfo p in parameters)
                {
                    argTypes[i] = p.ParameterType;
                    i++;
                }

                ConstructorBuilder cb = tb.DefineConstructor(MethodAttributes.Public,
                                CallingConventions.Standard, argTypes);

                ImplementConstructor(c, cb, parameters.Length);
            }

            //build properties
            PropertyInfo[] pi = GetProperties(klass);

            foreach(PropertyInfo p in pi)
            {
                FieldBuilder fb = tb.DefineField(p.Name, p.PropertyType, FieldAttributes.Private);

                PropertyBuilder pb = tb.DefineProperty(p.Name, PropertyAttributes.HasDefault, p.PropertyType, null);

                //get method

                MethodBuilder mbget = tb.DefineMethod("get_" + p.Name, 
                    MethodAttributes.Public | 
                    MethodAttributes.SpecialName | 
                    MethodAttributes.ReuseSlot, p.PropertyType, Type.EmptyTypes);

                ILGenerator il = mbget.GetILGenerator();
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, fb);
                il.Emit(OpCodes.Ret);

                //set method
                MethodBuilder mbset = tb.DefineMethod("set_"+p.Name, 
                    MethodAttributes.Public |
                    MethodAttributes.SpecialName |
                    MethodAttributes.ReuseSlot, null, new Type[] { p.PropertyType });

                ImplementMethods(mbset, p.GetSetMethod(), p.Name);

                pb.SetSetMethod(mbset);
                pb.SetGetMethod(mbget);
            }


            //build methods
            MethodInfo[] mi = GetMethods(klass);

            foreach (MethodInfo method in mi)
            {
                string name = method.Name;
                Type t = method.GetType();


                ParameterInfo[] parameters = method.GetParameters();
                Type[] array = new Type[pi.Length];
                int idx = 0;
                foreach (ParameterInfo p in parameters)
                {
                    array[idx] = p.ParameterType;
                    idx++;
                }
                MethodBuilder metBuilder = tb.DefineMethod(method.Name,
                    MethodAttributes.Public |
                    MethodAttributes.Virtual |
                    MethodAttributes.ReuseSlot,
                    method.ReturnType,
                    array.ToArray());
                ImplementMethods(metBuilder, method, name);
            }
            Type final = tb.CreateTypeInfo().AsType();
            asmBuilder.Save(NAME + ".dll");
            return (T)Activator.CreateInstance(final, args);
        }

        internal static Attribute getAttribute(Type type, string name)
        {
            return type.GetMember(name)[0].GetCustomAttribute(typeof(EnhancerAttribute));
        }

        public static MethodInfo getCheckMethod(Attribute a)
        {
            return a.GetType().GetMethod("Check");
        }

        private static void ImplementConstructor(ConstructorInfo c, ConstructorBuilder cb, int nparam)
        {
            ILGenerator il = cb.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            for (int i = 1; i <= nparam; i++)
            {
                il.Emit(OpCodes.Ldarg, i);
            }
            il.Emit(OpCodes.Call, c);
            il.Emit(OpCodes.Ret);

        }

        private static void ImplementMethods(MethodBuilder metBuilder, MethodInfo m, string memberName)
        {
            ILGenerator il = metBuilder.GetILGenerator();

            ParameterInfo[] pi = m.GetParameters();
            int nparam = pi.Length;

            LocalBuilder array = il.DeclareLocal(typeof(object[]));
            LocalBuilder member = il.DeclareLocal(typeof(MemberInfo));
            LocalBuilder a = il.DeclareLocal(typeof(Attribute));
            LocalBuilder check = il.DeclareLocal(typeof(MethodInfo));
            LocalBuilder baseMethod = il.DeclareLocal(typeof(MethodInfo));

            //Construção do array de parametros
            il.Emit(OpCodes.Ldc_I4, nparam);
            il.Emit(OpCodes.Newarr, typeof(Object));

            for (int i = 0; i < nparam; i++)
            {
                il.Emit(OpCodes.Dup);
                il.Emit(OpCodes.Ldc_I4, i);
                il.Emit(OpCodes.Ldarg, i + 1);
                il.Emit(OpCodes.Stelem_Ref);

            }

            il.Emit(OpCodes.Stloc_0);

            
            //get curr member
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Callvirt, typeof(Object).GetMethod("GetType"));
            il.Emit(OpCodes.Callvirt, typeof(Type).GetMethod("get_BaseType"));
            il.Emit(OpCodes.Ldstr, memberName);
            il.Emit(OpCodes.Callvirt, typeof(Type).GetMethod("GetMember", new Type[] { typeof(String) }));
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Ldelem_Ref);
            il.Emit(OpCodes.Stloc_S, member);

            //attribute
            il.Emit(OpCodes.Ldloc_S, member);
            il.Emit(OpCodes.Ldtoken, typeof(EnhancerAttribute));
            il.Emit(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle", new Type[] { typeof(RuntimeTypeHandle) }));
            il.Emit(OpCodes.Call, typeof(CustomAttributeExtensions).GetMethod("GetCustomAttribute", new Type[] { typeof(MemberInfo), typeof(Type) }));
            il.Emit(OpCodes.Stloc_S, a);


            //check method
            il.Emit(OpCodes.Ldloc_S, a);
            il.Emit(OpCodes.Callvirt, typeof(Object).GetMethod("GetType"));
            il.Emit(OpCodes.Ldstr, "Check");
            il.Emit(OpCodes.Callvirt, typeof(Type).GetMethod("GetMethod", new Type[] { typeof(String) }));
            il.Emit(OpCodes.Stloc_S, check);
            il.Emit(OpCodes.Ldloc_S, check);
            il.Emit(OpCodes.Ldloc_S, a);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Callvirt, typeof(MethodBase).GetMethod("Invoke", new Type[] { typeof(Object), typeof(Object[]) }));
            il.Emit(OpCodes.Pop);
            

            //base method
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, typeof(Object).GetMethod("GetType"));
            il.Emit(OpCodes.Callvirt, typeof(Type).GetMethod("get_BaseType"));
            il.Emit(OpCodes.Ldstr, memberName);
            il.Emit(OpCodes.Callvirt, typeof(MemberInfo).GetMethod("get_Name"));
            il.Emit(OpCodes.Callvirt, typeof(Type).GetMethod("GetMethod", new Type[] { typeof(String) }));
            il.Emit(OpCodes.Stloc_S, baseMethod);
            il.Emit(OpCodes.Ldloc_S, baseMethod);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Callvirt, typeof(MethodBase).GetMethod("Invoke", new Type[] { typeof(Object), typeof(Object[]) }));
            il.Emit(OpCodes.Pop);
            il.Emit(OpCodes.Ret);

        }

        private static MethodInfo[] GetMethods(Type klass)
        {
            MethodInfo[] mi = klass.GetMethods();
            ArrayList array = new ArrayList();
            foreach (MethodInfo m in mi)
            {
                if ((m.IsAbstract || m.IsVirtual) && m.GetCustomAttributes(typeof(EnhancerAttribute)).Any()) array.Add(m);
            }


            MethodInfo[] toRet = new MethodInfo[array.ToArray().Length];
            int i = 0;
            foreach (object o in array.ToArray())
            {
                toRet[i] = (MethodInfo)o;
                i++;
            }

            return toRet;
        }

        private static PropertyInfo[] GetProperties(Type klass)
        {
            ArrayList array = new ArrayList();
            PropertyInfo[] pi = klass.GetProperties();
            foreach (PropertyInfo p in pi)
            {
                if (p.GetCustomAttributes(typeof(EnhancerAttribute)).Any())
                {
                    MethodInfo m = p.GetSetMethod();
                    if (m.IsAbstract || m.IsVirtual) array.Add(p);
                }
            }

            PropertyInfo[] toRet = new PropertyInfo[array.ToArray().Length];
            int i = 0;
            foreach (object o in array.ToArray())
            {
                toRet[i] = (PropertyInfo)o;
                i++;
            }

            return toRet;
        }


    }
}
