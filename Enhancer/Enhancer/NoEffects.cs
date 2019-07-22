using System;
using System.Reflection;

namespace Enhancer
{
    public class NoEffects : EnhancerAttribute
    {
        Object[] state;

        public override void Check(object[] args)
        {
            if (state == null)//Before base method
            {
                PropertyInfo[] pi = args[0].GetType().GetProperties();
                FieldInfo[] fi = args[0].GetType().GetFields();
                state = new object[pi.Length + fi.Length];
                int i = 0;
                foreach(var p in pi)
                {
                    state[i] = p.GetValue(args[0]);
                    i++;
                }
                foreach (var f in fi)
                {
                    state[i] = f.GetValue(args[0]);
                    i++;
                }
            }
            else//After base method
            {
                PropertyInfo[] pi = args[0].GetType().GetProperties();
                FieldInfo[] fi = args[0].GetType().GetFields();
                int i = 0;
                foreach (var p in pi)
                {
                    if (state[i] == null)
                    {
                        if (p.GetValue(args[0]) != null) throw new Exception("Changed state of curr object");
                    }
                    else if(!state[i].Equals(p.GetValue(args[0]))) throw new Exception("Changed state of curr object");
                    i++;
                }
                foreach (var f in fi)
                {
                    if (state[i] == null)
                    {
                        if (f.GetValue(args[0]) != null) throw new Exception("Changed state of curr object");
                    }
                    else if (!state[i].Equals(f.GetValue(args[0]))) throw new Exception("Changed state of curr object");
                    i++;
                }

            }            
        }
    }
}
