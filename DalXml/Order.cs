using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
using System.Xml.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.CompilerServices;

namespace Dal
{
    //implement Order with linq to XML
    internal class Order : IOrder
    {
       

        const string s_orders = @"Orders";
        string configPath = @"config";

       
        /// <summary>
        /// Return list of orders by Filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? filter = null)
        {
            var ordersList = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
            return filter == null ? ordersList.OrderBy(lec => ((DO.Order)lec!).ID)
                                  : ordersList.Where(filter).OrderBy(lec => ((DO.Order)lec!).ID);
        }



        /// <summary>
        /// Return order by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.Order GetById(int id) =>
            XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders).FirstOrDefault(p => p?.ID == id)
        
            ?? throw new Exception(" The order does not exist in the list");


        /// <summary>
        /// Return Order by Predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.Order? GetById(Func<DO.Order?, bool>? predicate)
        {
            var ordersList = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders)!;
            if (ordersList.FirstOrDefault(predicate) == null)
            {
                throw new Exception("Order not Exsits");
            }
            return ordersList.FirstOrDefault(predicate);

        }


        /// <summary>
        /// Add order to list
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int Add(DO.Order order)
        {
            var ordersList = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);//Loads a list of orders from the XML file

            if (ordersList.Exists(item => item?.ID == order.ID))
                throw new Exception("id already exist");

            List<ImportentNumbers> runningList = XMLTools.LoadListFromXMLSerializer1<ImportentNumbers>(configPath);// Loads a list of runningNumbers from the XML file

            ImportentNumbers runningNum = (from number in runningList
                                           where (number.typeOfnumber == "Order Running Number")
                                           select number).FirstOrDefault();
            runningList.Remove(runningNum);

            runningNum.numberSaved++;
            order.ID = (int)runningNum.numberSaved;


            runningList.Add(runningNum);
            ordersList.Add(order);

            XMLTools.SaveListToXMLSerializer1(runningList, configPath);//save new list in xml File 
            XMLTools.SaveListToXMLSerializer(ordersList, s_orders);//save new list in xml File 

            return order.ID;
        }


        /// <summary>
        /// Delete  order from list
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Delete(int id)
        {
            var ordersList = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

            if (ordersList.RemoveAll(p => p?.ID == id) == 0)
                throw new Exception("missing id"); 

            XMLTools.SaveListToXMLSerializer(ordersList, s_orders);
        }


        /// <summary>
        /// Update Order from List
        /// </summary>
        /// <param name="order"></param>
        /// <exception cref="Exception"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update(DO.Order order)
        {
          
            Delete(order.ID);
            var ordersList = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
            if (ordersList.Exists(item => item?.ID == order.ID))
                throw new Exception("id already exist");
            ordersList.Add(order);
            XMLTools.SaveListToXMLSerializer(ordersList, s_orders);
        }

    }
}
