using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace AutoReservation.Wpf.Logic {
    public static class Extension {
        /// <summary>
        ///     Replace properties of an object with the properties of another object.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        public static void Replace(this object target, object source) {
            if (target.GetType() == source.GetType()) {
                PropertyInfo[] properties = target.GetType().GetProperties();
                foreach (PropertyInfo propertyInfo in properties) {
                    propertyInfo.SetValue(target, propertyInfo.GetValue(source));
                }
            } else {
                throw new ArgumentException("Target and Source are not of the same type.");
            }
        }

        /// <summary>
        ///     Replace properties of an object with the properties of another object.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        /// <param name="type"></param>
        public static void Replace(this object target, object source, Type type) {
            if (target.GetType().IsSubclassOf(type) && source.GetType().IsSubclassOf(type)) {
                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo propertyInfo in properties) {
                    propertyInfo.SetValue(target, propertyInfo.GetValue(source));
                }
            } else {
                throw new ArgumentException("Target(\"" + target.GetType().Name +
                                            "\") or Source(\"" + source.GetType().Name +
                                            "\") is not subclass of defined type (\"" + type.Name + "\").");
            }
        }

        /// <summary>
        /// Clones a Serializable object.
        /// </summary>
        /// <para>Source: https://stackoverflow.com/questions/129389/how-do-you-do-a-deep-copy-of-an-object-in-net-c-specifically </para>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Clone<T>(this T obj)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            using (Stream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, obj);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)serializer.ReadObject(stream);
            }
            /*
            var formatter = new BinaryFormatter();
            using (Stream stream = new MemoryStream())
            {
                formatter.Serialize(stream, obj);
                stream.Seek(0, SeekOrigin.Begin);

                return (T)formatter.Deserialize(stream);
            }*/
            
        }

        public static Object GetPropValue(this Object obj, String name) {
            foreach (String part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }
    }
}