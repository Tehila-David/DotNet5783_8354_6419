using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using DO;
using System.Xml;
using System.Runtime.CompilerServices;

namespace Dal
{
    static internal class XMLTools
    {

        static string? s_dir =  @"..\xml\";

        static XMLTools()
        {
            if (!Directory.Exists(s_dir))
                Directory.CreateDirectory(s_dir);
        }

        #region Extension Fuctions
        [MethodImpl(MethodImplOptions.Synchronized)]

        public static T? ToEnumNullable<T>(this XElement element, string name) where T : struct, Enum =>
            Enum.TryParse<T>((string)element.Element(name), out var result) ? (T)result : default;

        [MethodImpl(MethodImplOptions.Synchronized)]

        public static DateTime? ToDateTimeNullable(this XElement element, string name) =>
            DateTime.TryParse((string)element.Element(name), out var result) ? (DateTime?)result : null;

        [MethodImpl(MethodImplOptions.Synchronized)]

        public static double? ToDoubleNullable(this XElement element, string name) =>
            !double.TryParse((string)element.Element(name), out var result) ? default : (double)result;

        [MethodImpl(MethodImplOptions.Synchronized)]

        public static int? ToIntNullable(this XElement element, string name) =>
            int.TryParse((string)element.Element(name), out var result) ? (int)result : default;

        #endregion

        #region SaveLoadWithXElement
        [MethodImpl(MethodImplOptions.Synchronized)]

        public static void SaveListToXMLElement(XElement rootElem, string entity)
        {
            string filePath = $"{s_dir + entity}.xml";
            try
            {
                rootElem.Save(filePath);
            }
            catch (Exception ex)
            {
                // DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {dir + filePath}", ex);
                throw new Exception($"fail to create xml file: {filePath}", ex);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]

        public static XElement LoadListFromXMLElement(string entity)
        {
            string filePath = $"{s_dir + entity}.xml";
            try
            {
                if (File.Exists(filePath))
                    return XElement.Load(filePath);
                XElement rootElem = new(entity);
                rootElem.Save(filePath);
                return rootElem;
            }
            catch (Exception ex)
            {
                //new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {dir + filePath}", ex);
                throw new Exception($"fail to load xml file: {filePath}", ex);
            }
        }
        #endregion

        #region SaveLoadWithXMLSerializer
        static readonly bool s_writing = true;
        [MethodImpl(MethodImplOptions.Synchronized)]

        public static void SaveListToXMLSerializer<T>(List<T?> list, string entity) where T : struct
        {
            string filePath = $"{s_dir + entity}.xml";
            try
            {
                using FileStream file = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
                using XmlWriter writer = XmlWriter.Create(file, new XmlWriterSettings() { Indent = true });

                XmlSerializer serializer = new(typeof(List<T?>));
                if (s_writing)
                    serializer.Serialize(writer, list);
                else
                    serializer.Serialize(file, list);
            }
            catch (Exception ex)
            {
                // DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {dir + filePath}", ex);            }
                throw new Exception($"fail to create xml file: {filePath}", ex);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]

        public static List<T?> LoadListFromXMLSerializer<T>(string entity) where T : struct
        {
            string filePath = $"{s_dir + entity}.xml";
            try
            {
                if (!File.Exists(filePath)) return new();
                using FileStream file = new(filePath, FileMode.Open);
                XmlSerializer x = new(typeof(List<T?>));
                return x.Deserialize(file) as List<T?> ?? new();
            }
            catch (Exception ex)
            {
                // DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {dir + filePath}", ex);            }
                throw new Exception($"fail to load xml file: {filePath}", ex);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]

        public static List<T> LoadListFromXMLSerializer1<T>(string entity) where T : struct// for running numbers
        {
            string filePath = $"{s_dir + entity}.xml";
            try
            {
                if (!File.Exists(filePath)) return new();
                using FileStream file = new(filePath, FileMode.Open);
                XmlSerializer x = new(typeof(List<T>));
                return x.Deserialize(file) as List<T> ?? new();
            }
            catch (Exception ex)
            {
                // DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {dir + filePath}", ex);            }
                throw new Exception($"fail to load xml file: {filePath}", ex);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]

        public static void SaveListToXMLSerializer1<T>(List<T> list, string entity) where T : struct//for running numbers
        {
            string filePath = $"{s_dir + entity}.xml";
            try
            {
                using FileStream file = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
                using XmlWriter writer = XmlWriter.Create(file, new XmlWriterSettings() { NewLineOnAttributes = true, Indent = true });
                {
                    XmlSerializer serializer = new(typeof(List<T>));
                    if (s_writing)
                        serializer.Serialize(writer, list);
                    else
                        serializer.Serialize(file, list);
                }
            }
            catch (Exception ex)
            {
                // DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {dir + filePath}", ex);            }
                throw new Exception($"fail to create xml file: {filePath}", ex);
            }
        }

        
        #endregion
    }
}


