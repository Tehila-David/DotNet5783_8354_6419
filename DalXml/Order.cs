using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
using System.Xml.Linq;
using System.Runtime.Intrinsics.Arm;

namespace Dal
{

  

    internal class Order : IOrder
    {
       

        const string s_orders = @"Orders";
        string configPath = @"config";

        public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? filter = null)
        {
            var ordersList = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders)!;
            return filter == null ? ordersList.OrderBy(lec => ((DO.Order)lec!).ID)
                                  : ordersList.Where(filter).OrderBy(lec => ((DO.Order)lec!).ID);
        }

        public DO.Order GetById(int id) =>
            XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders).FirstOrDefault(p => p?.ID == id)
        
            ?? throw new Exception("missing id");

        public DO.Order? GetById(Func<DO.Order?, bool>? predicate)
        {
            var ordersList = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders)!;
            if (ordersList.FirstOrDefault(predicate) == null)
            {
                throw new Exception("Product not Exsits");
            }
            return ordersList.FirstOrDefault(predicate);

        }


        public int Add(DO.Order order)
        {
            var ordersList = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

            if (ordersList.Exists(item => item?.ID == order.ID))
                throw new Exception("id already exist");

            List<ImportentNumbers> runningList = XMLTools.LoadListFromXMLSerializer1<ImportentNumbers>(configPath);

            ImportentNumbers runningNum = (from number in runningList
                                           where (number.typeOfnumber == "Order Running Number")
                                           select number).FirstOrDefault();
            runningList.Remove(runningNum);

            runningNum.numberSaved++;
            order.ID = (int)runningNum.numberSaved;


            runningList.Add(runningNum);
            ordersList.Add(order);

            XMLTools.SaveListToXMLSerializer1(runningList, configPath);
            XMLTools.SaveListToXMLSerializer(ordersList, s_orders);

            return order.ID;
        }

        public void Delete(int id)
        {
            var ordersList = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

            if (ordersList.RemoveAll(p => p?.ID == id) == 0)
                throw new Exception("missing id"); 

            XMLTools.SaveListToXMLSerializer(ordersList, s_orders);
        }

        public void Update(DO.Order order)
        {
            Delete(order.ID);
            List<ImportentNumbers> runningList = XMLTools.LoadListFromXMLSerializer1<ImportentNumbers>(configPath);

            ImportentNumbers runningNum = (from number in runningList
                                           where (number.typeOfnumber == "Order Running Number")
                                           select number).FirstOrDefault();
            runningList.Remove(runningNum);

            runningNum.numberSaved--;
            runningList.Add(runningNum);

            XMLTools.SaveListToXMLSerializer1(runningList, configPath);
            Add(order);
        }

    }
}
