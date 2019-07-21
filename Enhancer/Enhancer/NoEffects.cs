using System;
using System.Reflection;

namespace Enhancer
{
    public class NoEffects : EnhancerAttribute
    {
        Object[] properties;

        public override void Check(object[] args)
        {
            if (properties == null)//pre base method
            {
                Console.WriteLine("pre NoEffects");
                PropertyInfo[] pi = args[0].GetType().GetProperties();
                properties = new object[pi.Length];
                int i = 0;
                foreach(var p in pi)
                {
                    properties[i] = p.GetValue(args[0]);
                    i++;
                }
                
                
            }
            else//after base method
            {
                
                PropertyInfo[] pi = args[0].GetType().GetProperties();
                int i = 0;
                foreach (var p in pi)
                {
                    if (properties[i] == null)
                    {
                        if (p.GetValue(args[0]) != null) throw new Exception("Changed state of curr object");
                    }
                    else if(!properties[i].Equals(p.GetValue(args[0]))) throw new Exception("Changed state of curr object");
                    i++;
                }

            }
           
            
        }
    }
}
