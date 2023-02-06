using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

//implement Order Item with linq to XML
namespace Dal
{
    /// <summary>
    /// Running Numbers for Order and OrderItem
    /// </summary>
    public struct ImportentNumbers
    {
        public double numberSaved { get; set; }
        public string typeOfnumber { get; set; }
    }

    internal class OrderItem : IOrderItem
    {

        const string s_orderItems = @"OrderItems";
        string configPath = @"config";// XML file of Running List Numbers


        /// <summary>
        /// Return list of orderItems by Filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? filter = null)
        {
            var orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems)!;
            return filter == null ? orderItemsList.OrderBy(lec => ((DO.OrderItem)lec!).ID)
                                  : orderItemsList.Where(filter).OrderBy(lec => ((DO.OrderItem)lec!).ID);
        }


        /// <summary>
        /// Return orderItem by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.OrderItem GetById(int id) =>
            XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems).FirstOrDefault(p => p?.ID == id)
            //DalMissingIdException(id, "Lecturer");
            ?? throw new Exception("missing id");


        /// <summary>
        /// Return OrderItem by Predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.OrderItem? GetById(Func<DO.OrderItem?, bool>? predicate)
        {
            var orderItemList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems)!;
            if (orderItemList.FirstOrDefault(predicate) == null)
            {
                throw new Exception("Order Item not Exsits");
            }
            return orderItemList.FirstOrDefault(predicate);

        }


        /// <summary>
        /// Add orderItem to list
        /// </summary>
        /// <param name="orderItem"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int Add(DO.OrderItem orderItem)
        {
            var orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);

            if (orderItemsList.Exists(lec => lec?.ID == orderItem.ID))
                throw new Exception("id already exist");

            List<ImportentNumbers> runningList = XMLTools.LoadListFromXMLSerializer1<ImportentNumbers>(configPath);//Loads a list of Running numbers from the XML file

            ImportentNumbers runningNum = (from number in runningList
                                           where (number.typeOfnumber == "OrderItem Running Number")
                                           select number).FirstOrDefault();
            runningList.Remove(runningNum);

            runningNum.numberSaved++;
            orderItem.ID = (int)runningNum.numberSaved;


            runningList.Add(runningNum);
            orderItemsList.Add(orderItem);

            XMLTools.SaveListToXMLSerializer1(runningList, configPath);
            XMLTools.SaveListToXMLSerializer(orderItemsList, s_orderItems);

            return orderItem.ID;
        }

        /// <summary>
        /// Delete  orderItem from list
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Delete(int id)
        {
            var orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);

            if (orderItemsList.RemoveAll(p => p?.ID == id) == 0)
                throw new Exception("missing id"); //new DalMissingIdException(id, "Lecturer");

            XMLTools.SaveListToXMLSerializer(orderItemsList, s_orderItems);
        }


        /// <summary>
        ///  Update OrderItem from List
        /// </summary>
        /// <param name="orderItem"></param>
        /// <exception cref="Exception"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update(DO.OrderItem orderItem)
        {
            Delete(orderItem.ID);
            var ordersList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
            if (ordersList.Exists(item => item?.ID == orderItem.ID))
                throw new Exception("id already exist");
            ordersList.Add(orderItem);
            XMLTools.SaveListToXMLSerializer(ordersList, s_orderItems);
        }

    }
}

